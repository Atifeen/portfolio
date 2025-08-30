using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Portfolio.Helpers;


namespace Portfolio.Admin
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Hide the message panel initially
            pnlMessage.Visible = false;

            // If admin is already logged in, redirect to dashboard
            if (Session["AdminUsername"] != null)
            {
                Response.Redirect("~/Admin/Dashboard.aspx");
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Basic validation
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ShowMessage("Please enter both username and password.", false);
                return;
            }

            try
            {
                // Use DBHelper's ValidateAdmin to check credentials
                bool isValid = DBHelper.ValidateAdmin(username, password);

                if (isValid)
                {
                    // Store session
                    Session["AdminUsername"] = username;

                    // Redirect to admin dashboard
                    Response.Redirect("~/Admin/Dashboard.aspx");
                }
                else
                {
                    ShowMessage("Invalid username or password.", false);
                }
            }
            catch (Exception ex)
            {
                // Show any unexpected errors
                ShowMessage("An error occurred: " + ex.Message, false);
            }
        }

        // Helper method to display messages
        private void ShowMessage(string message, bool isSuccess)
        {
            pnlMessage.Visible = true;
            lblMessage.Text = message;

            if (isSuccess)
            {
                lblMessage.CssClass = "success-message";
            }
            else
            {
                lblMessage.CssClass = "error-message";
            }
        }
    }
}
