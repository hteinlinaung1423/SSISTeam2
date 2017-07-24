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

        protected void Page_Load(object sender, EventArgs e)
        {
            currentDate = DateTime.Now.ToString("yyyy-MM-dd");
            lbCurrentDate.Text = currentDate;

            //???(Actual)
            //loginUserName = User.Identity.Name.ToString();
            //UserModel user = new UserModel(loginUserName);
            //currentDeptCode = user.Department.dept_code.ToString();
            //show Departmnet Name(Actual)
            //lbDeptName.Text = user.Department.name.ToString()+ " Department";

            //(testing example)
            loginUserName = "Low Kway Boo";
            currentDeptCode = ent.Dept_Registry.Where(b => b.username == loginUserName).Select(c => c.dept_code).First().ToString();
            //show Departmnet Name(testing example)
            lbDeptName.Text = currentDeptCode + " Department";

            //get login department all employee
            List<Dept_Registry> currendepReg = ent.Dept_Registry.Where(b => b.dept_code == currentDeptCode).ToList<Dept_Registry>();

            if (!IsPostBack)
            {
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
                    lbCheckDelegate.Text = "No Current Delegate! ";
                }
                //show data for New Choose Delegate Table
                ddlEmployee.DataSource = currendepReg;
                ddlEmployee.DataTextField = "username";
                ddlEmployee.DataValueField = "username";
                ddlEmployee.DataBind();
            }       
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            choosedDelegate = ddlEmployee.SelectedValue.ToString();
            //Date Validation
            selectStartDate = tbStartDate.Text;
            selectEndDate = tbEndDate.Text;
            if(selectStartDate.CompareTo(currentDate) == -1 || selectEndDate.CompareTo(currentDate)==-1)
            {
                lbDateError.Text = "cannot choose previous date!";
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
    }
}