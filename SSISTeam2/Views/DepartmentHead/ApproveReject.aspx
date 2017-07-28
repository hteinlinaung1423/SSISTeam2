<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApproveReject.aspx.cs" Inherits="SSISTeam2.Views.DepartmentHead.ApproveReject" 
     MasterPageFile="~/MasterPage.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <div class="panel panel-default">
    <div class="panel-heading"><h3>Approve Reject </h3></div>

    <div class="panel-body">     
        <asp:Label ID="lblInfo" runat="server" />
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
                        <asp:Label runat="server" Text='<%# Eval("orig_quantity") %>' ID="lbQty1"></asp:Label>
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
        <asp:TextBox ID="tbReason" runat="server"></asp:TextBox>
        <br /><br />
        <asp:Button ID="btnApprove" runat="server" Text="Approve" OnClick="btnApprove_Click" CssClass="btn-primary"/>
        &nbsp;&nbsp;
        <asp:Button ID="btnReject" runat="server" Text="Reject" OnClick="btnReject_Click" CssClass="btn-primary"/>
         &nbsp;&nbsp;
        <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" CssClass="btn-primary" />
        <br />

        <asp:Label ID="lbAppRej" runat="server" ForeColor="#FF3300"></asp:Label>
    </div> 
       </div> 
  

    <hr />
     <div class="panel panel-default">
           <div class="panel-heading"><h3> This is Last Approved Request ,but Not Received From Store</h3></div>     
       <br />
         <asp:Panel ID="Panel1" runat="server">
                 Requested ID: <asp:Label ID="lbLastReqID" runat="server" Text="-"></asp:Label>
                <br />
                 Requested Date: <asp:Label ID="lbLastReqDate" runat="server" Text="-"></asp:Label>
                 <br />
                 Requested Employee: <asp:Label ID="lbLastReqEmp" runat="server" Text="-"></asp:Label>            
         </asp:Panel>
    <asp:Label ID="lbLastApp" runat="server" Font-Size="Large" ></asp:Label>
     <div class="panel-body"> 
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
                        <asp:Label runat="server" Text='<%# Eval("orig_quantity") %>' ID="lbQty1"></asp:Label>
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
    </div>
        
</asp:Content>
