using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2.Views.StoreClerk
{
    public partial class ReceiveOrder : System.Web.UI.Page
    {
        SSISEntities ctx = new SSISEntities();
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {

                int orderID = Convert.ToInt32(Session["order"]);

                

                GridView1.DataSource = ctx.Purchase_Order_Details.Where(x => x.order_id == orderID && x.status == "Pending").Select(x => new { x.order_details_id, x.Tender_List_Details.Stock_Inventory.item_description, x.quantity }).ToList();
                GridView1.DataBind();
            }


        }

        protected void Confirm(object sender, EventArgs e)
        {
            int orderID = Convert.ToInt32(Session["order"]);

            Delivery_Orders d = new Delivery_Orders();

            d.clerk_user = User.Identity.Name;
            d.deleted = "N";
            d.order_id = orderID;
            d.receive_date = DateTime.Parse(deliverydate.Text);

            ctx.Delivery_Orders.Add(d);
            ctx.SaveChanges();

            //Get DeliveryOrderID
            Delivery_Orders order = ctx.Delivery_Orders.Where(x => x.clerk_user == User.Identity.Name).OrderBy(x => x.delivery_id).ToList().Last();

            foreach (GridViewRow rows in GridView1.Rows)
            {
                TextBox qty = (TextBox)rows.FindControl("quantity");
                TextBox r = (TextBox)rows.FindControl("remark");

                Label orderdetailid=(Label)rows.FindControl("Label_OrderDetailId");


                int quantity =Convert.ToInt32( qty.Text);
                string remarks = r.Text;
                int orderDetail =Convert.ToInt32( orderdetailid.Text);

               

                Purchase_Order_Details pod = ctx.Purchase_Order_Details.Where(x => x.order_details_id == orderDetail).First();

                Delivery_Details dd = new Delivery_Details();
                dd.delivery_id = order.delivery_id;
                dd.deleted = "N";
                dd.tender_id = pod.tender_id;
                dd.quantity = quantity;
                dd.remarks = remarks;

                ctx.Delivery_Details.Add(dd);
                ctx.SaveChanges();

            }


            Response.Redirect("~/default.aspx");
        }


    }

    
}