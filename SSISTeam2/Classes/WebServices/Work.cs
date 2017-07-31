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

        public void UpdateMonthlyCheck(List<WCF_MonthlyCheck> list, string username)
        {
            Inventory_Adjustment invAdjustmentSup = new Inventory_Adjustment();
            invAdjustmentSup.deleted = "N";
            invAdjustmentSup.clerk_user = username;
            invAdjustmentSup.status = "Pending";
            invAdjustmentSup.date = DateTime.Today;
            invAdjustmentSup.status_date = DateTime.Today;

            Inventory_Adjustment invAdjustmentMan = new Inventory_Adjustment();
            invAdjustmentMan.deleted = "N";
            invAdjustmentMan.clerk_user = username;
            invAdjustmentMan.status = "Pending";
            invAdjustmentMan.date = DateTime.Today;
            invAdjustmentMan.status_date = DateTime.Today;

            foreach (WCF_MonthlyCheck i in list)
            {
                int actual = int.Parse(i.actualQuantity);
                int current = int.Parse(i.CurrentQuantity);
                int adjusted = current - actual;

                if (adjusted == 0)
                    continue;

                Stock_Inventory inventory = ctx.Stock_Inventory.Where(x => x.item_code == i.ItemCode).ToList().First();
                inventory.current_qty = actual;

                MonthlyCheckModel itemModel = new MonthlyCheckModel(inventory);
                double cost = Math.Abs(adjusted) * itemModel.AveragePrice;

                Adjustment_Details adjDetails = new Adjustment_Details();
                adjDetails.deleted = "N";
                adjDetails.item_code = i.ItemCode;
                adjDetails.quantity_adjusted = adjusted;
                adjDetails.reason = i.Reason;

                if (cost < 250)
                {
                    invAdjustmentSup.Adjustment_Details.Add(adjDetails);
                }
                if (cost >= 250)
                {
                    invAdjustmentMan.Adjustment_Details.Add(adjDetails);
                }
                ctx.Adjustment_Details.Add(adjDetails);
            }

            if (invAdjustmentSup.Adjustment_Details.Count != 0)
            {
                ctx.Inventory_Adjustment.Add(invAdjustmentSup);
                ctx.SaveChanges();
            }
            if (invAdjustmentMan.Adjustment_Details.Count != 0)
            {
                ctx.Inventory_Adjustment.Add(invAdjustmentMan);
                ctx.SaveChanges();
            }
        }

        public void UpdateMonthlyCheckRecord(string username, bool discrepencyFound)
        {
            Monthly_Check_Records checkRecords = new Monthly_Check_Records();
            checkRecords.clerk_user = username;
            checkRecords.date_checked = DateTime.Today;
            checkRecords.deleted = "N";
            string yesOrNo = "";
            if (discrepencyFound)
                yesOrNo = "Y";
            else
                yesOrNo = "N";
            checkRecords.discrepancy = yesOrNo;
            ctx.Monthly_Check_Records.Add(checkRecords);
            ctx.SaveChanges();
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

        //By Yin
        public List<WCFItemTotalQty> wgetEachItemQty()
        {
            var q = (from r in ctx.Requests
                     join x in ctx.Request_Details on r.request_id equals x.request_id
                     join y in ctx.Stock_Inventory on x.item_code equals y.item_code
                     join ee in ctx.Request_Event on x.request_detail_id equals ee.request_detail_id
                     where r.current_status == "Approved" && ee.status == "Retrieving"
                     group x by y.item_description into g
                     select new WCFItemTotalQty
                     {
                         ItemDes = g.Key,
                         TotalQty = g.Sum(d => d.orig_quantity).ToString(),
                     }).ToList<WCFItemTotalQty>();

            return q.ToList<WCFItemTotalQty>();
        }

        public List<String> wgetCollectP()
        {
            var q = ctx.Collection_Point.Select(x => x.location).ToList<String>();
            return q.ToList<String>();
        }

        public List<String> wgetCollectDept(string cpid)
        {
            int i = Int16.Parse(cpid);
            var q = ctx.Departments.Where(x => x.collection_point == i)
                .Select(y => y.name);
            return q.ToList<String>();
        }

        public List<WCFDeptTQty> wgetDepDetail(string deptname)
        {
            var q = (from de in ctx.Departments
                     join rq in ctx.Requests on de.dept_code equals rq.dept_code
                     join rqd in ctx.Request_Details on rq.request_id equals rqd.request_id
                     join rqe in ctx.Request_Event on rqd.request_detail_id equals rqe.request_detail_id
                     join st in ctx.Stock_Inventory on rqd.item_code equals st.item_code
                     where de.name == deptname && rq.current_status == "Approved" || rq.current_status == "Part_Disbursed" && rqe.status == "Disbursing"
                     select new WCFDeptTQty
                     {
                         ItemDes = st.item_description,
                         ReqQty = rqe.quantity
                     }).ToList();

            return q.ToList<WCFDeptTQty>();
        }

        //Apply New Request

        public void ApplyNewRequest(Request r)
        {
            ctx.Entry(r).State = System.Data.Entity.EntityState.Added;
            ctx.SaveChanges();
        }
    }




}