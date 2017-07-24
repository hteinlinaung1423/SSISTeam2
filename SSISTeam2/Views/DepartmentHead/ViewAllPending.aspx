<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewAllPending.aspx.cs" Inherits="SSISTeam2.ViewAllPending"
       MasterPageFile="~/MasterPage.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="panel panel-default">

    <div class="table-responsive">
        <div class="panel-heading"><h3>View All Pending</h3></div>

        <asp:GridView ID="GridView1" runat="server" 
            OnSelectedIndexChanged="GridView1_SelectedIndexChanged" 
            AutoGenerateColumns="False"
                PageSize="10"
                HeaderStyle-CssClass="text-center-impt"
                CssClass="table table-responsive table-striped"
                GridLines="None"
                PagerStyle-HorizontalAlign="Center" PagerSettings-Position="TopAndBottom">

                <PagerSettings Mode="NumericFirstLast" FirstPageText="First" LastPageText="Last" PreviousPageText="Previous" NextPageText="Next" />
                <PagerTemplate>
                    <asp:Button Text="First" runat="server" CommandName="Page" CommandArgument="First" CssClass="btn btn-default btn-sm" />
                    <asp:Button Text="Prev" runat="server" CommandName="Page" CommandArgument="Prev" CssClass="btn btn-default btn-sm" />
                    <asp:Button Text="Next" runat="server" CommandName="Page" CommandArgument="Next" CssClass="btn btn-default btn-sm" />
                    <asp:Button Text="Last" runat="server" CommandName="Page" CommandArgument="Last" CssClass="btn btn-default btn-sm" />
                </PagerTemplate>

            <Columns>

                <asp:TemplateField HeaderText="No.">
                    <ItemTemplate>
                        <%# Container.DataItemIndex +1 %>.
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Request Id">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Eval("request_id") %>' ID="lbReqId"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Requested Employee">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Eval("username") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Status">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Eval("current_status") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Requested Date">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Eval("date_time") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:ButtonField ButtonType="Button" CommandName="Select" Text="Details" ControlStyle-CssClass="btn-primary" />
           
            </Columns>
        </asp:GridView>
       <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    </div>
     
    </div>
 </asp:Content>
