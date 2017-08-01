using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using SSISTeam2.Classes.Models;

namespace SSISTeam2
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            

            if (Request.IsAuthenticated)
            {
                string currentUser = Page.User.Identity.Name;

                string fullName = "";
                using (SSISEntities ctx = new SSISEntities())
                {
                    fullName = ctx.Dept_Registry.Find(currentUser).fullname;

                    // Check if user is department rep
                    int count = ctx.Departments.Where(d => d.rep_user == currentUser).Count();
                    if (count > 0)
                    {
                        // is a dept rep
                        linkBtnDepRepView.Visible = true;
                        //btnDepRepViewDisbursements.Visible = true;
                    } else
                    {
                        linkBtnDepRepView.Visible = false;
                        //btnDepRepViewDisbursements.Visible = false;
                    }
                }

                lblFullName.Text = "Welcome, "+fullName;
               

            }
            

        }

        protected void Register(object sender, EventArgs e)
        {
            Response.Redirect("~/register.aspx");
        }
        protected void Login(object sender, EventArgs e)
        {
            Response.Redirect("~/login.aspx");
        }

        protected void Logout(object sender, EventArgs e)
        {
            System.Web.Security.FormsAuthentication.SignOut();
            Session["tender"] = null;
            Session["item"] = null;
            Session.Clear();
            Response.Redirect("~/Login.aspx");
        }

        protected void Default(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

        protected void LowStock(object sender, EventArgs e)
        {
            
            Response.Redirect("~/Views/StoreClerk/lowstock.aspx");
        }

        protected void MakeOrder(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/StoreClerk/Cart.aspx");
        }

        protected void GenerateRetrieval(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/StoreClerk/GenerateRetrieval.aspx");
        }

        protected void MakeNewRequest(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Employee/MakeNewRequest.aspx");
        }

        protected void ViewRequestHistory(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Employee/EmpRequestHistory.aspx");
        }

        //protected void ViewPendingReq(object sender, EventArgs e)
        //{
        //    Response.Redirect("~/Views/Employee/EmpRequestHistory.aspx");
        //}

        protected void ApproveBtn(object sender, EventArgs e)
        {           

            Response.Redirect("~/Views/DepartmentHead/ViewPending.aspx");

            
            //Delegate  (access department-head'all pages except "DelegateAuthority.aspx")
            //try
            //{
            //    string loginUserame = Page.User.Identity.Name;
            //    UserModel loginUser = new UserModel(loginUserame);
            //    UserModel delegat = loginUser.FIndDelegateHead();

            //    if (loginUserame == delegat.Username)
            //    {
            //        Response.Redirect("~/Views/DepartmentHead/ViewPending.aspx");
            //    }

            //    else
            //    {
            //        Response.Redirect("~/Default.aspx");
            //    }

            //}
            //catch
            //{
            //    Response.Redirect("~/Default.aspx");
            //}

        }

        protected void DelegateAuth(object sender, EventArgs e)
        {
            //Yin
            if(Page.User.IsInRole("DeptHead"))
            {
                Response.Redirect("~/Views/DepartmentHead/DelegateAuthority.aspx");
            }
            Response.Redirect("~/Login.aspx");

        }
        protected void ChangeCPRep(object sender, EventArgs e)
        {
            //Yin
            SSISEntities context = new SSISEntities();
            string loginUser = Page.User.Identity.Name;
            string logindepCode = context.Dept_Registry.Where(b => b.username == loginUser).Select(c => c.dept_code).First().ToString();
            Department dept = context.Departments.Where(d => d.dept_code == logindepCode).Single();
            Session["sDept"] = dept;
            Response.Redirect("~/Views/DepartmentHead/ChangeCollectionnRep.aspx");
        }

        protected void genReport(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Reporting/ReportsMain.aspx");
        }
        protected void ViewCatalogue_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Employee/ViewCatalogue.aspx");
        }

        protected void btnDepRepViewDisbursements_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Employee/RepViewDisbursements.aspx");
        }

        protected void btnEmpDash_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Employee/Dashboard.aspx");
        }

        protected void btnClerkDash_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/StoreClerk/Dashboard.aspx");
        }

        protected void btnNewRequest_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Employee/MakeNewRequest.aspx");
        }

        protected void btnEmpRequestHistory_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Employee/EmpRequestHistory.aspx");
        }

        protected void btnEmpViewCatalogue_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Employee/ViewCatalogue.aspx");
        }

        protected void btnGenerateRetrieval_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/StoreClerk/GenerateRetrieval.aspx");
        }

        protected void btnStoreCart_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/StoreClerk/Cart.aspx");
        }

        protected void btnHeadDashboard_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/DepartmentHead/HeadDashboard.aspx");
        }

        protected void btnApproveRequests_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/DepartmentHead/ViewPending.aspx");
        }

        protected void btnGenerateReports_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Reporting/ReportsMain.aspx");
        }

        protected void btnMakeOrder_Mgr_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/StoreClerk/MakeOrder.aspx");
        }

        protected void btnLowStocks_Mgr_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/StoreClerk/lowstock.aspx");
        }

        protected void btnGenerateDisbursement_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/StoreClerk/GenerateDisbursement.aspx");
        }

        protected void btn_ViewPendingOrder_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/StoreClerk/ViewPendingOrder.aspx");
        }

        protected void btnMonthlyCheck_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/StoreClerk/MonthlyCheck.aspx");
        }

        protected void btnViewAllAdjustments_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/StoreManager/ViewAllAdjustment.aspx");
        }
    }
}