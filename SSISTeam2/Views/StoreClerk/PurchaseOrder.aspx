<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeBehind="PurchaseOrder.aspx.cs" Inherits="SSISTeam2.Views.StoreClerk.PurchaseOrder" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">

    <div class="panel panel-default">
        <!-- Default panel contents -->
        <div class="panel-heading">Create New Purchase Order</div>
        <div class="panel-body">
            <p>Step 1 of 3: Add Items</p>
            <br />

            <div class="row">
                <div class="col-md-4">
                    Select Category :<asp:DropDownList ID="DropDownList1" class="form-control" runat="server" DataSourceID="SqlDataSource1" DataTextField="cat_name" DataValueField="cat_name" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"></asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="Data Source=.;Initial Catalog=SSIS;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework" ProviderName="System.Data.SqlClient" SelectCommand="SELECT [cat_name] FROM [Category]"></asp:SqlDataSource>
                </div>
                <div class="col-md-4">
                    Select Supplier :<asp:DropDownList ID="DropDownList2" class="form-control" runat="server" DataSourceID="SqlDataSource2" DataTextField="name" DataValueField="name"></asp:DropDownList>                    
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="Data Source=.;Initial Catalog=SSIS;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework" ProviderName="System.Data.SqlClient" SelectCommand="SELECT distinct Supplier.name FROM Tender_List INNER JOIN Supplier ON Tender_List.supplier_id = Supplier.supplier_id INNER JOIN Tender_List_Details ON Tender_List.tender_year_id = Tender_List_Details.tender_year_id"></asp:SqlDataSource>
                </div>
            </div>



        </div>

        <!-- Table -->

        <div class="table-responsive">

            <asp:GridView ID="GridView1" runat="server"
                AutoGenerateColumns="false"
                AllowPaging="true"
                PageSize="10"
                HeaderStyle-CssClass="text-center-impt"
                CssClass="table table-responsive table-striped"
                GridLines="None"
                PagerStyle-HorizontalAlign="Center" PagerSettings-Position="TopAndBottom">

                 

                <Columns>

                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="Item Code"><%-- HeaderStyle-CssClass="text-center-impt">--%>

                        <ItemTemplate>
                            <asp:Label ID="Label_ItemCode" runat="server" Text='<%# Eval("item_code") %>' CssClass="text-bold"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="Item Description"><%-- HeaderStyle-CssClass="text-center-impt">--%>

                        <ItemTemplate>
                            <asp:Label ID="Label_ItemDesc" runat="server" Text='<%# Eval("item_description") %>' CssClass="text-bold"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="Supplier ID">

                        <ItemTemplate>
                            <asp:Label ID="Label_Supplier" runat="server" Text='<%# Eval("name") %>' CssClass="text-bold"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="Add to Cart"><%-- HeaderStyle-CssClass="text-center-impt">--%>

                        <ItemTemplate>
                            <asp:Button ID="add" runat="server" Text="Add"
                                CssClass="btn btn-primary" />
                        </ItemTemplate>
                    </asp:TemplateField>



                </Columns>
            </asp:GridView>

        </div>
    </div>

</asp:Content>
