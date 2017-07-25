using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2.Views.StoreClerk
{
    public partial class ViewCatalogueForm : System.Web.UI.Page
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
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (TextBox1.Text != null)
            {
                //GridView1.DataSource = SearchCatagories(TextBox1.Text);
                //GridView1.DataBind();
            }
        }

        private object SearchCatagories(string text)
        {
            throw new NotImplementedException();
        }
        //public List<ViewandEditCatalogueForShow> SearchCatagories(string param)

        //{
        //    //List<ViewandEditCatalogueForShow> lllist = new List<ViewandEditCatalogueForShow>();
        //    int RL = 0;
        //    int CQ = 0;
        //    int RQ = 0;
        //    int CategoryId = 0;
        //    try
        //    {
        //        RL = int.Parse(param);
        //        CQ = int.Parse(param);
        //        RQ = int.Parse(param);
        //        CategoryId = int.Parse(param);
        //        entities.Categories.Where(x => x.itemNumber.Contains(param) || x.categoryName.Contains(param) || x.Description.Contains(param) || x.RL == RL || x.CQ == CQ || x.UoM.Contains(param) || x.CategoryId == CategoryId).ToList();
        //    }
        //    catch (FormatException e)
        //    {

        //    }
        //    return
        //    //return new List<ViewandEditCatalogueForShow>();
        //}
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
    