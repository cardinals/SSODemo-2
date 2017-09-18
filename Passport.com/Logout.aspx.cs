using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Passport.com
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //注销全局会话
            if (Session["token"] != null)
            {
                ConnectionMultiplexer RedisClient = ConnectionMultiplexer.Connect("127.0.0.1:6379");

                IDatabase db = RedisClient.GetDatabase();
                //删除令牌Token
                db.KeyDelete(Session["token"].ToString());
                //清空sesson
                Session["token"] = null;
            }
        }
    }
}