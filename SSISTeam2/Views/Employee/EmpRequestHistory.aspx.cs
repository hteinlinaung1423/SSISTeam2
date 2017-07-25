using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2.Views.Employee
{
    public partial class EmpRequestHistory : System.Web.UI.Page
    {
        SSISEntities ent = new SSISEntities();
        int selectreqid = 4;//testing
        string username = "Sally";//testing
        string currentDept = "REGR";//testing

        //string username = User.Identity.Name.ToString();
        //UserModel user = new UserModel(username);
        //string currentDept = user.Department.dept_code.ToString();*/
        string statusPending = "Pending";
        string statusUpdated = "Updated";
        protected void Page_Load(object sender, EventArgs e)
        {
            //selectreqid = Int16.Parse(Request.QueryString["key"]); 

            if (!IsPostBack)
            {
                FillPage();
            }
        }

        protected Boolean IsEditable(String name, String status)
        {
            return name == username && (status == statusPending | status == statusUpdated);
        }
        private void FillPage()
        {
            var q = (from x in ent.Requests
                     where x.dept_code == currentDept
                     select new
                     {
                         x.request_id,
                         x.username,
                         x.date_time,
                         x.current_status
                     }).ToList();

            GridView2.DataSource = q;
            GridView2.DataBind();
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {   
            //not sure about the aspx name
            //Response.Redirect("~/ViewRequestDetails?username=" + e.CommandArgument);
            //Response.Redirect("~/CreateNewReq.aspx");
        }

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Template Field
            string id = (GridView2.SelectedRow.FindControl("lblreqid") as Label).Text;
            //Response.Redirect("~/ViewDetailsPage.aspx?key="+id);
        }

        protected void details_Click(object sender, EventArgs e)
        {
        }

        protected void update_Click(object sender, EventArgs e)
        {
        }

        protected void cancel_Click(object sender, EventArgs e)
        {

            //change status in request table
            var req = ent.Requests.SingleOrDefault(x => x.request_id == selectreqid);
            req.current_status = "Cancelled";

            //change status in request event table
            var q = from r in ent.Requests
                    where r.request_id == selectreqid
                    join de in ent.Request_Details on r.request_id equals de.request_id
                    select de.request_detail_id;
            List<int> reqDetails = q.ToList();
            int[] detailidint = reqDetails.ToArray();
            foreach (int i in detailidint)
            {
                cancelRequest(i);
            }
            ent.SaveChanges();
            //lbltest.Text = detailids.Count.ToString(); //test
            FillPage();
        }

        public void cancelRequest(int detailid)
        {
            var p = from de in ent.Request_Details
                    where de.request_detail_id == detailid
                    join ev in ent.Request_Event on de.request_detail_id equals ev.request_detail_id
                    select ev;
            List<Request_Event> events = p.ToList();
            foreach (Request_Event e in events)
            {
                e.status = "Cancelled";
            }
            ent.SaveChanges();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            var q = (from x in ent.Requests
                     where x.dept_code == currentDept
                     select new
                     {
                         x.request_id,
                         x.username,
                         x.date_time,
                         x.current_status
                     });
            string searchString = searchtext.Text;

            if (!String.IsNullOrEmpty(searchString))
            {
                q = q.Where(s => s.username.Contains(searchString));
            }
            GridView2.DataSource = q.ToList();
            GridView2.DataBind();
        }
        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //not sure about the aspx name
            //Response.Redirect("~/ViewRequestDetails?requestid=" + e.CommandArgument);
        }

    }
    
}
    
