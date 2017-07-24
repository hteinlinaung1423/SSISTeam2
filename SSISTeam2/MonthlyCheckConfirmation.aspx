<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="MonthlyCheckConfirmation.aspx.cs" Inherits="SSISTeam2.MonthlyCheckConfirmation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:gridview runat="server" ID="confirmationGV" AutoGenerateColumns="False">
        <Columns>
            <asp:TemplateField HeaderText="No.">
                <ItemTemplate>
                    <asp:Label ID="rowIndex" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>                    
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Item Description">
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

            <asp:TemplateField HeaderText="Recorded Quantity">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("actualQuantity") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Price of Discrepency">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("averagePrice") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Reason">
                <ItemTemplate>
                    <asp:TextBox ID="reasonTB" runat="server" Text='<%# Eval("reason") %>' AutoPostBack="True" OnTextChanged="reasonTB_TextChanged"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
    </asp:gridview>
    <asp:Button ID="confirmBtn" runat="server" Text="Button" OnClick="confirmBtn_Click" /><asp:Button ID="backBtn" runat="server" Text="Button" OnClick="backBtn_Click" />
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
</asp:Content>
