<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ElectronicsEcommerce.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /* Styling for the login container */
        .login-container {
            max-width: 400px;
            margin: auto;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <!-- Login Header -->
        <h2>Login</h2>

        <!-- Username Input -->
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="txtUsername">Username</asp:Label>
            <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control"></asp:TextBox>
        </div>

        <!-- Password Input -->
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="txtPassword">Password</asp:Label>
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
        </div>

        <!-- Login Button -->
        <asp:Button ID="btnLogin" runat="server" Text="Login"
            CssClass="btn btn-primary" OnClick="btnLogin_Click" />

        <!-- Message Label for Errors or Notifications -->
        <asp:Label ID="lblMessage" runat="server" CssClass="text-danger mt-2"></asp:Label>

        <!-- Link to Registration Page -->
        <p class="mt-3">
            <a href="CreateAccount.aspx">Don't have an account? Register here</a>
        </p>
    </div>
</asp:Content>
