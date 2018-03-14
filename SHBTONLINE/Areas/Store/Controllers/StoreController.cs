using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data;
using CommonData;

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
            #endregion
        }
    }
}