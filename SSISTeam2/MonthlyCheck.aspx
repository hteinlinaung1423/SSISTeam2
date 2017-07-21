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
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" AllowPaging="True" PageSize="2">
        <Columns>

            <asp:TemplateField HeaderText="No.">
                <ItemTemplate>
                    <%# Container.DataItemIndex + 1 %>
                    <%--<asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>--%>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Item Description">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("description") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Category">
                <ItemTemplate>
                    <%--<asp:Label runat="server" Text='<%# Eval("category") %>'></asp:Label>--%>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Item Quantity">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("currentQuantity") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Actual Quantity">
                <ItemTemplate>
                    <asp:TextBox runat="server" Text='<%# Eval("currentQuantity") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>

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
    <asp:Label ID="testLabel" runat="server" Text="Label"></asp:Label>
    <asp:Button ID="nextBtn" runat="server" Text="Button" OnClick="nextBtn_Click" />
</asp:Content>
