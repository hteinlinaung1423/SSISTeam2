using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2.Views.StoreClerk
{
    public partial class PurchaseOrder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string itemcode = Request.QueryString["itemcode"];
                string name = DropDownList1.SelectedValue;
                SSISEntities s = new SSISEntities();
                GridView1.DataSource = s.Tender_List_Details.Where(x => x.item_code==itemcode || x.Stock_Inventory.Category.cat_name=="Pens").Select(x => new { x.item_code, x.Stock_Inventory.item_description, x.Tender_List.Supplier.name }).ToList();
                GridView1.DataBind();
            
           
        }


        protected void GridView_DataBound(object sender, EventArgs e)
        {
           int count = GridView1.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                DropDownList supplier = (DropDownList)GridView1.Rows[i].FindControl("SupplierList");
                Label itemcode = (Label)GridView1.Rows[i].FindControl("Label_ItemCode");
                string ic = itemcode.Text;

                if (supplier != null)
                {
                    SSISEntities s = new SSISEntities();
                    List<Tender_List_Details> tender = s.Tender_List_Details.Where(x => x.item_code == ic).ToList<Tender_List_Details>();

                    for (int j = 0; i < tender.Count ; i++)
                    {
                        int year = tender[j].tender_year_id;
                        String name = s.Tender_List.Where(x => x.tender_year_id == year).Select(y => y.Supplier.name).First();
                        ListItem item = new ListItem(name);
                        supplier.Items.Add(item);
                        
                    }
                }

            }
        }

        

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
           string name = DropDownList1.SelectedValue;

            SSISEntities s = new SSISEntities();
            GridView1.DataSource = s.Tender_List_Details.Where(x => x.Stock_Inventory.Category.cat_name == name).Select(x=> new {x.item_code, x.Stock_Inventory.item_description,x.Tender_List.Supplier.name}).ToList();
            GridView1.DataBind();
           


        }
    }
}