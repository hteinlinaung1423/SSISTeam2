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
                <td>
                    <asp:SqlDataSource ID="sdsDepartment" runat="server" DataSourceMode="DataReader" ConnectionString="data source=(local);initial catalog=SSIS;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework" SelectCommand="SELECT name FROM SSIS.dbo.Department"></asp:SqlDataSource>
                    <asp:DropDownList DataSourceID="sdsDepartment" runat="server" DataTextField="name" DataValueField="name" Width="193px"></asp:DropDownList>
                     </td>
                <td>
                    <asp:SqlDataSource ID="sdsCategory" runat="server" DataSourceMode="DataReader" ConnectionString="data source=(local);initial catalog=SSIS;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework" SelectCommand="SELECT cat_name FROM SSIS.dbo.Category"></asp:SqlDataSource>
                    <asp:DropDownList DataSourceID="sdsCategory" DataTextField="cat_name" DataValueField="cat_name" runat="server" Width="193px"></asp:DropDownList>

                </td>
                <td>
                    <asp:Button ID="Generate" runat="server" Text="Generate" /></td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
