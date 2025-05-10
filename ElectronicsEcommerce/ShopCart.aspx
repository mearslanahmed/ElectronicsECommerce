<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ShopCart.aspx.cs" Inherits="ElectronicsEcommerce.ShopCart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <!-- Shopping Cart Header -->
        <h2>Shopping Cart</h2>

        <!-- Cart Table Wrapper -->
        <div id="cartContainer">
            <!-- Repeater for Cart Items -->
            <asp:Repeater ID="rptCartItems" runat="server" OnItemCommand="rptCartItems_ItemCommand">
                <HeaderTemplate>
                    <div class="table-responsive">
                        <table class="table table-bordered">
                            <thead class="thead-light">
                                <tr>
                                    <th>Product</th>
                                    <th>Price</th>
                                    <th>Quantity</th>
                                    <th>Total</th>
                                    <th>Action</th>
                                </tr>
                            </thead>
                            <tbody>
                </HeaderTemplate>

                <ItemTemplate>
                    <tr>
                        <!-- Product Name -->
                        <td><%# Eval("labelP") %></td>
                        <!-- Product Price -->
                        <td>$<%# Eval("priceP", "{0:N2}") %></td>
                        <!-- Product Quantity -->
                        <td>
                            <asp:TextBox ID="txtQuantity" runat="server"
                                Text='<%# Eval("Quantity") %>'
                                TextMode="Number" min="1" CssClass="form-control" Style="width: 70px;"
                                onchange="onQtyChange(this)" />
                        </td>
                        <!-- Total Price for the Product -->
                        <td>$<%# Convert.ToDecimal(Eval("priceP")) * Convert.ToInt32(Eval("Quantity")) %></td>
                        <!-- Update and Remove Actions -->
                        <td>
                            <asp:LinkButton runat="server" CommandName="RemoveItem"
                                CommandArgument='<%# Eval("idP") %>' CssClass="btn btn-sm btn-danger">
        <i class="fas fa-trash-alt"></i>
                            </asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>

                <FooterTemplate>
                    </tbody>
                        </table>
                    </div>
                </FooterTemplate>
            </asp:Repeater>
        </div>

        <!-- Grand Total and Checkout Section -->
        <div class="text-right">
            <h4 id="grandTotalContainer">Grand Total: $<asp:Literal ID="litGrandTotal" runat="server" /></h4>
            <asp:Button ID="btnCheckout" runat="server" Text="Proceed to Checkout"
                CssClass="btn btn-success btn-lg" OnClick="btnCheckout_Click" />
        </div>
    </div>

    <script>
        function onQtyChange(input) {
            var qty = parseInt(input.value, 10) || 0;
            if (qty < 1) { qty = 1; input.value = qty; }
            if (qty > 1000) {
                alert("Quantity cannot exceed 1,000. Resetting to 1,000.");
                qty = 1000; input.value = qty;
            }

            var row = input.closest("tr");
            var priceText = row.querySelector("td:nth-child(2)").innerText;
            var price = parseFloat(priceText.replace("$", "")) || 0;
            var rowTotal = price * qty;
            row.querySelector("td:nth-child(4)").innerText = "$" + rowTotal.toFixed(2);

            updateGrandTotal();
        }

        function updateGrandTotal() {
            var rows = document.querySelectorAll("#cartContainer table tbody tr");
            var total = 0;
            rows.forEach(function (r) {
                var cell = r.querySelector("td:nth-child(4)");
                var val = parseFloat(cell.innerText.replace("$", "")) || 0;
                total += val;
            });

            var grandTotalEl = document.getElementById("grandTotalContainer");
            if (grandTotalEl) {
                grandTotalEl.innerText = "Grand Total: $" + total.toFixed(2);
            }
        }

        document.addEventListener("DOMContentLoaded", updateGrandTotal);
    </script>
</asp:Content>
