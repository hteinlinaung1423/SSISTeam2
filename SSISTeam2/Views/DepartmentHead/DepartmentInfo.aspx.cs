using SSISTeam2.Classes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2.Views.DepartmentHead
{
    public partial class DepartmentInfo : System.Web.UI.Page
    {       
        SSISEntities ent = new SSISEntities();
        Department dept = null;
        string loginUsername = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            //???(actual)
            //loginUsername = User.Identity.Name.ToString();

            //(testing exmaple)
            loginUsername = "Jerry";

            string logindepCode = ent.Dept_Registry.Where(b => b.username == loginUsername).Select(c => c.dept_code).First().ToString();

            //get login a department
            dept = ent.Departments.Where(d => d.dept_code == logindepCode).Single();

            LabelDeptName.Text = dept.name.ToString();
            LabelContactName.Text = dept.contact_user.ToString();
            LabelPhNo.Text = dept.contact_num.ToString();
            LabelFaxNo.Text = dept.fax_num.ToString();
            LabelHeadName.Text = dept.head_user.ToString();
            //LabelCollectP.Text = dept.Collection_Point1.location.ToString();
            //LabelRepName.Text = dept.rep_user.ToString();
            tbCollectP.Text = dept.Collection_Point1.location.ToString();
            tbRepName.Text = dept.rep_user.ToString();

        }

        protected void BtnChangeCpRn_Click(object sender, EventArgs e)
        {
            Session["sDept"] = dept;
           Response.Redirect("ChangeCollectionnRep.aspx");
        }
    }
}