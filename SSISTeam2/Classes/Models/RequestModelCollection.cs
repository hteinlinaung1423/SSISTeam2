using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSISTeam2.Classes.Models
{
    public class RequestModelCollection : RecordModelCollection<RequestModel>
    {
        public RequestModelCollection(List<RequestModel> records) : base(records)
        {
        }
        public List<RequestModel> withStatuses(params RequestStatus[] status)
        {
            throw new NotImplementedException();
        }
    }
}
