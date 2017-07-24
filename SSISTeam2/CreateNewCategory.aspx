<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateNewCategory.aspx.cs" Inherits="SSISTeam2.CreateNewCategory" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
        <asp:Label ID="Label1" runat="server" Text="Add New Category"></asp:Label>
        <p>
            <asp:Label ID="Label2" runat="server" Text="New Category Name222:"></asp:Label>
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        </p>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Confirm" />
       
        <asp:Button ID="Button2" runat="server"  Text="Cancel" OnClick="Button2_Click1" />
       
    </form>
</body>
</html>
