<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="FileDiscrepency.aspx.cs" Inherits="SSISTeam2.FileDiscrepency" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:GridView ID="FileDiscrepencyGV" runat="server" AutoGenerateColumns="False">
        <Columns>
            <asp:TemplateField HeaderText="No.">
                <ItemTemplate>
                    <asp:Label ID="rowIndex" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Item Name">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("description") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Item Category">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("catName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Previous Quantity">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("currentQuantity") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Actual Quantity">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("actualQuantity") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Average Price">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("averagePrice") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Reason">
                <ItemTemplate>
                    <asp:TextBox ID="reasonTB" runat="server" OnTextChanged="reasonTB_TextChanged" AutoPostBack="false"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:Button ID="ConfirmBtn" runat="server" Text="Confirm" OnClick="ConfirmBtn_Click" />
</asp:Content>
