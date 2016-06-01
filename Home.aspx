<%@ Page Title="" Language="C#" MasterPageFile="~/ClientMaster.master" AutoEventWireup="true"
    CodeFile="Home.aspx.cs" Inherits="Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table style="width: 100%;">
        <tr>
            <td>
                <asp:Label ID="lblSearch" runat="server" Text="Search"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button1" runat="server" Text="Search" OnClick="Button1_Click1" />
                <asp:Label runat="server" ID="lblStatus"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label runat="server" ID="lblNoBook" ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand"
        OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowDataBound="GridView1_RowDataBound">
        <Columns>
            <asp:BoundField DataField="BookId" HeaderText="BookId" />
            <asp:BoundField DataField="BookTitle" HeaderText="BookTitle" />
            <asp:BoundField DataField="BookAuthor" HeaderText="BookAuthor" />
             <asp:BoundField DataField="AuthorName" HeaderText="AuthorName" />

            <asp:BoundField DataField="BookISBN" HeaderText="BookISBN" />
            <asp:TemplateField HeaderText="Check Out" ShowHeader="False">
                <EditItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update"
                        Text="Update"></asp:LinkButton>
                    &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel"
                        Text="Cancel"></asp:LinkButton>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="CheckOut"
                        CommandArgument='<%#Eval("BookId") %>' Text="CheckOut"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
