using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SHBTONLINE.Areas.PersonOperation.Controllers
{
    public class AttendanceController : Controller
    {
        /// GET: PersonOperation/Attendance
        public ActionResult AttendanceView()
        {
            return View();
        }

        /// <summary>
        /// 获取成员签到数据
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAttendance()
        {
            return null;
        }
    }
}