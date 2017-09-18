using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mas.cn
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] == null)
            {
                string token = Request.QueryString["t"];
                if (!string.IsNullOrEmpty(token))
                {

                    ConnectionMultiplexer RedisClient = ConnectionMultiplexer.Connect("127.0.0.1:6379");

                    IDatabase db = RedisClient.GetDatabase();

                    //取出令牌的值
                    string tokenValue = db.StringGet(token);

                    Session["username"] = tokenValue;

                    //把令牌存入session
                    Session["token"] = token;

                    //如果令牌过期或无效，去登录
                    if (string.IsNullOrEmpty(tokenValue))
                    {
                        //Response.Redirect("http://Passport.com/Login.aspx");
                        LoginPage();
                    }
                }
                else
                {
                    LoginPage();
                }
            }

        }

        void LoginPage()
        {
            Response.Write("<script>location.href='http://Passport.com/Login.aspx'</script>");
            Response.End();
        }
        
    }
}