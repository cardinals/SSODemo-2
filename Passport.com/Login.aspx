<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Passport.com.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>SSO统一登录中心</title>
</head>
<body>
    <form id="form1" runat="server">

        <div style="width: 1200px; text-align: center; margin: 20px;">
            <h1>SSO统一登录中心</h1>
            用户名：<asp:TextBox ID="txtName" runat="server"></asp:TextBox>
            <br />
            密&nbsp;&nbsp;&nbsp;码：<asp:TextBox ID="txPwd" TextMode="Password" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="Button1" runat="server" Text="登录" OnClick="Button1_Click" />
        </div>
    </form>
</body>
</html>
