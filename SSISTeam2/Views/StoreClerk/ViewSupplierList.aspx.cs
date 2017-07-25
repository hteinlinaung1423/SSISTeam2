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
        SSISEntities s = new SSISEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            
            GridView1.DataSource = s.Suppliers.Where(x=>x.deleted != "Y").ToList<Supplier>();
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
            Button btn = (Button)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;

            string supplierid = ((Label)gvr.FindControl("Label_SupplierId")).Text;

            Supplier supplier = s.Suppliers.Where(x => x.supplier_id == supplierid).First();

            supplier.deleted = "Y";

            s.SaveChanges();

            Response.Redirect("~/Views/StoreClerk/ViewSupplierList.aspx");
        }
        protected void Search_Click(object sender, EventArgs e)
        {
            string searchWord = TextBox1.Text;
            //Label1.Text = searchWord;
            var result = from t1 in s.Suppliers
                         where t1.deleted.Equals("N")
                         && (t1.name.Contains(searchWord))
                         orderby t1.name
                         select new { t1.supplier_id, t1.name, t1.contact_name, t1.fax_num, t1.contact_num, t1.address, t1.gst_reg_num };
            GridView1.DataSource = result.ToList();
            GridView1.DataBind();

        }



    }
}