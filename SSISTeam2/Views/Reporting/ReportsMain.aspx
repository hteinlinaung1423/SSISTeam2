<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ReportsMain.aspx.cs" Inherits="SSISTeam2.Views.Reporting.ReportsMain" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>
    Reports Management
    </p>

    <div>
        <h3>Please indicate the type of report to generate</h3>
        <table>
            <tr>
                <th>Report Type</th>
                <th></th>
            </tr>
            
            <tr>
            <td>Generate Request Volume Reports</td>
            <td><asp:Button ID="GRVReportBtn" Text="Select" width="142px" runat="server" OnClick="GRVReportBtn_Click"/></td>
            </tr>
           
            <tr>
            <td>Generate Trend Analysis Report </td>
            <td><asp:Button ID="GTAReportBtn" Text="Select" width="142px" runat="server" OnClick="GTAReportBtn_Click" /></td>
            
            </tr>
        </table>        
    
    </div>
</asp:Content>
