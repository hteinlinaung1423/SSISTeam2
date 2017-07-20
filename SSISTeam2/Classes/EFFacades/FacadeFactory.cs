using SSISTeam2.Classes.EFFServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSISTeam2.Classes.EFFacades
{
    public class FacadeFactory
    {
        public static IRequestService getRequestService(SSISEntities context)
        {
            return new RequestService(context);
        }
        public static IAllocatedService getAllocatedService(SSISEntities context)
        {
            return new AllocatedService(context);
        }
    }
}