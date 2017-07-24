using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSISTeam2.Classes.Models
{
    public class DisbursementModel : AllocatedModel
    {
        public DisbursementModel()
        {

        }
        public DisbursementModel(Request efRequest, Dictionary<ItemModel, int> items) : base(efRequest, items)
        {
        }
    }
}
