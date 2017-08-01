﻿
using SSISTeam2.Classes;
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
        int currentCollectId = 0;
        string loginUSer = null;
        string headName = null;
     

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                loginUSer = User.Identity.Name;
                UserModel user = new UserModel(loginUSer);
                headName = user.FindDelegateOrDeptHead().Username;

                //
                Department sdept = (Department) Session["sDept"];
                //LabelDeptName.Text = sdept.name.ToString();
                LabelContactName.Text = sdept.contact_user.ToString();
                LabelPhNo.Text = sdept.contact_num.ToString();
                LabelFaxNo.Text = sdept.fax_num.ToString();
                //LabelHeadName.Text = sdept.head_user.ToString();

                currentCollectId = sdept.collection_point;
                string currentCollectP = ent.Collection_Point.Where(x => x.collection_pt_id == currentCollectId).Select(y => y.location).First();
                lbCollectP.Text = currentCollectP;
                lbRepName.Text = sdept.rep_user.ToString();


 
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

            //save/update changed collection point & representative in database
            string selectColPoint = ddlCollectPoint.SelectedValue.ToString();           
            string repFullName = ddlRepName.SelectedItem.ToString();


            try
            {
                //Collection Point
                if (selectColPoint.Equals("0") || repFullName.Equals("0"))
                {
                    lbDDLError.Text = "Please select the required field!";
                }

                //show Full Name- save username
                Dept_Registry depReg = ent.Dept_Registry.SingleOrDefault(x => x.fullname == repFullName);
                string repUserName = depReg.username;

                Department sdept = (Department)Session["sDept"];
                var result = ent.Departments.SingleOrDefault(c => c.name == sdept.name);
                result.rep_user = repUserName;
                result.collection_point = Int16.Parse(selectColPoint);
                ent.SaveChanges();
                lbDDLError.Text = "Sucessfully Save!";

                _sendEmail(User.Identity.Name, repUserName);
            }
            catch
            {
               

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
    }
}