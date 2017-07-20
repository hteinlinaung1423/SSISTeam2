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
            List<Request> efRequests = context.Requests.Where(x => x.current_status == RequestStatus.APPROVED).ToList();

            if (efRequests.Count == 0)
            {
                throw new ItemNotFoundException();
            }

            foreach (var efRequest in efRequests)
            {
                AllocatedModel alloc = new AllocatedModel();
                RequestModel request = new RequestModel(efRequest, ItemGetter._getPendingOrUpdatedItemsForRequest(efRequest));
            }

            
            return request;
        }

        public AllocatedModelCollection getAllAllocatedForCollectionPoint(int collectionPointId)
        {
            throw new NotImplementedException();
        }

        public AllocatedModelCollection getAllAllocatedFromDepartment(int departmentId)
        {
            throw new NotImplementedException();
        }

        public bool saveNewAllocation(AllocatedModel allocation)
        {
            throw new NotImplementedException();
        }
    }
}