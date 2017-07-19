using SSISTeam2.Classes.Exceptions;
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

        public bool approveRequests(List<RequestModel> requests)
        {
            throw new NotImplementedException();
        }

        public RequestModel findRequestById(int requestId)
        {
            Request efRequest = context.Requests.Find(requestId);
            Dictionary<ItemModel, int> items = new Dictionary<ItemModel, int>();

            if (efRequest == null)
            {
                throw new ItemNotFoundException();
            }

            if (efRequest.current_status != "PartDisbursed")
            {

                try
                {
                    items = efRequest.Request_Details.Where(x => x.deleted != "Y")
                    .ToDictionary(x => new ItemModel(x.Stock_Inventory),
                        x => x.Request_Event.Where(r => r.status == RequestStatus.PENDING)
                            .FirstOrDefault().quantity);
                } catch (NullReferenceException nullExec)
                {
                    throw new ItemNotFoundException("Item not found", nullExec);
                }
                
            }

            RequestModel request = new RequestModel(efRequest, efRequest.Department, items);
            return request;
        }

        private Dictionary<ItemModel, int> _getItemsForRequest(Request efRequest, string status)
        {
            try
            {
                List<Request_Details> details = efRequest.Request_Details.Where(x => x.deleted != "Y").ToList();

                Dictionary<ItemModel, int> itemsAndQuantities = new Dictionary<ItemModel, int>();

                details.ForEach(x =>
                {
                    List<Request_Event> events = x.Request_Event.Where(e => e.status == status).ToList();
                    int count = events.Count();
                    int qty = 0;

                    if (count == 1)
                    { // There is one event for this item's status (e.g. no allocated)
                        qty = events.First().quantity;
                    }
                    else if (count > 1)
                    { // Multiple events for this status (e.g. allocated twice)
                        qty = events.Select(e => e.quantity).Sum();
                    }

                    itemsAndQuantities.Add(new ItemModel(x.Stock_Inventory), qty);
                });

                return itemsAndQuantities;

            }
            catch (NullReferenceException nullExec)
            {
                throw new ItemNotFoundException("Item not found", nullExec);
            }
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
                List<RequestModel> records = approved.Select(x => new RequestModel(x, x.Department, _getItemsForRequest(x, RequestStatus.APPROVED))).ToList();

                return new RequestModelCollection(records);
            }
        }

        public RequestModelCollection getAllPendingRequestsOfDepartment(string departmentCode)
        {
            List<Request> pending = context.Requests
                .Where(x => x.deleted != "Y"
                    && x.current_status == RequestStatus.PENDING
                    && x.dept_code == departmentCode
                    ).ToList();
            if (pending.Count == 0)
            {
                throw new ItemNotFoundException();
            }
            else
            {
                List<RequestModel> records = pending.Select(x => new RequestModel(x, x.Department, _getItemsForRequest(x, RequestStatus.PENDING))).ToList();

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
                    return new RequestModel(x, x.Department, _getItemsForRequest(x, status));
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
                List<RequestModel> records = all.Select(x => new RequestModel(x, x.Department, _getItemsForRequest(x, RequestStatus.PENDING))).ToList();

                return new RequestModelCollection(records);
            }
        }

        public bool rejectRequests(List<RequestModel> requests)
        {
            throw new NotImplementedException();
        }

        public bool saveNewRequest(RequestModel request)
        {
            throw new NotImplementedException();
        }

        public bool updateRequestChanges(RequestModel request)
        {
            throw new NotImplementedException();
        }
    }
}
