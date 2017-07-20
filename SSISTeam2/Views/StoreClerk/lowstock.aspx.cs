using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2.Views.StoreClerk
{
    public partial class lowstock : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SSISEntities s = new SSISEntities();
            GridView1.DataSource = s.Stock_Inventory.Where(x => x.current_qty < x.reorder_level).ToList<Stock_Inventory>();
            GridView1.DataBind();
        }

        protected void MakeOrder(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;

          
            string itemcode = ((Label)gvr.FindControl("Label_ItemCode")).Text;

            Response.Redirect("~/Default.aspx?itemcode="+itemcode);
        }
    }
}