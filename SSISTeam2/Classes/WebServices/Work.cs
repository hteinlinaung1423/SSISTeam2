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
        string statusFlag = "F";


        //Htein LastRequest

        public Request GetRequest()
        {
            return ctx.Requests.OrderBy(x => x.request_id).ToList().Last();
        }

        public Stock_Inventory GetStockInventory(string name)
        {
            return ctx.Stock_Inventory.Where(x => x.item_description == name).First();
        }

        public Request_Details GetLastRequestDetail()
        {
            return ctx.Request_Details.OrderBy(x => x.request_detail_id).ToList().Last();
        }



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
            List<Request> req = ctx.Requests.Where(x => x.dept_code == dept && x.current_status == "Pending").OrderByDescending(x=> x.date_time).ToList<Request>();

            return req;


        }

        public List<Request> GetAllRequestByDeptCode(string dept)
        {
            List<Request> req = ctx.Requests.Where(x => x.dept_code == dept).OrderByDescending(x=>x.date_time).ToList<Request>();

            return req;


        }

        public List<Request> GetAllRequestByUserName(string dept,string user)

        {
            

            List<Request> req = ctx.Requests.Where(x => x.dept_code == dept && x.Dept_Registry.fullname==user).OrderByDescending(x => x.date_time).ToList<Request>();

            return req;


        }



        public Dept_Registry login(string user)
        {

            Dept_Registry dr = ctx.Dept_Registry.Where(x => x.username == user).First();
            return dr;
        }
        //Getting ApprovalDuties Status
        //public string CheckApprovalDutiesStatus()
        //{           
        //    string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
        //    try
        //    {
        //        var result = ctx.Approval_Duties.Where(x => x.deleted == "N").Select(x => x.end_date).Max();
        //        DateTime date = Convert.ToDateTime(result.ToString());
        //        string endDate = date.ToString("yyyy-MM-dd");
        //        if (endDate.CompareTo(currentDate) == -1)
        //        {
        //            var q = ctx.Approval_Duties.Where(x => x.deleted == "N").First();
        //            q.deleted = "Y";
        //            ctx.SaveChanges();

        //        }
        //        return statusFlag = "T";

        //    }
        //    catch
        //    {
        //        return statusFlag = "T";
        //    }
        //}

        public string GetDepHeadRole(string user)
        {
            
            string flag = null;
            try
            {
                var result = ctx.Approval_Duties.Where(x => x.username == user && x.deleted == "N").Count();
                if (result != 0)
                {
                    flag = "Y";                   
                }
            }
            catch
            {
                flag = "N";           
            }
            return flag;
        }
        
        //public Dept_Registry login(string user)
        //{
        //    String falg = "Y";int count = 0;

        //    Dept_Registry dr = ctx.Dept_Registry.Where(x => x.username == user).First();
        //    try
        //    {
        //        var result = ctx.Approval_Duties.Where(x => x.username == user && x.deleted == "N").Count();
        //        if (result != 0)
        //        {

        //            return dr;
        //        }


        //    }catch
        //    {

        //    }

        //    Dept_Registry dr = ctx.Dept_Registry.Where(x => x.username == user).First();
        //    return dr;
        //}
        public List<String> ListEmployeeName(string deptcode)
        {
            var q = ctx.Departments.Where(x => x.dept_code.Equals(deptcode)).Select(x => x.head_user);
            string headUserName = q.First();

            var depts = ctx.Departments.Where(x => x.dept_code.Equals(deptcode)).ToList();
            var delegateHead = depts.Select(x => new UserModel(x.head_user).FindDelegateOrDeptHead().Username);
            string delegateUserName = delegateHead.First();

            var list = ctx.Dept_Registry
                .Where(c => 
                c.dept_code.Equals(deptcode)
                && c.username != headUserName
                && c.username != delegateUserName
                ).Select(c => c.fullname).ToList<String>();
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

        public bool UpdateMonthlyCheck(List<WCF_MonthlyCheck> monthlyChecks, string username)
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

            foreach (WCF_MonthlyCheck i in monthlyChecks)
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

            if (invAdjustmentSup.Adjustment_Details.Count != 0 || invAdjustmentMan.Adjustment_Details.Count != 0)
                return true;
            else
                return false;
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

        public void UpdateFileDiscrepancies(List<WCF_FileDiscrepancy> fileDiscrepancies, string username)
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

            foreach (WCF_FileDiscrepancy i in fileDiscrepancies)
            {
                int adjusted = int.Parse(i.adjustedQty);

                Stock_Inventory inventory = ctx.Stock_Inventory.Where(x => x.item_description == i.itemName).ToList().First();
                inventory.current_qty += adjusted;

                MonthlyCheckModel itemModel = new MonthlyCheckModel(inventory);
                double cost = Math.Abs(adjusted) * itemModel.AveragePrice;

                Adjustment_Details adjDetails = new Adjustment_Details();
                adjDetails.deleted = "N";
                adjDetails.item_code = inventory.item_code;
                adjDetails.quantity_adjusted = adjusted;
                adjDetails.reason = i.reason;

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

        public List<Request_Details> GetRequestDetail(string id)
        {
            int req_id = Convert.ToInt32(id);
            List<Request_Details> rd = ctx.Request_Details.Where(x => x.request_id == req_id && x.deleted=="N").ToList<Request_Details>();

            return rd;
        }

        public Approval_Duties ListAppDuties(string deptcode)
        {

            try
            {
                var q = ctx.Approval_Duties.Where(x => x.dept_code.Equals(deptcode) && x.duty_id == ctx.Approval_Duties.Select(y => y.duty_id).Max()).ToList<Approval_Duties>()[0];
                return q;
            }
            catch
            {
                return null;
            }





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

            List<Request_Details> rdetailList = ctx.Request_Details.Where(x => x.request_id == req_id).ToList();
            foreach (Request_Details rdetail in rdetailList)
            {
                Request_Event revent = ctx.Request_Event.Where(x => x.request_detail_id == rdetail.request_detail_id).First();
                revent.status = RequestStatus.APPROVED;
                revent.date_time = DateTime.Today;
                ctx.SaveChanges();

            }
        }

        public void Reject(String id)
        {
            int req_id = Convert.ToInt32(id);
            Request r = new Request();
            Request req = ctx.Requests.Where(x => x.request_id == req_id).First();
            req.current_status = RequestStatus.REJECTED;
            req.rejected = "Y";
            ctx.SaveChanges();
            List<Request_Details> rdetailList = ctx.Request_Details.Where(x => x.request_id == req_id).ToList();
            foreach (Request_Details rdetail in rdetailList)
            {
                Request_Event revent = ctx.Request_Event.Where(x => x.request_detail_id == rdetail.request_detail_id).First();
                revent.status = RequestStatus.REJECTED;
                revent.date_time = DateTime.Today;
                ctx.SaveChanges();

            }
        }

        //By Yin
        public List<WCFRetieve> wgetEachItemQty(string currentUser)
        {
            return MobileConfirmation.GetAllPossibleRetrievalsForUser(currentUser);
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

        //public List<WCFDisburse> wgetDepDetail(string deptname)
        //{
        //    var q = (from de in ctx.Departments
        //             join rq in ctx.Requests on de.dept_code equals rq.dept_code
        //             join rqd in ctx.Request_Details on rq.request_id equals rqd.request_id
        //             join rqe in ctx.Request_Event on rqd.request_detail_id equals rqe.request_detail_id
        //             join st in ctx.Stock_Inventory on rqd.item_code equals st.item_code
        //             where de.name == deptname && rqe.status == "Disbursing"
        //             where rq.current_status == "Approved" || rq.current_status == "PartDisbursed"
        //             select new WCFDisburse
        //             {
        //                 ItemName = st.item_description,
        //                 RetrievedQty = rqe.quantity.
        //             })
        //             .Where(w => w.RetrievedQty > 0)
        //             .ToList();

        //    return q.ToList<WCFDisburse>();
        //}

        public List<Inventory_Adjustment> GetAdjustmentList()
        {
            var invAdjList = ctx.Inventory_Adjustment.Where(x => x.deleted == "N" & x.status == "Pending").ToList();
            return invAdjList;
        }

        public List<Adjustment_Details> GetViewAdjustmentDetailList(string id)
        {
            int voucherId = Convert.ToInt32(id);
            var invAdjList = ctx.Adjustment_Details.Where(x => x.voucher_id.Equals(voucherId)).ToList();
            return invAdjList;

        }

        //Apply New Request

        public void ApplyNewRequest(Request r)
        {
            ctx.Entry(r).State = System.Data.Entity.EntityState.Added;
            ctx.SaveChanges();
        }

        public void CreateRequestDetail(Request_Details r)
        {
            ctx.Entry(r).State = System.Data.Entity.EntityState.Added;
            ctx.SaveChanges();
        }

        public void CreateRequestEvent(Request_Event r)
        {
            ctx.Entry(r).State = System.Data.Entity.EntityState.Added;
            ctx.SaveChanges();
        }

        public void updateAdjustment(String voucherId)
        {
            int vId = Convert.ToInt32(voucherId);
            var q = ctx.Inventory_Adjustment.Where(x => x.voucher_id == vId).First();
            q.status = "Approved";
            ctx.SaveChanges();

        }

        public void deleteAdjustment(String voucherId)
        {
            int vId = Convert.ToInt32(voucherId);
            var q = ctx.Inventory_Adjustment.Where(x => x.voucher_id == vId).First();
            q.status = "Rejected";
            ctx.SaveChanges();

        }
        public string GetUserName(String fullName)
        {
            var q = ctx.Dept_Registry.Where(x => x.fullname.Equals(fullName)).Select(x => x.username);
            return q.First();
        }

        public void UpdateRequestDetail(string id, string qty)
        {
            int req_id = Convert.ToInt32(id);
            
            Request_Details req = ctx.Request_Details.Where(x => x.request_id == req_id).First();
            req.orig_quantity = Convert.ToInt32( qty);
            ctx.SaveChanges();

            Request_Event revent = ctx.Request_Event.Where(x => x.request_detail_id == req.request_detail_id).First();
            revent.quantity = Convert.ToInt32(qty);
            ctx.SaveChanges();
        }
    }



}


