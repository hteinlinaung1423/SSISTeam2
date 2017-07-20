<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DelegateAuthority.aspx.cs" Inherits="SSISTeam2.Views.DepartmentHead.DelegateAuthority" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Label1" runat="server" Text="Label"> Department</asp:Label>
      

        Delegate Employee:<asp:DropDownList ID="ddlEmployee" runat="server"></asp:DropDownList>
        <br />

        Reason:<asp:TextBox ID="tbReason" runat="server" TextMode="MultiLine"></asp:TextBox>
        <br />

        Start Date:<asp:TextBox ID="tbStartDate" runat="server" TextMode="Date"></asp:TextBox>
        <br />
        End Date:<asp:TextBox ID="tbEndDate" runat="server" TextMode="Date"></asp:TextBox>
        <br />

        <asp:Button ID="btnSave" runat="server" Text="Save" />
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" />

    </div>
    </form>

</body>
</html>
