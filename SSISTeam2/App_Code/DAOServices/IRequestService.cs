using SSISTeam2.App_Code.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSISTeam2.App_Code.DAOServices
{
    public interface IRequestService
    {
        RequestDTOCollection getAllRequestsOfDepartment(string departmentCode);
        RequestDTOCollection getAllPendingRequestsOfDepartment(string departmentCode);
        RequestDTOCollection findRequestById(int requestId);
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
