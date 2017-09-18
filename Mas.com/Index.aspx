<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Mas.com.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            欢迎您，<%:Session["username"] %><br />
            Token=<%:Session["token"] %>
        </div>

        <a href="http://Passport.com/Logout.aspx">退出</a>

    </form>
</body>
</html>
