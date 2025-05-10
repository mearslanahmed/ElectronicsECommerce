using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace ElectronicsEcommerce
{
    public static class DBHelper
    {
        // Connection string to connect to the database
        public static readonly string connString = WebConfigurationManager.ConnectionStrings["ECommerceDB"].ConnectionString;

        // PRODUCT METHOD
        // Retrieves all products from the database
        public static DataTable GetProducts()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "SELECT idP, labelP, priceP, photoPath FROM Product";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader(); // Execute the query and read the results
                    dt.Load(reader); // Load the data into the DataTable
                }
            }
            return dt;
        }

        // CART METHOD
        // Adds a product to the user's shopping cart
        public static void AddToCart(int userId, int productId, int quantity = 1)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("ManageCart", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure; // Use stored procedure
                    cmd.Parameters.AddWithValue("@idU", userId); // Add user ID parameter
                    cmd.Parameters.AddWithValue("@idP", productId); // Add product ID parameter
                    cmd.Parameters.AddWithValue("@Quantity", quantity); // Add quantity parameter
                    cmd.ExecuteNonQuery(); // Execute the stored procedure
                }
            }
        }

        // Retrieves all items in the user's shopping cart
        public static DataTable GetCartItems(int userId)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = @"SELECT p.idP, p.labelP, p.priceP, sc.Quantity 
                                FROM ShopCart sc
                                INNER JOIN Product p ON sc.idP = p.idP
                                WHERE sc.idU = @idU";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idU", userId); // Add user ID parameter
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader(); // Execute the query and read the results
                    dt.Load(reader); // Load the data into the DataTable
                }
            }
            return dt;
        }

        // ORDER METHOD
        // Places an order for the user and returns the order ID
        public static int PlaceOrder(int userId, string fullName, string address, string city, string postalCode)
        {
            string shippingAddress = $"{fullName}, {address}, {city}, {postalCode}";

            using (SqlConnection conn = new SqlConnection(connString))
            using (SqlCommand cmd = new SqlCommand("PlaceOrder", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure; // Use stored procedure
                cmd.Parameters.AddWithValue("@idU", userId); // Add user ID parameter
                cmd.Parameters.AddWithValue("@ShippingAddress", shippingAddress); // Add shipping address parameter
                cmd.Parameters.Add("@idO", SqlDbType.Int).Direction = ParameterDirection.Output; // Output parameter for order ID

                conn.Open();
                cmd.ExecuteNonQuery(); // Execute the stored procedure
                return Convert.ToInt32(cmd.Parameters["@idO"].Value); // Return the order ID
            }
        }

        // Processes the payment and finalizes the order
        public static void ProcessOrder(string transactionId, int userId, decimal total, string shippingAddress)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            using (SqlCommand cmd = new SqlCommand("ProcessPayment", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure; // Use stored procedure
                cmd.Parameters.AddWithValue("@idU", userId); // Add user ID parameter
                cmd.Parameters.AddWithValue("@TotalPrice", total); // Add total price parameter
                cmd.Parameters.AddWithValue("@ShippingAddress", shippingAddress); // Add shipping address parameter
                cmd.Parameters.AddWithValue("@PayPalTransactionId", transactionId); // Add PayPal transaction ID parameter

                conn.Open();
                cmd.ExecuteNonQuery(); // Execute the stored procedure
            }
        }

        // ORDER DETAILS
        // Retrieves details of a specific order
        public static DataTable GetOrderDetails(int orderId)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = @"SELECT p.labelP, od.Quantity, od.Price 
                               FROM OrderDetails od
                               INNER JOIN Product p ON od.idP = p.idP
                               WHERE od.idO = @idO";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idO", orderId); // Add order ID parameter
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader(); // Execute the query and read the results
                    dt.Load(reader); // Load the data into the DataTable
                }
            }
            return dt;
        }

        // Clears the user's shopping cart
        public static void ClearCart(int userId)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "DELETE FROM ShopCart WHERE idU = @UserID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserID", userId); // Add user ID parameter
                conn.Open();
                cmd.ExecuteNonQuery(); // Execute the query
            }
        }

        // Retrieves details of a specific product by its ID
        public static DataTable GetProductById(int productId)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = @"SELECT idP, labelP, descriptionP, priceP, photoPath 
                               FROM Product 
                               WHERE idP = @productId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@productId", productId); // Add product ID parameter
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader(); // Execute the query and read the results
                dt.Load(reader); // Load the data into the DataTable
            }
            return dt;
        }
    }
}