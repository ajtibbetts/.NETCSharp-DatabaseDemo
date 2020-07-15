<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CapstoneWebApp.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Login ID="LoginForm" runat="server" OnAuthenticate="ValidateUser" OnLoggedIn="LoginComplete">
    </asp:Login>
</asp:Content>
