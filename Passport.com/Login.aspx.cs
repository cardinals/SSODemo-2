using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Passport.com
{
    public partial class Login : System.Web.UI.Page
    {
        /// <summary>
        /// 生成秘钥
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public static string CreateToken(DateTime timestamp)
        {
            StringBuilder securityKey = new StringBuilder(MD5Encrypt(timestamp.ToString("yyyy")));
            securityKey.Append(MD5Encrypt(timestamp.ToString("MM")));
            securityKey.Append(MD5Encrypt(timestamp.ToString("dd")));
            securityKey.Append(MD5Encrypt(timestamp.ToString("HH")));
            securityKey.Append(MD5Encrypt(timestamp.ToString("mm")));
            securityKey.Append(MD5Encrypt(timestamp.ToString("ss")));
            return MD5Encrypt(securityKey.ToString());
        }

        /// <summary>
        /// MD5 加密
        /// </summary>
        /// <param name="input"> 待加密的字符串 </param>
        /// <param name="encoding"> 字符编码 </param>
        /// <returns></returns>
        public static string MD5Encrypt(string input)
        {
            var data = MD5.Create().ComputeHash(Encoding.Default.GetBytes(input));
            return BitConverter.ToString(data).Replace("-", "");
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (Request.UrlReferrer != null)
                {
                    if (Request.Url.Host != Request.UrlReferrer.Host)
                    {
                        string website = Request.UrlReferrer.Scheme + "://" + Request.UrlReferrer.Authority;
                        //如果会话有效
                        if (Session["username"] != null)
                        {

                            string token = Guid.NewGuid().ToString();
                            token = CreateToken(DateTime.Now);

                            string username = Session["username"] as string;

                            ConnectionMultiplexer RedisClient = ConnectionMultiplexer.Connect("127.0.0.1:6379");

                            IDatabase db = RedisClient.GetDatabase();

                            //注册令牌到Redis  一分钟失效
                            db.StringSet(token, username, TimeSpan.FromMinutes(1));
                            //返回到原来的系统
                            Response.Redirect(website + "?t=" + token);
                        }
                        else
                        {
                            txtSite.Value = website;
                        }
                    }
                }



            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string username = txtName.Text.Trim();
            string pwd = txtPwd.Text.Trim();
            if (username == "admin" && pwd == "admin")
            {
                //生成令牌
                string token = Guid.NewGuid().ToString();

                ConnectionMultiplexer RedisClient = ConnectionMultiplexer.Connect("127.0.0.1:6379");

                IDatabase db = RedisClient.GetDatabase();

                //注册令牌到Redis  一分钟失效
                db.StringSet(token, username, TimeSpan.FromMinutes(1));

                string website = txtSite.Value;

                //保存全局会话
                Session["username"] = username;

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