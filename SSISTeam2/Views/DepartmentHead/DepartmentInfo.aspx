<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DepartmentInfo.aspx.cs" Inherits="SSISTeam2.Views.DepartmentHead.DepartmentInfo" 
     MasterPageFile="~/MasterPage.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="panel-heading" style="font-size: xx-large">
        <asp:Label ID="LabelDeptName" runat="server" Text="Label"></asp:Label> Department
    </div>

    <div class="table-responsive">  
   
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        

        <table class="active" style="table-layout: auto; font-size: large;">
            <tr>
                <td>Contact Name<br />
                </td>
                <td>: <asp:Label ID="LabelContactName" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
                <td>Telephone No<br />
                </td>
                <td>: <asp:Label ID="LabelPhNo" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
                <td>Fax Number<br />
                </td>
                <td>: <asp:Label ID="LabelFaxNo" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
                <td>Department Head Name<br />
                </td>
                <td>: <asp:Label ID="LabelHeadName" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
                <td>Collection Point<br />
                </td>
                <%--<td>: <asp:Label ID="LabelCollectP" runat="server" Text="Label" Font-Bold="True"></asp:Label></td>--%>
                <td>: <asp:TextBox ID="tbCollectP" runat="server" ReadOnly="True" BorderStyle="Outset" Width="350"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="active">Representative Name<br />
                </td>
                <%--<td>: <asp:Label ID="LabelRepName" runat="server" Text="Label" Font-Bold="True" BackColor="Silver"></asp:Label></td>--%>
                <td>: <asp:TextBox ID="tbRepName" runat="server" ReadOnly="True" BorderStyle="Outset" Width="350"></asp:TextBox></td>
            </tr>
            <tr>
                <td><asp:Button ID="BtnChangeCpRn" runat="server" Text="Change" OnClick="BtnChangeCpRn_Click" CssClass="btn-primary" /></td>
            </tr>
        </table>
    </div>
    </asp:Content>