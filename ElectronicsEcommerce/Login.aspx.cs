using System;
using System.Data.SqlClient;

namespace ElectronicsEcommerce
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Redirect to Home page if the user is already logged in
            if (Session["UserID"] != null)
            {
                Response.Redirect("Home.aspx");
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                lblMessage.Text = "Please enter username and password";
                return;
            }

            try
            {
                string query = "SELECT idU, uPass FROM [User] WHERE uName = @username";

                using (SqlConnection conn = new SqlConnection(DBHelper.connString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            string storedHash = reader["uPass"].ToString();
                            string inputHash = SecurityHelper.HashPassword(password);

                            if (storedHash == inputHash)
                            {
                                int userId = Convert.ToInt32(reader["idU"]);
                                Session["UserID"] = userId;
                                Session["Username"] = username;      // ← Store username in session
                                Response.Redirect("Home.aspx");
                            }
                            else
                            {
                                lblMessage.Text = "Invalid credentials";
                            }
                        }
                        else
                        {
                            lblMessage.Text = "User not found";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
            }
        }
    }
}
