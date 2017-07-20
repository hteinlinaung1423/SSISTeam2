using SSISTeam2.Classes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSISTeam2.Classes.EFFServices
{
    public interface IDisbursementService
    {
        DisbursementModelCollection getAllDisbursementsForCollectionPoint(int collectionPointId);
        DisbursementModelCollection getAllDisbursementsFromDepartment(string deptCode);
        DisbursementModelCollection getAllDisbursements();
        DisbursementModel findLatestDisbursementByRequestId(int requestId);
        int markRequestAsDisbursed(RequestModel toAllocate, string currentUser);
    }
}
