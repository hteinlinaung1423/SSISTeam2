using SSISTeam2.Classes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2.Views.Employee
{
    public partial class ChangeCollectionPoint : System.Web.UI.Page
    {

        SSISEntities ent = new SSISEntities();

        int currentCollectId = 0;

        string loginUSer = null;

        string headUSerName = null;

        string repFullName = null;
        Department sdept = null;
        protected void Page_Load(object sender, EventArgs e)
        {

            loginUSer = User.Identity.Name;
            UserModel user = new UserModel(loginUSer);
            headUSerName = user.FindDelegateOrDeptHead().Username;
            sdept = user.Department;

           LabelContactName.Text = sdept.contact_user.ToString();
            LabelPhNo.Text = sdept.contact_num.ToString();
            LabelFaxNo.Text = sdept.fax_num.ToString();


            currentCollectId = sdept.collection_point;

            string currentCollectP = ent.Collection_Point.Where(x => x.collection_pt_id == currentCollectId).Select(y => y.location).First();

            lbCollectP.Text = currentCollectP;

            lbRepName.Text = changeUsernameToFullName(sdept.rep_user.ToString());

            if (!IsPostBack)
            {
                ddlDataBinding();
            }


        }

        protected void btnSave_Click(object sender, EventArgs e)

        {

            //save/update changed collection point & representative in database
            string selectColPoint = ddlCollectPoint.SelectedValue.ToString();
            try

            {

                if (selectColPoint.Equals("0") )
                {

                    lbDDLError.Text = "Please select the collection point!";

                }
                //show Full Name- save username
                repFullName = lbRepName.Text.ToString();
                Dept_Registry depReg = ent.Dept_Registry.SingleOrDefault(x => x.fullname == repFullName);
                string repUserName = depReg.username;

                var result = ent.Departments.SingleOrDefault(c => c.name == sdept.name);
                result.collection_point = Int16.Parse(selectColPoint);
                ent.SaveChanges();
                lbDDLError.Text = "Sucessfully Save!";
            }

            catch

            {

            }

        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }


        public void  ddlDataBinding()
        {
            //get all collection point

            ddlCollectPoint.DataSource = ent.Collection_Point.ToList<Collection_Point>();

            ddlCollectPoint.DataTextField = "location";

            ddlCollectPoint.DataValueField = "collection_pt_id";

            ddlCollectPoint.DataBind();

        }

        public string changeUsernameToFullName(string username)
        {

            return ent.Dept_Registry.Where(x => x.username == username).Select(y => y.fullname).First().ToString();
        }
    }
}