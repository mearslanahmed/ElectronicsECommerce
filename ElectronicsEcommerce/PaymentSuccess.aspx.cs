using System;

namespace ElectronicsEcommerce
{
    public partial class PaymentSuccess : System.Web.UI.Page
    {
        // Event handler for page load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string orderId = Request.QueryString["orderId"];
                string txnId = Request.QueryString["txn"];

                if (!string.IsNullOrEmpty(orderId))
                {
                    litMessage.InnerText = $"Order ID: {orderId}";
                    litReceiptDetails.InnerHtml =
                        $"<strong>Order ID:</strong> {orderId}<br/>" +
                        $"<strong>Total:</strong> ${Session["CartTotal"] ?? "0.00"}";
                }
                else if (!string.IsNullOrEmpty(txnId))
                {
                    litMessage.InnerText = $"Transaction ID: {txnId}";
                    litReceiptDetails.InnerHtml =
                        $"<strong>Transaction ID:</strong> {txnId}<br/>" +
                        $"<strong>Total:</strong> ${Session["CartTotal"] ?? "0.00"}";
                }
                else
                {
                    Response.Redirect("Home.aspx");
                }
            }

        }
    }
}