using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SHBTONLINE.Controllers
{
    public class WebController : Controller
    {

        public ActionResult Main(string load)
        {
            ViewBag.isload = 0;
            if (load!="0")
            {
                ViewBag.isload = 1;
            }
            return View();
        }
    }
}