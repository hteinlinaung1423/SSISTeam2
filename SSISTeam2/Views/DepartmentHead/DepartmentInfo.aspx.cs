using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2.Views.DepartmentHead
{
    public partial class DepartmentInfo : System.Web.UI.Page
    {       
        SSISEntities ent = new SSISEntities();
        Department depts;

        protected void Page_Load(object sender, EventArgs e)
        {
            //* get a Depatment = login user (???)
            depts = ent.Departments.First();

            LabelDeptName.Text = depts.name.ToString();
            LabelContactName.Text = depts.contact_user.ToString();
            LabelPhNo.Text = depts.contact_num.ToString();
            LabelFaxNo.Text = depts.fax_num.ToString();
            LabelHeadName.Text = depts.head_user.ToString();
            //
            LabelCollectP.Text = depts.Collection_Point1.location.ToString();
            LabelRepName.Text = depts.rep_user.ToString();



        }

        protected void BtnChangeCpRn_Click(object sender, EventArgs e)
        {
            Session["sDept"] = depts;
            Response.Redirect("ChangeCollectionnRep.aspx");
        }
    }
}