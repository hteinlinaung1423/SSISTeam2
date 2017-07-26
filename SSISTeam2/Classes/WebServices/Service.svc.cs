﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web.Security;

using SSISTeam2.Classes.Models;

namespace SSISTeam2.Classes.WebServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service : IService
    {
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
                WCF_Request request = new WCF_Request(r.username, r.request_id, r.date_time, r.reason);
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
    }
}