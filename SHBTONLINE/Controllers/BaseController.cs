using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SHBTONLINE.Controllers
{
    public class BaseController : Controller
    {
        //HttpCookie cookie;
        //// GET: Base
        //public BaseController()
        //{
        //    cookie = new HttpCookie("HGCookie");//初使化并设置Cookie的名称
        //    DateTime dt = DateTime.Now;
        //    TimeSpan ts = new TimeSpan(0, 0, 30, 0, 0);//过期时间为30分钟
        //    cookie.Expires = dt.Add(ts);//设置过期时间
        //    cookie.Values.Add("userid", "userid_value");
        //    cookie.Values.Add("userid2", "userid2_value2");
        //    Response.AppendCookie(cookie);
        //}
        /// <summary>
        /// 页面登录认证
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //object[] attrs = filterContext.ActionDescriptor.GetCustomAttributes(typeof(NoLoginCheck), true);

            ////对于没有[NoLoginCheck]的强制执行登录认证
            //if (attrs.Length != 1)
            //{
            //执行登录验证
            //执行登录验证
            //if (filterContext.HttpContext.Session[SessionManager.Instance.SessionKey] == null)
            //{
            //    //Session不存在，判断用户票据是否有，
            //    //有的话做为浏览器进程重新登录
            //    string docment = CookieHelp.GetCookie(SessionManager.Instance.UserInfoTicketKey);
            //    if (docment != "")
            //    {

            //        QM_AccountTicket userticket = SerializableHelp.DecryptAndDerializable<QM_AccountTicket>(docment);
            //        if (userticket.SessionId == filterContext.HttpContext.Session.SessionID)
            //        {
            //            //重新模拟登录
            //            QM_Login loginmodel = new QM_Login();
            //            loginmodel.IsAutoLogin = false;
            //            loginmodel.UserAccount = userticket.UserAccount;
            //            loginmodel.UserPassword = userticket.Password;
            //            var container = BootstrapHelper.IoContainer;

            //            var accountservice = container.Resolve<IAccountService>();
            //            QM_OperationResult result = accountservice.Login(loginmodel);
            //            if (result.Status)
            //            {
            //                accountservice.FillLoginInfo(loginmodel, result.Obj as QM_Account);
            //                //填充用户自定义权限下的标段信息到临时表
            //                QM_UserRightInfo userrightinfo = RightManager.GetEmpRightInfo();
            //                if (userrightinfo.Scope.Scope == 2)
            //                {
            //                    //IList<Sys_TempSectionRight> templist = new List<Sys_TempSectionRight>();
            //                    //foreach (Guid g in userrightinfo.Scope.SectionsId)
            //                    //{
            //                    //    Sys_TempSectionRight systempsection = new Sys_TempSectionRight();
            //                    //    systempsection.Session_Id = Session.SessionID;
            //                    //    systempsection.Section_Id = g;
            //                    //    systempsection.CreateTime = DateTime.Now;
            //                    //    templist.Add(systempsection);
            //                    //}
            //                    //accountservice.FillUserScopeSection(templist);

            //                }
            //            }
            //        }
            //    }
            //}




            //if (filterContext.HttpContext.Session[SessionManager.Instance.SessionKey] == null)
            //{
            //    //如果模拟登录失败。跳转到登录页面
            //    filterContext.Result =
            //       new RedirectToRouteResult("Default",
            //           new System.Web.Routing.RouteValueDictionary(
            //               new { Action = "Login", Controller = "Home" })
            //               );
            //    return;

            //}

            base.OnActionExecuting(filterContext);

        }
    }
}