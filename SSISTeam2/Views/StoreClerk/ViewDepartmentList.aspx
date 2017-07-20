<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/MasterPage.master" CodeBehind="ViewDepartmentList.aspx.cs" Inherits="SSISTeam2.Views.StoreClerk.ViewDepartmentList" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">
    
    

    <div class="container-fluid">
        <div class="row">
            <div class="col-md-4"><h2>
                <asp:Label ID="Label2" runat="server" Text="Department List"></asp:Label></h2></div>
            
            <div class="col-md-4 col-md-offset-4"><asp:Label ID="Label3" runat="server" Text="Search"></asp:Label>
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></div>
                
         </div>   
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" DataKeyNames="dept_code" >
                <Columns>
                
                    <asp:BoundField DataField="dept_code" HeaderText="Department Code" ReadOnly="True" SortExpression="dept_code" />
                    <asp:BoundField DataField="name" HeaderText="Department Name" SortExpression="name" />
                    <asp:BoundField DataField="rep_user" HeaderText="Representative Name" SortExpression="rep_user" />
                    <asp:BoundField DataField="contact_user" HeaderText="Contact Name" SortExpression="contact_user" />
                    <asp:BoundField DataField="contact_num" HeaderText="Contact Number" SortExpression="contact_num" />
                    <asp:BoundField DataField="fax_num" HeaderText="Fax Number" SortExpression="fax_num" />
                    <asp:BoundField DataField="head_user" HeaderText="Department Head's Name" SortExpression="head_user" />
                    <asp:BoundField DataField="collection_point" HeaderText="Collection Point" SortExpression="collection_point" />
                    <asp:BoundField DataField="deleted" HeaderText="Deleted" SortExpression="deleted" />
                
                </Columns>
               
            </asp:GridView>
           
     
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="Data Source=(local);Initial Catalog=SSIS;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework" ProviderName="System.Data.SqlClient" SelectCommand="SELECT [dept_code], [name], [rep_user], [contact_user], [contact_num], [fax_num], [head_user], [collection_point], [deleted] FROM [Department]"></asp:SqlDataSource>
           
     
    </div>
         

</asp:Content>
