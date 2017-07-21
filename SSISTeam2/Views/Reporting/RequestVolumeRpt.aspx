<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RequestVolumeRpt.aspx.cs" Inherits="SSISTeam2.Views.Reporting.RequestVolumeRpt" %>

<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Request Volume Report</title>
</head>
<body>
    <form id="form1" runat="server">
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" ReportSourceID="VolReq" />
        <CR:CrystalReportSource ID="VolReq" runat="server">
            <Report FileName="C:\Users\veryt\Source\Repos\SSISTeam2\SSISTeam2\Views\Reporting\Reports\Volume_Report.rpt">
            </Report>
        </CR:CrystalReportSource>
    </form>
</body>
</html>
