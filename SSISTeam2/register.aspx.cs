using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using SSISTeam2.Classes.Models;

namespace SSISTeam2
{
    public partial class register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CreateUserWizard1_CreatedUser(object sender, EventArgs e)
        {
            SSISEntities context = new SSISEntities();
            Dept_Registry user = new Dept_Registry();
            user.username = CreateUserWizard1.UserName;
            user.dept_code = "REGR";
            user.deleted = "N";
            context.Dept_Registry.Add(user);
            context.SaveChanges();

            Response.Redirect("notifysuccess.aspx?");
        }

    }
}