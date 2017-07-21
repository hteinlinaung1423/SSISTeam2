using System.Collections.Generic;
using SSISTeam2.Classes.Models;

namespace SSISTeam2.Classes.EFFServices
{
    public interface IRequestMovementService
    {
        List<string> getRequestNonAllocatedItemCodes(int requestId);
        void moveFromAllocatedToRetrieving(int requestId, string currentUser);
        void moveToRetrievingToRetrieved(RequestModel requestModel, string currentUser);
        void moveToRetrievedToDisbursing(int requestId, string currentUser);
        void moveToDisbursingToDisbursed(RequestModel requestModel, string currentUser);

    }
}