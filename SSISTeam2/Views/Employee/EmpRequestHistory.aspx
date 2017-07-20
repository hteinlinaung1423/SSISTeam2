<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmpRequestHistory.aspx.cs" Inherits="SSISTeam2.Views.Employee.EmpRequestHistory" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">  
</asp:Content>



<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ISBN" DataSourceID="SqlDataSource1" OnLoad="Page_Load" HorizontalAlign="Center" BorderColor="Black" PageSize="6" AllowPaging="True" Font-Bold="True" GridLines="Horizontal" BorderWidth="0px" Width="100%" CssClass="auto-style1">
                <Columns>
                    <asp:TemplateField HeaderText="Request ID" SortExpression="Title">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("Title") %>'></asp:Label>
                            <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# Bind("Title") %>' Font-Bold="True" Font-Underline="True" Font-Size="Larger"></asp:HyperLink>
                        
                        </ItemTemplate>
                        <ItemStyle Width="200px" />
                        <ItemTemplate>
                            <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# Bind("Title") %>' Font-Bold="True" Font-Underline="True" Font-Size="Larger"></asp:HyperLink>
                        </ItemTemplate>

                    </asp:TemplateField>

                    <asp:BoundField DataField="ISBN" HeaderText="Employee Name" ReadOnly="True" SortExpression="ISBN" ItemStyle-Width="80" >
                        <ItemStyle Width="150px"></ItemStyle>
                    </asp:BoundField>

                    <asp:BoundField DataField="AuthorName" HeaderText="Date" SortExpression="AuthorName" ItemStyle-Width="130">
                        <ItemStyle Width="130px"></ItemStyle>
                    </asp:BoundField>

                    <asp:BoundField DataField="AuthorName" HeaderText="Status" SortExpression="AuthorName" ItemStyle-Width="130">
                        <ItemStyle Width="130px"></ItemStyle>
                    </asp:BoundField>

                    <asp:TemplateField HeaderText="Quantity" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10" >
                        <ItemTemplate>
                            <asp:TextBox ID="TextBoxQuantity" runat="server" Width="20" Height="18" MaxLength="100" Rows="1" ></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="AddToCart" ControlStyle-Width="80">
                        <ItemTemplate>
                            <asp:Button ID="AddtoCartBtn" runat="server" Text="AddToCart" OnClick="AddtoCartBtn_Click" Font-Size="Small" />
                        </ItemTemplate><ControlStyle Width="80px"></ControlStyle>
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle BackColor="#CCCCCC" HorizontalAlign="Center" />
                <RowStyle Height="10px" />
                <SortedAscendingCellStyle BackColor="Red" />
            </asp:GridView>

          
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:GreatBooksDBConnectionString %>" 
                SelectCommand="SELECT * FROM [booksearchview]"
                            
                FilterExpression="([ISBN] LIKE '%{0}%') OR ([Title] LIKE '%{1}%') OR ([Description] LIKE '%{2}%')">
                     <FilterParameters>
                        <asp:ControlParameter Name="ISBN" ControlID="tbxSearch" PropertyName="Text"/>
                        <asp:ControlParameter Name="Title" ControlID="tbxSearch" PropertyName="Text" />
                        <asp:ControlParameter Name="Description" ControlID="tbxSearch" PropertyName="Text" />
                    </FilterParameters>
            </asp:SqlDataSource>        
</asp:Content>     
    </div>
    </form>
</body>
</html>
