<%@ Page Language="C#"  AutoEventWireup="true" CodeBehind="ChangeCategoryName1.aspx.cs" Inherits="SSISTeam2.ChangeCategoryName1" 
    MasterPageFile="~/MasterPage.master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead"
    runat="server">
    <title>Edit Category Name</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">

  
        <!-- Default panel contents -->
        <div class="panel-heading"><h3>View Catagory</h3></div>
      

     <div>
        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="Button1_Click" />
        <%--<asp:Button ID="Button2" runat="server" Text="Add New Category" OnClick="Button2_Click" />--%>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
           DataKeyNames="cat_id" OnRowDeleting="OnRowDeleting"
            OnRowEditing="OnRowEditing" OnRowUpdating="OnRowUpdating" OnRowCancelingEdit="OnRowCancelingEdit"  AllowPaging="true"
                PageSize="10"
                HeaderStyle-CssClass="text-center-impt"
                CssClass="table table-responsive table-striped" 
                GridLines="None"
               PagerStyle-HorizontalAlign="Center" PagerSettings-Position="TopAndBottom"  >

             <PagerSettings Mode="NumericFirstLast" FirstPageText="First" LastPageText="Last" PreviousPageText="Previous" NextPageText="Next" />
                <PagerTemplate>
                    <asp:Button Text="First" runat="server" CommandName="Page" CommandArgument="First" CssClass="btn btn-default btn-sm" />
                    <asp:Button Text="Prev" runat="server" CommandName="Page" CommandArgument="Prev" CssClass="btn btn-default btn-sm" />
                    <asp:Button Text="Next" runat="server" CommandName="Page" CommandArgument="Next" CssClass="btn btn-default btn-sm" />
                    <asp:Button Text="Last" runat="server" CommandName="Page" CommandArgument="Last" CssClass="btn btn-default btn-sm" />
                </PagerTemplate>
              <%--  <PagerTemplate>
                    <asp:Button Text="First" runat="server" CommandName="Page" CommandArgument="First" CssClass="btn btn-default btn-sm" />
                    <asp:Button Text="Prev" runat="server" CommandName="Page" CommandArgument="Prev" CssClass="btn btn-default btn-sm" />
                    <asp:Button Text="Next" runat="server" CommandName="Page" CommandArgument="Next" CssClass="btn btn-default btn-sm" />
                    <asp:Button Text="Last" runat="server" CommandName="Page" CommandArgument="Last" CssClass="btn btn-default btn-sm" />
                </PagerTemplate>>--%>
                 
            <Columns>
                <asp:TemplateField ItemStyle-Width="10%" HeaderText="cat_id" SortExpression="cat_id">
 <%--                   <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Eval("cat_id") %>' BackColor="Yellow"></asp:TextBox>
                    </EditItemTemplate>--%>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" CssClass="text-bold"
                        Text='<%# Eval("cat_id")  %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="10%" HeaderText="cat_name" SortExpression="cat_name">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox2" runat="server"
                        Text='<%# Eval("cat_name")  %>' BackColor="Yellow"></asp:TextBox>
                    </EditItemTemplate> 
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" CssClass="text-bold"
                        Text='<%# Bind("cat_name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
               
                <%--<asp:CommandField ShowDeleteButton="True" ShowEditButton="True"  />--%>
                <asp:TemplateField ItemStyle-Width="50%">  
                    <ItemTemplate >  
                        <asp:Button ID="btn_Edit" runat="server" Text="Edit" CommandName="Edit" CssClass="btn btn-primary"/>
                        <asp:Button ID="btn_Delete" runat="server" Text="Delete" CommandName="Delete" CssClass="btn btn-danger"/>    
                    </ItemTemplate>  
                    <EditItemTemplate>  
                        <asp:Button ID="btn_Update" runat="server" Text="Update" CommandName="Update" CssClass="btn btn-primary"/>  
                        <asp:Button ID="btn_Cancel" runat="server" Text="Cancel" CommandName="Cancel" CssClass="btn btn-primary"/>  
                    </EditItemTemplate>  
                </asp:TemplateField>      
            </Columns>
            <%-- <FooterStyle ForeColor="#003399" 
               BackColor="#99CCCC"></FooterStyle>
            <PagerStyle ForeColor="#003399" HorizontalAlign="Left" 
               BackColor="#99CCCC"></PagerStyle>
            <HeaderStyle ForeColor="#CCCCFF" Font-Bold="True" 
               BackColor="#003399"></HeaderStyle>
            <SelectedRowStyle ForeColor="#CCFF99" Font-Bold="True" 
                BackColor="#009999"></SelectedRowStyle>
            <RowStyle ForeColor="#003399" BackColor="White"></RowStyle>--%>
        </asp:GridView>
        <asp:DetailsView ID="DetailsView1" runat="server" Height="50px" Width="446px">
        </asp:DetailsView>


        
        <asp:Button ID="Button2" runat="server" Text="Add New Category" CssClass="btn btn-primary" OnClick="Button2_Click" />
        
        <br/>
    </div> 
    
</asp:Content>