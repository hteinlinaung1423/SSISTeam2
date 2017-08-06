using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2.Views.StoreClerk
{
    public partial class DeptDetails : System.Web.UI.Page
    {
        SSISEntities context = new SSISEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            //selectreqid = Int16.Parse(Request.QueryString["key"]); 
            string deptcode = Request.QueryString["deptcode"].ToString();
            if (!IsPostBack)
            {
                FillPage(deptcode);
            }
        }

        private void FillPage(string deptcode)
        {
            Department d = context.Departments.Find(deptcode);
            Collection_Point cp = context.Collection_Point.Where(x => x.collection_pt_id == d.collection_point).First();
            lbldeptcode.Text = d.dept_code;
            lbldeptname.Text = d.name;
            lblhead.Text = d.head_user ;
            lblrep.Text = d.rep_user ;
            lblconname.Text = d.contact_user;
            lblconnum.Text = d.contact_num;
            lblfax.Text = d.fax_num ;
            lblcollpoint.Text = cp.location;
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewDepartmentList.aspx");
        }
    }
}