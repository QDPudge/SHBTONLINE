using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SHBTONLINE.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string load)
        {
            ViewBag.isload = 0;
            if (!string.IsNullOrEmpty(load))
            {
                ViewBag.isload = 1;
            }
            return View();
        }


    }
}