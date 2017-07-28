﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeBehind="MakeNewRequest.aspx.cs" Inherits="SSISTeam2.Views.StoreClerk.MakeNewRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead"
    runat="server">
    <title>Make New Request</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">
    <div class="table-responsive ">
        <div class="panel-heading">
            <h3>

                <asp:Label ID="lblPageTitle" Text="Create New Request" runat="server" />

            </h3>
        </div>
    </div>
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="row">

                    <asp:GridView ID="GridView1" Width="100%" runat="server"
                        AutoGenerateColumns="false"
                        OnRowDataBound="GridView1_RowDataBound"
                        CssClass="table table-responsive table-striped"
                        GridLines="None"
                        BorderColor="White" >
                        <Columns>
                            <asp:TemplateField HeaderText="No.">
                                <ItemTemplate>
                                    <asp:Label ID="NumLabel" runat="server" Text='<%# Eval("Num") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Category">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlCategories" runat="server" OnSelectedIndexChanged="ddlCategories_SelectedIndexChanged" AutoPostBack="True" CssClass="form-control">
                                    </asp:DropDownList>
                                    <%--<asp:Label runat="server" Text='<%# Eval("CurrentCatName") %>'></asp:Label>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description">
                                <ItemTemplate>
                                    <asp:ListBox ID="lbDescriptions" runat="server" AutoPostBack="True" Height="150px" OnSelectedIndexChanged="lbDescriptions_SelectedIndexChanged" CssClass="form-control"></asp:ListBox>
                                    <%--<asp:DropDownList ID="ddlDescriptions" runat="server"></asp:DropDownList>--%>
                                    <%--<asp:Label runat="server" Text='<%# Eval("CurrentDescription") %>'></asp:Label>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit of Measure">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("UnitOfMeasure") %>' EnableViewState="false"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Quantity">
                                <ItemTemplate>
                                    <asp:TextBox ID="tbQuantity" runat="server" width="80px" Text='<%# Eval("Quantity") %>'
                                        max='<%# Eval("Quantity") %>'
                                        OnTextChanged="tbQuantity_TextChanged"
                                        AutoPostBack="True"
                                        CssClass="form-control" />
                                    <%--onchange="checkQty(event)"--%>
                                    <%--<asp:Label runat="server" Text='<%# Eval("Quantity") %>'></asp:Label>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Previously Approved">
                                <ItemTemplate>
                                    <asp:ListBox ID="lbPrevApproved" runat="server" Enabled="False" CssClass="form-control" height="150px" EnableViewState="false"></asp:ListBox>
                                    <%--<asp:Label runat="server" Text='<%# "hi" %>'></asp:Label>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:Button runat="server" AutoPostBack="True" Text="Remove" ID="btnRemoveRow" OnClick="btnRemoveRow_Click" CssClass="btn btn-danger" />
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>
                </div>
                <div class="row">
                    <asp:Panel runat="server" ID="panelCannotChange">
                        <div class="alert alert-warning">
                            <asp:Label Text="This request cannot be changed. " runat="server" />
                            <asp:Label ID="lblCannotChangeInfo" runat="server" />
                        </div>
                    </asp:Panel>
                </div>
                <div class="row">
                    <asp:Panel runat="server" ID="panelNormalBtns">
                        <div class="col-xs-6">
                            <div class="row">
                                <asp:Button ID="btnNewRow" Text="Add new entry" runat="server" OnClick="btnNewRow_Click" CssClass="btn btn-success" />
                            </div>
                            <br />
                            <div class="row">
                                <div class="panel panel-info">
                                    <div class="panel-heading">
                                        <asp:Label Text="Reason for request" runat="server" />
                                    </div>
                                    <div class="">
                                        <asp:TextBox ID="tbReason" runat="server" TextMode="MultiLine" MaxLength="4000" Height="100px" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <asp:Button ID="btnSubmit" Text="Submit request" runat="server" OnClick="btnSubmit_Click" CssClass="btn btn-primary" />
                                <br />
                                <asp:Button ID="btnCancelRequest" Text="Remove this request" runat="server" OnClick="btnCancelRequest_Click" CssClass="btn btn-danger" />
                                <br />
                                <asp:LinkButton ID="btnBack" runat="server" Text="Back"  CssClass="btn btn-primary" OnClientClick="JavaScript:window.history.back(1);return false;"/>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <script>
            function checkQty(event) {
                console.log(event)
                var tb = event.target;
                var isNum = /^\d+$/.test(tb.value);
                if (!isNum) {
                    tb.value = tb.max;
                }
            }
        </script>
    </div>
    <%--</form>--%>
</asp:Content>

<%--</body>
</html>--%>
