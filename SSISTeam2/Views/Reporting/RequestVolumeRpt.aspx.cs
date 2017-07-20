using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using System.Data.SqlClient;
using System.Configuration;
using SSISTeam2.Views.Reporting.Reports;
using CrystalDecisions.Shared;

namespace SSISTeam2.Views.Reporting
{
    public partial class RequestVolumeRpt : System.Web.UI.Page
    {
        //SSISEntities ctx = new SSISEntities();

        protected void Page_Load(object sender, EventArgs e)
        {

            /*List<Volume_Request> vrList = ctx.Volume_Request.ToList();
            Volume_Report_v2 volCrystal = new Volume_Report_v2();
            volCrystal.SetDataSource(vrList);
            CrystalReportViewer1.ReportSource = volCrystal;*/
            /* 
                 if (!Page.IsPostBack)
             {
                 //Fill Years
                 for (int i = 2013; i <= 2040; i++)
                 {
                     Year.Items.Add(i.ToString());
                 }
                 Year.Items.FindByValue(System.DateTime.Now.Year.ToString()).Selected = true;

                 //Fill Months
                 DateTime month = Convert.ToDateTime("1/1/2000");
                 for (int i = 0; i <= 11; i++)
                 {
                     DateTime NextMont = month.AddMonths(i);
                     ListItem list = new ListItem();
                     list.Text = NextMont.ToString("MMMM");
                     list.Value = NextMont.Month.ToString();
                     Month.Items.Add(list);
                 }
                 Month.Items.FindByValue(System.DateTime.Now.Month.ToString()).Selected = true;
                 */


        }


    }

      
    }
