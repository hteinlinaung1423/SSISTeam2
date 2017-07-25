<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DelegateAuthority.aspx.cs" Inherits="SSISTeam2.Views.DepartmentHead.DelegateAuthority" 
         MasterPageFile="~/MasterPage.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <div>
        <asp:Panel ID="Panel1" runat="server" BackColor="#CCCCCC" CssClass="active">
            <asp:Label ID="lbDeptName" runat="server" Text="Label" Font-Size="XX-Large"></asp:Label>
        </asp:Panel> 

       <asp:Label ID="lbCheckDelegate" runat="server" Font-Size="X-Large"></asp:Label>
  
        <asp:Table ID="CurrentTable" runat="server" class="active" style="table-layout: auto; font-size: large;">
               
            <asp:TableRow>
                <asp:TableCell>Created Date </asp:TableCell>
                <asp:TableCell>: <asp:Label ID="lbCurDate" runat="server" Text="Label" ></asp:Label></asp:TableCell>
            </asp:TableRow>

             <asp:TableRow>
                <asp:TableCell>Current Delegate </asp:TableCell>
                <asp:TableCell>: <asp:Label ID="lbCurDelegate" runat="server" Text="-" ></asp:Label></asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>Reason</asp:TableCell>
                <asp:TableCell>: <asp:Label ID="lbCurReason" runat="server" Text="No"></asp:Label></asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>Start Date</asp:TableCell>
                <asp:TableCell>: <asp:Label ID="lbCurStart" runat="server" Text="No"></asp:Label></asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>End Date</asp:TableCell>
                <asp:TableCell>: <asp:Label ID="lbCurEnd" runat="server" Text="No" ></asp:Label></asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell><asp:Button runat="server" Text="Delete" ID="btnDelete" OnClick="btnDelete_Click"/></asp:TableCell>
            </asp:TableRow>

        </asp:Table>
    </div>

    <div>
  
             
        <asp:Table ID="ChooseNewTable" runat="server" class="active" style="table-layout: auto; font-size: large;">
               
            <asp:TableRow>
                <asp:TableCell>Date </asp:TableCell>
                <asp:TableCell>: <asp:Label ID="lbCurrentDate" runat="server" Text="Label" ></asp:Label></asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>Choose Delegate Employee</asp:TableCell>
                <asp:TableCell>: <asp:DropDownList ID="ddlEmployee" runat="server"></asp:DropDownList></asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>Reason</asp:TableCell>
                <asp:TableCell>: <asp:TextBox ID="tbReason" runat="server" TextMode="MultiLine"></asp:TextBox></asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>Start Date</asp:TableCell>
                <asp:TableCell>: <asp:TextBox ID="tbStartDate" runat="server" TextMode="Date"></asp:TextBox></asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>End Date</asp:TableCell>
                <asp:TableCell>: <asp:TextBox ID="tbEndDate" runat="server" TextMode="Date"></asp:TextBox></asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell> <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" /></asp:TableCell>
                <asp:TableCell> <asp:Button ID="btnCancel" runat="server" Text="Cancel" /></asp:TableCell>
            </asp:TableRow>
        </asp:Table>

        <asp:Label ID="lbDateError" runat="server" ForeColor="#FF3300"></asp:Label>
    </div>
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
 </asp:Content>
