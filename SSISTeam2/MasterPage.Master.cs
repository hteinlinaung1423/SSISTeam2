using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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

        protected void MakeNewRequest(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/StoreClerk/MakeNewRequest.aspx");
        }

        protected void ViewRequestHistory(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Employee/EmpRequestHistory.aspx");
        }

        protected void ViewPendingReq(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Employee/EmpRequestHistory.aspx");
        }

        protected void ApproveBtn(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/DepartmentHead/ApproveReject.aspx");
        }

        protected void DelegateAuth(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/DepartmentHead/DelegateAuthority.aspx");
        }

        protected void genReport(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Reporting/ReportsMain.aspx");
        }
    }
}