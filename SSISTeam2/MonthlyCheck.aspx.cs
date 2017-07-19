using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2
{
    public partial class MonthlyCheck : System.Web.UI.Page
    {
        DateTime today;

        protected void Page_Load(object sender, EventArgs e)
        {
            today = DateTime.Today;
            DateTB.Text = today.Date.ToString("dd/MM/yyyy");
        }

        protected void AddBtn_Click(object sender, EventArgs e)
        {

        }
    }
}