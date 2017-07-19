using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SSISTeam2.App_Code.DTOModels
{
    public class RequestDTOCollection : RecordDTOCollection<RequestDTO>
    {
        public List<RequestDTO> withStatuses(params RequestStatus[] status)
        {
            throw new NotImplementedException();
        }
    }
}
