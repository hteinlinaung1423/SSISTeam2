using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SSISTeam2.Classes.Models;

namespace SSISTeam2
{
    public partial class MonthlyCheck : System.Web.UI.Page
    {
        DateTime today;

        protected void Page_Load(object sender, EventArgs e)
        {
            SSISEntities context = new SSISEntities();
            UserModel um = new UserModel("hengtiong", context);
            today = DateTime.Today;
            DateTB.Text = today.Date.ToString("dd/MM/yyyy");
            Label1.Text = um.ContactNumber + um.Email + um.Username;
        }

        protected void AddBtn_Click(object sender, EventArgs e)
        {
            
        }
    }
}