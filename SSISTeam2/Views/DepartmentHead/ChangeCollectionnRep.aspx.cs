using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2.Views.DepartmentHead
{
    public partial class ChangeCollection_Rep : System.Web.UI.Page
    {
        SSISEntities ent = new SSISEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                Department dept = (Department) Session["sDept"];
                LabelDeptName.Text = dept.name.ToString();
                LabelContactName.Text = dept.contact_user.ToString();
                LabelPhNo.Text = dept.contact_num.ToString();
                LabelFaxNo.Text = dept.fax_num.ToString();
                LabelHeadName.Text = dept.head_user.ToString();

                //get all collection point
                ddlCollectPoint.DataSource = ent.Collection_Point.ToList<Collection_Point>();
                ddlCollectPoint.DataTextField = "location";
                ddlCollectPoint.DataValueField = "collection_pt_id";
                ddlCollectPoint.DataBind();

                //get all employee = * department
                ddlRepName.DataSource = ent.Dept_Registry.Where(a=>a.dept_code == dept.dept_code).ToList<Dept_Registry>();
                ddlCollectPoint.DataTextField = "username";
                ddlRepName.DataValueField = "username";
                ddlRepName.DataBind();
              
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //save/update changed collection point & representative in database
            string selectColPoint = ddlCollectPoint.SelectedItem.ToString();
            string selectRepName = ddlRepName.SelectedItem.ToString();

            Department dept = new Department();
           


    }
    }
}