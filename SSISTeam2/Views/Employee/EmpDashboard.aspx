﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeBehind="EmpDashboard.aspx.cs" Inherits="SSISTeam2.Views.Employee.EmpDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead"
    runat="server">
<%--    <title>Employee Dashboard</title>--%>
</asp:Content>
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
            <h3 class="panel-title">YOUR LATEST 3 REQUEST(S)</h3>
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
                    <asp:TemplateField HeaderText="Status" >
                                <ItemTemplate>
                                    <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("current_status") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                    </Columns>
              </asp:GridView>
          </div>

           <div class="panel-footer">
               <asp:Button ID="btnMakeNewReq" runat="server" Text="Make New Request" CssClass="btn btn-primary" OnClick="btnMakeNewReq_Click"/></div>
        </div>
    </div>

    <div class="col-md-6">
        <div class="panel panel-default">
      <div class="panel-heading">
        <h3 class="panel-title">DEPARTMENT LATEST 3 REQUEST(S)</h3>
      </div>
      <div class="panel-body">
         <asp:GridView ID="GridView2" runat="server" GridLines="None"
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
                    <asp:TemplateField HeaderText="Status" >
                                <ItemTemplate>
                                    <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("current_status") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                    </Columns>
              </asp:GridView>
      </div>
       <div class="panel-footer">
           <asp:Button ID="btnShowHistory" runat="server" Text="Show Request History" CssClass="btn btn-primary" OnClick="btnShowHistory_Click"/></div></div>
    </div>
    </asp:content>