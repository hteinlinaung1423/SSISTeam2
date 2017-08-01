using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2.Views.Home
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (User.IsInRole("Employee"))
            {
                Response.Redirect("~/Views/Employee/EmpDashboard.aspx");
            }
            else if (User.IsInRole("Clerk") || User.IsInRole("Supervisor")) 
            {
                Response.Redirect("~/Views/StoreClerk/Dashboard.aspx");
            }
            else if (User.IsInRole("DeptHead"))
            {
                Response.Redirect("~/Views/DepartmentHead/HeadDashboard.aspx");
            }

            SSISEntities context = new SSISEntities();
            Category cat = context.Categories.Where(x => x.cat_id == 1).ToList().First();
            Label1.Text = cat.cat_name;
        }
    }
}