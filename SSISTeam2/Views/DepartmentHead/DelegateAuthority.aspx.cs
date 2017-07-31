using SSISTeam2.Classes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2.Views.DepartmentHead
{
    public partial class DelegateAuthority : System.Web.UI.Page
    {
        SSISEntities ent = new SSISEntities();
        string loginUserName= null;
        string currentDeptCode = null;
        string currentDate;
        string choosedDelegate = null;
        string selectStartDate = null;
        string selectEndDate = null;
        string checkDelegate = null;
        int currentDutyId = 0;
        List<String> allEmp = null;
        string headName = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            currentDate = DateTime.Now.ToString("yyyy-MM-dd");
            lbCurrentDate.Text = currentDate;

            //???(Actual)
            loginUserName = User.Identity.Name.ToString();
            UserModel user = new UserModel(loginUserName);
            currentDeptCode = user.Department.dept_code.ToString();
            //show Departmnet Name(Actual)
           lbDeptName.Text = user.Department.name.ToString()+ " Department";

            if (!IsPostBack)
            {
                try
                {

                    //get login department all employee , remove Depthead name in Delegate
                    allEmp = ent.Dept_Registry.Where(b => b.dept_code == currentDeptCode).Select(x => x.fullname).ToList<String>();
                    headName = user.Department.head_user;
                    allEmp.Remove(headName);
                    //get Last delgate id whethere have or not depend on department
                    int lastDutyId = ent.Approval_Duties.Where(x => x.dept_code == currentDeptCode).Max(x => x.duty_id);
                    var w = ent.Approval_Duties.Where(x => x.duty_id == lastDutyId).First();

                //if current delegate already exist, cannot delegate another
                if (w.deleted.ToString() == "N" )
                {
                    ChooseNewTable.Visible = false;
                    //get data for Current Delegate Table
                    var q = ent.Approval_Duties.Where(x => x.dept_code == currentDeptCode&&x.deleted=="N").First();
                    checkDelegate = q.deleted.ToString();
                    currentDutyId = q.duty_id;
                    //show data for Current Delegate Table
                    lbCurDate.Text = q.created_date.ToString("yyyy-MM-dd");
                    lbCurDelegate.Text = q.username.ToString();
                    lbCurReason.Text = q.reason.ToString();
                    lbCurStart.Text = q.start_date.ToString("yyyy-MM-dd");
                    lbCurEnd.Text = q.end_date.ToString("yyyy-MM-dd");
                }
                else
                {
                    CurrentTable.Visible = false;
                    lbCheckDelegate.Text = "There is no Current Delegate! ";
                }

            }catch
                {
                    lbCheckDelegate.Text = "There is no Current Delegate! ";
                    CurrentTable.Visible = false;
                }
                finally
                {
                    //show data for New Choose Delegate Table
                    ddlEmployee.DataSource = allEmp;
                    ddlEmployee.DataBind();

                }
            }
            
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //Select Dropdown List Validation
            try
            {
                if (ddlEmployee.SelectedIndex == 0)
                {
                   
                }

            choosedDelegate = ddlEmployee.SelectedValue.ToString();
            //Date Validation
            selectStartDate = tbStartDate.Text;
            selectEndDate = tbEndDate.Text;
            if(selectStartDate == "" || selectEndDate =="")
            {
                lbDateError.Text = "Please choose the Date!";
            }
            else
            { 
                if (selectStartDate.CompareTo(currentDate) == -1 || selectEndDate.CompareTo(currentDate)==-1)
                {
                    lbDateError.Text = "Cannot choose previous date!";
                }
                else if (selectEndDate.CompareTo(selectStartDate) == -1)
                {
                    lbDateError.Text = "Start Date should before End Date!";
                }
                else
                {

                    lbDateError.Text = "";
                    Approval_Duties ad = new Approval_Duties
                    {
                        username = choosedDelegate,
                        start_date = Convert.ToDateTime(tbStartDate.Text),
                        end_date = Convert.ToDateTime(tbEndDate.Text),
                        dept_code = currentDeptCode,
                        created_date = Convert.ToDateTime(currentDate),
                        deleted = "N",
                        reason = tbReason.Text
                    };
                    ent.Approval_Duties.Add(ad);
                    ent.SaveChanges();
                    lbDateError.Text = "Successfully Save!";
                }
            }

            }
            catch
            {
                Label1.Text = "Please choose one Delegate!";
            }

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            var q = ent.Approval_Duties.Where(x => x.dept_code == currentDeptCode && x.deleted == "N").First();
            q.deleted = "Y";
            ent.SaveChanges();

            CurrentTable.Visible = false;
            lbCheckDelegate.Text = "No Current Delegate! ";
            ChooseNewTable.Visible = true;

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");           
        }
    }
}