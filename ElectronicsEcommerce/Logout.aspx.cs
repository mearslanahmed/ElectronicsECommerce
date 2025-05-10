using System;
using System.Web;
using System.Web.UI;

namespace ElectronicsEcommerce
{
    public partial class Logout : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Clear all session data
            Session.Clear();
            Session.Abandon();

            // Remove the session cookie
            if (Request.Cookies["ASP.NET_SessionId"] != null)
            {
                var cookie = new HttpCookie("ASP.NET_SessionId", "")
                {
                    Expires = DateTime.Now.AddDays(-1),
                    Path = "/"
                };
                Response.Cookies.Add(cookie);
            }

            // Redirect to Home.aspx
            Response.Redirect("~/Home.aspx");
        }
    }
}
