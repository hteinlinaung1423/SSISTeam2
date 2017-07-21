<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeBehind="ViewDetailsOfRequests.aspx.cs" Inherits="SSISTeam2.Views.Employee.ViewDetailsOfRequests" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">

    <div class="container-fluid">
        <div class="row">
            <div class="col-md-4"><h2>
                <asp:Label ID="Label2" runat="server" Text="View Details of Requests"></asp:Label></h2></div>
            
            <div class="col-md-4 col-md-offset-4"><asp:Label ID="Label3" runat="server" Text="Search"></asp:Label>
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></div>
                
         </div>   
    
       

        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1">
            <Columns>
                <asp:BoundField DataField="username" HeaderText="Employee Name" SortExpression="username" />
                <asp:BoundField DataField="reason" HeaderText="Reason" SortExpression="reason" />
                <asp:BoundField DataField="current_status" HeaderText="Status" SortExpression="current_status" />
                <asp:BoundField DataField="quantity" HeaderText="Quantity" SortExpression="quantity" />
                <asp:BoundField DataField="date_time" HeaderText="Request date_time" SortExpression="date_time" />
                <asp:BoundField DataField="item_description" HeaderText="Description" SortExpression="item_description" />
                <asp:BoundField DataField="unit_of_measure" HeaderText="Unit_of_measure" SortExpression="unit_of_measure" />
            </Columns>
                
            </asp:GridView>


        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="Data Source=(local);Initial Catalog=SSIS;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework" ProviderName="System.Data.SqlClient" SelectCommand="SELECT Requests.username, Requests.reason, Requests.current_status, Request_Event.quantity, Request_Event.date_time, Stock_Inventory.item_description, Stock_Inventory.unit_of_measure FROM Requests INNER JOIN Request_Details ON Requests.request_id = Request_Details.request_id INNER JOIN Request_Event ON Request_Details.request_detail_id = Request_Event.request_detail_id INNER JOIN Stock_Inventory ON Request_Details.item_code = Stock_Inventory.item_code"></asp:SqlDataSource>


     <%-- <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Back" CssClass="btn btn-primary" />--%>

    </div>

</asp:Content>