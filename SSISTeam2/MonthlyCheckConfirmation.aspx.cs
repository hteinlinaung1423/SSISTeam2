using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SSISTeam2.Classes.Models;

namespace SSISTeam2
{
    public partial class MonthlyCheckConfirmation : System.Web.UI.Page
    {

        SSISEntities context;

        protected void Page_Load(object sender, EventArgs e)
        {
            context = new SSISEntities();
            if (!IsPostBack)
            {
                List<MonthlyCheckModel> itemList = (List<MonthlyCheckModel>)Session["Confirmation"];
                try
                {
                    if (itemList.Count == 0)
                    {
                        confirmationGV.Visible = false;
                        Label1.Text = "There are not discrepencies for this month, confirm?";
                    }
                    confirmationGV.DataSource = itemList;
                    confirmationGV.DataBind();
                    Label1.Text = itemList.Count.ToString();

                }
                catch (Exception exec)
                {
                    Label1.Text = "exception";
                }
            }
            //else
            //{
            //    List<MonthlyCheckModel> itemList = (List<MonthlyCheckModel>)Session["Confirmation"];
            //    confirmationGV.DataSource = itemList;
            //    confirmationGV.DataBind();
            //    Label1.Text = itemList.Count.ToString();
            //}

        }

        protected void confirmBtn_Click(object sender, EventArgs e)
        {
            //Inventory_Adjustment inventoryAdj = new Inventory_Adjustment();
            //foreach (Adjustment_Details i in adjDetails)
            //{
            //    inventoryAdj.Adjustment_Details.Add(i);
            //}
            //Label1.Text = inventoryAdj.Adjustment_Details.Count.ToString();
            List<MonthlyCheckModel> itemList = (List<MonthlyCheckModel>)Session["Confirmation"];
            if (itemList == null)
            {
                Monthly_Check_Records checkRecord = new Monthly_Check_Records();
                checkRecord.date_checked = DateTime.Today;
                checkRecord.clerk_user = HttpContext.Current.User.Identity.Name;
                checkRecord.deleted = "N";
                checkRecord.discrepancy = "N";

                context.Monthly_Check_Records.Add(checkRecord);
                context.SaveChanges();

                Response.Redirect("Default.aspx");
            }
            else
            {
                Inventory_Adjustment invAdjustmentSup = new Inventory_Adjustment();
                invAdjustmentSup.date = DateTime.Today;
                invAdjustmentSup.clerk_user = HttpContext.Current.User.Identity.Name;
                invAdjustmentSup.status = "Pending";
                invAdjustmentSup.status_date = DateTime.Today;
                invAdjustmentSup.deleted = "N";

                Inventory_Adjustment invAdjustmentMan = new Inventory_Adjustment();
                invAdjustmentMan.date = DateTime.Today;
                invAdjustmentMan.clerk_user = HttpContext.Current.User.Identity.Name;
                invAdjustmentMan.status = "Pending";
                invAdjustmentMan.status_date = DateTime.Today;
                invAdjustmentMan.deleted = "N";


                foreach (MonthlyCheckModel i in itemList)
                {
                    //get price of adjustment for MonthlyCheckModel
                    double priceAdj = i.AveragePrice * Math.Abs(i.ActualQuantity - i.CurrentQuantity);


                    Adjustment_Details adjDetails = new Adjustment_Details();
                    adjDetails.deleted = "N";
                    adjDetails.item_code = i.ItemCode;
                    adjDetails.quantity_adjusted = i.ActualQuantity - i.CurrentQuantity;
                    adjDetails.reason = i.Reason;

                    if (priceAdj < 250)
                    {
                        invAdjustmentSup.Adjustment_Details.Add(adjDetails);
                    } else if (priceAdj >= 250)
                    {
                        invAdjustmentMan.Adjustment_Details.Add(adjDetails);
                    }

                    context.Adjustment_Details.Add(adjDetails);
                }



                Monthly_Check_Records checkRecord = new Monthly_Check_Records();
                checkRecord.date_checked = DateTime.Today;
                checkRecord.clerk_user = HttpContext.Current.User.Identity.Name;
                checkRecord.deleted = "N";
                checkRecord.discrepancy = "Y";

                if (invAdjustmentSup.Adjustment_Details.Count != 0)
                {
                    context.Inventory_Adjustment.Add(invAdjustmentSup);
                }
                if (invAdjustmentMan.Adjustment_Details.Count != 0)
                {
                    context.Inventory_Adjustment.Add(invAdjustmentMan);
                }

                context.Monthly_Check_Records.Add(checkRecord);
                context.SaveChanges();
            }

        }

        protected void backBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("MonthlyCheck.aspx");
        }

        protected void reasonTB_TextChanged(object sender, EventArgs e)
        {
            GridViewRow gridViewRow = (GridViewRow)(sender as Control).Parent.Parent;
            TextBox reasonTB = (TextBox)sender;
            Label rowIndex = (Label)gridViewRow.FindControl("rowIndex");
            int index = int.Parse(rowIndex.Text) - 1;
            List<MonthlyCheckModel> itemList = (List<MonthlyCheckModel>)Session["Confirmation"];
            itemList[index].Reason = reasonTB.Text;

            Session["Confirmation"] = itemList;
            confirmationGV.DataSource = itemList;
            confirmationGV.DataBind();
            Label1.Text = itemList[index].Reason;
        }
    }
}