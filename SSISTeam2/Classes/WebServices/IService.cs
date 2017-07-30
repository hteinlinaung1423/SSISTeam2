﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

using SSISTeam2.Classes.Models;

namespace SSISTeam2.Classes.WebServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService
    {

        //Operations by Htein Lin Aung 

        [OperationContract]
        [WebGet(UriTemplate = "/DeliveryOrder", ResponseFormat = WebMessageFormat.Json)]
        List<int> GetDeliveryOrderId();

        [OperationContract]
        [WebGet(UriTemplate = "/Approve/{id}", ResponseFormat = WebMessageFormat.Json)]
        void Approve(string id);


        

        [OperationContract]
        [WebGet(UriTemplate = "/Reject/{id}", ResponseFormat = WebMessageFormat.Json)]
        void Reject(string id);

        [OperationContract]
        [WebGet(UriTemplate = "/DeliveryOrder/{id}", ResponseFormat = WebMessageFormat.Json)]
        List<Delivery_Details> GetDeliveryOrdersDetails(string id);

        [OperationContract]
        [WebGet(UriTemplate = "/PendingRequest/{dept}", ResponseFormat = WebMessageFormat.Json)]
        List<WCF_Request> GetAllRequest(string dept);

        [OperationContract]
        [WebGet(UriTemplate = "/RequestDetail/{id}", ResponseFormat = WebMessageFormat.Json)]
        List<WCF_RequestDetail> GetRequestDetail(string id);



        [OperationContract]
        [WebGet(UriTemplate = "/Category/", ResponseFormat = WebMessageFormat.Json)]
        List<string> GetCatName();

        [OperationContract]
        [WebGet(UriTemplate = "/Category/{name}", ResponseFormat = WebMessageFormat.Json)]
        List<WCF_Item> GetItemDetail(string name);

        [OperationContract]
        [WebGet(UriTemplate = "/login/{name}/{pass}", ResponseFormat = WebMessageFormat.Json)]
        WCF_User login(string name, string pass);

        [OperationContract]
        [WebGet(UriTemplate = "/EmployeeList/{deptcode}", ResponseFormat = WebMessageFormat.Json)]
        string[] GetDelgateEmployeeName(string deptcode);

        [OperationContract]
        [WebInvoke(UriTemplate = "/Create", Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json)]
        void Create(WCF_AppDuties Approval_Duties);

        //Service Contract by Heng Tiong

        [OperationContract]
        [WebGet(UriTemplate = "/InventoryCheck/", ResponseFormat = WebMessageFormat.Json)]
        List<WCF_MonthlyCheck> GetIMonthlyCheckModel();

        [OperationContract]
        [WebGet(UriTemplate = "/InventoryCheckName", ResponseFormat = WebMessageFormat.Json)]
        List<string> GetMonthlyCheckName();

        [OperationContract]
        [WebInvoke(UriTemplate = "/InventoryCheck/Update/{username}", Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json)]
        //void UpdateMonthlyCheck(List<WCF_MonthlyCheck> monthlyCheckList, string username);
        void UpdateMonthlyCheck(List<WCF_MonthlyCheck> monthlyCheck, string username);


        [OperationContract]
        [WebGet(UriTemplate = "/CheckApprovalDuties/{deptcode}", ResponseFormat = WebMessageFormat.Json)]
        WCF_AppDuties CheckAppDuties(string deptcode);

        [OperationContract]
        [WebInvoke(UriTemplate = "/Update", Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json)]
        string Update(WCF_AppDuties app);

        //By Yin
        [OperationContract]
        [WebGet(UriTemplate = "/RetriveTQty", ResponseFormat = WebMessageFormat.Json)]
        WCFItemTotalQty[] GetEachItemQty();

        [OperationContract]
        [WebGet(UriTemplate = "/DisbCollectP", ResponseFormat = WebMessageFormat.Json)]
        List<String> GetDisbCollectP();

        [OperationContract]
        [WebGet(UriTemplate = "/DisbCollectDept/{cpid}", ResponseFormat = WebMessageFormat.Json)]
        List<String> GetDisbCollectDept(string cpid);

        [OperationContract]
        [WebGet(UriTemplate = "/DisbDeptDetail/{deptname}", ResponseFormat = WebMessageFormat.Json)]
        List<WCFDeptTQty> GetDeptDetail(string deptname);


        //Htein Lin Aung Create New Request

        [OperationContract]
        [WebInvoke(UriTemplate = "/CreateNewRequest", Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json)]
        void ApplyNewRequest(WCF_NewReqeust req);
    }

    [DataContract]
    public class WCF_MonthlyCheck
    {
        [DataMember]
        public string itemCode;
        [DataMember]
        public string itemDescription;
        [DataMember]
        public string categoryName;
        [DataMember]
        public string currentQuantity;
        [DataMember]
        public string actualQuantity;
        [DataMember]
        public string reason;

        public WCF_MonthlyCheck(string itemCode, string itemDescription, string categoryName, string currentQuantity, string actualQuantity, string reason)
        {
            this.itemCode = itemCode;
            this.itemDescription = itemDescription;
            this.categoryName = categoryName;
            this.currentQuantity = currentQuantity;
            this.actualQuantity = actualQuantity;
            this.reason = reason;
        }

        [DataMember]
        public string ItemCode
        {
            get { return itemCode; }
            set { itemCode = value; }
        }
        [DataMember]
        public string ItemDescription
        {
            get { return itemDescription; }
            set { itemDescription = value; }
        }
        [DataMember]
        public string CategoryName
        {
            get { return categoryName; }
            set { categoryName = value; }
        }
        [DataMember]
        public string CurrentQuantity
        {
            get { return currentQuantity; }
            set { currentQuantity = value; }
        }
        [DataMember]
        public string ActualQuantity
        {
            get { return actualQuantity; }
            set { actualQuantity = value; }
        }
        [DataMember]
        public string Reason
        {
            get { return reason; }
            set { reason = value; }
        }
    }


    [DataContract]
    public class WCF_Item
    {
        [DataMember]
        public string itemDesc;

        [DataMember]
        public string itemCode;

        [DataMember]
        public string uom;

        [DataMember]
        public int catId;



        public WCF_Item(string itemDesc, string itemCode, string uom, int catId)
        {
            this.itemDesc = itemDesc;
            this.itemCode = itemCode;
            this.uom = uom;
            this.catId = catId;

        }

    }

    [DataContract]
    public class WCF_Request
    {
        [DataMember]
        string user;
        [DataMember]
        int req_id;
        [DataMember]
        string requestdate;
        [DataMember]
        string reason;

        public WCF_Request(string user, int req_id, string requestdate, string reason)
        {
            this.user = user;
            this.req_id = req_id;

            this.requestdate = requestdate;
            this.reason = reason;
        }

    }


    [DataContract]
    public class WCF_User
    {
        [DataMember]
        string dept_code;
        [DataMember]
        string user_name;
        [DataMember]
        string role;

        public WCF_User(string dept_code, string user_name, string role)
        {
            this.dept_code = dept_code;
            this.user_name = user_name;
            this.role = role;
        }




    }

    [DataContract]
    public class WCF_AppDuties
    {

        public string username;
        public string startDate;
        public string endDate;
        public string deptCode;
        public string createdDate;
        public string deleted;
        public string reason;

        public static WCF_AppDuties Make(string username, string startDate, string endDate, string deptCode, string createdDate, string deleted, string reason)
        {
            WCF_AppDuties c = new WCF_AppDuties();
            c.username = username;
            c.createdDate = createdDate;
            c.deptCode = deptCode;
            c.reason = reason;
            c.startDate = startDate;
            c.endDate = endDate;
            c.deleted = deleted;
            return c;

        }
        [DataMember]
        public string UserName
        {
            get { return username; }
            set { username = value; }
        }

        [DataMember]
        public string StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        [DataMember]
        public string EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }


        [DataMember]
        public string DeptCode
        {
            get { return deptCode; }
            set { deptCode = value; }
        }


        [DataMember]
        public string CreatedDate
        {
            get { return createdDate; }
            set { createdDate = value; }
        }
        [DataMember]
        public string Deleted
        {
            get { return deleted; }
            set { deleted = value; }
        }
        [DataMember]
        public string Reason
        {
            get { return reason; }
            set { reason = value; }
        }


    }

    [DataContract]
    public class WCF_RequestDetail
    {
        [DataMember]
        string itemdesc;
        [DataMember]
        int quantity;

        
        

        public WCF_RequestDetail(string itemdesc, int quantity)
        {
            this.itemdesc = itemdesc;
            this.quantity = quantity;
        }

       

    }

    //By Yin
    [DataContract]
    public class WCFItemTotalQty
    {
        [DataMember]
        string itemDes;
        [DataMember]
        string totalQty;

        public WCFItemTotalQty() : this("", "")
        {

        }
        public WCFItemTotalQty(string itemDes, string totalQty)
        {
            this.itemDes = itemDes;
            this.totalQty = totalQty;
        }

        public string ItemDes
        {
            get
            {
                return itemDes;
            }

            set
            {
                itemDes = value;
            }
        }

        public string TotalQty
        {
            get
            {
                return totalQty;
            }

            set
            {
                totalQty = value;
            }
        }

    }

    [DataContract]
    public class WCF_NewReqeust
    {
        [DataMember]
        string user;
        [DataMember]
        string dept_code;
        [DataMember]
        string reason;
        [DataMember]
        string status;
        [DataMember]
        string date_time;




        public WCF_NewReqeust(string user, string dept_code, string reason, string status, string date_time)
        {
            this.user = user;
            this.dept_code = dept_code;
            this.reason = reason;
            this.status = status;
            this.date_time = date_time;
        }

        [DataMember]
        public string Name
        {
            get { return user; }
            set { user = value; }
        }


        [DataMember]
        public string DeptCode
        {
            get { return dept_code; }
            set { dept_code = value; }
        }

        [DataMember]
        public string Reason
        {
            get { return reason; }
            set { reason = value; }
        }

        [DataMember]
        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        [DataMember]
        public string Date
        {
            get { return date_time; }
            set { date_time = value; }
        }



    }

    [DataContract]
    public class WCFDeptTQty
    {
        [DataMember]
        string itemName;
        [DataMember]
        int reqQty;

        public WCFDeptTQty() : this("", 0)
        {

        }
        public WCFDeptTQty(string itemDes, int reqQty)
        {
            this.ItemDes = itemDes;
            this.ReqQty = reqQty;
        }

        public string ItemDes
        {
            get
            {
                return itemName;
            }

            set
            {
                itemName = value;
            }
        }

        public int ReqQty
        {
            get
            {
                return reqQty;
            }

            set
            {
                reqQty = value;
            }
        }


    }

}


