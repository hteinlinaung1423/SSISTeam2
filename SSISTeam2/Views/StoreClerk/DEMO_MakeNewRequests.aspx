<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DEMO_MakeNewRequests.aspx.cs" Inherits="SSISTeam2.Views.StoreClerk.DEMO_MakeNewRequests" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <%= User.Identity.Name %>
    <div>
        <asp:GridView ID="GridView1" runat="server"
            AutoGenerateColumns = "false" OnRowDataBound="GridView1_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="No.">
                    <ItemTemplate>
                        <asp:Label ID="NumLabel" runat="server" Text='<%# Eval("Num") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Item Category">
                    <ItemTemplate>
                        <asp:DropDownList ID="ddlCategories" runat="server" OnSelectedIndexChanged="ddlCategories_SelectedIndexChanged" AutoPostBack="True">
                        </asp:DropDownList>
                        <%--<asp:Label runat="server" Text='<%# Eval("CurrentCatName") %>'></asp:Label>--%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Description">
                    <ItemTemplate>
                        <asp:ListBox ID="lbDescriptions" runat="server" AutoPostBack="True" OnSelectedIndexChanged="lbDescriptions_SelectedIndexChanged"></asp:ListBox>
                        <%--<asp:DropDownList ID="ddlDescriptions" runat="server"></asp:DropDownList>--%>
                        <%--<asp:Label runat="server" Text='<%# Eval("CurrentDescription") %>'></asp:Label>--%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Unit of Measure">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Eval("UnitOfMeasure") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Quantity">
                    <ItemTemplate>
                        <asp:TextBox ID="tbQuantity" runat="server" Text='<%# Eval("Quantity") %>' AutoPostBack="True" OnTextChanged="tbQuantity_TextChanged" />
                        <%--<asp:Label runat="server" Text='<%# Eval("Quantity") %>'></asp:Label>--%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Previously Approved">
                    <ItemTemplate>
                        <asp:ListBox ID="lbPrevApproved" runat="server" Enabled="False" Width="100"></asp:ListBox>
                        <%--<asp:Label runat="server" Text='<%# "hi" %>'></asp:Label>--%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Actions">
                    <ItemTemplate>
                        <asp:Button runat="server" Text="Hi" ID="btnRemoveRow" OnClick="btnRemoveRow_Click" />
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>
        <br />
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:Button ID="btnNewRow" Text="Add new entry" runat="server" OnClick="btnNewRow_Click" />
        <br />
        <asp:Button ID="btnSubmit" Text="Submit request" runat="server" OnClick="btnSubmit_Click" />
    </div>
    </form>
</body>
</html>
