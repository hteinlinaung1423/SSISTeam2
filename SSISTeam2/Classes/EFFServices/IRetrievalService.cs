using SSISTeam2.Classes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSISTeam2.Classes.EFFServices
{
    public interface IRetrievalService
    {
        RetrievalModelCollection getAllRetrievedForCollectionPoint(int collectionPointId);
        RetrievalModelCollection getAllRetrievedFromDepartment(string deptCode);
        RetrievalModelCollection getAllRetrieved();
        RetrievalModelCollection findLatestRetrievalsByRequestId(int requestId);
        //int markRequestAsRetrieved(RequestModel toAllocate, string currentUser);
    }
}
