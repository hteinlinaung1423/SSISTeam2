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
        Inventory_Adjustment inventoryAdj = new Inventory_Adjustment();
        List<int> initialQuantity = new List<int>();

        protected void Page_Load(object sender, EventArgs e)
        {
            context = new SSISEntities();
            UserModel um = new UserModel(HttpContext.Current.User.Identity.Name);
            today = DateTime.Today;

            DateTB.Text = today.Date.ToString("dd/MM/yyyy");
            testLabel.Text = um.ContactNumber + um.Email + um.Username + um.Role;
            
            if (!IsPostBack)
            {
                Session["Adjustment"] = adjDetails;

                //Trying to figure out how to control quantity change
                foreach (GridViewRow row in GridView1.Rows)
                {
                    initialQuantity.Add(int.Parse(row.Cells[1].Text));
                }
                Session["Quantity"] = initialQuantity;
            }

            testLabel.Text = (initialQuantity[0] + initialQuantity[1]).ToString();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //Retaining initial quantity values
            int initial = int.Parse(GridView1.Rows[e.NewEditIndex].Cells[1].Text);
            Session["discrepency"] = initial;
            testLabel.Text = Session["discrepency"].ToString();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //Finding new quantity and comparision
            string updatedString = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[1].Controls[0])).Text;
            int updated = int.Parse(updatedString);
            int initial = (int) Session["discrepency"];
            if (updated != initial)
            {
                Adjustment_Details details = new Adjustment_Details();
                details.item_code = GridView1.Rows[e.RowIndex].Cells[6].Text.ToString();
                details.quantity_adjusted = initial - updated;
                details.reason = "Monthly Check";
                adjDetails = (List<Adjustment_Details>)Session["Adjustment"];
                if (!CheckRepeatAdjustment(details, adjDetails))
                {
                    adjDetails.Add(details);
                    Session["Adjustment"] = adjDetails;

                }
                testLabel.Text = details.item_code;
            }
        }

        protected void nextBtn_Click(object sender, EventArgs e)
        {
            adjDetails = (List<Adjustment_Details>)Session["Adjustment"];
            for (int i = 0; i < adjDetails.Count; i++)
            {
                if (adjDetails[i].quantity_adjusted == 0)
                    adjDetails.RemoveAt(i);
            }
            Session["Adjustment"] = adjDetails;
            Response.Redirect("MonthlyCheckConfirmation.aspx");
        }

        public bool CheckRepeatAdjustment(Adjustment_Details detail, List<Adjustment_Details> adjDetails)
        {
            List<Adjustment_Details> adjDetail = (List<Adjustment_Details>)Session["Adjustment"];
            foreach (Adjustment_Details i in adjDetails)
            {
                if (i.item_code == detail.item_code)
                {
                    i.quantity_adjusted += detail.quantity_adjusted;
                    return true;
                }
            }
            return false;
        }
    }
}