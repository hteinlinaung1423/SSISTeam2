<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangeCollectionnRep.aspx.cs" Inherits="SSISTeam2.Views.DepartmentHead.ChangeCollection_Rep"
          MasterPageFile="~/MasterPage.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="panel-heading" style="font-size: xx-large">
        <asp:Label ID="LabelDeptName" runat="server" Text="Label"></asp:Label> Department
    </div>

    <div>
        <table class="active" style="table-layout: auto; font-size: large;">
            <tr>
                <td>Contact Name</td>
                <td>: <asp:Label ID="LabelContactName" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
                <td>Telephone No</td>
                <td>: <asp:Label ID="LabelPhNo" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
                <td>Fax Number</td>
                <td>: <asp:Label ID="LabelFaxNo" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
                <td>Department Head Name<br />
                </td>
                <td>: <asp:Label ID="LabelHeadName" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
                <td>Current Collection Point</td>
               <%-- <td>: <asp:Label ID="lbCurrentCollectP" runat="server" Text="Label"></asp:Label> </td>--%>
               <td>: <asp:TextBox ID="tbCollectP" runat="server" ReadOnly="True" BorderStyle="Outset" Width="350"></asp:TextBox></td>
            </tr>
            <tr>
                <td>New Collection Point</td>
                <td>: <asp:DropDownList ID="ddlCollectPoint" runat="server" AutoPostBack="True"></asp:DropDownList></td>
            </tr>
            <tr>
                <td>Current Representative Name</td>
                <%--<td>: <asp:Label ID="lbRepName" runat="server" Text="Label"></asp:Label></td>--%>
                 <td>: <asp:TextBox ID="tbRepName" runat="server" ReadOnly="True" BorderStyle="Outset" Width="350"></asp:TextBox></td>
            </tr>

            <tr>
                <td>New Representative Name</td>
                <td>: <asp:DropDownList ID="ddlRepName" runat="server" AutoPostBack="True"></asp:DropDownList></td>
            </tr>
            <tr>
                <td>        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="btn-primary" /></td>
                <td>        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn-primary"/></td>
            </tr>
        </table>
    </div>
 </asp:Content>
