<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeBehind="EmpRequestHistory.aspx.cs" Inherits="SSISTeam2.Views.Employee.EmpRequestHistory" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">
    <div class="table-responsive ">
        <div class="panel-heading"><h3>View Request History</h3></div>
    </div>

    <asp:Button ID="btnCreate" runat="server" Text="Create New Request" OnClick="btnCreate_Click" CssClass="btn btn-primary"/>
    <br />
    <br />
    Search by name: <asp:TextBox ID="searchtext" runat="server"></asp:TextBox>
    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />  
    <br />
    <br />
    <asp:GridView ID="GridView2" runat="server" 
        OnSelectedIndexChanged="GridView2_SelectedIndexChanged"
        OnRowCommand="GridView2_RowCommand"
        GridLines="None"
        AutoGenerateColumns="false"
        AllowPaging="true"
        PageSize="10"
        HeaderStyle-CssClass="text-center-impt"
        CssClass="table table-responsive table-striped"
        PagerStyle-HorizontalAlign="Center" 
        PagerSettings-Position="TopAndBottom"
         
        >

<HeaderStyle CssClass="text-center-impt"></HeaderStyle>

        <PagerSettings Mode="NumericFirstLast" FirstPageText="First" LastPageText="Last" PreviousPageText="Previous" NextPageText="Next" />

<PagerStyle HorizontalAlign="Center"></PagerStyle>
                <PagerTemplate>
                    <asp:Button Text="First" runat="server" CommandName="Page" CommandArgument="First" CssClass="btn btn-default btn-sm" />
                    <asp:Button Text="Prev" runat="server" CommandName="Page" CommandArgument="Prev" CssClass="btn btn-default btn-sm" />
                    <asp:Button Text="Next" runat="server" CommandName="Page" CommandArgument="Next" CssClass="btn btn-default btn-sm" />
                    <asp:Button Text="Last" runat="server" CommandName="Page" CommandArgument="Last" CssClass="btn btn-default btn-sm" />
                </PagerTemplate>

                <Columns>

             <asp:TemplateField HeaderText="No.">
               <ItemTemplate>
                   <%#Container.DataItemIndex+1 %>
               </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Request ID" SortExpression="Request ID">
                        <ItemTemplate>
                            <asp:Label ID="lblreqid" runat="server" Text='<%# Eval("request_id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
            <asp:TemplateField HeaderText="Request Employee" SortExpression="Request Employee">
                        <ItemTemplate>
                            <asp:Label ID="lblempname" runat="server" Text='<%# Eval("username") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
            <asp:TemplateField HeaderText="Date" SortExpression="Date">
                        <ItemTemplate>
                            <asp:Label ID="lbldate" runat="server" Text='<%# Eval("date_time") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
            <asp:TemplateField HeaderText="Status" SortExpression="Status">
                        <ItemTemplate>
                            <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("current_status") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="10%" >
                        <ItemTemplate>
                            <asp:Button ID="details" runat="server" Text="Details"
                                CommandName="view" CommandArgument='<%# Bind("request_id") %>'
                                CssClass="btn btn-primary" OnClick="details_Click"/>
                        </ItemTemplate>
                        <ItemStyle Width="10%"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="10%" >
                        <ItemTemplate>
                            <asp:Button ID="update" runat="server" Text="Update" 
                                Visible='<%# IsEditable((String)Eval("username"),(String)Eval("current_status"))%>'
                                CssClass="btn btn-primary" OnClick="update_Click"/>
                            
                        </ItemTemplate>
<ItemStyle Width="10%"></ItemStyle>
                    </asp:TemplateField>
             <asp:TemplateField ItemStyle-Width="10%" >

                        <ItemTemplate>
                            <asp:Button ID="cancel" runat="server" Text="Cancel"
                                Visible='<%# IsEditable((String)Eval("username"),(String)Eval("current_status"))%>'
                                CssClass="btn btn-primary" OnClick="cancel_Click"
                                OnClientClick="return confirm('Are you sure you want to cancel this request?');" CommandName="Cancel"/>
                        </ItemTemplate>

<ItemStyle Width="10%"></ItemStyle>
                    </asp:TemplateField>
            <%--<asp:ButtonField HeaderText="Details">
                        <ItemTemplate>
                            <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("current_status") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:ButtonField>--%>

            <%-- <asp:TemplateField HeaderText="Options">
                 <ItemTemplate>
                     <a href ="UpdateReq.aspx?username=<%# Eval("username") %>">Update</a>
                     |<a href ="ViewReqDetails.aspx?username=<%# Eval("username") %>">Details</a>
                     |<asp:LinkButton ID="LinkButtonCancel" runat="server" OnClick="LinkButtonCancel_Click" OnClientClick ="return confirm('Are you sure?')">Cancel</asp:LinkButton>
                     <asp:HiddenField ID="HiddenFieldusername" runat="server" Value='<%# Eval("username") %>'/>
                 </ItemTemplate>
             </asp:TemplateField>--%>
        </Columns>
    </asp:GridView>
    <asp:Label ID="lbltest" runat="server"></asp:Label>
    </asp:Content>
   

            