using CommonData;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SHBTONLINE.Areas.Play
{
    public class PlayController : Controller
    {
        // GET: Play/Play
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetPlays()
        {
            ReturnJson r = new ReturnJson() { s = "ok" };
            using (var db = new SHBTONLINEContext())
            {
                var queryall = db.Plays.Where(p=>p.Status=="未开始").ToList();

                r.r = queryall;
            }
            return Json(r);
        }

    }
}