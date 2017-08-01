<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="SSISTeam2.Dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
          <div class="col-md-13">
 <div class="panel panel-default">
     <div class="panel-heading">
        <h3 class="panel-title">

                <asp:Label runat="server" Font-Size="XX-Large" ID="lblFullName" CssClass="modal-header"></asp:Label></h3>
                <h4>Welcome to the SSIS</h4>
            </div>

        </div>
          </div>
    <div class="row">
        <div class="col-lg-4">
            <div class="panel-group">

                <asp:Panel ID="panelLowStocks" runat="server" CssClass="panel panel-default">
                    <div class="panel-heading">
                        <a data-toggle="collapse" href="#collapseLowStocks"><h3 class="panel-title">Top 5 items below reorder level<span class="glyphicon glyphicon-chevron-down" style="float:right;" /></h3>
                        </a>
                    </div>
                    <div id="collapseLowStocks" class="panel-collapse collapse in">
                        <div class="panel-body">
                    
                            <asp:Panel ID="panelLowStocksEmpty" runat="server">
                                No items are in need of reordering.
                            </asp:Panel>

                            <asp:Panel ID="panelLowStocksNormal" runat="server">
                                These items are running low.
                            </asp:Panel>
                        </div>

                        <asp:GridView ID="gridViewLowStocks" runat="server" GridLines="None" AutoGenerateColumns="false" CssClass="table table-striped">
                            <Columns>
                                <asp:TemplateField HeaderText="Item Code">
                                    <ItemTemplate>
                                        <asp:Label Text='<%# Eval("ItemCode")  %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item">
                                    <ItemTemplate>
                                        <asp:Label Text='<%# Eval("Description")  %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField  HeaderText="Available Qty">
                                    <ItemTemplate>
                                        <asp:Label Text='<%# Eval("AvailableQuantity")  %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reorder Level">
                                    <ItemTemplate>
                                        <asp:Label Text='<%# Eval("ReorderLevel")  %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                        <div class="panel-footer">
                            <asp:Panel ID="panelLowStocksBtn" runat="server">
                                <a href="Views/StoreClerk/lowstock.aspx"><span class="btn btn-primary">Show all</span></a>
                                <asp:Label ID="lblNumLowStock" runat="server" Text=""></asp:Label>
                            </asp:Panel>
                        </div>
                    </div>

                </asp:Panel>
            </div>
        </div>
        <div class="col-md-4">
            <%-- ToRetrieve --%>
            <div class="panel-group">
                <asp:Panel ID="panelToRetrieve" runat="server" CssClass="panel panel-default">
                    <div class="panel-heading">
                        <a data-toggle="collapse" href="#collapseRetrieve"><h3 class="panel-title">Next 3 Retrievals<span class="glyphicon glyphicon-chevron-down" style="float:right;" /></h3></a>
                    </div>
                    <div id="collapseRetrieve" class="panel-collapse collapse in">
                        <%-- Empty --%>
                        <asp:Panel ID="panelToRetrieve_Empty" runat="server">
                            <div class="panel-body">
                                No items in need of retrieving.
                            </div>
                        </asp:Panel>

                        <%-- From Warehouse --%>
                        <asp:Panel ID="panelToRetrieve_FromWarehouse" runat="server">
                            <div class="panel-body">
                                These items need to be retrieved from the warehouse.
                            </div>
                            <asp:GridView ID="gridViewToRetrieve_FromWarehouse" runat="server" GridLines="None" AutoGenerateColumns="false" CssClass="table table-striped" HeaderStyle-CssClass="info">
                                <Columns>
                                    <asp:TemplateField HeaderText="Item Code">
                                        <ItemTemplate>
                                            <asp:Label Text='<%# Eval("ItemCode")  %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Item">
                                        <ItemTemplate>
                                            <asp:Label Text='<%# Eval("Description")  %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Quantity">
                                        <ItemTemplate>
                                            <asp:Label Text='<%# Eval("Quantity")  %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <div class="panel-footer">
                                <a href="Views/StoreClerk/GenerateRetrieval.aspx"><span class="btn btn-sm btn-primary">Show all to retrieve</span>
                                </a>
                                <asp:Label ID="lblNumToRetrieve" runat="server" Text=""></asp:Label>

                            </div>
                        </asp:Panel>
                    
                        <%-- Confirm amount --%>
                        <asp:Panel ID="panelToRetrieve_ToConfirm" runat="server">
                            <div class="panel-body">
                                Please confirm the quantities of the items collected from the warehouse.
                            </div>
                            <asp:GridView ID="gridViewToRetrieve_ToConfirm" runat="server" GridLines="None" AutoGenerateColumns="false" CssClass="table table-striped" HeaderStyle-CssClass="info">
                                <Columns>
                                    <asp:TemplateField HeaderText="Item Code">
                                        <ItemTemplate>
                                            <asp:Label Text='<%# Eval("ItemCode")  %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Item">
                                        <ItemTemplate>
                                            <asp:Label Text='<%# Eval("Description")  %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Quantity">
                                        <ItemTemplate>
                                            <asp:Label Text='<%# Eval("Quantity")  %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <div class="panel-footer">
                                <a href="Views/StoreClerk/ConfirmRetrieval.aspx"><span class="btn btn-sm btn-primary">Show all to confirm</span>
                                </a>
                                <asp:Label ID="lblNumToConfirm" runat="server" Text=""></asp:Label>

                            </div>
                        </asp:Panel>
                    </div>
                </asp:Panel>
            </div>
            
        </div>
        <div class="col-md-4">
            <%-- ToDisburse --%>
            <div class="panel-group">
                <asp:Panel ID="panelToDisburse" runat="server" CssClass="panel panel-default">
                    <div class="panel-heading">
                        <a data-toggle="collapse" href="#collapseToDisburse"><h3 class="panel-title">Next 3 Disbursements<span class="glyphicon glyphicon-chevron-down" style="float:right;" /></h3></a>
                    </div>
                    <div id="collapseToDisburse" class="panel-collapse collapse in">
                        <%-- Empty --%>
                        <asp:Panel ID="panelToDisburse_Empty" runat="server">
                            <div class="panel-body">
                                No items in need of disbursement.
                            </div>
                        </asp:Panel>

                        <%-- To be disbursed --%>
                        <asp:Panel ID="panelToDisburse_ToCollectionPt" runat="server">
                            <div class="panel-body">
                                These collection points have disbursements waiting.
                            </div>
                            <asp:GridView ID="gridViewToDisburse_ToCollectionPt" runat="server" GridLines="None" AutoGenerateColumns="false" CssClass="table table-striped" HeaderStyle-CssClass="success">
                                <Columns>
                                    <asp:TemplateField HeaderText="Collection Point">
                                        <ItemTemplate>
                                            <asp:Label Text='<%# Eval("location")  %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <div class="panel-footer">
                                <a href="Views/StoreClerk/GenerateDisbursement.aspx"><span class="btn btn-sm btn-primary">Show all to disburse</span>
                                </a>
                                <asp:Label ID="lblNumToDisburse" runat="server" Text=""></asp:Label>

                            </div>
                        </asp:Panel>
                    
                        <%-- Confirm amount --%>
                        <asp:Panel ID="panelToDisburse_ToConfirm" runat="server">
                            <div class="panel-body">
                                Please confirm the sign off from representatives for these departments.
                            </div>
                            <asp:GridView ID="gridViewToDisburse_ToConfirm" runat="server" GridLines="None" AutoGenerateColumns="false" CssClass="table table-striped" HeaderStyle-CssClass="success">
                                <Columns>
                                    <asp:TemplateField HeaderText="Departments to be signed-off">
                                        <ItemTemplate>
                                            <asp:Label Text='<%# Eval("DepartmentName")  %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <div class="panel-footer">
                                <a href="Views/StoreClerk/ConfirmDisbursement.aspx"><span class="btn btn-sm btn-primary">Show all to confirm</span>
                                </a>
                                <asp:Label ID="lblNumToSignOff" runat="server" Text=""></asp:Label>

                            </div>
                        </asp:Panel>
                    </div>
                </asp:Panel>
            </div>

        </div>
    </div>
</asp:Content>
