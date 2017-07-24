using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;

namespace SSISTeam2.Views.Reporting.Reports
{
    public partial class RequestVolumeRpt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ReportDocument crTA = new ReportDocument();
            crTA.Load(Server.MapPath("~/Views/Reporting/Reports/Volume_Report.rpt"));
            CrystalReportViewer1.ReportSource = crTA;
            CrystalReportViewer1.RefreshReport();
        }

        protected void genNewRep_OnClick(object Sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        protected void backBtn_OnClick(object Sender, EventArgs e)
        {
            Response.Redirect("ReportsMain.aspx");
        }
    }
}