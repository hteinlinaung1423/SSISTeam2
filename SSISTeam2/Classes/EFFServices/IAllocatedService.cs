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
        AllocatedModelCollection getAllAllocatedFromDepartment(int departmentId);
        AllocatedModelCollection findAllocatedByRequestId(int requestId);
        //RequestDTOCollection getAllAllocatedRequests();
        //RequestDTOCollection getAllPartiallyAllocatedRequests();
        //RequestDTOCollection getAllRequestsDisbursing();
        //RequestDTOCollection getAllDisbursedRequests();
        bool saveNewAllocation(AllocatedModel allocation);
    }
}
