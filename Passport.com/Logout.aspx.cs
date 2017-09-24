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
            if (Session["username"] != null)
            {
                //清空sesson
                Session["username"] = null;
            }
        }
    }
}