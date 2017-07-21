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
        public DisbursementModel findLatestPossibleDisbursingByRequestId(int requestId)
        { // For generating disbursement forms (all those as retrieved)

            return _findLatestBetweenStatuses(requestId, EventStatus.RETRIEVED, EventStatus.DISBURSING, null);
        }

        public DisbursementModel findLatestSignOffsByRequestId(int requestId, string currentUser)
        { // For signing off forms (all disbursing ones)

            return _findLatestBetweenStatuses(requestId, EventStatus.DISBURSING, EventStatus.DISBURSED, currentUser);
        }

        private DisbursementModel _findLatestBetweenStatuses(int requestId, string fromStatus, string toStatus, string currentUser)
        { // For signing off forms (all disbursing ones)
            Request efRequest = context.Requests
                .Where(x => x.request_id == requestId
                            && (x.current_status == RequestStatus.APPROVED
                            || x.current_status == RequestStatus.PART_DISBURSED)
                ).First();

            if (efRequest == null)
            {
                throw new ItemNotFoundException("No records exist");
            }

            Dictionary<ItemModel, int> itemsToFulfill = new Dictionary<ItemModel, int>();

            List<Request_Details> details = efRequest.Request_Details.ToList();
            foreach (var detail in details)
            {
                int itemQty = 0;
                if (detail.deleted == "Y")
                {
                    continue;
                }
                List<Request_Event> events = detail.Request_Event.OrderByDescending(o => o.date_time).ToList();

                foreach (var ev in events)
                {
                    if (ev.status == EventStatus.APPROVED
                        || ev.status == EventStatus.ALLOCATED
                        || ev.deleted == "Y")
                    {
                        continue;
                    }
                    if (ev.status == toStatus)
                    {
                        break;
                    }
                    else if (ev.status == fromStatus && (currentUser == null) ? true : (ev.username == currentUser))
                    {
                        itemQty += ev.quantity;
                    }
                }

                if (itemQty > 0)
                {
                    Stock_Inventory s = detail.Stock_Inventory;
                    itemsToFulfill.Add(new ItemModel(s), itemQty);
                }
            }

            DisbursementModel retrieved = new DisbursementModel(efRequest, itemsToFulfill);

            return retrieved;
        }

        public DisbursementModelCollection getAllThatCanBeSignedOff(string currentUser)
        {
            //{ PENDING, APPROVED, REJECTED, DISBURSED, PART_DISBURSED, CANCELLED, UPDATED });
            List<Request> efRequests = context.Requests
                .Where(x => x.current_status == RequestStatus.APPROVED
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
                DisbursementModel retrieval = findLatestSignOffsByRequestId(efRequest.request_id, currentUser);
                if (retrieval == null) continue; // SKIP
                results.Add(retrieval);
            }

            return new DisbursementModelCollection(results);
        }

        public DisbursementModelCollection getAllThatWereDisbursed()
        {
            //{ PENDING, APPROVED, REJECTED, DISBURSED, PART_DISBURSED, CANCELLED, UPDATED });
            List<Request> efRequests = context.Requests
                .Where(x => x.current_status == RequestStatus.DISBURSED && x.deleted != "Y"
                ).ToList();

            if (efRequests.Count == 0)
            {
                //throw new ItemNotFoundException("No records exist");
                return null;
            }

            List<DisbursementModel> results = new List<DisbursementModel>();
            foreach (var efRequest in efRequests)
            {
                Dictionary<ItemModel, int> items = new Dictionary<ItemModel, int>();

                foreach (var item in efRequest.Request_Details
                    .Select(s => s.Request_Event.OrderByDescending(o => o.date_time)
                    .Where(w => w.status == EventStatus.DISBURSED)
                    .First())) {

                    items.Add(new ItemModel(item.Request_Details.Stock_Inventory), item.quantity);
                }

                DisbursementModel disbursed = new DisbursementModel(efRequest, items);
                if (disbursed == null) continue; // SKIP
                results.Add(disbursed);
            }

            return new DisbursementModelCollection(results);
        }

        public DisbursementModelCollection getAllSignOffsForCollectionPoint(int collectionPointId, string currentUser)
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
                DisbursementModel disbursement = findLatestSignOffsByRequestId(efRequest.request_id, currentUser);
                if (disbursement == null) continue; // SKIP
                results.Add(disbursement);
            }

            return new DisbursementModelCollection(results);
        }

        public DisbursementModelCollection getAllPossibleDisbursementsForCollectionPoint(int collectionPointId)
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
                DisbursementModel disbursement = findLatestPossibleDisbursingByRequestId(efRequest.request_id);
                if (disbursement == null) continue; // SKIP
                results.Add(disbursement);
            }

            return new DisbursementModelCollection(results);
        }
        /*
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
        */
    }
}