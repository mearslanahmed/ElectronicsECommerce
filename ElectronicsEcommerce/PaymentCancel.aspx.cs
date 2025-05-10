using System;

namespace ElectronicsEcommerce
{
    public partial class PaymentCancel : System.Web.UI.Page
    {
        // Event handler for page load
        protected void Page_Load(object sender, EventArgs e)
        {
            // Redirect to the Login page if the user is not logged in
            if (Session["UserID"] == null)
            {
                Response.Redirect("Login.aspx");
            }
        }
    }
}