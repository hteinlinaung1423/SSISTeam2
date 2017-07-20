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
        string currentDeptCode = null;
        string currentDate;
        string assignEmp = null;
        string selectStartDate = null;
        string selectEndDate = null;
        protected void Page_Load(object sender, EventArgs e)
        {

            currentDate = DateTime.Now.ToString("yyyy-MM-dd");
            lbCurrentDate.Text = currentDate;

            //???get current department employee list
            string loginuserDHead = "Low Kway Boo";
            //string loginuserDHead = User.Identity.Name;
             currentDeptCode = ent.Dept_Registry.Where(b => b.username == loginuserDHead).Select(c => c.dept_code).First().ToString();
            List<Dept_Registry> currendepReg = ent.Dept_Registry.Where(b => b.dept_code == currentDeptCode).ToList<Dept_Registry>();

            if (!IsPostBack)
            {

                ddlEmployee.DataSource = currendepReg;
                ddlEmployee.DataTextField = "username";
                ddlEmployee.DataValueField = "username";
                ddlEmployee.DataBind();
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            assignEmp = ddlEmployee.SelectedValue.ToString();

            //check Date
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
                    username = assignEmp,
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
    }
}