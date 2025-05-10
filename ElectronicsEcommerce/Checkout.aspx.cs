using System;
using System.Data;
using System.Web;
using System.Web.Services;

namespace ElectronicsEcommerce
{
    public partial class Checkout : System.Web.UI.Page
    {
        // Page load event handler
        protected void Page_Load(object sender, EventArgs e)
        {
            // Redirect to login page if the user is not logged in
            if (Session["UserID"] == null) Response.Redirect("Login.aspx");

            if (!IsPostBack)
            {
                // Load the order summary and calculate the total cart value
                LoadOrderSummary();
                Session["CartTotal"] = GetGrandTotal();
            }
        }

        // Loads the order summary by fetching cart items from the database
        private void LoadOrderSummary()
        {
            // Retrieve the cart items for the logged-in user
            DataTable dt = DBHelper.GetCartItems((int)Session["UserID"]);

            // Add a computed column for the total price of each item (price * quantity)
            dt.Columns.Add("Total", typeof(decimal), "priceP * Quantity");

            // Bind the cart items to the repeater control
            rptOrderSummary.DataSource = dt;
            rptOrderSummary.DataBind();

            // Display the grand total of the cart
            litGrandTotal.Text = GetGrandTotal().ToString("N2");
        }

        // Calculates the grand total of the cart
        public decimal GetGrandTotal()
        {
            decimal total = 0;

            // Retrieve the cart items for the logged-in user
            DataTable dt = DBHelper.GetCartItems((int)Session["UserID"]);

            // Calculate the total by summing up the price * quantity for each item
            foreach (DataRow row in dt.Rows)
            {
                total += Convert.ToDecimal(row["priceP"]) * Convert.ToInt32(row["Quantity"]);
            }

            return total;
        }

        // Event handler for the "Place Order Now" button click
        protected void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
                Response.Redirect("Login.aspx");

            //  basic shipping validation
            if (string.IsNullOrWhiteSpace(txtFullName.Text) ||
                string.IsNullOrWhiteSpace(txtAddress.Text) ||
                string.IsNullOrWhiteSpace(txtCity.Text) ||
                string.IsNullOrWhiteSpace(txtPostalCode.Text))
            {
                ClientScript.RegisterStartupScript(GetType(), "alert",
                    "alert('Please fill all required shipping fields!');", true);
                return;
            }

            // If card selected, just pull the dummy values
            if (rbCredit.Checked)
            {
                string dummyCardNum = txtCardNumber.Text.Trim();
                string dummyCardName = txtCardName.Text.Trim();
                string dummyExpiry = txtExpiry.Text.Trim();
                string dummyCvv = txtCVV.Text.Trim();
                // …and do nothing else with them
            }

            try
            {
                int userId = (int)Session["UserID"];
                int orderId = DBHelper.PlaceOrder(
                    userId,
                    txtFullName.Text.Trim(),
                    txtAddress.Text.Trim(),
                    txtCity.Text.Trim(),
                    txtPostalCode.Text.Trim()
                );

                DBHelper.ClearCart(userId);
                Session["CartCount"] = 0;

                ClientScript.RegisterStartupScript(GetType(), "success",
                    $"alert('Order #{orderId} placed successfully!'); window.location='PaymentSuccess.aspx?orderId={orderId}';",
                    true);
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "error",
                    $"alert('Order failed: {ex.Message}');", true);
            }
        }

        // Validates the shipping form fields to ensure they are not empty
        private bool ValidateForm()
        {
            return !string.IsNullOrWhiteSpace(txtFullName.Text) &&
                   !string.IsNullOrWhiteSpace(txtAddress.Text) &&
                   !string.IsNullOrWhiteSpace(txtCity.Text) &&
                   !string.IsNullOrWhiteSpace(txtPostalCode.Text);
        }

        // Web method to handle dummy payment processing
        [WebMethod(EnableSession = true)]
        public static PaymentResult CompleteDummyPayment(
            string transactionId,
            string amount,
            string fullName,
            string address,
            string city,
            string postalCode)
        {
            try
            {
                // Ensure the user session is active
                if (HttpContext.Current.Session["UserID"] == null)
                    return new PaymentResult(false, "Session expired");

                // Retrieve user ID from session and parse the payment amount
                int userId = (int)HttpContext.Current.Session["UserID"];
                decimal total = Convert.ToDecimal(amount);

                // Construct the shipping address
                string shippingAddress = $"{fullName}, {address}, {city}, {postalCode}";

                // Process the order in the database
                DBHelper.ProcessOrder(
                    transactionId,
                    userId,
                    total,
                    shippingAddress
                );

                // Return a success result
                return new PaymentResult(true, "");
            }
            catch (Exception ex)
            {
                // Return a failure result with the error message
                return new PaymentResult(false, ex.Message);
            }
        }

        // Class to represent the result of a payment operation
        public class PaymentResult
        {
            public bool success { get; set; }
            public string message { get; set; }

            // Constructor to initialize the result
            public PaymentResult(bool success, string message)
            {
                this.success = success;
                this.message = message;
            }
        }
    }
}