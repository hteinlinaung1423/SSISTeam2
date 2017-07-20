﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="MonthlyCheck.aspx.cs" Inherits="SSISTeam2.MonthlyCheck" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>
        Monthly Stocktake
    </p>
        Sort By:
        <asp:DropDownList ID="SortDDL" runat="server">
            <asp:ListItem>Category</asp:ListItem>
            <asp:ListItem Value="Name">Item Name</asp:ListItem>
            <asp:ListItem>Quantity</asp:ListItem>
            <asp:ListItem Value="Accounted">Accounted For</asp:ListItem>
        </asp:DropDownList>
        Date:
        <asp:TextBox ID="DateTB" runat="server"></asp:TextBox>
        <br />
    <asp:GridView ID="MonthlyCheckGV" runat="server" ShowHeaderWhenEmpty="True">       
    <Columns>
        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Pay Scale">
            <ItemTemplate>
                <asp:TextBox ID="txtPayScale" runat="server" Text='<%# Eval("PayScale") %>'></asp:TextBox>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Increment Amount">
            <ItemTemplate>
                <asp:TextBox ID="txtIncrementAmount" runat="server" Text='<%# Eval("IncrementAmount") %>'></asp:TextBox>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Period">
            <ItemTemplate>
                <asp:TextBox ID="txtPeriod" runat="server" Text='<%# Eval("Period") %>'></asp:TextBox>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>

    <asp:Table ID="Table1" runat="server">
        <asp:TableRow>
            <asp:TableCell>
                No.
            </asp:TableCell>
            <asp:TableCell>
                Category
            </asp:TableCell>
            <asp:TableCell>
                Item Name
            </asp:TableCell>
            <asp:TableCell>
                Quantity
            </asp:TableCell>
            <asp:TableCell>
                All Accounted For
            </asp:TableCell>
            <asp:TableCell>
                Note Discrepency
            </asp:TableCell>
            <asp:TableCell>
                Remove
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <asp:Button ID="AddBtn" runat="server" Text="Add" OnClick="AddBtn_Click" />
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
</asp:Content>