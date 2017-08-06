using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2.Views.StoreClerk
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        SSISEntities context = new SSISEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            var catList = context.Categories.Where(x => x.deleted == "N" && x.cat_name.Contains("E")).Select(x => x.cat_id).ToList();
            GridView1.DataSource = catList;
            GridView1.DataBind();

        }
    }
}