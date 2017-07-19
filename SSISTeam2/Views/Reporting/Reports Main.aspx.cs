using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;

namespace SSISTeam2.Views.Reporting
{
    public partial class Reports_Main : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GRVReportBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/RequestVolumeRpt.aspx/");
        }

        protected void GTAReportBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/TrendAnalysisRpt.aspx/");
        }
    }
}