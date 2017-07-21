<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="RequestVolumeRpt.aspx.cs" Inherits="SSISTeam2.Views.Reporting.RequestVolumeRpt1" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="True" Height="1202px" ReportSourceID="VolumeRptSrc" ToolPanelWidth="200px" Width="1104px" />
    <CR:CrystalReportSource ID="VolumeRptSrc" runat="server">
        <Report FileName="C:\Users\veryt\Source\Repos\SSISTeam2\SSISTeam2\Views\Reporting\Reports\Volume_Report.rpt">
        </Report>
    </CR:CrystalReportSource>
</asp:Content>
