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
            string fullname = ent.Dept_Registry.Where(x => x.username == sdept.contact_user).Select(x => x.fullname).First().ToString();
            LabelContactName.Text = fullname;
            LabelPhNo.Text = sdept.contact_num.ToString();
            LabelFaxNo.Text = sdept.fax_num.ToString();


            currentCollectId = sdept.collection_point;

            Collection_Point cp = ent.Collection_Point.SingleOrDefault(x => x.collection_pt_id == currentCollectId);
            lbCollectP.Text = cp.location + " (" + cp.day_of_week + " -" + cp.date_time.TimeOfDay + " )";

            lbRepName.Text = changeUsernameToFullName(sdept.rep_user.ToString());

            if (!IsPostBack)
            {
                ddlDataBinding();
            }


        }

        protected void btnSave_Click(object sender, EventArgs e)

        {

            //save/update changed collection point & representative in database
            int selectColPoint = ddlCollectPoint.SelectedIndex;
            

            if (selectColPoint == 0 )
            {

                lbDDLError.Text = "Please select the required field!";

            }
            else
            {
                //show Full Name- save username
                Dept_Registry depReg = ent.Dept_Registry.SingleOrDefault(x => x.fullname == repFullName);
                string repUserName = depReg.username;

                var result = ent.Departments.SingleOrDefault(c => c.name == sdept.name);
                result.rep_user = repUserName;
                result.collection_point = selectColPoint;
                ent.SaveChanges();
                lbDDLError.Text = "Sucessfully Save!";
            }


        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }


        public void  ddlDataBinding()
        {
            //get all collection point
            List<Collection_Point> cpWdateList = ent.Collection_Point.ToList<Collection_Point>();
            List<string> stList = new List<string>();
            foreach (Collection_Point each in cpWdateList)
            {
                string s1 = each.location;
                string s2 = each.day_of_week;
                string s = s1 + " (" + s2 + ")";
                stList.Add(s);
            }
            ddlCollectPoint.DataSource = stList;
            ddlCollectPoint.DataBind();
        }

        public string changeUsernameToFullName(string username)
        {

            var emplist = ent.Dept_Registry.Where(x => x.username == username).Select(y => y.fullname);
            return emplist.First();
        }
    }
}