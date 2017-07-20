<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/MasterPage.master"  CodeBehind="ViewTenderList.aspx.cs" Inherits="SSISTeam2.Views.StoreClerk.ViewTenderList" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">
    
    

    <div class="container-fluid">
        <div class="row">
            <div class="col-md-4"><h2>
                <asp:Label ID="Label2" runat="server" Text="Tender List"></asp:Label></h2></div>
            
            <div class="col-md-4 col-md-offset-4"><asp:Label ID="Label3" runat="server" Text="Search"></asp:Label>
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></div>
                
         </div>   
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" DataKeyNames="tender_id,tender_year_id" >
                <Columns>
                    
                    <asp:BoundField DataField="tender_date" HeaderText="tender_date" SortExpression="tender_date" />
                    <asp:BoundField DataField="deleted" HeaderText="deleted" SortExpression="deleted" />
                    <asp:BoundField DataField="rank" HeaderText="rank" SortExpression="rank" />
                    <asp:BoundField DataField="price" HeaderText="price" SortExpression="price" />
                    <asp:BoundField DataField="tender_id" HeaderText="tender_id" InsertVisible="False" ReadOnly="True" SortExpression="tender_id" />
                    <asp:BoundField DataField="item_code" HeaderText="item_code" SortExpression="item_code" />
                    <asp:BoundField DataField="tender_year_id" HeaderText="tender_year_id" InsertVisible="False" ReadOnly="True" SortExpression="tender_year_id" />
                    
                </Columns>
               
            </asp:GridView>
           

     
       
           

     
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="Data Source=(local);Initial Catalog=SSIS;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework" ProviderName="System.Data.SqlClient" SelectCommand="SELECT Tender_List.tender_date, Tender_List.deleted, Tender_List_Details.rank, Tender_List_Details.price, Tender_List_Details.tender_id, Tender_List_Details.item_code, Tender_List.tender_year_id FROM Tender_List INNER JOIN Tender_List_Details ON Tender_List.tender_year_id = Tender_List_Details.tender_year_id"></asp:SqlDataSource>
           

     
       
           

     
    </div>
         

</asp:Content>
