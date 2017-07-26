<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewGeneratedDisbursements.aspx.cs" Inherits="SSISTeam2.Views.StoreClerk.ViewGeneratedDisbursements" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label Text="Collection point:" runat="server" />
        <asp:DropDownList ID="ddlCollectionPoints" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCollectionPoints_SelectedIndexChanged"></asp:DropDownList>
        <asp:Label Text="Department:" runat="server" />
        <asp:DropDownList ID="ddlDepartments" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartments_SelectedIndexChanged"></asp:DropDownList>
    </div>

        <asp:GridView ID="gvDisbursement" runat="server" AutoGenerateColumns="false">
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
                        <asp:TemplateField HeaderText="Quantity">
                            <ItemTemplate>
                                <asp:Label ID="lblQtyExpected" runat="server" Text='<%# Eval("QuantityExpected") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Actual Quantity">
                            <ItemTemplate>
                                <%--<asp:TextBox runat="server"
                                    AutoPostBack="true"
                                    OnTextChanged="tbQtyActual_TextChanged"
                                    ID="tbQtyActual"
                                    Text='<%# Eval("QuantityActual") %>'
                                    Width="5em"
                                    TextMode="Number"
                                    min="0"
                                    max='<%# Eval("QuantityExpected") %>'
                                    step="1"
                            
                                    />--%>
                                <%--<asp:Button ID="btnResetRowQty" Text="Reset" runat="server"  OnClick="btnResetRowQty_Click" />--%>
                                    <%--onchange="checkQty(event)"--%>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
    </form>
</body>
</html>
