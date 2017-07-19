using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSISTeam2.App_Code.DAOServices
{
    interface IAllocatedService
    {
        RequestDTOCollection getAllAllocatedForCollectionPoint(int collectionPointId);
        //RequestDTOCollection getAllAllocatedFromDepartment(int departmentId);
        RequestDTOCollection findAllocatedByRequestId(int requestId);
        RequestDTOCollection getAllRequestsByUser(string username);
        RequestDTOCollection getAllApprovedRequests();
        //RequestDTOCollection getAllAllocatedRequests();
        //RequestDTOCollection getAllPartiallyAllocatedRequests();
        //RequestDTOCollection getAllRequestsDisbursing();
        //RequestDTOCollection getAllDisbursedRequests();
        bool saveNewRequest(RequestDTO request);
        bool updateRequestChanges(RequestDTO request);
        bool approveRequests(List<RequestDTO> requests);
        bool rejectRequests(List<RequestDTO> requests);
    }
}
