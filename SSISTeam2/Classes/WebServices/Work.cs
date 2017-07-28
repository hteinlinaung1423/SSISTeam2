using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using SSISTeam2.Classes.Models;

namespace SSISTeam2.Classes.WebServices
{
    public class Work
    {



        SSISEntities ctx = new SSISEntities();

        public List<string> GetCatName()
        {
            List<string> catname = new List<string>();
            List<Category> catList = ctx.Categories.ToList<Category>();
            foreach (Category cat in catList)
            {
                string name = cat.cat_name;
                catname.Add(name);
            }
            return catname;
        }

        public List<Stock_Inventory> GetItemDetail(string name)
        {
            List<Stock_Inventory> itemList = ctx.Stock_Inventory.Where(x => x.Category.cat_name == name).ToList<Stock_Inventory>();
            return itemList;
        }

        public List<Request> GetAllRequest(string dept)
        {
            List<Request> req = ctx.Requests.Where(x => x.dept_code == dept && x.current_status == "Pending").ToList<Request>();

            return req;


        }

        public Dept_Registry login(string user)
        {
            Dept_Registry dr = ctx.Dept_Registry.Where(x => x.username == user).First();
            return dr;
        }
        public List<String> ListEmployeeName(string deptcode)
        {
            var list = ctx.Dept_Registry.Where(c => c.dept_code.Equals(deptcode)).Select(c => c.username).ToList<String>(); 
            return list;
        }

        public void CreateAppDuties(Approval_Duties ap)
        {
                ctx.Entry(ap).State = System.Data.Entity.EntityState.Added;
                ctx.SaveChanges();   
        }

        

        public List<MonthlyCheckModel> GetAllMonthlyCheck()
        {
            List<MonthlyCheckModel> modelList = new List<MonthlyCheckModel>();
            List<Stock_Inventory> inventoryList = ctx.Stock_Inventory.Where(x => x.deleted == "N").ToList();

            foreach (Stock_Inventory i in inventoryList)
            {
                MonthlyCheckModel model = new MonthlyCheckModel(i);
                modelList.Add(model);
            }

            return modelList;
        }

        public List<Request_Details> GetRequestDetail(string id)
        {
            int req_id = Convert.ToInt32(id);
            List<Request_Details> rd = ctx.Request_Details.Where(x => x.request_id == req_id).ToList<Request_Details>();

            return rd;
        }

        public Approval_Duties ListAppDuties(string deptcode)
        {
                var q = ctx.Approval_Duties.Where(x => x.dept_code.Equals(deptcode) && x.duty_id == ctx.Approval_Duties.Select(y => y.duty_id).Max()).ToList<Approval_Duties>()[0];
                return q;
        }

        /* public void UpdateDuty(Approval_Duties c)
         {
             ctx.Entry(c).State = System.Data.Entity.EntityState.Modified;
             ctx.SaveChanges();

         }*/

        public void UpdateDuty(String deptCode)
        {
            var q = ctx.Approval_Duties.Where(x => x.dept_code == deptCode && x.deleted == "N").First();
            q.deleted = "Y";
            ctx.SaveChanges();

        }

        public void Approve(String id)
        {
            int req_id = Convert.ToInt32(id);
            Request r = new Request();
            Request req = ctx.Requests.Where(x => x.request_id == req_id).First();
            req.current_status = RequestStatus.APPROVED;
            ctx.SaveChanges();
        }

        public void Reject(String id)
        {
            int req_id = Convert.ToInt32(id);
            Request r = new Request();
            Request req = ctx.Requests.Where(x => x.request_id == req_id).First();
            req.current_status = RequestStatus.REJECTED;
            req.rejected = "Y";
            ctx.SaveChanges();
        }


        public void CreateNewRequest(Request r)
        {
            ctx.Entry(r).State = System.Data.Entity.EntityState.Added;
            ctx.SaveChanges();
        }

        //Htein Lin Aung New Request

        public void NewRequest(string user, string dept_code, string reason, string date)
        {
            Request r = new Request();
            r.current_status = RequestStatus.PENDING;
            r.username = user;
            r.dept_code = dept_code;
            r.reason = reason;
            r.date_time = Convert.ToDateTime(date);
            r.deleted = "N";
            r.rejected = "N";
        }
    }
}