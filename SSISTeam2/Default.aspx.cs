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
            if (User.IsInRole("Clerk"))
            {
                Response.Redirect("Dashboard.aspx");
            }

            SSISEntities context = new SSISEntities();
            Category cat = context.Categories.Where(x => x.cat_id == 1).ToList().First();
            Label1.Text = cat.cat_name;
        }
    }
}