using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SHBTONLINE.Models.SystemModel
{
    public class SessionManager
    {
        /// <summary>
        /// 用户sessionkey
        /// </summary>
        private SessionManager()
        {
            //string cookieHead = Framework.Utilities.ConfigHelper.GetAppSetting("CookieHead");
            //UserInfoTicketKey = "UserTicket";
            //UserAutoLoginTicketKey =  "UserTicketAuto";
        }

        /// <summary>
        /// _sessionmanager静态对象
        /// </summary>
        private static SessionManager _sessionmanager = null;
        /// <summary>
        /// SessionKey值
        /// </summary>
        public string SessionKey = "UserinfoSesssionKey";

        /// <summary>
        /// 用户登录信息CookieKey
        /// </summary>
        public string UserInfoTicketKey = "UserTicket";

        /// <summary>
        /// 用户自动登录票据
        /// </summary>
        public string UserAutoLoginTicketKey = "UserTicketAuto";

        /// <summary>
        /// 用户权限相关信息
        /// </summary>
        public string UserRightInfo = "UserRightInfo";

        /// <summary>
        /// 获取Session实例
        /// </summary>
        public static SessionManager Instance
        {
            get { return _sessionmanager ?? (_sessionmanager = new SessionManager()); }
        }
        /// <summary>
        /// 清空session
        /// </summary>
        public static void Logout()
        {
            _sessionmanager = null;
        }
        /// <summary>
        /// 获取登录用户Session
        /// </summary>
        public UserLoginInfo UserInfoSession
        {
            get
            {
                object obj = System.Web.HttpContext.Current.Session[SessionKey] ?? new UserLoginInfo();
                return obj as UserLoginInfo;
            }
        }


    }
}