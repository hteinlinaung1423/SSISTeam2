<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeBehind="ViewSupplierList.aspx.cs" Inherits="SSISTeam2.Views.StoreClerk.ViewSupplierList" %>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">
    
    

    <div class="container-fluid">
        <div class="row">
            <div class="col-md-4"><h2>
                <asp:Label ID="Label2" runat="server" Text="Supplier List"></asp:Label></h2></div>
            
            <div class="col-md-4 col-md-offset-4"><asp:Label ID="Label3" runat="server" Text="Search"></asp:Label>
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></div>
                
         </div>   
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" DataKeyNames="supplier_id">
                <Columns>
                    <asp:BoundField DataField="name" HeaderText="Supplier Name" SortExpression="name" />
                    <asp:BoundField DataField="contact_name" HeaderText="Contact Name" SortExpression="contact_name" />
                    <asp:BoundField DataField="contact_num" HeaderText="Contact Number" SortExpression="contact_num" />
                    <asp:BoundField DataField="fax_num" HeaderText="Fax Number" SortExpression="fax_num" />
                    <asp:BoundField DataField="address" HeaderText="Address" SortExpression="address" />
                    
                    <asp:BoundField DataField="gst_reg_num" HeaderText="GST Registration Number" SortExpression="gst_reg_num" />
                    <asp:BoundField DataField="supplier_id" HeaderText="Supplier ID" ReadOnly="True" SortExpression="supplier_id" />
                    <asp:BoundField DataField="deleted" HeaderText="Deleted" SortExpression="deleted" />
                </Columns>
               
            </asp:GridView>
           

     
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="Data Source=(local);Initial Catalog=SSIS;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework" ProviderName="System.Data.SqlClient" SelectCommand="SELECT [name], [contact_name], [contact_num], [fax_num], [address], [logo_path], [gst_reg_num], [supplier_id], [deleted] FROM [Supplier]"></asp:SqlDataSource>
           

     
    </div>
         

</asp:Content>
