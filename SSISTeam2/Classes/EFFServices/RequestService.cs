﻿using SSISTeam2.Classes.Exceptions;
using SSISTeam2.Classes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSISTeam2.Classes.EFFServices
{
    public class RequestService : IRequestService
    {
        private SSISEntities context;
        public RequestService(SSISEntities context)
        {
            this.context = context;
        }

        public RequestModel findRequestById(int requestId)
        {
            Request efRequest = context.Requests.Find(requestId);

            if (efRequest == null)
            {
                throw new ItemNotFoundException();
            }

            RequestModel request = new RequestModel(efRequest, efRequest.Department, ItemGetter._getPendingOrUpdatedItemsForRequest(efRequest));
            return request;
        }

        public RequestModelCollection getAllApprovedRequests()
        {
            List<Request> approved = context.Requests
                .Where(x => x.deleted != "Y"
                    && x.current_status == RequestStatus.APPROVED).ToList();
            if (approved.Count == 0)
            {
                throw new ItemNotFoundException();
            } else
            {
                List<RequestModel> records = approved.Select(x => new RequestModel(x, x.Department, ItemGetter._getItemsForRequest(x, RequestStatus.APPROVED))).ToList();

                return new RequestModelCollection(records);
            }
        }

        public RequestModelCollection getAllPendingRequestsOfDepartment(string departmentCode)
        {
            List<Request> pending = context.Requests
                .Where(x => x.deleted != "Y"
                    && (x.current_status == RequestStatus.PENDING || x.current_status == RequestStatus.UPDATED)
                    && x.dept_code == departmentCode
                    ).ToList();
            if (pending.Count == 0)
            {
                throw new ItemNotFoundException();
            }
            else
            {
                List<RequestModel> records = pending.Select(x => new RequestModel(x, ItemGetter._getPendingOrUpdatedItemsForRequest(x))).ToList();

                return new RequestModelCollection(records);
            }
        }

        public RequestModelCollection getAllRequestsByUser(string username)
        {
            List<Request> all = context.Requests
                .Where(x => x.deleted != "Y"
                    && x.username == username
                    ).ToList();
            if (all.Count == 0)
            {
                throw new ItemNotFoundException();
            }
            else
            {
                List<RequestModel> records = all.Select(x =>
                {
                    string status = x.current_status;
                    return new RequestModel(x, x.Department, ItemGetter._getItemsForRequest(x, status));
                }).ToList();

                return new RequestModelCollection(records);
            }
        }

        public RequestModelCollection getAllRequestsOfDepartment(string departmentCode)
        {
            List<Request> all = context.Requests
                .Where(x => x.deleted != "Y"
                    && x.dept_code == departmentCode
                ).ToList();
            if (all.Count == 0)
            {
                throw new ItemNotFoundException();
            }
            else
            {
                List<RequestModel> records = all.Select(x => new RequestModel(x, x.Department, ItemGetter._getPendingOrUpdatedItemsForRequest(x))).ToList();

                return new RequestModelCollection(records);
            }
        }

        private int _setStatusOfRequests(List<RequestModel> requests, string currentUser, string status)
        {
            int added = 0;
            DateTime timestamp = DateTime.Now;
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    foreach (RequestModel request in requests)
                    {
                        if (RequestStatus.requestHasHadStatus(request, status))
                        {
                            continue;
                        }
                        Request efRequest = context.Requests.Find(request.RequestId);
                        efRequest.current_status = status;
                        List<Request_Details> efDetails = context.Request_Details.Where(x => x.request_id == request.RequestId).ToList();
                        if (efRequest == null) throw new ItemNotFoundException("Could not find Request object");
                        if (efDetails.Count == 0) throw new ItemNotFoundException("Could not find Request Details objects");

                        foreach (Request_Details efDetail in efDetails)
                        {
                            // For each item, add a new event that is "_the_status"
                            Request_Event newEvent = new Request_Event();
                            newEvent.request_detail_id = efDetail.request_detail_id;

                            // get the quantity from the provided item quantity in the method arguments
                            newEvent.quantity = request.Items.Where(x => x.Key.ItemCode == efDetail.item_code).First().Value;
                            newEvent.status = status;
                            newEvent.username = currentUser;
                            newEvent.date_time = timestamp;
                            newEvent.deleted = "N";

                            context.Request_Event.Add(newEvent);
                            added++;
                        }
                    }
                } catch (Exception exec)
                {
                    transaction.Rollback();
                    throw exec;
                }

                transaction.Commit();
            }
            return added;
        }

        public int approveRequests(List<RequestModel> requests, string currentUser)
        {
            return _setStatusOfRequests(requests, currentUser, RequestStatus.APPROVED);
        }

        public int rejectRequests(List<RequestModel> requests, string currentUser)
        {
            return _setStatusOfRequests(requests, currentUser, RequestStatus.REJECTED);
        }

        public bool saveNewRequest(RequestModel request)
        {
            DateTime timestamp = DateTime.Now;
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Request newRequest = new Request();
                    newRequest.current_status = RequestStatus.PENDING;
                    newRequest.deleted = "N";
                    newRequest.dept_code = request.Department.dept_code;
                    newRequest.reason = request.Reason;
                    newRequest.rejected_reason = "";
                    newRequest.username = request.UserModel.Username;

                    List<Request_Details> newDetails = new List<Request_Details>();
                    foreach (KeyValuePair<ItemModel, int> itemAndQty in request.Items)
                    {
                        Request_Details newDetail = new Request_Details();
                        newDetail.deleted = "N";
                        newDetail.item_code = itemAndQty.Key.ItemCode;

                        Request_Event newEvent = new Request_Event();
                        newEvent.deleted = "N";
                        newEvent.date_time = timestamp;
                        newEvent.quantity = itemAndQty.Value;
                        newEvent.status = RequestStatus.PENDING;
                        newEvent.username = request.UserModel.Username;

                        // Establish relationships
                        newDetail.Request_Event.Add(newEvent);
                        newRequest.Request_Details.Add(newDetail);
                    }
                }
                catch (Exception exec)
                {
                    transaction.Rollback();
                    throw exec;
                }

                transaction.Commit();
            }
            return true;
        }

        public bool updateRequestChanges(RequestModel request)
        {
            if (RequestStatus.requestHasHadStatus(request, RequestStatus.APPROVED))
            {
                throw new RequestAlreadyApprovedException("Already approved or rejected.");
                //return false;
            }
            DateTime timestamp = DateTime.Now;
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Request targetRequest = context.Requests.Find(request.RequestId);

                    List<Request_Details> targetDetails = targetRequest.Request_Details.ToList();
                    foreach (KeyValuePair<ItemModel, int> itemAndQty in request.Items)
                    {
                        Request_Event newEvent = new Request_Event();
                        newEvent.deleted = "N";
                        newEvent.date_time = timestamp;
                        newEvent.quantity = itemAndQty.Value;
                        newEvent.status = RequestStatus.UPDATED;
                        newEvent.username = request.UserModel.Username;

                        // Establish relationships
                        Request_Details targetDetail = targetDetails.Where(x => x.item_code == itemAndQty.Key.ItemCode).First();
                        targetDetail.Request_Event.Add(newEvent);
                    }
                }
                catch (Exception exec)
                {
                    transaction.Rollback();
                    throw exec;
                }

                transaction.Commit();
            }
            return true;
        }
    }
}