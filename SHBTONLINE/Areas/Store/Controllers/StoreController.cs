using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data;
using CommonData;
using Data.Domain;

namespace SHBTONLINE.Areas.Store.Controllers
{
    public class StoreController : Controller
    {
        // GET: Store/Store
        public ActionResult Index()
        {
            return View();
        }
        #region 接口
        /// <summary>
        /// 获取商品列表
        /// </summary>
        /// <returns></returns>
        public JsonResult GetItem()
        {

            ReturnJson r = new ReturnJson() { s = "ok" };
            using (var db = new SHBTONLINEContext())
            {
                var items = db.GoodsLists.AsNoTracking().ToList();
                r.r = items;
            }
            return Json(r);
        }
        /// <summary>
        /// 获取已有的物品
        /// </summary>
        /// <param name="Loginname"></param>
        /// <returns></returns>
        public JsonResult GetMineItem(string Loginname)
        {
            ReturnJson r = new ReturnJson() { s = "ok" };
            using (var db = new SHBTONLINEContext())
            {
                var items = db.Goodsinfoes.AsNoTracking().Where(p=>p.LoginName==Loginname).ToList();
                r.r = items;
            }
            return Json(r);
        }
        /// <summary>
        /// 获取已有的物品
        /// </summary>
        /// <param name="Loginname"></param>
        /// <returns></returns>
        public JsonResult GetMineCard(string Loginname)
        {
            ReturnJson r = new ReturnJson() { s = "ok" };
            using (var db = new SHBTONLINEContext())
            {
                var items = db.Goodsinfoes.AsNoTracking().Where(p => p.LoginName == Loginname&&p.Type== "名片背景").Select(p=>p.GoodsID).ToList();
                var goods = db.GoodsLists.AsNoTracking().Where(p => items.Contains(p.ID)).ToList();
                r.r = goods;
            }
            return Json(r);
        }
        /// <summary>
        /// 购买商品
        /// </summary>
        /// <param name="Loginname"></param>
        /// <param name="Goodid"></param>
        /// <returns></returns>
        public JsonResult BuyItem(string Loginname,string Goodsid,int? spend1,int? spend2)
        {
            ReturnJson r = new ReturnJson() { s = "ok" };
            using (var db = new SHBTONLINEContext())
            {
                try
                {
                    var items = db.Goodsinfoes.AsNoTracking().Where(p => p.LoginName == Loginname && p.GoodsID == Goodsid);
                    if (items.Count() > 0)
                    {
                        r.s = "error";
                        r.r = "已经拥有该物品，无法重复购买";
                        return Json(r);
                    }
                    else
                    {
                        var queryuser = db.userinfoes.AsNoTracking().Where(p => p.LoginName == Loginname).FirstOrDefault();
                        queryuser.SCrrency = queryuser.SCrrency - (int)spend1;
                        if (queryuser.SCrrency<0)
                        {
                            r.s = "error";
                            r.r = "S币余额不足";
                            return Json(r);
                        }
                        var goodsinfo = db.GoodsLists.AsNoTracking().Where(p => p.ID == Goodsid).FirstOrDefault();
                        Goodsinfo buyit = new Goodsinfo()
                        {
                            GoodsID = Goodsid,
                            GoodsName = goodsinfo.Name,
                            ID = Guid.NewGuid().ToString(),
                            LoginName = Loginname,
                            Type= goodsinfo.Type,
                            RewardDate = DateTime.Now,
                            Spend1 = spend1,
                            Spend2 = spend2
                        };
                        db.Goodsinfoes.Add(buyit);
                        db.userinfoes.Attach(queryuser);
                        db.Entry(queryuser).Property(p => p.SCrrency).IsModified = true;
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    r.s = "error";
                    r.r = "购买失败";
                }
           
            }
            return Json(r);
        }
        /// <summary>
        /// 使用名片背景
        /// </summary>
        /// <param name="Loginname"></param>
        /// <param name="Url"></param>
        /// <returns></returns>
        public JsonResult UseCard(string Loginname,string Url)
        {
            ReturnJson r = new ReturnJson() { s = "ok" };
            using (var db = new SHBTONLINEContext())
            {
                var user = db.userinfoes.AsNoTracking().Where(p => p.LoginName == Loginname).FirstOrDefault();
                user.Card_bg = Url;
                db.userinfoes.Attach(user);
                db.Entry(user).Property(p => p.Card_bg).IsModified = true;
                db.SaveChanges();
            }
            return Json(r);
        }
        #endregion
    }
}