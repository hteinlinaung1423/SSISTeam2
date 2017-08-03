using SSISTeam2.Classes;
using SSISTeam2.Classes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            LabelContactName.Text = new UserModel(sdept.contact_user).Fullname;
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

                _sendEmail(User.Identity.Name, repUserName);
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

            var emplist = ent.Dept_Registry.Where(x => x.username == username).Select(y => y.fullname);
            return emplist.First();
        }

        private void _sendEmail(string username, string repUserName)

        {
            string cpId = ddlCollectPoint.SelectedValue;
            Collection_Point cp;

            using (SSISEntities context = new SSISEntities())
            {
                cp = context.Collection_Point.Find(cpId);
            }

            /* Email logic */

            UserModel currentUserModel = new UserModel(username);



            string fromEmail = currentUserModel.Email;

            string fromName = currentUserModel.Fullname;

            UserModel rep = new UserModel(repUserName);

            string toEmail = rep.Email;

            string toName = rep.Fullname;



            string subject = string.Format("Department representative assignment");

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Dear " + toName + ",");

            sb.AppendLine("<br />");

            sb.AppendLine("<br />");

            sb.AppendLine(string.Format("Your department head has appointed you to be the department representative."));

            sb.AppendLine("<br />");

            sb.AppendLine(string.Format("The collection point for your department is: {0}", cp.location));

            sb.AppendLine("<br />");

            sb.AppendLine(string.Format("The collection time is: {0} on {1}", cp.date_time.ToShortTimeString(), cp.day_of_week));

            sb.AppendLine("<br />");

            sb.AppendLine(string.Format("Please <a href=\"{0}\">follow this link to login to the system</a>.", "http://bit.ly/ssis-login"));

            sb.AppendLine("<br />");

            sb.AppendLine("<br />");

            sb.AppendLine("Thank you.");

            sb.AppendLine("<br />");

            sb.AppendLine("<br />");

            sb.AppendLine("<i>This message was auto-generated by the Staionery Store Inventory System.</i>");



            string body = sb.ToString();



            new Emailer(fromEmail, fromName).SendEmail(toEmail, toName, subject, body);

            /* End of email logic */

        }
    }
}