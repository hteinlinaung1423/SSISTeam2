<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeBehind="HeadDashboard.aspx.cs" Inherits="SSISTeam2.Views.HeadDashboard" %>

<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1"
    runat="server">
      <div class="col-md-12">
 <div class="panel panel-default">
     <div class="panel-heading">
        <h3 class="panel-title">

                <asp:Label runat="server" Font-Size="XX-Large" ID="lblFullName" CssClass="modal-header"></asp:Label></h3>
                <h4>Welcome to the SSIS</h4>
            </div>

        </div>
          </div>

    <div class="col-md-6">

        <div class="panel panel-default">
          <div class="panel-heading">
            <h3 class="panel-title"><asp:Label ID="lblPendingNum" runat="server" ></asp:Label></h3>
          </div>

          <div class="panel-body">
              <asp:GridView ID="GridView1" runat="server" GridLines="None"
                AutoGenerateColumns="false"
                PageSize="3"
                HeaderStyle-CssClass="text-center-impt"
                CssClass="table table-responsive table-striped">

                   <Columns>

                    <asp:TemplateField HeaderText="Request ID">
                                <ItemTemplate>
                                    <asp:Label ID="lblreqid" runat="server" Text='<%# Eval("request_id") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                    <asp:TemplateField HeaderText="Employee" >
                                <ItemTemplate>
                                    <asp:Label ID="lblempname" runat="server" Text='<%# Eval("username") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                       <asp:TemplateField HeaderText="Date" >
                                <ItemTemplate>
                                    <asp:Label ID="lbldate" runat="server" Text='<%# Eval("date_time") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        <asp:TemplateField HeaderText="Reason" >
                                <ItemTemplate>
                                    <asp:Label ID="lblreason" runat="server" Text='<%# Eval("reason") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                    </Columns>
              </asp:GridView>
          </div>

           <div class="panel-footer">
               <asp:Button ID="btnViewAllPending" runat="server" Text="View All Pending" CssClass="btn btn-primary" OnClick="btnViewAllPending_Click"/>
               <asp:Button ID="btnViewAllReq" runat="server" Text="View Request History" CssClass="btn btn-primary" OnClick="btnShowHistory_Click"/>
           </div>
         </div>
    </div>

    <div class="col-md-6">
        <div class="panel panel-default">
      <div class="panel-heading">
        <h3 class="panel-title">DEPARTMENT INFORMATION</h3>
      </div>

      <div class="panel-body">
        Representative: <asp:Label ID="lblrep" runat="server" ></asp:Label>
        <br />
        <br />
        Collection Point: <asp:Label ID="lblcolpoint" runat="server" ></asp:Label>
      </div>

       <div class="panel-footer">
           <asp:Button ID="btnchangecoll" runat="server" Text="Change Collection Point & Representative" CssClass="btn btn-primary" OnClick="btnchangecoll_Click" />
       </div>

       
      <div class="panel-body">
        Delegation: <asp:Label ID="lbldelegation" runat="server" Text="No delegation for now"></asp:Label>
        
      </div>

       <div class="panel-footer">
           <asp:Button ID="btndelegate" runat="server" Text="Maintain Delegation" CssClass="btn btn-primary" OnClick="btndelegate_Click"/>
       </div>

        </div>
    </div>
    
    </asp:content>