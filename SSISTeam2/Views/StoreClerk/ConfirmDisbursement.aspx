<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeBehind="ConfirmDisbursement.aspx.cs" Inherits="SSISTeam2.Views.StoreClerk.ConfirmDisbursement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead"
    runat="server">
    <title>Confirm Disbursements</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">
    <div class="row">
        <div class="col-xs-12">
            <asp:Label Text="Confirm Disbursements" runat="server" CssClass="h1" />
        </div>
    </div>

    <div style="margin: 10px"></div>

    <div class="row">
        <div class="col-xs-12">
            <asp:Label Text="Select a department and enter the quantities of items passed to the representative." runat="server" CssClass="h5 grey-text" />
        </div>
    </div>    
    
    <div style="margin: 10px"></div>

    <div class="row">
        <div class="col-xs-12">
            <asp:Label Text="Select department:" runat="server" CssClass="h4 grey-text" />
        </div>
    </div>
        
    <div class="row">
        <div class="col-xs-4">
            <asp:DropDownList ID="ddlDepartments" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartments_SelectedIndexChanged" CssClass="form-control"></asp:DropDownList>
        </div>
    </div>
    
    <div style="margin: 10px"></div>

    <div class="row">
        <div class="col-xs-12">
            <asp:Label ID="lblRepName" Text="Representative: " runat="server" CssClass="h5" />
        </div>
    </div>

    <div style="margin: 10px"></div>
    
    <div class="row">
        <div class="col-xs-12">
            <asp:GridView ID="gvDisbursement" runat="server" AutoGenerateColumns="false" CssClass="table table-responsive table-striped">
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
        <div class="col-xs-6">

            <asp:Label ID="lblWarningInfo" runat="server" EnableViewState="false"></asp:Label>

            <asp:Panel ID="panelNoItems" runat="server">

                <div class="alert alert-warning">
                    <asp:Label runat="server" Text="There are no items that were to be disbursed to this department"></asp:Label>
                </div>

            </asp:Panel>

            <asp:Panel ID="panelNormal" runat="server">
                
                <asp:Button ID="btnSubmit" runat="server" Text="Confirm quantities" OnClick="btnSubmit_Click" CssClass="btn btn-primary" />

            </asp:Panel>

        </div>
    </div>

</asp:Content>
