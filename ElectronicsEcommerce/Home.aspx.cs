using System;
using System.Web.UI.WebControls;

namespace ElectronicsEcommerce
{
    public partial class Home : System.Web.UI.Page
    {
        // Event handler for page load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Redirect non‑logged‑in users
                if (Session["UserID"] == null)
                    Response.Redirect("Login.aspx");

                BindProducts();
            }
        }

        // Method to bind products to the Repeater control
        private void BindProducts()
        {
            try
            {
                // Fetch products from the database and bind them to the Repeater control
                rptProducts.DataSource = DBHelper.GetProducts();
                rptProducts.DataBind();
            }
            catch (Exception ex)
            {
                // Show an error message if fetching products fails
                ShowMessage($"Error loading products: {ex.Message}", isError: true);
            }
        }

        // Event handler for Repeater item commands (e.g., Add to Cart)
        protected void rptProducts_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            // Check if the command is "AddToCart"
            if (e.CommandName == "AddToCart")
            {
                HandleAddToCart(e);
            }
        }

        // Handles the logic for adding a product to the cart
        private void HandleAddToCart(RepeaterCommandEventArgs e)
        {
            // Redirect to the login page if the user is not logged in
            if (Session["UserID"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            try
            {
                // Retrieve the product ID from the command argument
                int productId = Convert.ToInt32(e.CommandArgument);

                // Retrieve the user ID from the session
                int userId = (int)Session["UserID"];

                // Add the product to the cart in the database
                DBHelper.AddToCart(userId, productId);

                // Show a success message
                ShowMessage("Item added to cart successfully!");
            }
            catch (Exception ex)
            {
                // Show an error message if adding to the cart fails
                ShowMessage($"Error adding to cart: {ex.Message}", isError: true);
            }
        }

        // Displays a message to the user using client-side JavaScript alerts
        private void ShowMessage(string message, bool isError = false)
        {
            // Prepare the JavaScript alert message
            string script = $@"alert('{message.Replace("'", @"\'")}');";

            // Log errors to the browser console
            if (isError) script = $"console.error('{message}');" + script;

            // Register the JavaScript snippet to execute on the client-side
            ClientScript.RegisterStartupScript(GetType(), "alert", script, true);
        }
    }
}