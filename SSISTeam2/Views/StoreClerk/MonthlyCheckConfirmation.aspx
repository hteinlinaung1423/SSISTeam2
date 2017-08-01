<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="MonthlyCheckConfirmation.aspx.cs" Inherits="SSISTeam2.MonthlyCheckConfirmation" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderHead"
    runat="server">
    <title>Confirmation of Monthly Check</title>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="table-responsive ">
        <div class="panel-heading"><h3>Monthly Check Comfirmation</h3></div>
    </div>
    <asp:gridview runat="server" ID="confirmationGV" 
        AutoGenerateColumns="False"
        GridLines="None"
        AllowPaging="true"
        PageSize="10"
        HeaderStyle-CssClass="text-center-impt"
        CssClass="table table-responsive table-striped"
        PagerStyle-HorizontalAlign="Center" 
        PagerSettings-Position="TopAndBottom"
        >
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
    <asp:Button ID="confirmBtn" runat="server" Text="Button" OnClick="confirmBtn_Click" CssClass="btn btn-primary"/>
    <asp:Button ID="backBtn" runat="server" Text="Button" OnClick="backBtn_Click" CssClass="btn btn-primary"/>
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
</asp:Content>
