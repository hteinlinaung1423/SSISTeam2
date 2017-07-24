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
        //Inventory_Adjustment inventoryAdj = new Inventory_Adjustment();
        //List<int> initialQuantity = new List<int>();

        protected void Page_Load(object sender, EventArgs e)
        {
            context = new SSISEntities();

            if (!IsPostBack)
            {
                List<MonthlyCheckModel> itemList = new List<MonthlyCheckModel>();
                List<Stock_Inventory> stockList = context.Stock_Inventory.Where(x => x.deleted == "N").ToList();
                foreach (Stock_Inventory i in stockList)
                {
                    MonthlyCheckModel item = new MonthlyCheckModel(i);
                    itemList.Add(item);
                }

                Session["Monthly"] = itemList;
                MonthlyCheckGV.DataSource = itemList;
                MonthlyCheckGV.DataBind();


                today = DateTime.Today;
                DateTB.Text = today.Date.ToString("dd/MM/yyyy");

                itemList = (List<MonthlyCheckModel>)Session["Monthly"];
                //Session["Monthly"] = itemList;
                MonthlyCheckGV.DataSource = itemList;
                MonthlyCheckGV.DataBind();
            }
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //Retaining initial quantity values
            int initial = int.Parse(MonthlyCheckGV.Rows[e.NewEditIndex].Cells[1].Text);
            Session["discrepency"] = initial;
            testLabel.Text = Session["discrepency"].ToString();
        }

        //protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        //{
        //    //Finding new quantity and comparision
        //    string updatedString = ((TextBox)(MonthlyCheckGV.Rows[e.RowIndex].Cells[1].Controls[0])).Text;
        //    int updated = int.Parse(updatedString);
        //    int initial = (int) Session["discrepency"];
        //    if (updated != initial)
        //    {
        //        Adjustment_Details details = new Adjustment_Details();
        //        details.item_code = MonthlyCheckGV.Rows[e.RowIndex].Cells[6].Text.ToString();
        //        details.quantity_adjusted = initial - updated;
        //        details.reason = "Monthly Check";
        //        adjDetails = (List<Adjustment_Details>)Session["Adjustment"];
        //        if (!CheckRepeatAdjustment(details, adjDetails))
        //        {
        //            adjDetails.Add(details);
        //            Session["Adjustment"] = adjDetails;

        //        }
        //        testLabel.Text = details.item_code;
        //    }
        //}
        protected void nextBtn_Click(object sender, EventArgs e)
        {
            List<MonthlyCheckModel> itemList = (List<MonthlyCheckModel>)Session["Monthly"];
            List<MonthlyCheckModel> confirmList = new List<MonthlyCheckModel>();
            foreach (MonthlyCheckModel i in itemList)
            {
                if (i.ActualQuantity != i.CurrentQuantity)
                {
                    confirmList.Add(i);
                }
            }
            Session["Confirmation"] = confirmList;
            Response.Redirect("MonthlyCheckConfirmation.aspx");
        }
        protected void MonthlyCheckGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            List<MonthlyCheckModel> itemList = (List<MonthlyCheckModel>)Session["Monthly"];
            MonthlyCheckGV.PageIndex = e.NewPageIndex;
            MonthlyCheckGV.DataSource = itemList;
            MonthlyCheckGV.DataBind();
        }
        protected void MonthlyCheckGV_OnTextChange(object sender, EventArgs e)
        {
            GridViewRow gridViewRow = (GridViewRow)(sender as Control).Parent.Parent;
            TextBox textbox = (TextBox) sender;
            Label rowIndex = (Label)gridViewRow.FindControl("rowIndex");
            int index = int.Parse(rowIndex.Text) - 1;
            List<MonthlyCheckModel> model = (List<MonthlyCheckModel>) Session["Monthly"];
            model[index].ActualQuantity = int.Parse(textbox.Text);

            Session["Monthly"] = model;
            testLabel.Text = "hi";
            MonthlyCheckGV.DataSource = model;
            MonthlyCheckGV.DataBind();
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

    }
}