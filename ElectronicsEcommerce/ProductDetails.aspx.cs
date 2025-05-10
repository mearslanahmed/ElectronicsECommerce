using System;
using System.Data;
using System.Web.UI.WebControls;

namespace ElectronicsEcommerce
{
    public partial class ProductDetails : System.Web.UI.Page
    {
        // Event handler for the page load event
        protected void Page_Load(object sender, EventArgs e)
        {
            // Ensure the logic executes only on the initial page load
            if (!IsPostBack)
            {
                // Check if the product ID is provided in the query string and is valid
                if (Request.QueryString["id"] != null && int.TryParse(Request.QueryString["id"], out int productId))
                {
                    DataTable product = DBHelper.GetProductById(productId); // Fetch the product details

                    // If the product exists, bind it to the FormView
                    if (product.Rows.Count > 0)
                    {
                        productForm.DataSource = product.DefaultView;
                        productForm.DataBind();
                    }
                    else
                    {
                        // Redirect to the Home page if the product doesn't exist
                        Response.Redirect("Home.aspx");
                    }
                }
                else
                {
                    // Redirect to the Home page if the product ID is invalid
                    Response.Redirect("Home.aspx");
                }
            }
        }

        // Event handler for the Add to Cart button click
        protected void btnAddToCart_Click(object sender, EventArgs e)
        {
            // Redirect to the Login page if the user is not logged in
            if (Session["UserID"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            try
            {
                // Retrieve the product ID from the button's CommandArgument
                Button btn = (Button)sender;
                int productId = Convert.ToInt32(btn.CommandArgument);

                // Retrieve the user ID from the session
                int userId = (int)Session["UserID"];

                // Add the product to the cart in the database
                DBHelper.AddToCart(userId, productId);

                // Display a success message and redirect to the shopping cart
                ClientScript.RegisterStartupScript(GetType(), "alert",
                    "alert('Added to cart!'); window.location.href = 'ShopCart.aspx?r=' + Math.random();", true);
            }
            catch (Exception ex)
            {
                // Display an error message in case of an exception
                ClientScript.RegisterStartupScript(GetType(), "error",
                    $"alert('Error: {ex.Message}');", true);
            }
        }
    }
}