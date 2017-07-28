<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangeCollectionnRep.aspx.cs" Inherits="SSISTeam2.Views.DepartmentHead.ChangeCollection_Rep"
          MasterPageFile="~/MasterPage.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-default">
    <div class="panel-heading" style="font-size: xx-large">
        <asp:Label ID="LabelDeptName" runat="server" Text="Label"></asp:Label> Department
    </div>

        <div class="panel-body">
             
        <asp:Table ID="Table" runat="server" class="active" style="table-layout: auto; font-size: large;" CssClass="table table-responsive table-striped">
         
            <asp:TableRow>
                <asp:TableCell>Contact Name</asp:TableCell>
                <asp:TableCell>: <asp:Label ID="LabelContactName" runat="server" Text="Label"></asp:Label></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>Telephone No</asp:TableCell>
                <asp:TableCell>: <asp:Label ID="LabelPhNo" runat="server" Text="Label"></asp:Label></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>Fax Number</asp:TableCell>
                <asp:TableCell>: <asp:Label ID="LabelFaxNo" runat="server" Text="Label"></asp:Label></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>Department Head Name<br />
                </asp:TableCell>
                <asp:TableCell>: <asp:Label ID="LabelHeadName" runat="server" Text="Label"></asp:Label></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>Current Collection Point</asp:TableCell>
               <asp:TableCell>: <asp:TextBox ID="tbCollectP" runat="server" ReadOnly="True" BorderStyle="Outset" Width="350"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>New Collection Point</asp:TableCell>
                <asp:TableCell>: <asp:DropDownList ID="ddlCollectPoint" runat="server" AutoPostBack="True" AppendDataBoundItems="true">
                    <asp:ListItem Text="Select---" Value="0"></asp:ListItem>
                      </asp:DropDownList></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>Current Representative Name</asp:TableCell>
                 <asp:TableCell>: <asp:TextBox ID="tbRepName" runat="server" ReadOnly="True" BorderStyle="Outset" Width="350"></asp:TextBox></asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>New Representative Name</asp:TableCell>
                <asp:TableCell>: <asp:DropDownList ID="ddlRepName" runat="server" AutoPostBack="True" AppendDataBoundItems="true">
                     <asp:ListItem Text="Select---" Value="0"></asp:ListItem>
                      </asp:DropDownList></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="btn-primary" /></asp:TableCell>
                <asp:TableCell>        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn-primary"/></asp:TableCell>
            </asp:TableRow>
       </asp:Table>
        <asp:Label ID="lbDDLError" runat="server" ForeColor="#FF3300"></asp:Label>
    </div>
        </div>
        
 </asp:Content>
