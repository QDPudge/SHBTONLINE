using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SHBTONLINE.Common
{
    public class CookieHelp
    {
        //public CookieHelp();

        //
        // 摘要:
        //     获取cookie值
        public static string GetCookie(string cookiename)
        {
            return HttpContext.Current.Request.Cookies[cookiename].Value;
        }
        //
        // 摘要:
        //     设置Cookie值
        //
        // 参数:
        //   cookiename:
        //
        //   cookievalue:
        //
        //   dt:
        public static void SetCookie(string cookiename, string cookievalue, DateTime dt)
        {
            HttpCookie cookie = new HttpCookie(cookiename);
            cookie.Value = cookievalue;
            cookie.Expires = DateTime.Now.AddDays(1);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
    }
}