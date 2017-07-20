<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeBehind="ViewCatalogue.aspx.cs" Inherits="SSISTeam2.Views.Employee.ViewCatalogue" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">
    
    

    <div class="container-fluid">
        <div class="row">
            <div class="col-md-4"><h2>
                <asp:Label ID="Label2" runat="server" Text="View Catalogue"></asp:Label></h2></div>
            
            <div class="col-md-4 col-md-offset-4"><asp:Label ID="Label3" runat="server" Text="Search"></asp:Label>
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></div>
                
         </div>   
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1">
                <Columns>
                    <asp:BoundField DataField="cat_name" HeaderText="cat_name" SortExpression="cat_name" />
                    <asp:BoundField DataField="item_description" HeaderText="item_description" SortExpression="item_description" />
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="Data Source=(local);Initial Catalog=SSIS;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework" ProviderName="System.Data.SqlClient" SelectCommand="SELECT Category.cat_name, Stock_Inventory.item_description FROM Category INNER JOIN Stock_Inventory ON Category.cat_id = Stock_Inventory.cat_id"></asp:SqlDataSource>

           <%-- <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Back" CssClass="btn btn-primary" />
            <br />--%>

     
    </div>
         

</asp:Content>
