using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2
{
    public partial class ChangeCategoryName1 : System.Web.UI.Page
    {
        SSISEntities s = new SSISEntities();

        //public object GridView1 { get; private set; }

        protected void Page_Load(object sender, EventArgs e)

        {
            if (!this.IsPostBack)
            {
                //GridView1.DataSource = s.Categories.ToList();
                //GridView1.DataBind();
                BindGrid();

            }
        }



        private void BindGrid()

        {
            GridView1.DataSource = s.Categories.Where(x => x.deleted !="Y").ToList();
            GridView1.DataBind();
        }


        //protected void LinkButton1_Click(object sender, EventArgs e)
        //{
        //    s.SaveChanges();
        //}

        //protected void LinkButton2_Click(object sender, EventArgs e)
        //{
        //    s.SaveChanges();
        //}

        //protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    int cat_Id = (int)GridView1.SelectedDataKey.Value;

        //    GridView1.DataSource = 
        //    //using (Categories entities = new Categories())
        //    //{

        //    //    DetailsView1.DataSource = new Order[] { order };
        //    //    DetailsView1.DataBind();
        //    //}

        //}

        protected void OnRowEditing(object sender, GridViewEditEventArgs e)

        {
            GridView1.EditIndex = e.NewEditIndex;
            BindGrid();
            GridViewRow gvr = GridView1.Rows[e.NewEditIndex];
            LinkButton linkEdit = (LinkButton)gvr.FindControl("Edit");
            LinkButton linkDelete = (LinkButton)gvr.FindControl("Delete");

        }
        protected void OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridView1.Rows[e.RowIndex];
            int cat_id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
            string cat_name = (row.FindControl("TextBox2") as TextBox).Text;


            using (SSISEntities entities = new SSISEntities())
            {
                Category category = entities.Categories.Where(p => p.cat_id == cat_id).First<Category>();
                //SSISEntities.cat_id = cat_id;
                category.cat_name = cat_name;
                entities.SaveChanges();
            }
            GridView1.EditIndex = -1;
            this.BindGrid();
        }

        protected void OnRowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            this.BindGrid();
        }

        protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int cat_id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
            using (SSISEntities entities = new SSISEntities())
            {
                Category category = entities.Categories.Where(p => p.cat_id == cat_id).First<Category>();
                category.deleted = "Y";
                entities.SaveChanges();
            }
            this.BindGrid();
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            if (TextBox3.Text != null)
            {


                GridView1.DataSource = SearchCatagories(TextBox3.Text);
                GridView1.DataBind();
            }
        }
        public List<Category> SearchCatagories(string name)
        {
            return s.Categories.Where(x => x.cat_name == name).ToList();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateNewCategory.aspx");
        }

        protected void On(object sender, GridViewCancelEditEventArgs e)
        {

        }

        




        //protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    int cat_id = (int)GridView1.SelectedDataKey.Value;
        //    using (SSISEntities entities = new SSISEntities())
        //    {
        //        Category category = entities.Categories.Where(p => p.cat_id == cat_id).First<Category>();
        //        DetailsView1.DataSource = new Category[] { cat_id };
        //        DetailsView1.DataBind();
        //    }
    }



    //protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //}


}





//protected void Button1_Click(object sender, EventArgs e)
//    {
//        if (TextBox1.Text != null)
//        {
//            GridView1.DataSource = s.SearchCatagories(TextBox1.Text);
//            GridView1.DataBind();
//        }
//    }
//}

