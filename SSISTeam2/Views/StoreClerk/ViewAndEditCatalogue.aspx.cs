using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2.Views.StoreClerk
{
    public partial class ViewAndEditCatalogue : System.Web.UI.Page
    {
        SSISEntities entities = new SSISEntities();
        List<ViewandEditCatalogueForShow> list = new List<ViewandEditCatalogueForShow>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                BindGrid();
            }
        }
        private void BindGrid()

        {

            List<Stock_Inventory> SIList = entities.Stock_Inventory.Where(x => x.deleted.Equals("N")).ToList();
            list = new List<ViewandEditCatalogueForShow>();
            foreach (Stock_Inventory si in SIList)
            {
                ViewandEditCatalogueForShow vec = new ViewandEditCatalogueForShow();
                vec.itemNumber = si.item_code;
                vec.Description = si.item_description;

                vec.categoryName = (from t1 in entities.Categories
                                    where t1.deleted.Equals("N")
                                     && (t1.cat_id == si.cat_id)
                                    select new { t1.cat_name }).ToList().First().cat_name;
                vec.RL = si.reorder_level;
                vec.RQ = si.reorder_qty;
                vec.UoM = si.unit_of_measure;
                vec.CQ = si.current_qty;
                vec.CategoryId = si.cat_id;

                list.Add(vec);
            }
            GridView1.Columns[1].Visible = false;
            GridView1.DataSource = list;
            GridView1.DataBind();
        }
        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (e.NewPageIndex < 0)
            {
                GridView1.PageIndex = 0;
            }
            else
            {
                GridView1.PageIndex = e.NewPageIndex;
            }

            this.BindGrid();

        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            this.BindGrid();
        }

        protected void OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridView1.Rows[e.RowIndex];

            string categoryName = (row.FindControl("TextBox1") as TextBox).Text;
            string description = (row.FindControl("TextBox2") as TextBox).Text;
            int curQty = Convert.ToInt32((row.FindControl("TextBox6") as TextBox).Text);
            int reorderLevel = Convert.ToInt32((row.FindControl("TextBox3") as TextBox).Text);
            int reorderQuantity = Convert.ToInt32((row.FindControl("TextBox4") as TextBox).Text);
            string unitOfMeasure = (row.FindControl("TextBox5") as TextBox).Text;
            string itemCode = (row.FindControl("Label1") as Label).Text;
            int categoryID = Convert.ToInt32((row.FindControl("Label9") as Label).Text);

            var result1 = entities.Stock_Inventory.SingleOrDefault(x => x.item_code == itemCode);
            result1.current_qty = curQty;
            result1.reorder_level = reorderLevel;
            result1.reorder_qty = reorderQuantity;
            result1.unit_of_measure = unitOfMeasure;
            result1.item_description = description;
            entities.SaveChanges();

            var result2 = entities.Categories.SingleOrDefault(x => x.cat_id == categoryID);
            result2.cat_name = categoryName;
            entities.SaveChanges();

            GridView1.EditIndex = -1;
            this.BindGrid();

        }
        protected void OnRowCancelingEdit(object sender, EventArgs e)
        {
            GridView1.EditIndex = -1;
            this.BindGrid();
        }

        protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = GridView1.Rows[e.RowIndex];

            string itemCode = (row.FindControl("Label1") as Label).Text;
            Label4.Text = itemCode;
            var result1 = entities.Stock_Inventory.SingleOrDefault(x => x.item_code == itemCode);
            result1.deleted = "Y";
            entities.SaveChanges();
            GridView1.EditIndex = -1;
            this.BindGrid();

        }
        protected void Search_Click(object sender, EventArgs e)
        {
            string searchWcord = TextBox1.Text;
            int categoryId = Convert.ToInt32(Label2.Text);
            Label4.Text = searchWcord.ToString();
            //table import into arraylist
            List<Category> Lcat = entities.Categories.ToList<Category>();
            List<Stock_Inventory> Lsi = entities.Stock_Inventory.ToList<Stock_Inventory>();
            //Users can search Category Name, Item Description, 
            
            Category cat = entities.Categories.Where(x => x.cat_name == searchWcord).First<Category>();
            //scenario 1, user search via category
            //get category object
            //scenario 2, user search via item in stock_inventory
            //using category object, find all stock_inventory item that belongs to this category

            //var result = (from t1 in entities.Categories
            //              join t2 in entities.Stock_Inventory
            //              on t1.cat_id equals t2.cat_id
            //              where t1.deleted.Equals("N")
            //              && t2.deleted.Equals("N")
            //              select new { t1.cat_name }).ToList().First().cat_name;


            //var result2 = from t1 in entities.Categories
            //                   join t2 in entities.Stock_Inventory
            //                   on t1.cat_id equals t2.cat_id
            //                   where result.Contains(t1.cat_name)
            //                   orderby t1.cat_name
            //                   select new { t2.item_code, t1.cat_name, t2.item_description, t2.current_qty, t2.reorder_level, t2.reorder_qty, t2.unit_of_measure };
            //GridView1.DataSource = result2.ToList();
            //GridView1.DataBind();




        }
        class ViewandEditCatalogueForShow
        {
            public string itemNumber { get; set; }
            public string categoryName { get; set; }
            public string Description { get; set; }
            public int CQ { get; set; }
            public int RL { get; set; }
            public int RQ { get; set; }
            public string UoM { get; set; }
            public int CategoryId { get; set; }
            public string deleted { get; set; }


        }


    }
}