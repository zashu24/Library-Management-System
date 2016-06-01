<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Login.master" CodeFile="frmUserMaster.aspx.cs" Inherits="ADMIN_frmUserMaster" %>

<%--<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <table style="width: 100%;">
            <tr>
                <td>
                    <asp:Label ID="lblUserName" runat="server" Text="UserName"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblPassword" runat="server" Text="Password"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox>

                </td>
                <td>&nbsp;</td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lblUserAdmin" runat="server" Text="IsUserAdmin"></asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="chkIsUserAdmin" runat="server" />

                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblAddress" runat="server" Text="Address"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblBranchId" runat="server" Text="Branch"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="drpBranch" runat="server" AutoPostBack="true">
                    </asp:DropDownList>


                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="Save" OnClick="Button1_Click1" />
                    <asp:Label runat="server" ID="lblStatus"></asp:Label>
                </td>
                <td>&nbsp;</td>
            </tr>
        </table>

    </div>
</asp:Content>
<%--   </form>
</body>
</html>--%>
