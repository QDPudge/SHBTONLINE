using Data;
using SHBTONLINE.Areas.PersonOperation.Models.Attendance;
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
            using (var db = new SHBTONLINEContext())
            {
                AttendModel model = new AttendModel();
                //查询人员以及签到日期
                var query = db.AttendanceInfos.Where(p => !string.IsNullOrEmpty(p.ID)).Select(p => new AttendList
                {
                    ID=p.ID,
                    Name = p.AD_LoginName,
                    AttendTime = p.AD_AttendTime,
                }).ToList();
                model.AttendList = query;
                //返回给签到记录
                return Json(query);
            }
        }
    }
}