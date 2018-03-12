using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data;

namespace SHBTONLINE.Areas.Store.Controllers
{
    public class StoreController : Controller
    {
        // GET: Store/Store
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetItem()
        {
            using (var db = new SHBTONLINEContext)
            {

            }
        }
    }
}