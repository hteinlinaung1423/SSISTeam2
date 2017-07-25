using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SSISTeam2.Classes.WebServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class ServiceTest : IServiceTest
    {
        public void DoWork()
        {
        }

        public string TestEmit()
        {
            return "Hello, world!";
        }

        public List<string> TestEmit2()
        {
            List<string> catNames = new List<string>();
            using (SSISEntities context = new SSISEntities())
            {
                catNames = context.Categories.Select(s => s.cat_name).ToList();
            }
            return catNames;
        }
    }
}
