using SSISTeam2.Classes.EFFacades;
using SSISTeam2.Classes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2
{
    public partial class ViewAllPending : System.Web.UI.Page
    {
        SSISEntities ent = new SSISEntities();
        string userName=null;
        string currentDeptCode = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            //???(actual)
            userName = User.Identity.Name.ToString();
            UserModel user = new UserModel(userName);
            currentDeptCode = user.Department.dept_code.ToString();

            //(testing example)
            //currentDeptCode = "REGR";

            if (!IsPostBack)
            {
                var q = (from r in ent.Requests
                         where r.dept_code== currentDeptCode && r.current_status=="Pending"
                         select new
                         {
                             r.request_id,
                             r.username,
                             r.current_status,
                             r.date_time
                         }).ToList();
                GridView1.DataSource = q;
                GridView1.DataBind();
            }          
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Bound Field
            //string id = GridView1.SelectedRow.Cells[1].Text;

            //Template Field
            string id = (GridView1.SelectedRow.FindControl("lbReqId") as Label).Text;
            Response.Redirect("ApproveReject.aspx?key="+id);
        }


    }
}