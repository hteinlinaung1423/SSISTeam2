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
            int added = 0;
            DateTime timestamp = DateTime.Now;

            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    // For each item:
                    // - Get the latest status (e.g. retrieved or disbursed)
                    // - Compare this latest status to the approved amount
                    // - If there is a difference, try to reallocate it

                    // Get a list of details (items)
                    List<Request_Details> details = context.Requests.Find(toAllocate.RequestId).Request_Details.ToList();
                    if (details.Count == 0) throw new ItemNotFoundException("Request could not be found");

                    // For each of these, get the latest status, and approved event
                    foreach (var detail in details)
                    {
                        Request_Event latest = detail.Request_Event.OrderBy(o => o.date_time).Last();
                        if (latest == null) throw new ItemNotFoundException("event item could not be found");

                        Request_Event approved = detail.Request_Event.Where(e => e.status == RequestServiceStatus.APPROVED).OrderBy(o => o.date_time).Last();
                        if (approved == null) throw new ItemNotFoundException("event item could not be found");

                        // Get the difference in quantity
                        int difference = approved.quantity - latest.quantity;

                        if (difference < 0)
                        {
                            throw new RequestDataIntegrityFailureException(
                                string.Format("Request id ({0}) has quantities that don't match up!\n==>Request_Event id: {1}\n==>Request_Event id: {2}", toAllocate.RequestId, latest.request_event_id, approved.request_event_id)
                                );
                        }

                        if (difference > 0)
                        {
                            // There is a difference, so reAllocate it, using the difference quantity
                            // Get the item model
                            Stock_Inventory stock = context.Stock_Inventory.Find(detail.item_code);
                            if (stock == null) throw new ItemNotFoundException("Could not find stock_inventory for itemCode: " + detail.item_code);

                            ItemModel item = new ItemModel(stock);

                            // First, make sure each item can be allocated in its entirety. If it can, allocate it.
                            if (item.AvailableQuantity >= difference)
                            {
                                // For each item, add a new event that is allocated
                                Request_Event newEvent = new Request_Event();
                                newEvent.request_detail_id = detail.request_detail_id;

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
    }
}