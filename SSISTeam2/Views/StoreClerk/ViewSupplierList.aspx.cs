using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2.Views.StoreClerk
{
    public partial class ViewSupplierList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SSISEntities s = new SSISEntities();
            GridView1.DataSource = s.Suppliers.ToList<Supplier>();
            GridView1.DataBind();
        }


        protected void Add_New_Supplier_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/StoreClerk/AddNewSupplier.aspx");
        }

        protected void Edit_Supplier(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;

            string supplierid = ((Label)gvr.FindControl("Label_SupplierId")).Text;

            Response.Redirect("~/Views/StoreClerk/EditSupplier.aspx?supplier="+supplierid);
        }

        protected void delete_Supplier(object sender, EventArgs e)
        {

        }
    }
}