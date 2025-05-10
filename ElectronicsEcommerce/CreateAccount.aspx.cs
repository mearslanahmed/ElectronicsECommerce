using System;
using System.Data.SqlClient;

namespace ElectronicsEcommerce
{
    public partial class CreateAccount : System.Web.UI.Page
    {
        // Event handler for page load
        protected void Page_Load(object sender, EventArgs e)
        {
            // This method can remain empty unless you need initialization logic
        }

        // Event handler for the "Register" button click
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            // Retrieve user inputs
            string username = txtUsername.Text.Trim();
            string password = SecurityHelper.HashPassword(txtPassword.Text); // Hash the password for security
            string email = txtEmail.Text.Trim();

            // Check if the username is already taken
            if (IsUsernameTaken(username))
            {
                lblMessage.Text = "Username already exists!";
                return;
            }

            // SQL query to insert a new user into the database
            string query = @"INSERT INTO [User] (uName, uPass, Email) 
                             VALUES (@username, @password, @email)";

            try
            {
                // Create a connection to the database
                using (SqlConnection conn = new SqlConnection(DBHelper.connString))
                {
                    // Create and configure the SQL command
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.Parameters.AddWithValue("@email", string.IsNullOrEmpty(email) ? DBNull.Value : (object)email); // Handle optional email

                    // Open the database connection and execute the query
                    conn.Open();
                    cmd.ExecuteNonQuery();

                    // Display success message and redirect to the login page
                    lblMessage.Text = "Registration successful!";
                    Response.Redirect("Login.aspx");
                }
            }
            catch (Exception ex)
            {
                // Display an error message if the query fails
                lblMessage.Text = "Error: " + ex.Message;
            }
        }

        // Method to check if the username is already taken
        private bool IsUsernameTaken(string username)
        {
            // SQL query to check for existing usernames
            string query = "SELECT COUNT(*) FROM [User] WHERE uName = @username";
            using (SqlConnection conn = new SqlConnection(DBHelper.connString))
            {
                // Create and configure the SQL command
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);

                // Open the database connection and execute the query
                conn.Open();
                int count = (int)cmd.ExecuteScalar();

                // Return true if the username exists, otherwise false
                return count > 0;
            }
        }
    }
}