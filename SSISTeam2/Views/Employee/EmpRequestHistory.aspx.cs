using SSISTeam2.Classes.Models;
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
        //string username = "yht";//testing
        //string currentDept = "REGR";//testing
        
        //string currentDept = user.Department.dept_code.ToString();*/
        
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {
                FillPage();
            }
        }

        protected Boolean IsEditable(String name, String status)
        {
           // string username = "Sally";//testing
            return name == (User.Identity.Name.ToString()) && (status == RequestStatus.PENDING  | status == RequestStatus.UPDATED);
            //return name == (username) && (status == RequestStatus.PENDING | status == RequestStatus.UPDATED);
        }
        private void FillPage()
        {
            // need to login
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/login.aspx?return=Views/Employee/EmpRequestHistory.aspx");
            }

            //UserModel currentUser = new UserModel(User.Identity.Name);

            //string username = "Sally";//testing
            string username = User.Identity.Name.ToString();
            UserModel user = new UserModel(username);
            string currentDept = user.Department.dept_code;
            var q = (from x in ent.Requests
                     where x.dept_code == currentDept
                     select new
                     {
                         x.request_id,
                         x.username,
                         x.date_time,
                         x.current_status
                     })
                     .Reverse()
                     .ToList();

            GridView2.DataSource = q;
            GridView2.DataBind();


        }
        protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            FillPage();
            GridView2.PageIndex = e.NewPageIndex;
            GridView2.DataBind();
        }
        protected void btnCreate_Click(object sender, EventArgs e)
        {   
            Response.Redirect("MakeNewRequest.aspx");
        }

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Template Field
            string id = (GridView2.SelectedRow.FindControl("lblreqid") as Label).Text;
            //Response.Redirect("~/ViewDetailsPage.aspx?key="+id);
        }
        
        protected void cancel_Click(int requestID)
        {

            //change status in request table
            var req = ent.Requests.SingleOrDefault(x => x.request_id == requestID);
            req.current_status = RequestStatus.CANCELLED;

            //change status in request event table
            var q = from r in ent.Requests
                    where r.request_id == requestID
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
                e.status = RequestStatus.CANCELLED;
            }
            ent.SaveChanges();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string username = User.Identity.Name.ToString();
            //string username = "Sally";
            UserModel user = new UserModel(username);
            var q = (from x in ent.Requests
                     where x.dept_code == user.Department.dept_code
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
            //
            
            if (e.CommandName == "view")
            {
                Response.Redirect("EmpRequestDetail.aspx?requestid=" + e.CommandArgument.ToString());
            }
            else if(e.CommandName == "update")
            {
                Response.Redirect("MakeNewRequest.aspx?edit=" + e.CommandArgument.ToString());
            }
            else if(e.CommandName == "cancel")
            {
                cancel_Click(Convert.ToInt32(e.CommandArgument.ToString()));
            }
        }

        protected void GridView2_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView2.EditIndex = -1;
            FillPage();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }



    }

}
    
