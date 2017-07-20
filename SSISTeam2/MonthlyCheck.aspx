<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="MonthlyCheck.aspx.cs" Inherits="SSISTeam2.MonthlyCheck" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>
        Monthly Stocktake
    </p>
        Sort By:
        <asp:DropDownList ID="SortDDL" runat="server">
            <asp:ListItem>Category</asp:ListItem>
            <asp:ListItem Value="Name">Item Name</asp:ListItem>
            <asp:ListItem>Quantity</asp:ListItem>
            <asp:ListItem Value="Accounted">Accounted For</asp:ListItem>
        </asp:DropDownList>
        Date:
        <asp:TextBox ID="DateTB" runat="server"></asp:TextBox>
        <br />

    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" DataKeyNames="item_code" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating">
        <Columns>
            <asp:CommandField ShowEditButton="True" ButtonType="Button" EditText="Discrepency" HeaderText="Discrepency" UpdateText="Confirm" />
            <asp:BoundField DataField="current_qty" HeaderText="current_qty" SortExpression="current_qty" />
            <asp:BoundField DataField="unit_of_measure" HeaderText="unit_of_measure" SortExpression="unit_of_measure" />
            <asp:BoundField DataField="item_description" HeaderText="item_description" SortExpression="item_description" />
            <asp:BoundField DataField="image_path" HeaderText="image_path" SortExpression="image_path" />
            <asp:BoundField DataField="cat_id" HeaderText="cat_id" SortExpression="cat_id" />
            <asp:BoundField DataField="item_code" HeaderText="item_code" ReadOnly="True" SortExpression="item_code" Visible="False" />
        </Columns>
    </asp:GridView>

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SSISConnectionString %>" SelectCommand="SELECT [current_qty], [unit_of_measure], [item_description], [image_path], [cat_id], [item_code] FROM [Stock_Inventory]" DeleteCommand="DELETE FROM [Stock_Inventory] WHERE [item_code] = @item_code" InsertCommand="INSERT INTO [Stock_Inventory] ([current_qty], [unit_of_measure], [item_description], [image_path], [cat_id], [item_code]) VALUES (@current_qty, @unit_of_measure, @item_description, @image_path, @cat_id, @item_code)" UpdateCommand="UPDATE [Stock_Inventory] SET [current_qty] = @current_qty, [unit_of_measure] = @unit_of_measure, [item_description] = @item_description, [image_path] = @image_path, [cat_id] = @cat_id WHERE [item_code] = @item_code">
        <DeleteParameters>
            <asp:Parameter Name="item_code" Type="String" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="current_qty" Type="Int32" />
            <asp:Parameter Name="unit_of_measure" Type="String" />
            <asp:Parameter Name="item_description" Type="String" />
            <asp:Parameter Name="image_path" Type="String" />
            <asp:Parameter Name="cat_id" Type="Int32" />
            <asp:Parameter Name="item_code" Type="String" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="current_qty" Type="Int32" />
            <asp:Parameter Name="unit_of_measure" Type="String" />
            <asp:Parameter Name="item_description" Type="String" />
            <asp:Parameter Name="image_path" Type="String" />
            <asp:Parameter Name="cat_id" Type="Int32" />
            <asp:Parameter Name="item_code" Type="String" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
</asp:Content>
