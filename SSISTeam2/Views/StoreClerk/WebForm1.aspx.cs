using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2.Views.StoreClerk
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        SSISEntities ctx = new SSISEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            var result = ctx.Approval_Duties.Where(x => x.deleted == "N").Select(x => x.end_date).Max();
            DateTime date = Convert.ToDateTime(result.ToString());
            string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
            string endDate = date.ToString("yyyy-MM-dd");
            Label1.Text = endDate;
            if (endDate.CompareTo(currentDate) < 0)
            {
                var q = ctx.Approval_Duties.Where(x => x.deleted == "N").First();
                q.deleted = "Y";
                ctx.SaveChanges();

            }
        }
    }
}