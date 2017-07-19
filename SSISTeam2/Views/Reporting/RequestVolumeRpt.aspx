<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RequestVolumeRpt.aspx.cs" Inherits="SSISTeam2.Views.Reporting.RequestVolumeRpt" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Request Volume Report</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h3>Kindly indicate the following report attributes:</h3>
        <table>
            <tr>
                <th>Year</th>
                <th>Month</th>
                <th>Department</th>
                <th>Stationery Category</th>
            </tr>
            <tr>
                <td><asp:DropDownList ID="Year" runat="server" Width="95px"></asp:DropDownList></td>
                <td><asp:DropDownList ID="Month" runat="server" Width="95px"></asp:DropDownList></td>
                <td><asp:DropDownList ID="Department" runat="server" Width="193px"></asp:DropDownList></td>
                <td><asp:DropDownList ID="Category" runat="server" Width="193px"></asp:DropDownList></td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
