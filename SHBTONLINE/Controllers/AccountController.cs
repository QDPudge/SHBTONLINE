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
using System.Web.Script.Serialization;
using System.Net;
using System.IO;
using System.Text;
using System.Drawing;

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
                info.WechatID = query[0].WechatID;
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
        /// 登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public JsonResult MobileLogin(LoginViewModel model)
        {
            ReturnJson r = new ReturnJson() { s="ok"};
            int count = 0;
            var bty = HashCode.EncryptWithMD5(model.Password);
            model.Password = bty;
            var query = db.userinfoes.Where(p => p.LoginName == model.LoginName && p.PSW == bty).ToList();
            count = query.Count;
            if (count>0)
            {
                r.r = new  {
                    loginname=query[0].LoginName,
                    avatar = query[0].IMG,
                    dota2id= query[0].DOTA2ID,
                    pubgid= query[0].PubgID,
                    key= query[0].PrivateKey,
                    sb= query[0].SCrrency,
                    name= query[0].Name,
                    card=query[0].Card_bg,
                    wechatid=string.IsNullOrEmpty(query[0].WechatID)?false:true
                };
            }
            else
            {
                r.s = "error";
                r.r = "帐号或密码错误";
            }
            return Json(r);
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
                            PrivateKey = Guid.NewGuid().ToString(),
                            SCrrency=0,
                            sacrifNum=0
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
        [HttpPost]
        [AllowAnonymous]
        public JsonResult MobileRegister(RegisterViewModel model)
        {
            ReturnJson r = new ReturnJson() { s="ok"};
            if (ModelState.IsValid)
            {
                var query = db.userinfoes.Where(p => p.LoginName == model.LoginName);
                if (query.Count() > 0)
                {
                    r.s = "error";
                    r.r = "注册失败，账号已被注册过。";
                }
                var query2 = db.userinfoes.Where(p => p.LoginName == model.LoginName);
                if (query2.Count() > 0)
                {
                    r.s = "error";
                    r.r = "该邮箱已被注册过";
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
                            DOTA2ID = model.DOTA2ID,
                            PubgID = model.PubgID,
                            PrivateKey = Guid.NewGuid().ToString(),
                            SCrrency = 0,
                            sacrifNum = 0
                        };
                        var bty = HashCode.EncryptWithMD5(model.Password);
                        mode.PSW = bty;
                        db.userinfoes.Add(mode);
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        r.s = "error";
                        r.r = "注册失败" + ex.Message;
                    }
                    //ModelState.AddModelError("", "注册失败，未知错误，请联系管理员。");
                }
            }
            else
            {

                r.s = "error";
                r.r = "注册失败，请检查邮箱格式是否正确并表单填写完整";
            }
            // 如果我们进行到这一步时某个地方出错，则重新显示表单
            return Json(r);
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
                DOTA2ID=p.DOTA2ID,
                PubgID=p.PubgID,
                IMG=p.IMG,
                WechatID=p.WechatID,
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
            var query = db.userinfoes.Where(p => p.LoginName == SessionManager.Instance.UserInfoSession.LoginName).Select(p => new EditUserInfo
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
        public JsonResult SaveUserEdit(EditUserInfo mode)
        {
            ReturnJson r = new ReturnJson() { s = "ok", r = "修改成功！" };
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
        /// 修改个人资料
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public JsonResult MobileSaveUserEdit(EditUserInfo mode)
        {
            ReturnJson r = new ReturnJson() { s = "ok", r = "修改成功！" };
            var querypsw = db.userinfoes.Where(p => p.LoginName == mode.LoginName).FirstOrDefault();
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
                IMG=p.IMG,
                WechatID=p.WechatID,
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
        /// <summary>
        /// 绑定微信号
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public JsonResult BindWechat(LoginViewModel model)
        {
            ReturnJson r = new ReturnJson() { s = "ok" };
            int count = 0;
            var bty = HashCode.EncryptWithMD5(model.Password);
            model.Password = bty;
            var query = db.userinfoes.Where(p => p.LoginName == model.LoginName && p.PSW == bty).ToList();
            count = query.Count;
            if (count > 0)
            {
                r.r = "登陆成功";
            }
            else
            {
                r.s = "error";
                r.r = "帐号或密码错误";
            }
            return Json(r);
        }
        /// <summary>
        /// 微信登录状态获取
        /// </summary>
        /// <param name="JSCODE"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetWechatLoginState(string JSCODE,string LoginName)
        {
            ReturnJson r = new ReturnJson() { s = "ok"};
            var AppID = System.Configuration.ConfigurationSettings.AppSettings["AppID"];
            var AppSecret = System.Configuration.ConfigurationSettings.AppSettings["AppSecret"];
            //实例化一个能够序列化数据的类
            JavaScriptSerializer js = new JavaScriptSerializer();
            var Url = "https://api.weixin.qq.com/sns/jscode2session?appid="+ AppID + "&secret="+ AppSecret + "&js_code=" + JSCODE + "&grant_type=authorization_code";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "GET";
            request.UserAgent = " Mozilla/5.0 AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2272.118 Safari/537.36 ApiMaxJia/1.0";


            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            var ifno = js.Deserialize<WechatSession>(retString);
            if (ifno.openid==null)
            {
                r.s = "error";
                r.r = "微信登录状态获取失败";
            }
            else
            {
                using (var db=new SHBTONLINEContext())
                {

                    var query = db.userinfoes.Where(p => p.LoginName == LoginName).FirstOrDefault();
                    if (query!=null)
                    {
                        query.WechatID = ifno.openid;
                        db.Entry(query).Property(p => p.WechatID).IsModified = true;
                        var count = db.SaveChanges();
                        if (count==0)
                        {
                            r.s = "error";
                            r.r = "微信绑定失败";
                        }
                    }
                }
            }
            return Json(r,JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 特殊接口
        /// <summary>
        /// 获取图片
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public  void GetImage(string src)
        {
            try
            {
                WebRequest myrequest = WebRequest.Create(src);//前台js传的path，可以是远程服务器上的，也可以是本地的
                WebResponse myresponse = myrequest.GetResponse();
                Stream imgstream = myresponse.GetResponseStream();
                System.Drawing.Image img = System.Drawing.Image.FromStream(imgstream);
                MemoryStream ms = new MemoryStream();
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

                Response.AddHeader("Content-Length", ms.Length.ToString());
                Response.Clear();
                Response.ContentType = "image/jpeg";

                Response.BinaryWrite(ms.ToArray());
                Response.OutputStream.Flush();
                Response.OutputStream.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public JsonResult GetCardInfo(string LoginName)
        {
            ReturnJson r = new ReturnJson() { s = "ok" };
            var querypsw = db.userinfoes.Where(p => p.LoginName == LoginName).Select(p => new {
                loginname = p.LoginName,
                avatar = p.IMG,
                dota2id = p.DOTA2ID,
                pubgid = p.PubgID,
                key = p.PrivateKey,
                sb = p.SCrrency,
                name = p.Name,
                card = p.Card_bg,
                wechatid = string.IsNullOrEmpty(p.WechatID) ? false : true
            }).FirstOrDefault();
            r.r = querypsw;
            return Json(r);
        }
        #endregion
    }
}