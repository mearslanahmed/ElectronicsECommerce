using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI.WebControls;

namespace ElectronicsEcommerce
{
    public partial class ShopCart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Redirect to Login page if the user is not logged in
            if (!IsPostBack)
            {
                if (Session["UserID"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
                // Load cart items on initial page load
                LoadCartItems();
            }
            else
            {
                // Prevent duplicate updates on refresh by disabling cache
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
            }
        }

        private void LoadCartItems()
        {
            // Query to fetch items in the shopping cart for the logged-in user
            string query = @"SELECT p.idP, p.labelP, p.priceP, sc.Quantity 
                            FROM ShopCart sc
                            INNER JOIN Product p ON sc.idP = p.idP
                            WHERE sc.idU = @userId";

            using (SqlConnection conn = new SqlConnection(DBHelper.connString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userId", Session["UserID"]);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Bind the fetched data to the Repeater control
                rptCartItems.DataSource = dt;
                rptCartItems.DataBind();

                // Calculate the grand total of the cart
                decimal grandTotal = 0;
                foreach (DataRow row in dt.Rows)
                {
                    grandTotal += Convert.ToDecimal(row["priceP"]) * Convert.ToInt32(row["Quantity"]);
                }
                litGrandTotal.Text = grandTotal.ToString("N2");
            }
        }

        protected void rptCartItems_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            // Get the product ID from the command argument
            int productId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "UpdateItem")
            {
                // Update item quantity in the cart
                TextBox txtQuantity = (TextBox)e.Item.FindControl("txtQuantity");
                UpdateCartItem(productId, Convert.ToInt32(txtQuantity.Text));
            }
            else if (e.CommandName == "RemoveItem")
            {
                // Remove item from the cart
                RemoveCartItem(productId);
            }

            // Reload cart items to reflect changes
            LoadCartItems();
        }

        private void UpdateCartItem(int productId, int quantity)
        {
            // Query to update the quantity of an item in the cart
            string query = @"UPDATE ShopCart 
                            SET Quantity = @quantity 
                            WHERE idU = @userId AND idP = @productId";

            using (SqlConnection conn = new SqlConnection(DBHelper.connString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@quantity", quantity);
                cmd.Parameters.AddWithValue("@userId", Session["UserID"]);
                cmd.Parameters.AddWithValue("@productId", productId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private void RemoveCartItem(int productId)
        {
            // Query to remove an item from the cart
            string query = "DELETE FROM ShopCart WHERE idU = @userId AND idP = @productId";

            using (SqlConnection conn = new SqlConnection(DBHelper.connString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userId", Session["UserID"]);
                cmd.Parameters.AddWithValue("@productId", productId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        protected void btnCheckout_Click(object sender, EventArgs e)
        {
            // Redirect to the Checkout page
            Response.Redirect("Checkout.aspx");
        }
    }
}