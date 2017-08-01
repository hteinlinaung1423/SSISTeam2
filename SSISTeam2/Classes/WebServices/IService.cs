using System;
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
        List<WCFRetieve> GetEachItemQty();

        //[OperationContract]
        //[WebInvoke(UriTemplate = "/RetriveTQty/Update/{loginUserName}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //void UpdateRetrieveQty(string loginUserName,List<WCFRetieve> retrieveList);


        [OperationContract]
        [WebGet(UriTemplate = "/DisbCollectP", ResponseFormat = WebMessageFormat.Json)]
        List<String> GetDisbCollectP();

        [OperationContract]
        [WebGet(UriTemplate = "/DisbCollectDept/{cpid}", ResponseFormat = WebMessageFormat.Json)]
        List<String> GetDisbCollectDept(string cpid);

        [OperationContract]
        [WebGet(UriTemplate = "/DisbDeptDetail/{deptname}", ResponseFormat = WebMessageFormat.Json)]
        List<WCFDisburse> GetDeptDetail(string deptname);

        [OperationContract]
        [WebGet(UriTemplate = "/ViewAllAdjustment/{role}", ResponseFormat = WebMessageFormat.Json)]
        List<WCFInventoryAdjustmentModel> GetAllAdjustmentList(string role);

        [OperationContract]
        [WebGet(UriTemplate = "/ViewAllAdjustmentDetail/{id}", ResponseFormat = WebMessageFormat.Json)]
        List<WCFInventoryAdjustmentDetailModel> AdjustmentDetailList(string id);

        [OperationContract]
        [WebInvoke(UriTemplate = "/UpdateInventoryAdjustment", Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json)]
        void UpdateInventoryAdj(String voucherId);

        [OperationContract]
        [WebInvoke(UriTemplate = "/DeleteInventoryAdjustment", Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json)]
        void DeleteInventoryAdj(String voucherId);


        //Htein Lin Aung Create New Request

        [OperationContract]
        [WebInvoke(UriTemplate = "/CreateNewRequest", Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json)]
        void ApplyNewRequest(WCF_NewReqeust req);
        

        [OperationContract]
        [WebInvoke(UriTemplate = "/DisburseTQty/Update/{loginUserName}/{deptCode}", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void UpdateDisburseQty(string loginUserName, string deptcode, List<WCFDisburse> disburseList);
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
        [DataMember]
        string flag;

        public WCF_User(string dept_code, string user_name, string role,string flag)
        {
            this.dept_code = dept_code;
            this.user_name = user_name;
            this.role = role;
            this.flag = flag;
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
    public class WCFRetieve
    {
        [DataMember]
        string itemDes;
        [DataMember]
        string totalQty;
        [DataMember]
        int retrieveQty;

        public WCFRetieve() : this("", "",0)
        {

        }
        public WCFRetieve(string itemDes, string totalQty, int retrieveQty)
        {
            this.itemDes = itemDes;
            this.totalQty = totalQty;
            this.retrieveQty = retrieveQty;
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
        public int RetrieveQty
        {
            get
            {
                return retrieveQty;
            }

            set
            {
                retrieveQty = value;
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



    }

[DataContract]
public class WCFDisburse
{
    [DataMember]
    string itemName;
    [DataMember]
    int retrievedQty;
    [DataMember]
    int disbursedQty;


    public WCFDisburse() : this("", 0, 0)
    {

    }
    public WCFDisburse(string itemName, int retrievedQty, int disbursedQty)
    {
        this.itemName = itemName;
        this.retrievedQty = retrievedQty;
        this.disbursedQty = disbursedQty;
    }


    public string ItemName
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

    public int RetrievedQty
    {
        get
        {
            return retrievedQty;
        }

        set
        {
            retrievedQty = value;
        }
    }


    public int DisbursedQty
    {
        get
        {
            return disbursedQty;
        }
        set
        {
            disbursedQty = value;
        }
    }
}
    [DataContract]
    public class WCFInventoryAdjustmentModel
    {

        public string voucherID;
        public string clerk;
        public string date;
        public string status;
        public string highestCost;


        public WCFInventoryAdjustmentModel(string voucherID, string clerk, string date, string status, string highestCost)
        {
            
            this.voucherID = voucherID;
            this.clerk = clerk;
            this.date = date;
            this.status = status;
            this.highestCost = highestCost;
            

        }
        [DataMember]
        public string VoucherID
        {
            get { return voucherID; }
            set { voucherID = value; }
        }
        [DataMember]
        public string Clerk
        {
            get { return clerk; }
            set { clerk = value; }
        }
        [DataMember]
        public string Date
        {
            get { return date; }
            set { date = value; }
        }
        [DataMember]
        public string Status
        {
            get { return status; }
            set { status = value; }
        }
        [DataMember]
        public string HighestCost
        {
            get { return highestCost; }
            set { highestCost = value; }
        }      
    }

    [DataContract]
    public class WCFInventoryAdjustmentDetailModel
    {

        public string itemdesc;
        public string qtyadjust;
        public string priceadjust;
        public string reason;


        public WCFInventoryAdjustmentDetailModel(string itemdesc, string qtyadjust, string priceadjust, string reason)
        {

            this.itemdesc = itemdesc;
            this.qtyadjust = qtyadjust;
            this.priceadjust = priceadjust;
            this.reason = reason;


        }
        [DataMember]
        public string ItemDesc
        {
            get { return itemdesc; }
            set { itemdesc = value; }
        }
        [DataMember]
        public string QtyAdjust
        {
            get { return qtyadjust; }
            set { qtyadjust = value; }
        }
        [DataMember]
        public string PriceAdjust
        {
            get { return priceadjust; }
            set { priceadjust = value; }
        }
        [DataMember]
        public string Reason
        {
            get { return reason; }
            set { reason = value; }
        }
        
    }


