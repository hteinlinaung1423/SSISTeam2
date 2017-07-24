<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApproveReject.aspx.cs" Inherits="SSISTeam2.Views.DepartmentHead.ApproveReject" 
     MasterPageFile="~/MasterPage.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    <div class="panel-heading"><h3>Approve Reject </h3></div>

    <div>     
        Requested ID: <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <br />
        Requested Date: <asp:Label ID="lbRqDate" runat="server" Text="-"></asp:Label>
         <br />
         Requested Employee: <asp:Label ID="lbRqEmp" runat="server" Text="-"></asp:Label>
         <br />
            <div class="table-responsive">
                <asp:GridView ID="GridView1" runat="server"
                   HeaderStyle-CssClass="text-center-impt" 
                   CssClass="table table-responsive table-striped"
                   GridLines="None"
                    AutoGenerateColumns="False">
                 <Columns>
                <asp:TemplateField HeaderText="No.">
                    <ItemTemplate>
                        <%# Container.DataItemIndex +1 %>.
                    </ItemTemplate>
                </asp:TemplateField>
                 
              <asp:TemplateField HeaderText="Item Description">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Eval("item_description") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Requested Quantity">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Eval("quantity") %>' ID="lbQty1"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                  
                <asp:TemplateField HeaderText="Unit">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Eval("unit_of_measure") %>' ID="lbUni1"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                 </Columns>
                </asp:GridView>
         </div>
         </div> 
         
    <div>
        <asp:Button ID="btnApprove" runat="server" Text="Approve" OnClick="btnApprove_Click" />
        &nbsp;&nbsp;
        <asp:Button ID="btnReject" runat="server" Text="Reject" OnClick="btnReject_Click" />
        <br />
        <asp:Label ID="lbAppRej" runat="server" ForeColor="#FF3300"></asp:Label>
    </div> 
      

    <hr /><br />
         <div>
         <asp:Label ID="Label3" runat="server" Text=" This is Last Approved Request ,but Not Received From Store" Font-Size="Medium"></asp:Label>
        <br />
         Requested ID: <asp:Label ID="lbLastReqID" runat="server" Text="Label"></asp:Label>
        <br />
         Requested Date: <asp:Label ID="lbLastReqDate" runat="server" Text="-"></asp:Label>
         <br />
         Requested Employee: <asp:Label ID="lbLastReqEmp" runat="server" Text="-"></asp:Label>
          <div class="table-responsive">
            <asp:GridView ID="GridView2" runat="server"
                HeaderStyle-CssClass="text-center-impt" 
                   CssClass="table table-responsive table-striped"
                   GridLines="None"
                AutoGenerateColumns="False">                 
                <Columns>

                <asp:TemplateField HeaderText="No.">
                    <ItemTemplate>
                        <%# Container.DataItemIndex +1 %>.
                    </ItemTemplate>
                </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Item Description">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Eval("item_description") %>' ID="lbDesp1"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Requested Quantity">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Eval("quantity") %>' ID="lbQty1"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                  
                <asp:TemplateField HeaderText="Unit">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Eval("unit_of_measure") %>' ID="lbUni1"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                 </Columns>
                </asp:GridView>
              </div>
             </div>
        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
        
</asp:Content>
