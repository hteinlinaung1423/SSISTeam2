using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web.Security;

namespace SSISTeam2.Classes.WebServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service : IService
    {
        List<string> IService.GetCatName()
        {
            return new Work().GetCatName();
        }

        List<int> IService.GetDeliveryOrderId()
        {

            return null;
        }

        List<Delivery_Details> IService.GetDeliveryOrdersDetails(string id)
        {
            return null;
        }

        List<WCF_Item> IService.GetItemDetail(string name)
        {

            List<WCF_Item> itemList = new List<WCF_Item>();

            List<Stock_Inventory> stock = new Work().GetItemDetail(name);

            foreach (Stock_Inventory s in stock)
            {
                WCF_Item item = new WCF_Item(s.item_description, s.item_code, s.unit_of_measure, s.cat_id);
                itemList.Add(item);
            }

            return itemList;

        }



        List<WCF_Request> IService.GetAllRequest(string dept)
        {
            List<WCF_Request> reqList = new List<WCF_Request>();
            List<Request> req = new Work().GetAllRequest(dept);

            foreach (Request r in req)
            {
                string date = string.Format("{0:dd/MM/yyy}", r.date_time);
                WCF_Request request = new WCF_Request(r.username, r.request_id, date, r.reason);
                reqList.Add(request);
            }

            return reqList;


        }

        public WCF_User login(string name, string pass)
        {
            WCF_User user;
            bool validate = Membership.ValidateUser(name, pass);

            if (validate)
            {

                string[] role = Roles.GetRolesForUser(name);
                Dept_Registry dept = new Work().login(name);
                user = new WCF_User(dept.dept_code, name, role[0]);
                return user;
            }
            else { return user = new WCF_User(null, "failed", null); }


        }

        public List<WCF_RequestDetail> GetRequestDetail(string id)
        {
            List<WCF_RequestDetail> req_detail_list = new List<WCF_RequestDetail>();
            List<Request_Details> rdList = new Work().GetRequestDetail(id);

            foreach (Request_Details r in rdList)
            {
                int quantity = Convert.ToInt32(r.orig_quantity);
                WCF_RequestDetail req_detail = new WCF_RequestDetail(r.Stock_Inventory.item_description, quantity);
                req_detail_list.Add(req_detail);
            }

            return req_detail_list;
        }
    }
}