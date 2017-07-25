using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SSISTeam2.Classes.WebServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IServiceTest
    {
        [OperationContract]
        void DoWork();

        [OperationContract]
        [WebGet(UriTemplate = "/TestEmit", ResponseFormat = WebMessageFormat.Json)]
        string TestEmit();

        [OperationContract]
        [WebGet(UriTemplate = "/TestEmit2", ResponseFormat = WebMessageFormat.Json)]
        List<string> TestEmit2();
    }
}
