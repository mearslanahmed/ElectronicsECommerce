<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeFile="CreateAccount.aspx.cs"
    Inherits="ElectronicsEcommerce.CreateAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Head Content Placeholder (if needed) -->
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <h2>Create Account</h2>

        <!-- Username Input -->
        <div class="form-group">
            <label>Username:</label>
            <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" required></asp:TextBox>
        </div>

        <!-- Password Input -->
        <div class="form-group">
            <label>Password:</label>
            <asp:TextBox ID="txtPassword" runat="server"
                TextMode="Password" CssClass="form-control" required></asp:TextBox>

            <!-- Validator: 4–16 chars, at least 1 uppercase, 1 lowercase, 1 special -->
            <asp:RegularExpressionValidator
                ID="revPassword" runat="server"
                ControlToValidate="txtPassword"
                ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\W).{4,16}$"
                ErrorMessage="Password must  be 4–16 chars, include uppercase, lowercase & special"
                CssClass="text-danger"
                Display="Dynamic" />
        </div>


        <!-- Email Input (Optional) -->
        <div class="form-group">
            <label>Email (optional):</label>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
        </div>

        <!-- Register Button -->
        <asp:Button ID="btnRegister" runat="server" Text="Register"
            CssClass="btn btn-primary" OnClick="btnRegister_Click" />

        <!-- Link to Login Page -->
        <p class="mt-3">
            <a href="Login.aspx">Already have an account? Login here</a>
        </p>

        <!-- Display Error/Success Messages -->
        <asp:Label ID="lblMessage" runat="server" CssClass="text-danger"></asp:Label>
    </div>
</asp:Content>
