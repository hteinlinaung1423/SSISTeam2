using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2
{
    public partial class MontlyCheckConfirmation : System.Web.UI.Page
    {
        List<Adjustment_Details> adjDetails;

        protected void Page_Load(object sender, EventArgs e)
        {
            adjDetails = (List<Adjustment_Details>)Session["Adjustment"];
            confirmationGV.DataSource = adjDetails;
            confirmationGV.DataBind();


        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Inventory_Adjustment inventoryAdj = new Inventory_Adjustment();
            foreach (Adjustment_Details i in adjDetails)
            {
                inventoryAdj.Adjustment_Details.Add(i);
            }
            Label1.Text = inventoryAdj.Adjustment_Details.Count.ToString();



            Monthly_Check_Records checkRecord = new Monthly_Check_Records();
            checkRecord.date_checked = DateTime.Today;
            checkRecord.clerk_user = HttpContext.Current.User.Identity.Name;
            checkRecord.deleted = "N";
            checkRecord.discrepancy = "N";
        }

        protected void backBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("MonthlyCheck.aspx");
        }
    }
}