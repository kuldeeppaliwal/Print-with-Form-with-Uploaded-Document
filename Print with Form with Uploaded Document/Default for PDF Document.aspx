<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default for PDF Document.aspx.cs" Inherits="Default_for_PDF_Document" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <title>Registration Form</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            padding: 20px;
        }
        .form-group {
            margin-bottom: 15px;
        }
        .form-group label {
            display: block;
        }
        .form-group input {
            width: 100%;
            padding: 8px;
            font-size: 14px;
        }
        .form-group input[type="submit"] {
            width: auto;
            background-color: #4CAF50;
            color: white;
            border: none;
            padding: 10px 20px;
            cursor: pointer;
        }
        .form-group input[type="submit"]:hover {
            background-color: #45a049;
        }
        .error {
            color: red;
            font-size: 12px;
        }
    </style>
</head>
<body>

    <h2>Registration Form</h2>
    <form id="registerForm" runat="server">
        <div class="form-group">
            <label for="txtName">Full Name:</label>
            <asp:TextBox ID="txtname" runat="server" placeholder="Enter your name" />            
        </div>

        <div class="form-group">
            <label for="txtEmail">Email:</label>
            <asp:TextBox ID="txtemail" runat="server" placeholder="Enter your email" />
        </div>

        <div class="form-group">
            <label for="txtPassword">Password:</label>
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" placeholder="Enter your password" />
        </div>

        <div class="form-group">
            <label for="txtConfirmPassword">Confirm Password:</label>
            <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" placeholder="Confirm your password" />
        </div>

        <asp:FileUpload ID="fileUpload" runat="server" /><br />
        <asp:Button ID="btnGeneratePdf" runat="server" Text="Generate PDF" OnClick="GeneratePdf" /><br />
    </form>
</body>
</html>
