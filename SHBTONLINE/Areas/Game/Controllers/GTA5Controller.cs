using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SHBTONLINE.Areas.Game.Controllers
{
    public class GTA5Controller : Controller
    {
        // GET: Game/GTA5
        public ActionResult GTA5Index()
        {
            return View();
        }
    }
}