
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
        string loginUSer = null;
        string headName = null;
     

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                loginUSer = User.Identity.Name;
                UserModel user = new UserModel(loginUSer);
                headName = user.Department.head_user;

                //
                Department sdept = (Department) Session["sDept"];
                LabelDeptName.Text = sdept.name.ToString();
                LabelContactName.Text = sdept.contact_user.ToString();
                LabelPhNo.Text = sdept.contact_num.ToString();
                LabelFaxNo.Text = sdept.fax_num.ToString();
                LabelHeadName.Text = sdept.head_user.ToString();

                currentCollectId = sdept.collection_point;
                string currentCollectP = ent.Collection_Point.Where(x => x.collection_pt_id == currentCollectId).Select(y => y.location).First();
                tbCollectP.Text = currentCollectP;
                tbRepName.Text = sdept.rep_user.ToString();


 
                        //get all collection point
                        ddlCollectPoint.DataSource = ent.Collection_Point.ToList<Collection_Point>();
                        ddlCollectPoint.DataTextField = "location";
                        ddlCollectPoint.DataValueField = "collection_pt_id";
                        ddlCollectPoint.DataBind();


                  //get all employee depend on * department & remove department head name
                     List<String> empList = ent.Dept_Registry.Where(a => a.dept_code == sdept.dept_code).Select(y=>y.fullname).ToList<String>();
                    empList.Remove(headName);                     
                    ddlRepName.DataSource = empList;
                    ddlRepName.DataBind();
         
              
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            try
            {
                //Collection Point
                if (ddlCollectPoint.SelectedIndex == 0 || ddlRepName.SelectedIndex == 0)
                {

                }

                //save/update changed collection point & representative in database
                string selectColPoint = ddlCollectPoint.SelectedValue.ToString();
                string selectRepName = ddlRepName.SelectedValue.ToString();

                Department sdept = (Department)Session["sDept"];
                var result = ent.Departments.SingleOrDefault(c => c.name == sdept.name);
                result.rep_user = selectRepName;
                result.collection_point = Int16.Parse(selectColPoint);
                ent.SaveChanges();
                lbDDLError.Text = "Sucessfully Save!";

            }
            catch
            {
                lbDDLError.Text = "Please select the required field!";

            }

      


        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }
    }
}