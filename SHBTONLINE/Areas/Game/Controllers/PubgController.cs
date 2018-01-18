using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SHBTONLINE.Areas.Game.Controllers
{
    public class PubgController : Controller
    {
        // GET: Game/Pubg
        public ActionResult PubgIndex()
        {
            return View();
        }
    }
}