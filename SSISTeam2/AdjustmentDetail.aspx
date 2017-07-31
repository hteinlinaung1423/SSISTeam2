<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AdjustmentDetail.aspx.cs" Inherits="SSISTeam2.ApproveStockAdjustment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="table-responsive ">
        <div class="panel-heading"><h3>Adjustment Details</h3></div>
    </div>
    <asp:GridView ID="GridView1" runat="server" 
        AutoGenerateColumns="False"
        GridLines="None"
        AllowPaging="true"
        PageSize="10"
        HeaderStyle-CssClass="text-center-impt"
        CssClass="table table-responsive table-striped"
        PagerStyle-HorizontalAlign="Center" 
        PagerSettings-Position="TopAndBottom"
        >
        <Columns>
            <asp:TemplateField HeaderText="No.">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Entry">
                <ItemTemplate>
                    <asp:Table ID="Table1" runat="server">
                        <asp:TableRow>
                            <asp:TableHeaderCell>Item Description</asp:TableHeaderCell>
                            <asp:TableCell><asp:Label ID="itemDescpLbl" runat="server" Text='<%# Eval("catName") %>'></asp:Label></asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow>
                            <asp:TableHeaderCell>Quantity Adjusted</asp:TableHeaderCell>
                            <asp:TableCell><asp:Label ID="qtyAdjustedLbl" runat="server" Text='<%# Eval("quantityAdjusted") %>'></asp:Label></asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow>
                            <asp:TableHeaderCell>Cost Adjusted</asp:TableHeaderCell>
                            <asp:TableCell><asp:Label ID="costAdjustedLbl" runat="server" Text='<%# Eval("costAdjusted") %>'></asp:Label></asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow>
                            <asp:TableHeaderCell>Reason</asp:TableHeaderCell>
                            <asp:TableCell><asp:Label ID="reasonLbl" runat="server" Text='<%# Eval("reason") %>'></asp:Label></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView><asp:Label ID="testLbl" runat="server"></asp:Label>
    <asp:GridView ID="GridView2" runat="server"></asp:GridView>
    <asp:Button ID="Approve" runat="server" Text="approveBtn" OnClick="Approve_Click" CssClass="btn btn-primary"/>
    <asp:Button ID="Reject" runat="server" Text="RejectBtn" OnClick="Reject_Click" CssClass="btn btn-primary"/>
</asp:Content>
