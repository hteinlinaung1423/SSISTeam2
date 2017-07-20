using SSISTeam2.Classes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSISTeam2.Classes.EFFServices
{
    interface IAllocatedService
    {
        AllocatedModelCollection getAllAllocatedForCollectionPoint(int collectionPointId);
        AllocatedModelCollection getAllAllocatedFromDepartment(string deptCode);
        AllocatedModelCollection findAllocatedByRequestId(int requestId);
        //RequestDTOCollection getAllAllocatedRequests();
        //RequestDTOCollection getAllPartiallyAllocatedRequests();
        //RequestDTOCollection getAllRequestsDisbursing();
        //RequestDTOCollection getAllDisbursedRequests();
        int allocatedRequest(RequestModel toAllocate, string currentUser);
        int reAllocateRequest(RequestModel toAllocate, string currentUser);
    }
}
