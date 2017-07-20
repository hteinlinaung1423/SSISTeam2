using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SSISTeam2.Classes.Models;
using SSISTeam2.Classes.Exceptions;

namespace SSISTeam2.Classes.EFFServices
{
    public class AllocatedService : IAllocatedService
    {
        private SSISEntities context;
        public AllocatedService(SSISEntities context)
        {
            this.context = context;
        }
        public AllocatedModelCollection findAllocatedByRequestId(int requestId)
        {
            //{ PENDING, APPROVED, REJECTED, DISBURSED, PART_DISBURSED, CANCELLED, UPDATED });
            List<Request> efRequests = context.Requests.Where(x => x.current_status == RequestStatus.APPROVED).ToList();

            if (efRequests.Count == 0)
            {
                throw new ItemNotFoundException();
            }

            List<AllocatedModel> results = new List<AllocatedModel>();
            foreach (var efRequest in efRequests)
            {
                AllocatedModel alloc = new AllocatedModel(efRequest, ItemGetter._getItemsForRequest(efRequest, RequestServiceStatus.ALLOCATED));
                results.Add(alloc);
            }
            
            return new AllocatedModelCollection(results);
        }

        public AllocatedModelCollection getAllAllocatedForCollectionPoint(int collectionPointId)
        {
            //{ PENDING, APPROVED, REJECTED, DISBURSED, PART_DISBURSED, CANCELLED, UPDATED });
            List<Request> efRequests = context.Requests
                .Where(x =>
                    x.current_status == RequestStatus.APPROVED
                    && x.Department.collection_point == collectionPointId
                ).ToList();

            if (efRequests.Count == 0)
            {
                throw new ItemNotFoundException();
            }

            List<AllocatedModel> results = new List<AllocatedModel>();
            foreach (var efRequest in efRequests)
            {
                AllocatedModel alloc = new AllocatedModel(efRequest, ItemGetter._getItemsForRequest(efRequest, RequestServiceStatus.ALLOCATED));
                results.Add(alloc);
            }

            return new AllocatedModelCollection(results);
        }

        public AllocatedModelCollection getAllAllocatedFromDepartment(string deptCode)
        {
            //{ PENDING, APPROVED, REJECTED, DISBURSED, PART_DISBURSED, CANCELLED, UPDATED });
            List<Request> efRequests = context.Requests
                .Where(x =>
                    x.current_status == RequestStatus.APPROVED
                    && x.dept_code == deptCode
                ).ToList();

            if (efRequests.Count == 0)
            {
                throw new ItemNotFoundException();
            }

            List<AllocatedModel> results = new List<AllocatedModel>();
            foreach (var efRequest in efRequests)
            {
                AllocatedModel alloc = new AllocatedModel(efRequest, ItemGetter._getItemsForRequest(efRequest, RequestServiceStatus.ALLOCATED));
                results.Add(alloc);
            }

            return new AllocatedModelCollection(results);
        }

        public int allocatedRequest(RequestModel toAllocate, string currentUser)
        {
            List<ItemModel> itemsAllocateable = new List<ItemModel>();
            // First, make sure each item can be allocated in its entirety. If it can, allocate it.
            foreach (var item in toAllocate.Items)
            {
                if (item.Key.AvailableQuantity >= item.Value)
                {
                    // Can be completely allocated
                    itemsAllocateable.Add(item.Key);
                }
            }

            int added = 0;
            DateTime timestamp = DateTime.Now;
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Request efRequest = context.Requests.Find(toAllocate.RequestId);
                    if (efRequest == null) throw new ItemNotFoundException("Could not find Request object");

                    foreach (var item in itemsAllocateable)
                    {
                        // get associated request_detail
                        Request_Details targetDetail = efRequest.Request_Details.Where(r => r.item_code == item.ItemCode).First();
                        if (targetDetail == null) throw new ItemNotFoundException("Could not find Request_Details object");

                        // For each item, add a new event that is allocated
                        Request_Event newEvent = new Request_Event();
                        newEvent.request_detail_id = targetDetail.request_detail_id;

                        // get the quantity from the provided item quantity in the method arguments
                        newEvent.quantity = toAllocate.Items.Where(i => i.Key.ItemCode == item.ItemCode).First().Value;
                        newEvent.status = RequestServiceStatus.ALLOCATED;
                        newEvent.username = currentUser;
                        newEvent.date_time = timestamp;
                        newEvent.deleted = "N";

                        context.Request_Event.Add(newEvent);
                        added++;
                    }

                }
                catch (Exception exec)
                {
                    transaction.Rollback();
                    throw exec;
                }

                transaction.Commit();
            }
            return added;
        }

        public int reAllocateRequest(RequestModel toAllocate, string currentUser)
        {
            throw new NotImplementedException();
            //List<ItemModel> notYetAllocated = new List<ItemModel>();

            // Get items that have only been approved
            //Request efR = context.Requests.Find(toAllocate.RequestId);
            //List<Request_Event> evs = efR.Request_Details.Select(x => x.Request_Event.OrderBy(o => o.date_time).Last()).ToList();

            //foreach (var item in evs)
            //{
            //    if (item.status == RequestServiceStatus.APPROVED)
            //    {

            //    }
            //}
        }
    }
}