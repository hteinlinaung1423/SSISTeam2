using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2.Views.StoreClerk
{
    public partial class MakeOrder : System.Web.UI.Page
    {
        SSISEntities s = new SSISEntities();
        protected void Page_Load(object sender, EventArgs e)
        {

            if(!IsPostBack)
            GridView1.DataSource = s.Stock_Inventory.Where(x => x.deleted!="N").ToList<Stock_Inventory>();
            GridView1.DataBind();
        }

        protected void CreateOrder(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            bool duplicate = false;


            string itemcode = ((Label)gvr.FindControl("Label_ItemCode")).Text;

            SSISEntities ctx = new SSISEntities();
            Stock_Inventory item = ctx.Stock_Inventory.Where(x => x.item_code == itemcode).First();

            HashSet<Stock_Inventory> itemList = (HashSet<Stock_Inventory>)Session["item"];

            if (itemList == null)
            {
                itemList = new HashSet<Stock_Inventory>();
                itemList.Add(item);
                lblResult.Visible = true;
                lblduplicate.Visible = false;
                lblResult.Text = "Added Item Code (" + itemcode + ") Successfully";
                Session["item"] = itemList;
            }

            else
            {



                foreach (Stock_Inventory s in itemList)
                {

                    if (s.item_code == item.item_code)
                    {
                        duplicate = true;
                        break;
                    }

                }

                if (!duplicate)
                {

                    itemList.Add(item);
                    lblResult.Visible = true;
                    lblduplicate.Visible = false;
                    lblResult.Text = "Added Item Code (" + itemcode + ") Successfully";
                    Session["item"] = itemList;
                }
                else
                {
                    lblduplicate.Visible = true;
                    lblResult.Visible = false;
                    lblduplicate.Text = "Duplicate item!";
                    Session["item"] = itemList;
                }

            }





        }
    }
}