<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConfirmRetrieval.aspx.cs" Inherits="SSISTeam2.Views.StoreClerk.ConfirmRetrieval" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager runat="server"></asp:ScriptManager>

        <asp:UpdatePanel runat="server">
            <ContentTemplate>

            
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
                                <asp:Label ID="lblTotalQty" runat="server" Text='<%# Eval("QuantityExpected") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Quantity">
                            <ItemTemplate>
                                <asp:Label ID="lblQtyExpected" runat="server" Text='<%# Eval("QuantityExpected") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Actual Quantity">
                            <ItemTemplate>
                                <asp:TextBox runat="server"
                                    AutoPostBack="true"
                                    OnTextChanged="tbQtyActual_TextChanged"
                                    ID="tbQtyActual"
                                    Text='<%# Eval("QuantityActual") %>'
                                    Width="5em"
                                    TextMode="Number"
                                    min="0"
                                    max='<%# Eval("QuantityExpected") %>'
                                    step="1"
                            
                                    />
                                <asp:Button ID="btnResetRowQty" Text="Reset" runat="server"  OnClick="btnResetRowQty_Click" />
                                    <%--onchange="checkQty(event)"--%>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

                <asp:Label ID="lblWarningInfo" runat="server" EnableViewState="false"></asp:Label>

                <asp:Panel ID="panelNoItems" runat="server">
                    <asp:Label runat="server" Text="There are no items to retrieve"></asp:Label>
                </asp:Panel>
                <asp:Panel ID="panelNormal" runat="server">
                    <asp:Button ID="btnSubmit" runat="server" Text="Confirm retrieval quantities" OnClick="btnSubmit_Click" />
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <script>
            function checkQty(event) {
                console.log(event)
                var tb = event.target;
                if (parseInt(tb.value) > tb.max) {
                    tb.value = tb.max;
                    console.log("Greater than max => curr: " + tb.value + ", max: " + tb.max);
                } else {
                    tb.value = tb.value;
                }
                var isNum = /^\d+$/.test(tb.value);
                if (!isNum) {
                    tb.value = tb.max;
                }
            }
        </script>
    </div>
    </form>
</body>
</html>
