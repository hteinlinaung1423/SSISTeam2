<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangeCategoryName1.aspx.cs" Inherits="SSISTeam2.ChangeCategoryName1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
     <div>
        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="Search" OnClick="Button1_Click" />
        <%--<asp:Button ID="Button2" runat="server" Text="Add New Category" OnClick="Button2_Click" />--%>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AllowPaging="True" BorderWidth="1px" BackColor="White"
            CellPadding="4" BorderStyle="None" BorderColor="#3366CC" DataKeyNames="cat_id" OnRowDeleting="OnRowDeleting"
            OnRowEditing="OnRowEditing" OnRowUpdating="OnRowUpdating" OnRowCancelingEdit="OnRowCancelingEdit" >
                 <HeaderStyle BackColor="#86C708" Font-Bold="True" ForeColor="White" HorizontalAlign="Left"></HeaderStyle>
            <Columns>
                <asp:TemplateField HeaderText="cat_id" SortExpression="cat_id">
 <%--                   <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Eval("cat_id") %>' BackColor="Yellow"></asp:TextBox>
                    </EditItemTemplate>--%>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server"
                        Text='<%# Eval("cat_id") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="cat_name" SortExpression="cat_name">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox2" runat="server"
                        Text='<%# Eval("cat_name") %>' BackColor="Yellow"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server"
                        Text='<%# Bind("cat_name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
               
                <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
            </Columns>
             <FooterStyle ForeColor="#003399" 
               BackColor="#99CCCC"></FooterStyle>
            <PagerStyle ForeColor="#003399" HorizontalAlign="Left" 
               BackColor="#99CCCC"></PagerStyle>
            <HeaderStyle ForeColor="#CCCCFF" Font-Bold="True" 
               BackColor="#003399"></HeaderStyle>
            <SelectedRowStyle ForeColor="#CCFF99" Font-Bold="True" 
                BackColor="#009999"></SelectedRowStyle>
            <RowStyle ForeColor="#003399" BackColor="White"></RowStyle>
        </asp:GridView>
        <asp:DetailsView ID="DetailsView1" runat="server" Height="50px" Width="446px">
        </asp:DetailsView>

        
        <asp:Button ID="Button2" runat="server" Text="Add New Category" OnClick="Button2_Click" />
        
        <br/>
    </div>
    </form>
</body>
</html>
