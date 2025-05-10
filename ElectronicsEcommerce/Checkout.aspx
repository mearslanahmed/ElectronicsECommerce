<%@ Page Title="Checkout" Language="C#" MasterPageFile="~/Site.Master" 
    AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" 
    Inherits="ElectronicsEcommerce.Checkout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <h2 class="mb-4">Checkout</h2>
        
        <!-- Shipping Information -->
        <div class="card mb-4 shadow">
            <div class="card-header bg-primary text-white">
                <h4 class="mb-0"><i class="fas fa-shipping-fast"></i> Shipping Details</h4>
            </div>
            <div class="card-body">
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label>Full Name:</label>
                            <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" required></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>Address:</label>
                            <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" required></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label>City:</label>
                            <asp:TextBox ID="txtCity" runat="server" CssClass="form-control" required></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>Postal Code:</label>
                            <asp:TextBox ID="txtPostalCode" runat="server" CssClass="form-control"
                                required minlength="4" MaxLength="10"
                                oninput="this.value=this.value.replace(/\D/g,'')"
                                placeholder="Enter postal code (4 to 10 characters)"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Payment Method -->
                <!-- Payment Method -->
                <!-- Payment Method -->
        <div class="card mb-4 shadow">
            <div class="card-header bg-info text-white">
                <h4 class="mb-0"><i class="fas fa-credit-card"></i> Payment Method</h4>
            </div>
            <div class="card-body">
                <div class="form-group">
                    <div class="form-check">
                        <asp:RadioButton ID="rbCash" runat="server" GroupName="PaymentMethod"
                            CssClass="form-check-input" Checked="true"
                            AutoPostBack="false" onclick="toggleCardFields()" />
                        <label class="form-check-label" for="<%= rbCash.ClientID %>">
                            Cash on Delivery
                        </label>
                    </div>
                    <div class="form-check">
                        <asp:RadioButton ID="rbCredit" runat="server" GroupName="PaymentMethod"
                            CssClass="form-check-input"
                            AutoPostBack="false" onclick="toggleCardFields()" />
                        <label class="form-check-label" for="<%= rbCredit.ClientID %>">
                            Credit/Debit Card
                        </label>
                    </div>
                </div>

                <div id="cardDetails" style="display:none; margin-top:1rem;">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Card Number</label>
                                <asp:TextBox ID="txtCardNumber" runat="server"
                                    CssClass="form-control" placeholder="4111 1111 1111 1111"
                                    MaxLength="16"
                                    oninput="this.value=this.value.replace(/\D/g,'')"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Name on Card</label>
                                <asp:TextBox ID="txtCardName" runat="server"
                                    CssClass="form-control" placeholder="John Doe"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Expiry Date</label>
                                <asp:TextBox ID="txtExpiry" runat="server"
                                    CssClass="form-control" placeholder="MM/YY" MaxLength="5"
                                    onkeyup="formatExpiry(this)"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>CVV</label>
                                <asp:TextBox ID="txtCVV" runat="server"
                                    CssClass="form-control" placeholder="123" MaxLength="3"
                                    oninput="this.value=this.value.replace(/\D/g,'')"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>


        <!-- Order Summary -->
        <div class="card shadow">
            <div class="card-header bg-info text-white">
                <h4 class="mb-0"><i class="fas fa-receipt"></i> Order Summary</h4>
            </div>
            <div class="card-body">
                <asp:Repeater ID="rptOrderSummary" runat="server">
                    <HeaderTemplate>
                        <div class="row font-weight-bold mb-2">
                            <div class="col-6">Product</div>
                            <div class="col-3">Quantity</div>
                            <div class="col-3 text-right">Total</div>
                        </div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class="row mb-3">
                            <div class="col-6"><%# Eval("labelP") %></div>
                            <div class="col-3">Qty: <%# Eval("Quantity") %></div>
                            <div class="col-3 text-right">$<%# string.Format("{0:N2}", Convert.ToDecimal(Eval("priceP")) * Convert.ToInt32(Eval("Quantity"))) %></div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                
                <hr />
                <h4 class="text-right">Grand Total: $<asp:Literal ID="litGrandTotal" runat="server" /></h4>
                
                <div class="text-right mt-4">
                    <asp:Button ID="btnPlaceOrder" runat="server" 
                        Text="Place Order Now" 
                        CssClass="btn btn-success btn-lg mr-3" 
                        OnClick="btnPlaceOrder_Click" />
                    
                    <button type="button" class="btn btn-primary btn-lg" 
                        style="background: #003087; color: white;" 
                        onclick="processDummyPayment()">
                        <i class="fab fa-paypal"></i> PayPal Checkout
                    </button>
                </div>
                <div id="paymentStatus" class="mt-3"></div>
            </div>
        </div>
    </div>

    <script>
        function toggleCardFields() {
            var show = document.getElementById('<%= rbCredit.ClientID %>').checked;
            document.getElementById('cardDetails').style.display = show ? 'block' : 'none';
        }

        function formatExpiry(input) {
            var v = input.value.replace(/\D/g, '');
            if (v.length > 2) v = v.slice(0, 2) + '/' + v.slice(2, 4);
            input.value = v;
        }

        // ensure correct initial state
        document.addEventListener('DOMContentLoaded', toggleCardFields);

        function processDummyPayment() {
            const statusDiv = document.getElementById('paymentStatus');
            statusDiv.innerHTML = '';

            if (!validateForm()) {
                statusDiv.className = 'alert alert-danger';
                statusDiv.innerHTML = 'Please fill all required fields';
                return;
            }

            statusDiv.className = 'alert alert-info';
            statusDiv.innerHTML = '<i class="fas fa-spinner fa-spin"></i> Processing payment...';

            setTimeout(() => {
                const transactionId = 'DEMO_' + Date.now();
                const amount = document.getElementById('<%= litGrandTotal.ClientID %>').innerText.replace('$', '');

                fetch('Checkout.aspx/CompleteDummyPayment', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                    },
                    body: JSON.stringify({
                        transactionId: transactionId,
                        amount: amount,
                        fullName: document.getElementById('<%= txtFullName.ClientID %>').value,
                        address: document.getElementById('<%= txtAddress.ClientID %>').value,
                        city: document.getElementById('<%= txtCity.ClientID %>').value,
                        postalCode: document.getElementById('<%= txtPostalCode.ClientID %>').value
                    })
                })
                .then(response => response.json())
                .then(data => {
                    if (data.d.success) {
                        window.location.href = 'PaymentSuccess.aspx?txn=' + transactionId;
                    } else {
                        statusDiv.className = 'alert alert-danger';
                        statusDiv.innerHTML = 'Payment failed: ' + data.d.message;
                    }
                })
                .catch(error => {
                    statusDiv.className = 'alert alert-danger';
                    statusDiv.innerHTML = 'Error: ' + error.message;
                });
            }, 1500);
        }

        function validateForm() {
            const requiredFields = [
                '<%= txtFullName.ClientID %>',
                '<%= txtAddress.ClientID %>',
                '<%= txtCity.ClientID %>',
                '<%= txtPostalCode.ClientID %>'
            ];

            return requiredFields.every(fieldId => {
                const field = document.getElementById(fieldId);
                return field && field.value.trim();
            });
        }
    </script>
</asp:Content>