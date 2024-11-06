<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <!-- Default.aspx -->
<asp:TextBox ID="txtName" runat="server" Placeholder="Enter your name"></asp:TextBox><br />
<asp:TextBox ID="txtEmail" runat="server" Placeholder="Enter your email"></asp:TextBox><br />
<asp:FileUpload ID="fileUpload" runat="server" /><br />
<asp:Button ID="btnGeneratePdf" runat="server" Text="Generate PDF" OnClick="GeneratePdf" /><br />

    </form>
</body>
</html>
