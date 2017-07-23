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

        protected void Button1_Click(object sender, EventArgs e)
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
                Inventory_Adjustment invAdjustment = new Inventory_Adjustment();
                invAdjustment.date = DateTime.Today;
                invAdjustment.clerk_user = HttpContext.Current.User.Identity.Name;
                invAdjustment.status = "Pending";
                invAdjustment.status_date = DateTime.Today;
                invAdjustment.deleted = "N";


                foreach (MonthlyCheckModel i in itemList)
                {
                    Adjustment_Details adjDetails = new Adjustment_Details();
                    adjDetails.deleted = "N";
                    adjDetails.item_code = i.ItemCode;
                    adjDetails.quantity_adjusted = i.ActualQuantity - i.CurrentQuantity;
                    adjDetails.reason = i.Reason;

                    context.Adjustment_Details.Add(adjDetails);

                    invAdjustment.Adjustment_Details.Add(adjDetails);

                }

                Monthly_Check_Records checkRecord = new Monthly_Check_Records();
                checkRecord.date_checked = DateTime.Today;
                checkRecord.clerk_user = HttpContext.Current.User.Identity.Name;
                checkRecord.deleted = "N";
                checkRecord.discrepancy = "Y";

                context.Inventory_Adjustment.Add(invAdjustment);
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