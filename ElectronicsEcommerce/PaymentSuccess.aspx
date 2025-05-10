<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="PaymentSuccess.aspx.cs"
    Inherits="ElectronicsEcommerce.PaymentSuccess" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  <div class="container mt-5 text-center">

    <!-- Success Alert -->
    <div class="alert alert-success">
      <h2><i class="fas fa-check-circle"></i> Order Confirmed!</h2>
      <p class="mt-3" runat="server" id="litMessage"></p>
      <p>Your order has been successfully processed.</p>
    </div>

    <!-- Receipt Container -->
    <div id="receipt" class="border p-4 mb-4 mx-auto" style="max-width:400px; text-align:left;">
      <h3 class="text-center mb-3"><i class="fas fa-receipt"></i> Receipt</h3>
      <p><strong>Date:</strong> <%= DateTime.Now.ToString("MMMM dd, yyyy HH:mm") %></p>
      <p runat="server" id="litReceiptDetails"></p>
      <!-- If you want, you can add placeholders for items, totals, etc. here -->
    </div>

    <!-- Buttons -->
    <div class="mt-4">
      <button class="btn btn-secondary btn-lg mr-3" onclick="printReceipt()">
        <i class="fas fa-print"></i> Print Receipt
      </button>
      <a href="Home.aspx" class="btn btn-primary btn-lg">
        <i class="fas fa-shopping-bag"></i> Continue Shopping
      </a>
    </div>
  </div>

  <script>
      function printReceipt() {
          // Temporarily show only the receipt for printing
          var originalBody = document.body.innerHTML;
          var receiptHtml = document.getElementById('receipt').outerHTML;
          document.body.innerHTML = receiptHtml;
          window.print();
          location.reload(); // reload the page to restore full view
      }
  </script>
</asp:Content>
