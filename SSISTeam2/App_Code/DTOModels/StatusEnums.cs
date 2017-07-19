using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSISTeam2.App_Code.DTOModels
{
    public enum RequestStatus
    {
        PENDING,
        APPROVED,
        REJECTED,
        DISBURSED,
        PART_DISBURSED,
        CANCELLED,
        UPDATED
    }

    public enum RequestServiceStatus
    {
        PENDING,
        APPROVED,
        REJECTED,
        DISBURSED,
        //PART_DISBURSED,
        CANCELLED,
        UPDATED,

        ALLOCATED,
        DISBURSING
        //PART_ALLOCATED,

    }
}
