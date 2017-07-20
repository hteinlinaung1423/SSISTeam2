using System;

namespace SSISTeam2.Classes.Models
{
    public class AllocatedModel : RecordModel
    {
        public AllocatedModel()
        {

        }

        public override int RequestId
        {
            get
            {
                return base.RequestId;
            }
        }

        public RequestModel getRequest(SSISEntities context)
        {
            throw new NotImplementedException();
        }
    }
}