<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PaymentCancel.aspx.cs" Inherits="ElectronicsEcommerce.PaymentCancel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5 text-center">
        <!-- Alert for canceled payment -->
        <div class="alert alert-danger">
            <h2><i class="fas fa-times-circle"></i>Payment Canceled</h2>
        </div>
        <!-- Redirect back to the checkout page -->
        <a href="Checkout.aspx" class="btn btn-primary">Try Again</a>
    </div>
</asp:Content>
