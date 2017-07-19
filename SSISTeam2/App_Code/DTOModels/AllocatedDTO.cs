using System;

namespace SSISTeam2.App_Code.DTOModels
{
    public class AllocatedDTO : RecordDTO
    {
        public override int RequestId
        {
            get
            {
                return base.RequestId;
            }
        }

        public RequestDTO getRequest(Ctx context)
        {
            throw new NotImplementedException();
        }
    }
}