<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeBehind="ViewCatalogue.aspx.cs" Inherits="SSISTeam2.Views.Employee.ViewCat" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">

    <div class="panel panel-default">
        <!-- Default panel contents -->
        <div class="panel-heading">
            <h3>View Catalogue</h3>
        </div>

        <div class="panel-body">

            <table>
                <tr>
                    <td>
                        <div class="input-group">
                            <asp:TextBox ID="TextBox1" class="form-control" runat="server"></asp:TextBox>
                            <span class="input-group-addon">
                                <i class="fa fa-search"></i>
                            </span>
                        </div>
                    </td>
                    <td>
                        <asp:Button ID="Button1" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="Search_Click" />
                    </td>

                </tr>
            </table>


        </div>

    <%--</div>--%>


    <!-- Table -->

    <div class="table-responsive">

        <asp:GridView ID="GridView1" runat="server"
            AutoGenerateColumns="False"
            AllowPaging="True"
            HeaderStyle-CssClass="text-center-impt"
            CssClass="table table-responsive table-striped"
            GridLines="None"
             PagerStyle-HorizontalAlign="Center" PagerSettings-Position="TopAndBottom" EmptyDataText="No records Found">

            <HeaderStyle CssClass="text-center-impt"></HeaderStyle>

            <PagerSettings Mode="NumericFirstLast" FirstPageText="First" LastPageText="Last" PreviousPageText="Previous" NextPageText="Next" />

            <PagerStyle HorizontalAlign="Center"></PagerStyle>
            <PagerTemplate>
                <asp:Button Text="First" runat="server" CommandName="Page" CommandArgument="First" CssClass="btn btn-default btn-sm" />
                <asp:Button Text="Prev" runat="server" CommandName="Page" CommandArgument="Prev" CssClass="btn btn-default btn-sm" />
                <asp:Button Text="Next" runat="server" CommandName="Page" CommandArgument="Next" CssClass="btn btn-default btn-sm" />
                <asp:Button Text="Last" runat="server" CommandName="Page" CommandArgument="Last" CssClass="btn btn-default btn-sm" />
            </PagerTemplate>

            <Columns>



                <asp:BoundField DataField="categoryName" HeaderText="Category Name" SortExpression="categoryName" />
                <asp:BoundField DataField="Description" HeaderText="Item Description" SortExpression="Description" />
            </Columns>
        </asp:GridView>
        <asp:GridView ID="GridView2" runat="server"></asp:GridView>
        <%--<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SSISConnectionString %>" SelectCommand="SELECT Category.cat_name, Stock_Inventory.item_description FROM Category INNER JOIN Stock_Inventory ON Category.cat_id = Stock_Inventory.cat_id"></asp:SqlDataSource>--%>

    </div>


</asp:Content>
