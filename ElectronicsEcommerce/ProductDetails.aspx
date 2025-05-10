<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductDetails.aspx.cs" Inherits="ElectronicsEcommerce.ProductDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <!-- FormView to display product details -->
        <asp:FormView ID="productForm" runat="server" DataKeyNames="idP" ItemType="System.Data.DataRowView">
            <ItemTemplate>
                <div class="row">
                    <!-- Product Image -->
                    <div class="col-md-6">
                        <img src='<%# Eval("photoPath") %>'
                            class="img-fluid product-image"
                            alt='<%# Eval("labelP") %>' />
                    </div>

                    <!-- Product Details -->
                    <div class="col-md-6">
                        <!-- Product Name -->
                        <h1><%# Eval("labelP") %></h1>
                        <!-- Product Price -->
                        <h3 class="text-primary">$<%# Eval("priceP", "{0:N2}") %></h3>

                        <!-- Product Description -->
                        <div class="mt-4">
                            <h4>Product Details</h4>
                            <p><%# Eval("descriptionP") %></p>
                        </div>

                        <!-- Add to Cart Button -->
                        <div class="mt-4">
                            <asp:Button ID="btnAddToCart" runat="server"
                                Text="Add to Cart"
                                CssClass="btn btn-primary btn-lg"
                                OnClick="btnAddToCart_Click"
                                CommandArgument='<%# Eval("idP") %>' />
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:FormView>
    </div>
</asp:Content>
