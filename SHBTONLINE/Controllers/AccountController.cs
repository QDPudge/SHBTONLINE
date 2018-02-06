using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommonData;
using Data.Domain;
using SHBTONLINE.Common;
using SHBTONLINE.Models.SystemModel;
using Data;
using SHBTONLINE.Models.PUBG;

namespace SHBTONLINE.Controllers
{
    public class AccountController : BaseController
    {
        // GET: Account
        //
        // GET: /Account/Login
        /// <summary>
        /// 数据上下文对象
        /// </summary>
        static SHBTONLINEContext db = new SHBTONLINEContext();
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="model"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            int count = 0;
            var bty = HashCode.EncryptWithMD5(model.Password);
            model.Password = bty;
            var query = db.userinfoes.Where(p => p.LoginName == model.LoginName && p.PSW == bty).ToList();
            count = query.Count;
            if (count > 0)
            {
                UserLoginInfo info = new UserLoginInfo();
                info.userName = query[0].Name;
                info.MateLoginName = query[0].MateLoginName;
                info.MateName = query[0].MateName;
                info.LoginName = query[0].LoginName;
                info.Key = query[0].PrivateKey;
                //SessionManager.Instance.UserInfoSession = new UserLoginInfo();
                // Console.WriteLine(myreader.GetInt32(0) + "," + myreader.GetString(1) + "," + myreader.GetString(2));
                FillLoginInfo(info, model);
                //Session["Name"] = model.userName;
                return RedirectToLocal(returnUrl);
            }
            else
            {
                ModelState.AddModelError("", "突然不知道你是谁了，再试试吧。");
                return View(model);
            }
        }
        /// <summary>
        /// 填充用户登录信息
        /// </summary>
        /// <param name="model">登录模型</param>
        /// <param name="info">登录用户信息</param>
        /// <returns>true 成功 false 失败</returns>
        public bool FillLoginInfo(UserLoginInfo info, LoginViewModel model = null)
        {

            try
            {
                //写入Session信息
                Session[SessionManager.Instance.SessionKey] = info;
                if (model != null)
                {
                    //写入用户信息票据，有效期为浏览器进程
                    AccountTicket usertick = new AccountTicket();
                    usertick.Password = model.Password;
                    usertick.SessionId = Session.SessionID;
                    usertick.UserAccount = model.LoginName;
                    string strticket = usertick.UserAccount;
                    CookieHelp.SetCookie(SessionManager.Instance.UserInfoTicketKey, strticket, DateTime.Now.AddDays(1));
                }


                //if (model.IsAutoLogin)
                //{
                //    //下次自动登录，写入自动登录票据
                //    QM_LoginTicket loginticket = new QM_LoginTicket();
                //    loginticket.CreateTime = DateTime.Now;
                //    loginticket.UserName = model.UserAccount;
                //    loginticket.UserPassword = model.UserPassword;
                //    string strlogin = SerializableHelp.SerializableAndEncrypt<QM_LoginTicket>(loginticket);
                //    CookieHelp.SetCookie(SessionManager.Instance.UserAutoLoginTicketKey, strlogin, DateTime.MaxValue);


                //}
            }
            catch (Exception ex)
            {
                //LogHelper.WriteLog("登录信息填充失败", ex);
                return false;
            }

            return true;


        }
        /// <summary>
        /// 站内链接跳转
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
        /// <summary>
        /// 账户登出
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            Session[SessionManager.Instance.SessionKey] = null;
            SessionManager.Logout();
            CookieHelp.SetCookie(SessionManager.Instance.UserInfoTicketKey, "", DateTime.Now.AddSeconds(5));
            return RedirectToAction("Index", "Home");
        }
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }
        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var query = db.userinfoes.Where(p => p.LoginName == model.LoginName);
                if (query.Count() > 0)
                {
                    ModelState.AddModelError("", "注册失败，账号已被注册过。");
                    return View(model);
                }
                var query2 = db.userinfoes.Where(p => p.LoginName == model.LoginName);
                if (query2.Count() > 0)
                {
                    ModelState.AddModelError("", "注册失败，该邮箱已被注册过。");
                    return View(model);
                }
                if (query2.Count() == 0 && query.Count() == 0)
                {
                    try
                    {
                        userinfo mode = new userinfo()
                        {
                            ID = Guid.NewGuid().ToString(),
                            Email = model.Email,
                            LoginName = model.LoginName,
                            Name = model.userName,
                            PSW = model.Password,
                            DOTA2ID=model.DOTA2ID,
                            PubgID=model.PubgID,
                            PrivateKey = Guid.NewGuid().ToString()
                        };
                        var bty = HashCode.EncryptWithMD5(model.Password);
                        mode.PSW = bty;
                        db.userinfoes.Add(mode);
                        db.SaveChanges();
                        UserLoginInfo info = new UserLoginInfo();
                        info.userName = model.userName;
                        //info.Mate = myreader.GetString("PSW");
                        info.LoginName = model.LoginName;
                        info.Key = mode.PrivateKey;
                        LoginViewModel loginmodel = new LoginViewModel();
                        loginmodel.Password = bty;
                        loginmodel.LoginName = model.LoginName;
                        FillLoginInfo(info, loginmodel);
                        return RedirectToAction("Index", "Home");
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", "注册失败，" + ex.Message);
                        return View(model);
                    }
                    //ModelState.AddModelError("", "注册失败，未知错误，请联系管理员。");
                }
            }
            // 如果我们进行到这一步时某个地方出错，则重新显示表单
            return View(model);
        }
        /// <summary>
        /// 个人信息页
        /// </summary>
        /// <returns></returns>
        public ActionResult UserInfo()
        {
            var query = db.userinfoes.Where(p => p.LoginName == SessionManager.Instance.UserInfoSession.LoginName).Select(p => new EditUserInfo
            {
                Email = p.Email,
                LoginName = p.LoginName,
                userName = p.Name,
                MateName = p.MateName,
                Key = p.PrivateKey
            }).First();
            return View(query);
        }
        /// <summary>
        /// 个人信息页
        /// </summary>
        /// <returns></returns>
        public ActionResult EditUserInfo()
        {
            var query = db.userinfoes.Where(p => p.LoginName == SessionManager.Instance.UserInfoSession.LoginName).Select(p => new RegisterViewModel
            {
                Email = p.Email,
                LoginName = p.LoginName,
                userName = p.Name,
                DOTA2ID=p.DOTA2ID,
                PubgID=p.PubgID
            }).First();
            return View(query);
        }       
        /// <summary>
        /// 修改个人资料
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public JsonResult SaveUserEdit(RegisterViewModel mode)
        {
            ReturnJson r = new ReturnJson() { s = "ok", r = "修改成功！请牢记密码！" };
            var querypsw = db.userinfoes.Where(p => p.LoginName == SessionManager.Instance.UserInfoSession.LoginName).FirstOrDefault();
                try
                {
                    querypsw.DOTA2ID = mode.DOTA2ID;
                querypsw.PubgID = mode.PubgID;
                querypsw.Email = mode.Email;
                querypsw.Name = mode.userName;
                db.userinfoes.Attach(querypsw);
                db.Entry(querypsw).Property(x => x.DOTA2ID).IsModified = true;
                db.Entry(querypsw).Property(x => x.PubgID).IsModified = true;
                db.Entry(querypsw).Property(x => x.Email).IsModified = true;
                db.Entry(querypsw).Property(x => x.Name).IsModified = true;
                db.SaveChanges();
                    return Json(r);
                }
                catch (Exception ex)
                {
                    r.r = "修改失败，" + ex.Message;
                    r.s = "error";
                    return Json(r);
                }
        }
        /// <summary>
        /// 个人信息页
        /// </summary>
        /// <returns></returns>
        public ActionResult BaseInfo()
        {
            var query = db.userinfoes.Where(p => p.LoginName == SessionManager.Instance.UserInfoSession.LoginName).Select(p => new EditUserInfo
            {
                Email = p.Email,
                LoginName = p.LoginName,
                userName = p.Name,
                MateName = p.MateName,
                DOTA2ID=p.DOTA2ID,
                PubgID=p.PubgID,
                Key=p.PrivateKey
            }).First();
            return PartialView(query);
        }

        /// <summary>
        /// 个人信息页
        /// </summary>
        /// <returns></returns>
        public ActionResult ModifyPassword()
        {
            ModiftPassword mode = new ModiftPassword();
            return PartialView(mode);
        }
        /// <summary>
        /// 修改密码的保存
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public JsonResult SavePassword(ModiftPassword mode)
        {
            ReturnJson r = new ReturnJson() { s = "ok", r = "修改成功！请牢记密码！" };
            var querypsw = db.userinfoes.Where(p => p.LoginName == SessionManager.Instance.UserInfoSession.LoginName).FirstOrDefault();
            var old = HashCode.EncryptWithMD5(mode.Used);
            if (old == querypsw.PSW)
            {
                try
                {
                    querypsw.PSW = HashCode.EncryptWithMD5(mode.ConfirmPassword);
                    db.userinfoes.Attach(querypsw);
                    db.Entry(querypsw).Property(x => x.PSW).IsModified = true;
                    db.SaveChanges();
                    return Json(r);
                }
                catch (Exception ex)
                {
                    r.r = "修改失败，" + ex.Message;
                    r.s = "error";
                    return Json(r);
                }
            }
            else
            {
                var ran = new Random();
                r.s = "error";
                switch (ran.Next(4))
                {
                    case 3: r.r = ("原密码不对！糊弄猴呢！"); break;
                    case 1: r.r = ("原密码不对！糊弄猪呢！"); break;
                    case 2: r.r = ("修改成功个P！原密码不对！"); break;
                    default: r.r = ("你还挺精神呢？"); break;
                }
                return Json(r);

            }
        }
        #region 社交
        /// <summary>
        /// 社交
        /// </summary>
        /// <returns></returns>
        public ActionResult Social()
        {
            Social mode = new Social();
            return PartialView(mode);
        }
        /// <summary>
        /// 囍
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public JsonResult SaveMate(Social mode)
        {
            ReturnJson r = new ReturnJson() { s = "ok", r = "恭贺新禧！" };
            try
            {
                var query = db.userinfoes.Where(p => p.PrivateKey == mode.Key).FirstOrDefault();
                if (query != null)
                {
                    if (query.Name == mode.Mate || query.LoginName == mode.Mate)
                    {
                        query.MateName = SessionManager.Instance.UserInfoSession.userName;
                        query.MateLoginName = SessionManager.Instance.UserInfoSession.LoginName;
                        db.userinfoes.Attach(query);
                        db.Entry(query).Property(p => p.MateName).IsModified = true;
                        db.Entry(query).Property(p => p.MateLoginName).IsModified = true;

                        var query2 = db.userinfoes.Where(p => p.LoginName == SessionManager.Instance.UserInfoSession.LoginName).FirstOrDefault();
                        query2.MateName = query.Name;
                        query2.MateLoginName = query.MateLoginName;
                        db.userinfoes.Attach(query2);

                        db.Entry(query2).Property(p => p.MateName).IsModified = true;
                        db.Entry(query2).Property(p => p.MateLoginName).IsModified = true;
                        db.SaveChanges();
                        SessionManager.Instance.UserInfoSession.MateLoginName = query.LoginName;
                        SessionManager.Instance.UserInfoSession.MateName = query.Name;
                    }
                }
            }
            catch (Exception ex)
            {
                r.s = "error";
                r.r = "抱歉，出了点问题，" + ex.Message;
            }
            return Json(r);
        }
        #endregion
    }
}