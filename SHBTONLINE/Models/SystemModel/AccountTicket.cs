using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SHBTONLINE.Models.SystemModel
{
    /// <summary>
    /// 登录用户身份票据
    /// </summary>
    [Serializable]
    public class AccountTicket
    {

        private string _sessionid = "";
        private string _useraccount = "";
        private string _password = "";


        /// <summary>
        /// 当前用户票据SessionID
        /// </summary>
        public string SessionId
        {
            get { return _sessionid; }
            set { _sessionid = value; }
        }

        /// <summary>
        /// 用户帐号
        /// </summary>
        public string UserAccount
        {
            get { return _useraccount; }
            set { _useraccount = value; }
        }

        /// <summary>
        /// 用户密码
        /// </summary>
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }




    }
}