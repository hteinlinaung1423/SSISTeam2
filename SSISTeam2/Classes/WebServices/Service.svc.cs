using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web.Security;

using SSISTeam2.Classes.Models;
using System.IO;

namespace SSISTeam2.Classes.WebServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service : IService
    {
        SSISEntities context = new SSISEntities();
        Work work = new Work();

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

        WCF_User IService.login(string name, string pass)
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

        // Heng Tiong's MonthlyCheck implementation

        public List<WCF_MonthlyCheck> GetIMonthlyCheckModel()
        {
            List<MonthlyCheckModel> modelList = new Work().GetAllMonthlyCheck();

            List<WCF_MonthlyCheck> WCFList = new List<WCF_MonthlyCheck>();

            foreach (MonthlyCheckModel i in modelList)
            {

                string currentQuantity = i.CurrentQuantity.ToString();
                string actualQuantity = i.ActualQuantity.ToString();
                WCF_MonthlyCheck wcf = new WCF_MonthlyCheck(i.ItemCode, i.Description, i.CatName, currentQuantity, actualQuantity, i.Reason);

                WCFList.Add(wcf);
            }

            return WCFList;
        }

        public List<string> GetMonthlyCheckName()
        {
            List<MonthlyCheckModel> modelList = new Work().GetAllMonthlyCheck();
            List<string> strings = new List<string>();

            foreach (MonthlyCheckModel i in modelList)
            {

                string names = i.Description;
                strings.Add(names);
            }

            return strings;
        }

        public void UpdateMonthlyCheck(List<WCF_MonthlyCheck> monthlyCheckList)
        {
            foreach (WCF_MonthlyCheck i in monthlyCheckList)
            {
                //MonthlyCheckModel model = new MonthlyCheckModel();
            }


        }
        public string[] GetDelgateEmployeeName(string deptcode)
        {
            
            return work.ListEmployeeName(deptcode).ToArray<String>();
        }

        public void Create(WCF_AppDuties dr)
        {
           
            Approval_Duties appduties = new Approval_Duties
            {
                username = dr.UserName,
                start_date = Convert.ToDateTime(dr.StartDate),
                end_date = Convert.ToDateTime(dr.EndDate),
                dept_code = dr.DeptCode,
                created_date = Convert.ToDateTime(dr.CreatedDate),
                deleted = dr.Deleted,
                reason = dr.Reason

            };

            work.CreateAppDuties(appduties);

        }

        public List<WCF_RequestDetail> GetRequestDetail(string id)
        {
            List<WCF_RequestDetail> rd = new List<WCF_RequestDetail>();

            List<Request_Details> rd_List = new Work().GetRequestDetail(id);
            foreach (Request_Details r in rd_List)
            {
                int quantity = Convert.ToInt32(r.orig_quantity);
                WCF_RequestDetail req = new WCF_RequestDetail(r.Stock_Inventory.item_description, quantity);

                rd.Add(req);
            }

            return rd;
        }

        public WCF_AppDuties CheckAppDuties(string deptcode)
        {
            Approval_Duties c = work.ListAppDuties(deptcode);
            return WCF_AppDuties.Make(c.username, c.start_date.ToString(), c.end_date.ToString(), c.dept_code, c.created_date.ToString(), c.deleted, c.reason);
            //return Work.ListAppDuties(deptcode).ToArray<String>();
        }

        public string Update(WCF_AppDuties c)
        {
            System.Diagnostics.Debug.WriteLine("Testingnnnnnnnn");
            /*Approval_Duties ap = new Approval_Duties
            {
                username = c.UserName,
                dept_code = c.DeptCode,
                deleted = "N"

            };*/
            work.UpdateDuty(c.DeptCode);
            return c.DeptCode;
            
        }

        public void Approve(string id)
        {
            new Work().Approve(id);
        }

        public void Reject(string id)
        {
            new Work().Reject(id);
        }

        //By Yin
 

        public WCFItemTotalQty[] GetEachItemQty()
        {
            List<WCFItemTotalQty> list = work.wgetEachItemQty();
            return list.ToArray<WCFItemTotalQty>();

        }

        public List<String> GetDisbCollectP()
        {
            return work.wgetCollectP();

        }

        public List<string> GetDisbCollectDept(string cpid)
        {
            return work.wgetCollectDept(cpid);
        }

        public List<WCFDeptTQty> GetDeptDetail(string deptname)
        {
            List<WCFDeptTQty> list = work.wgetDepDetail(deptname);

            List<WCFDeptTQty> disSL = null;

            var q = (from x in list
                     group x by x.ItemDes into g
                     select new WCFDeptTQty
                     {
                         ItemDes = g.Key,
                         ReqQty = g.Sum(y => y.ReqQty)
                     }).ToList();

            return q.ToList<WCFDeptTQty>();
        }

    }
}