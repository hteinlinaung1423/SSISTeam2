using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SSISTeam2.Classes.Models;
using SSISTeam2.Classes.Exceptions;

namespace SSISTeam2.Classes.EFFServices
{
    public class DisbursementService : IDisbursementService
    {
        private SSISEntities context;
        public DisbursementService(SSISEntities context)
        {
            this.context = context;
        }
        public DisbursementModel findLatestDisbursementByRequestId(int requestId)
        {
            // Get all allocated: depending on:
            // Determine there's any latest retrieval
            // Find the difference with the previous retrieval, to see how much to fulfill

            //{ PENDING, APPROVED, REJECTED, DISBURSED, PART_DISBURSED, CANCELLED, UPDATED });
            Request efRequest = context.Requests
                .Where(x => x.request_id == requestId
                            && (x.current_status == RequestStatus.DISBURSED
                            || x.current_status == RequestStatus.PART_DISBURSED)
                ).First();

            if (efRequest == null)
            {
                throw new ItemNotFoundException("No records exist");
            }

            IEnumerable<IGrouping<string, Request_Event>> events = efRequest.Request_Details.SelectMany(x => x.Request_Event).GroupBy(g => g.Request_Details.item_code).ToList();

            Dictionary<ItemModel, int> itemsToFulfill = new Dictionary<ItemModel, int>();

            foreach (IGrouping<string, Request_Event> eventItem in events)
            {
                // Grouping:
                // A101
                // - Approved, 10
                // - Allocated, 9
                // A102
                // - Approved, 10
                List<Request_Event> latestDisbursement = eventItem.Where(x => x.status == EventStatus.RETRIEVED).OrderBy(o => o.date_time).ToList();
                if (latestDisbursement.Count == 0) continue;

                int quantityToFulfil = 0;

                if (latestDisbursement.Count > 1)
                {
                    Request_Event last = latestDisbursement.Last();
                    Request_Event secondLast = latestDisbursement[latestDisbursement.Count - 2];

                    quantityToFulfil = last.quantity - secondLast.quantity;
                }
                else
                {
                    quantityToFulfil = latestDisbursement.Last().quantity;
                }

                Stock_Inventory inv = context.Stock_Inventory.Find(eventItem.Key);
                itemsToFulfill.Add(new ItemModel(inv), quantityToFulfil);
            }

            if (itemsToFulfill.Count == 0)
            {
                return null;
            }

            DisbursementModel disbursement = new DisbursementModel(efRequest, itemsToFulfill);

            return disbursement;
        }

        public DisbursementModelCollection getAllDisbursements()
        {
            //{ PENDING, APPROVED, REJECTED, DISBURSED, PART_DISBURSED, CANCELLED, UPDATED });
            List<Request> efRequests = context.Requests
                .Where(x => x.current_status == RequestStatus.DISBURSED
                            || x.current_status == RequestStatus.PART_DISBURSED
                ).ToList();

            if (efRequests.Count == 0)
            {
                //throw new ItemNotFoundException("No records exist");
                return null;
            }

            List<DisbursementModel> results = new List<DisbursementModel>();
            foreach (var efRequest in efRequests)
            {
                DisbursementModel retrieval = findLatestDisbursementByRequestId(efRequest.request_id);
                if (retrieval == null) continue; // SKIP
                results.Add(retrieval);
            }

            return new DisbursementModelCollection(results);
        }

        public DisbursementModelCollection getAllDisbursementsForCollectionPoint(int collectionPointId)
        {
            //{ PENDING, APPROVED, REJECTED, DISBURSED, PART_DISBURSED, CANCELLED, UPDATED });
            List<Request> efRequests = context.Requests
                .Where(x =>
                    (x.current_status == RequestStatus.DISBURSED
                    || x.current_status == RequestStatus.PART_DISBURSED)
                    && x.Department.collection_point == collectionPointId
                ).ToList();

            if (efRequests.Count == 0)
            {
                //throw new ItemNotFoundException("No records exist");
                return null;
            }

            List<DisbursementModel> results = new List<DisbursementModel>();
            foreach (var efRequest in efRequests)
            {
                DisbursementModel disbursement = findLatestDisbursementByRequestId(efRequest.request_id);
                if (disbursement == null) continue; // SKIP
                results.Add(disbursement);
            }

            return new DisbursementModelCollection(results);
        }

        public DisbursementModelCollection getAllDisbursementsFromDepartment(string deptCode)
        {
            //{ PENDING, APPROVED, REJECTED, DISBURSED, PART_DISBURSED, CANCELLED, UPDATED });
            List<Request> efRequests = context.Requests
                .Where(x =>
                    (x.current_status == RequestStatus.DISBURSED
                    || x.current_status == RequestStatus.PART_DISBURSED)
                    && x.dept_code == deptCode
                ).ToList();

            if (efRequests.Count == 0)
            {
                //throw new ItemNotFoundException();
                return null;
            }

            List<DisbursementModel> results = new List<DisbursementModel>();
            foreach (var efRequest in efRequests)
            {
                DisbursementModel disbursements = findLatestDisbursementByRequestId(efRequest.request_id);
                if (disbursements == null) continue; // SKIP
                results.Add(disbursements);
            }

            return new DisbursementModelCollection(results);
        }

        public int markRequestAsDisbursed(RequestModel toAllocate, string currentUser)
        {
            throw new NotImplementedException();
        }
    }
}