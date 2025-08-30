<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Portfolio.Admin.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin Login - Portfolio</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link href="../Styles/admin-login.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-container">
            
            <!-- Header -->
            <div class="login-header">
                <h2>Admin Panel</h2>
                <p>Please login to access your portfolio dashboard</p>
            </div>

            <!-- Error / Success Message Panel -->
            <asp:Panel ID="pnlMessage" runat="server" Visible="false">
                <asp:Label ID="lblMessage" runat="server" CssClass="error-message"></asp:Label>
            </asp:Panel>

            <!-- Username -->
            <div class="form-group">
                <label for="txtUsername">Username</label>
                <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" 
                    placeholder="Enter your username" required="true"></asp:TextBox>
            </div>

            <!-- Password -->
            <div class="form-group">
                <label for="txtPassword">Password</label>
                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" 
                    TextMode="Password" placeholder="Enter your password" required="true"></asp:TextBox>
            </div>

            <!-- Login Button -->
            <div class="form-group">
                <asp:Button ID="btnLogin" runat="server" CssClass="btn-login" Text="Login" OnClick="btnLogin_Click" />
            </div>

            <!-- Footer -->
            <div class="footer">
                <p>&copy; 2025 Portfolio Admin Panel</p>
            </div>
        </div>
    </form>
</body>
</html>