using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SSISTeam2.Classes.Models;
namespace SSISTeam2
{
    public partial class ViewAllAdjustment : System.Web.UI.Page
    {
        SSISEntities context;
        protected void Page_Load(object sender, EventArgs e)
        {
            context = new SSISEntities();
            List<Adjustment_Details> adjList = context.Adjustment_Details.ToList();
            List<AdjustmentModel> detailList = new List<AdjustmentModel>();
            foreach (Adjustment_Details i in adjList)
            {
                AdjustmentModel model = new AdjustmentModel(i);
                detailList.Add(model);
            }
            Session["ViewAdj"] = detailList;
            ViewAdjustmentGV.DataSource = detailList;
            ViewAdjustmentGV.DataBind();
        }

        protected void detailBtn_Click(object sender, EventArgs e)
        {

        }
    }
}