<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Logout.aspx.cs" Inherits="Passport.com.Logout" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="http://apps.bdimg.com/libs/jquery/2.1.4/jquery.min.js"></script>
</head>
<body>
    <script>
        //注销子系统登录会话
        $.get('http://mas.com/Logout.aspx', function () { }, {}, 'jsonp');
        $.get('http://mas.cn/Logout.aspx', function () { }, {}, 'jsonp');
        $.get('http://mas.cc/Logout.aspx', function () { }, {}, 'jsonp');

        setTimeout(function () { location.href = "login.aspx"; }, 0);
    </script>
</body>
</html>
