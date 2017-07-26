<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GenerateDisbursement.aspx.cs" Inherits="SSISTeam2.Views.StoreClerk.GenerateDisbursement" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:DropDownList ID="ddlCollectionPoint" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCollectionPoint_SelectedIndexChanged"></asp:DropDownList>

        <asp:Label ID="lblDebug" Text="text" runat="server" />

        <asp:GridView ID="gvToRetrieve" runat="server" AutoGenerateColumns="false">
            <Columns>
                <asp:TemplateField HeaderText="No.">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Item">
                    <ItemTemplate>
                        <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("ItemDescription") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Total Quantity">
                    <ItemTemplate>
                        <asp:Label ID="lblTotalQty" runat="server" Text='<%# Eval("IncludedQty") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Department">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Eval("DeptName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Quantity">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Eval("Quantity") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Include?">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkbxInclude" runat="server" Checked='<%# Eval("Include") %>' AutoPostBack="true" OnCheckedChanged="chkbxInclude_CheckedChanged" />
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:ButtonField ButtonType="Button" CommandName="Select" HeaderText="View" ShowHeader="True" Text="Details" />--%>

            </Columns>
        </asp:GridView>
        <asp:Panel ID="panelNoItems" runat="server">
            <asp:Label runat="server" Text="There are no items to disburse at this collection point"></asp:Label>
        </asp:Panel>
        <asp:Panel ID="panelNormal" runat="server">
            <asp:Button ID="btnSubmit" runat="server" Text="Generate Disbursment Forms" OnClick="btnSubmit_Click" />
        </asp:Panel>
    </div>
    </form>
</body>
</html>
