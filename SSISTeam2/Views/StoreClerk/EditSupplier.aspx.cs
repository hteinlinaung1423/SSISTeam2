using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2.Views.StoreClerk
{
    public partial class EditSupplier : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            string SupplierId = tb_supplierId.Text;
            string SupplierName = tb_supplierName.Text;
            string ContactName = tb_contactName.Text;
            string ContactNum = tb_contactNum.Text;
            string FaxNum = tb_faxNum.Text;
            string Address = tb_address.Text;
            string Gst = tb_gst.Text;

            

            Response.Redirect("~/Views/StoreClerk/viewsupplierList.aspx");


        }
    }
}