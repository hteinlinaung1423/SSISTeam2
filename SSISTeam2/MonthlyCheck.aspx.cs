using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SSISTeam2.Classes.Models;

namespace SSISTeam2
{
    public partial class MonthlyCheck : System.Web.UI.Page
    {
        DateTime today;
        SSISEntities context;
        List<Adjustment_Details> adjDetails = new List<Adjustment_Details>();

        protected void Page_Load(object sender, EventArgs e)
        {
            context = new SSISEntities();
            UserModel um = new UserModel(HttpContext.Current.User.Identity.Name);
            today = DateTime.Today;

            Monthly_Check_Records checkRecord = new Monthly_Check_Records();
            checkRecord.date_checked = today;
            checkRecord.clerk_user = HttpContext.Current.User.Identity.Name;
            checkRecord.deleted = "N";
            checkRecord.discrepancy = "N";
            DateTB.Text = today.Date.ToString("dd/MM/yyyy");
            Label1.Text = um.ContactNumber + um.Email + um.Username + um.Role;

        }

        protected void AddBtn_Click(object sender, EventArgs e)
        {

        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //Retaining initial quantity values
            int initial = int.Parse(GridView1.Rows[e.NewEditIndex].Cells[1].Text);
            Session["discrepency"] = initial;
            Label1.Text = Session["discrepency"].ToString();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //Finding new quantity and comparision
            string updatedString = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[1].Controls[0])).Text;
            int updated = int.Parse(updatedString);
            int initial = (int) Session["discrepency"];
            if (updated != initial)
            {
                Label1.Text = (initial - updated).ToString();
                Adjustment_Details details = new Adjustment_Details();
                details.quantity_adjusted = initial - updated;
                details.reason = "Monthly Check";
              
            }
        }
    }
}