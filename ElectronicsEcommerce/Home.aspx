<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeFile="Home.aspx.cs" Inherits="ElectronicsEcommerce.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        /* CSS for product card hover effects and layout */
        .product-card {
            transition: transform 0.3s ease, box-shadow 0.3s ease;
            height: 100%;
        }
        .product-card:hover {
            transform: translateY(-5px);
            box-shadow: 0 8px 16px rgba(0,0,0,0.1);
        }
        .card-img-top {
            height: 250px;
            object-fit: contain;
            padding: 15px;
        }
        .card-title {
            font-size: 1.1rem;
            min-height: 3rem;
        }
        .price-tag {
            color: #2c3e50;
            font-weight: 600;
            font-size: 1.25rem;
            margin-bottom: 1rem;
        }
        .btn-group {
            display: flex;
            gap: 10px;
        }
    </style>

    <!-- Welcome Message -->
    <h1 class="display-4 mb-4">
        Welcome, <%: Session["Username"] ?? "Guest" %>!
    </h1>

    <!-- Product Listing Section -->
    <div class="row">
        <asp:Repeater ID="rptProducts" runat="server" OnItemCommand="rptProducts_ItemCommand">
            <ItemTemplate>
                <div class="col-md-4 mb-4">
                    <div class="card product-card">
                        <img src='<%# Eval("photoPath") %>' class="card-img-top" alt='<%# Eval("labelP") %>' />
                        <div class="card-body">
                            <h5 class="card-title"><%# Eval("labelP") %></h5>
                            <p class="price-tag">$<%# Eval("priceP", "{0:N2}") %></p>
                            <div class="btn-group">
                                <asp:Button
                                    ID="btnAddToCart"
                                    runat="server"
                                    Text="Add to Cart"
                                    CssClass="btn btn-primary me-2"
                                    CommandName="AddToCart"
                                    CommandArgument='<%# Eval("idP") %>' />
                                <asp:HyperLink
                                    ID="lnkDetails"
                                    runat="server"
                                    CssClass="btn btn-outline-secondary"
                                    NavigateUrl='<%# "ProductDetails.aspx?id=" + Eval("idP") %>'
                                    Text="View Details" />
                            </div>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
