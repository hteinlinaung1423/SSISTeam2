using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SSISTeam2.Classes.Models;

namespace SSISTeam2
{
    public partial class FileDiscrepency : System.Web.UI.Page
    {
        SSISEntities context;
        protected void Page_Load(object sender, EventArgs e)
        {
            context = new SSISEntities();
            List<MonthlyCheckModel> modelList = new List<MonthlyCheckModel>();
            //Dictionary<string, int> SessionInfo = (Dictionary<string, int>)Session["Discrepency"];
            Dictionary<string, int> SessionInfo = new Dictionary<string, int>()
            {
                {"P030", 5 },
                {"P031", 6 }
            };

            foreach (KeyValuePair<string, int> pair in SessionInfo)
            {
                string itemCode = pair.Key;
                int qtyAdjusted = pair.Value;

                Stock_Inventory inventory = context.Stock_Inventory.Where(x => x.item_code == itemCode).ToList().First();

                MonthlyCheckModel model = new MonthlyCheckModel(inventory);
                model.ActualQuantity = model.CurrentQuantity - qtyAdjusted;
                modelList.Add(model);
            }

            FileDiscrepencyGV.DataSource = modelList;
            FileDiscrepencyGV.DataBind();
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
            FileDiscrepencyGV.DataSource = itemList;
            FileDiscrepencyGV.DataBind();
        }

        protected void ConfirmBtn_Click(object sender, EventArgs e)
        {

        }
    }
}