using SSISTeam2.Classes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSISTeam2.Classes.EFFServices
{
    public interface IRequestService
    {
        RequestModelCollection getAllRequestsOfDepartment(string departmentCode);
        RequestModelCollection getAllPendingRequestsOfDepartment(string departmentCode);
        RequestModel findRequestById(int requestId);
        RequestModelCollection getAllRequestsByUser(string username);
        RequestModelCollection getAllApprovedRequests();
        //RequestDTOCollection getAllAllocatedRequests();
        //RequestDTOCollection getAllPartiallyAllocatedRequests();
        //RequestDTOCollection getAllRequestsDisbursing();
        //RequestDTOCollection getAllDisbursedRequests();
        bool saveNewRequest(RequestModel request);
        bool updateRequestChanges(RequestModel request);
        bool approveRequests(List<RequestModel> requests);
        bool rejectRequests(List<RequestModel> requests);
    }
}
