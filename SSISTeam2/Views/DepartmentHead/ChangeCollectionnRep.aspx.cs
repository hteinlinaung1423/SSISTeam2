﻿using SSISTeam2.Classes;

using SSISTeam2.Classes.Models;

using System;

using System.Collections.Generic;

using System.Linq;

using System.Text;

using System.Web;

using System.Web.UI;

using System.Web.UI.WebControls;



namespace SSISTeam2.Views.DepartmentHead

{

    public partial class ChangeCollection_Rep : System.Web.UI.Page

    {

        SSISEntities ent = new SSISEntities();
        Department sdept = null;

        int currentCollectId = 0;

        string loginUSer = null;

        string headUSerName = null;





        protected void Page_Load(object sender, EventArgs e)

        {

            loginUSer = User.Identity.Name;
            UserModel user = new UserModel(loginUSer);
            headUSerName = user.FindDelegateOrDeptHead().Username;
            sdept = user.Department;


            //sdept = (Department)Session["sDept"];
            string fullname = ent.Dept_Registry.Where(x => x.username == sdept.contact_user).Select(x => x.fullname).First().ToString();
            LabelContactName.Text = fullname;
            LabelPhNo.Text = sdept.contact_num.ToString();
            LabelFaxNo.Text = sdept.fax_num.ToString();
            currentCollectId = sdept.collection_point;

            Collection_Point cp = ent.Collection_Point.SingleOrDefault(x => x.collection_pt_id == currentCollectId);
            lbCollectP.Text = cp.location + " ("+ cp.day_of_week+")";
            lbRepName.Text = changeUsernameToFullName(sdept.rep_user.ToString());

            if (!IsPostBack)
            {
                ddlDataBinding();

            }

        }

        public void ddlDataBinding()
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

            //get all employee depend on * department & remove department head name
            List<String> empList = ent.Dept_Registry.Where(a => a.dept_code == sdept.dept_code).Select(y => y.fullname).ToList<String>();          
            empList.Remove(changeUsernameToFullName(headUSerName));
            ddlRepName.DataSource = empList;
            ddlRepName.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)

        {
            //save/update changed collection point & representative in database

            int selectColPoint = ddlCollectPoint.SelectedIndex;
            string repFullName = ddlRepName.SelectedItem.ToString();

            if (selectColPoint == 0 || repFullName.Equals("Select---"))
            {

                lbDDLError1.Text = "Please select the required field!";
               
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
                lbDDLError1.Text = "Sucessfully Save!";

                _sendEmail(User.Identity.Name, repUserName);
            }
        }


        private void _sendEmail(string username, string repUserName)

        {

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

            sb.AppendLine(string.Format("The collection point for your department is: ", ddlCollectPoint.SelectedItem.ToString()));

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



        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

        public string changeUsernameToFullName(string username)
        {

            return ent.Dept_Registry.Where(x => x.username == username).Select(y => y.fullname).First().ToString();
        }
    }

}