using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2.Views.Employee
{
    public partial class EmpRequestHistory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*if(!IsPostBack)
            {
                SSISEntities ent = new SSISEntities();
                var q = (from r in ent.Requests
                         join d in ent.Request_Details on this.ReqestId equals d.request_id
                         join v in ent.Request_Event on d.request_detail_id equals v.request_detail_id
                         select new
                         {
                             v.date_time,
                             r.username
                         }).ToList();
                GridView2.DataSource = q;
                GridView2.DataBind();
            }*/
        }

        private void FillPage()
        {

        }
    }
}