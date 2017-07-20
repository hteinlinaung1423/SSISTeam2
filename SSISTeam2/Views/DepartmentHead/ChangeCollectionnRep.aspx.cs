using SSISTeam2.Classes.Models;
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
        int currentCollectId = 0;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                Department sdept = (Department) Session["sDept"];
                LabelDeptName.Text = sdept.name.ToString();
                LabelContactName.Text = sdept.contact_user.ToString();
                LabelPhNo.Text = sdept.contact_num.ToString();
                LabelFaxNo.Text = sdept.fax_num.ToString();
                LabelHeadName.Text = sdept.head_user.ToString();

                currentCollectId = sdept.collection_point;
                string currentCollectP = ent.Collection_Point.Where(x => x.collection_pt_id == currentCollectId).Select(y => y.location).First();
                lbCurrentCollectP.Text = currentCollectP;
                lbRepName.Text = sdept.rep_user.ToString();

                //get all collection point
                ddlCollectPoint.DataSource = ent.Collection_Point.ToList<Collection_Point>();
                ddlCollectPoint.DataTextField = "location";
                ddlCollectPoint.DataValueField = "collection_pt_id";
                ddlCollectPoint.DataBind();

                //get all employee = * department
                ddlRepName.DataSource = ent.Dept_Registry.Where(a=>a.dept_code == sdept.dept_code).ToList<Dept_Registry>();
                ddlCollectPoint.DataTextField = "username";
                ddlRepName.DataValueField = "username";
                ddlRepName.DataBind();
              
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //save/update changed collection point & representative in database
            string selectColPoint = ddlCollectPoint.SelectedValue.ToString();
            string selectRepName = ddlRepName.SelectedValue.ToString();

            Department sdept = (Department)Session["sDept"];
            var result = ent.Departments.SingleOrDefault(c => c.name == sdept.name);
            result.rep_user = selectRepName;
            result.collection_point = Int16.Parse(selectColPoint);
            ent.SaveChanges();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("DepartmentInfo.aspx");
        }
    }
}