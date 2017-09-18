using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Passport.com
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (Request.UrlReferrer != null)
                {
                    if (Request.Url.Host != Request.UrlReferrer.Host)
                    {
                        Session["referrer"] = Request.UrlReferrer.Scheme + "://" + Request.UrlReferrer.Authority;
                    }
                }

                //如果会话有效
                if (Session["token"] != null)
                {
                    string website = Session["referrer"] as string;
                    string token = Session["token"] as string;
                    //返回到原来的系统
                    Response.Redirect(website + "?t=" + token);
                }

            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string username = txtName.Text.Trim();
            string pwd = txPwd.Text.Trim();
            if (username == "admin" && pwd == "admin")
            {
                //生成令牌
                string token = Guid.NewGuid().ToString();

                ConnectionMultiplexer RedisClient = ConnectionMultiplexer.Connect("127.0.0.1:6379");

                IDatabase db = RedisClient.GetDatabase();

                //注册令牌到Redis  一分钟失效
                db.StringSet(token, username, TimeSpan.FromMinutes(1));

                string website = Session["referrer"] as string;

                //保存全局会话
                Session["token"] = token;

                //把令牌带到原来的系统
                Response.Redirect(website + "?t=" + token);

            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "login", "<script>alert('登录失败');location.href=location.href;</script>");
            }
        }
    }
}