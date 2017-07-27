<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeBehind="ConfirmRetrieval.aspx.cs" Inherits="SSISTeam2.Views.StoreClerk.ConfirmRetrieval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead"
    runat="server">
    <title>Confirm Retrievals</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">
    <div class="row">
        <div class="col-xs-12">
            <asp:Label Text="Confirm Retrievals" runat="server" CssClass="h1" />
        </div>
    </div>

    <div style="margin: 10px"></div>

    <div class="row">
        <div class="col-xs-12">
            <asp:Label Text="Confirm the quantites that were actually retrieved. If there are discrepancies, you will be directed to file them." runat="server" CssClass="h5 grey-text" />
        </div>
    </div>

    <div style="margin: 10px"></div>


    <asp:ScriptManager runat="server"></asp:ScriptManager>

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-xs-12">

                    <asp:GridView ID="gvToRetrieve" runat="server" AutoGenerateColumns="false" CssClass="table table-responsive table-striped">
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
                                        step="1" />
                                    <asp:Button ID="btnResetRowQty" Text="Reset" runat="server" OnClick="btnResetRowQty_Click" />
                                    <%--onchange="checkQty(event)"--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>

            <div class="row">
                <div class="col-xs-12">

                    <asp:Label ID="lblWarningInfo" runat="server" EnableViewState="false"></asp:Label>

                    <asp:Panel ID="panelNoItems" runat="server">

                        <div class="alert alert-warning">
                            <asp:Label runat="server" Text="There are no items to retrieve"></asp:Label>
                        </div>

                    </asp:Panel>

                    <asp:Panel ID="panelNormal" runat="server">

                        <asp:Button ID="btnSubmit" runat="server" Text="Confirm retrieval quantities" OnClick="btnSubmit_Click" CssClass="btn btn-primary" />
                    
                    </asp:Panel>

                </div>
            </div>
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

</asp:Content>
