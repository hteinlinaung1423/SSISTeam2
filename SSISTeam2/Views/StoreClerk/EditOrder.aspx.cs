using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2.Views.StoreClerk
{
    public partial class EditOrder : System.Web.UI.Page
    {
        SSISEntities ctx = new SSISEntities();
        int orderId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                orderId =Convert.ToInt32(Request.QueryString["order"]) ;
                GridView1.DataSource = ctx.Purchase_Order_Details.Where(x => x.order_id == orderId && x.deleted != "Y").Select(x=> new {x.Tender_List_Details.Stock_Inventory.item_description, x.quantity, x.status, x.order_details_id }).ToList();
                GridView1.DataBind();
            }
        }

        protected void delete_OrderItem(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;

            int orderId = Convert.ToInt32(((Label)gvr.FindControl("Label_OrderDetailId")).Text);

            Purchase_Order_Details order = ctx.Purchase_Order_Details.Where(x => x.order_details_id == orderId).First();

            order.deleted = "Y";

            ctx.SaveChanges();

            Response.Redirect("~/Views/StoreClerk/ViewPendingOrder.aspx");
        }

        protected void ReceiveOrder(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/StoreClerk/ReceiveOrder.aspx?orderId="+orderId);
        }
    }
}