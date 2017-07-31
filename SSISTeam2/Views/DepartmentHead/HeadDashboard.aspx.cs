using SSISTeam2.Classes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2.Views
{
    public partial class HeadDashboard : System.Web.UI.Page
    {
        SSISEntities ent = new SSISEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillPage();
            }
        }
        private void FillPage()
        {
            // need to login
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("/login.aspx?return=Views/DepartmentHead/HeadDashboard.aspx");
            }

            string username = User.Identity.Name.ToString();
            UserModel user = new UserModel(username);
            string currentDept = user.Department.dept_code;
            var q = (from o in ent.Requests
                     where (currentDept == o.dept_code) && (o.current_status == RequestStatus.PENDING | o.current_status == RequestStatus.UPDATED)
                     select new { o.request_id, o.username , o.date_time, o.reason}).ToList();
            GridView1.DataSource = q;
            GridView1.DataBind();
            string pendingnum = GridView1.Rows.Count.ToString();
            lblPendingNum.Text = "YOU HAVE "+ pendingnum + " REQUEST(S) TO VIEW";

            Department dept = ent.Departments.Where(x => currentDept == x.dept_code).First();
            Collection_Point colpoint = ent.Collection_Point.Where(y => y.collection_pt_id == dept.collection_point).First();
            lblrep.Text = dept.rep_user;
            lblcolpoint.Text = colpoint.location + " (" + colpoint.day_of_week +")";

            var deldept = (from x in ent.Approval_Duties select x.dept_code).ToList();
            if (deldept.Contains(currentDept))
            {
                List<Approval_Duties> ads = ent.Approval_Duties.Where(x => currentDept == x.dept_code).ToList();
                foreach (Approval_Duties ad in ads)
                {
                    DateTime start = ad.start_date;
                    DateTime end = ad.end_date;
                    if ((DateTime.Now > start && DateTime.Now < end) && (ad.deleted == "N"))
                    {
                        lbldelegation.Text = ad.username + " (" + start + " - " + end + ")";
                        break;
                    }
                }
            }

           
        }
        
        protected void btnViewAllPending_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/DepartmentHead/ViewPending.aspx");
        }

        protected void btnchangecoll_Click(object sender, EventArgs e)
        {
            Response.Redirect("ChangeCollectionnRep.aspx");
        }

        protected void btnShowHistory_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Employee/EmpRequestHistory.aspx");
        }

        protected void btndelegate_Click(object sender, EventArgs e)
        {
            Response.Redirect("DelegateAuthority.aspx");
        }
    }
}
