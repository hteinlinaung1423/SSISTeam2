<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeBehind="PurchaseOrderSuccess.aspx.cs" Inherits="SSISTeam2.Views.StoreClerk.PurchaseOrderSuccess" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">
    <div class="alert alert-success" role="alert">
        Purchase  <asp:HyperLink NavigateUrl="~/views/Storeclerk/ViewPendingOrder.aspx" runat="server" ForeColor="black"> Orders</asp:HyperLink> has been created Successfully

You will receive a notification through email once the Purchase Order has been successfully sent

        
    </div>
</asp:Content>
