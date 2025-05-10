using System;
using System.Web.UI;

public partial class Site : MasterPage
{
    // Empty code-behind (required for compilation)
    protected void Page_Load(object sender, EventArgs e)
    {
     
    }

    protected void btnLogout_Click(object sender, EventArgs e)
    {
        // Clear all session values
        Session.Clear();
        Session.Abandon();

        // Redirect to home page
        Response.Redirect("~/Home.aspx");
    }
}