using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

using SSISTeam2.Classes.Models;

namespace SSISTeam2
{
    public partial class MonthlyCheck : System.Web.UI.Page
    {
        DateTime today;
        SSISEntities context;
        List<Adjustment_Details> adjDetails = new List<Adjustment_Details>();
        List<MonthlyCheckModel> itemList = new List<MonthlyCheckModel>();
        Inventory_Adjustment inventoryAdj = new Inventory_Adjustment();
        List<Stock_Inventory> stockList;
        List<int> initialQuantity = new List<int>();

        protected void Page_Load(object sender, EventArgs e)
        {
            context = new SSISEntities();

            stockList = context.Stock_Inventory.Where(x => x.deleted == "N").ToList();
            foreach (Stock_Inventory i in stockList)
            {
                MonthlyCheckModel item = new MonthlyCheckModel(i);
                itemList.Add(item);
            }
            today = DateTime.Today;
            DateTB.Text = today.Date.ToString("dd/MM/yyyy");
            testLabel.Text = itemList.Count.ToString();
            MonthlyCheckGV.DataSource = itemList;
            MonthlyCheckGV.DataBind();

            //testLabel.Text = um.Role;
            
            if (!IsPostBack)
            {
                Session["Adjustment"] = adjDetails;

                //Trying to figure out how to control quantity change
                //foreach (GridViewRow row in GridView1.Rows)
                //{
                //    initialQuantity.Add(int.Parse(row.Cells[1].Text));
                //}
                //Session["Quantity"] = initialQuantity;
                //testLabel.Text = (initialQuantity[0] + initialQuantity[1]).ToString();

            }
            //CheckIfMonthlyDone();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //Retaining initial quantity values
            int initial = int.Parse(MonthlyCheckGV.Rows[e.NewEditIndex].Cells[1].Text);
            Session["discrepency"] = initial;
            testLabel.Text = Session["discrepency"].ToString();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //Finding new quantity and comparision
            string updatedString = ((TextBox)(MonthlyCheckGV.Rows[e.RowIndex].Cells[1].Controls[0])).Text;
            int updated = int.Parse(updatedString);
            int initial = (int) Session["discrepency"];
            if (updated != initial)
            {
                Adjustment_Details details = new Adjustment_Details();
                details.item_code = MonthlyCheckGV.Rows[e.RowIndex].Cells[6].Text.ToString();
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

        public void CheckIfMonthlyDone()
        {
            List<Monthly_Check_Records> recordList = context.Monthly_Check_Records.Where(x => x.deleted == "N").ToList();

            DateTime recordDate = recordList.Max(x => x.date_checked);
            if (recordDate.Month == today.Month)
            {
                MonthlyCheckGV.Enabled = false;
                nextBtn.Enabled = false;
                testLabel.Text = "Monthly check has already been done this month";
            }
        }

        protected void MonthlyCheckGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            MonthlyCheckGV.PageIndex = e.NewPageIndex;
            MonthlyCheckGV.DataSource = itemList;
            MonthlyCheckGV.DataBind();
        }
        protected void MonthlyCheckGV_OnTextChange(object sender, EventArgs e)
        {
            testLabel.Text = "changed";
        }

    }
}