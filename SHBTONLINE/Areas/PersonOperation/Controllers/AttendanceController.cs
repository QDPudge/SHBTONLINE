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
        public JsonResult GetAttendance(string Name)
        {
            using (var db = new SHBTONLINEContext())
            {
                AttendModel model = new AttendModel();
                //查询人员以及签到日期(条件为签到人员名字与登录名相同)
                var query = db.AttendanceInfos.Where(p =>p.AD_LoginName==Name).Select(p => new AttendList
                {
                    //ID = p.ID,
                    //Name = p.AD_LoginName,
                    AttendTime = p.AD_AttendTime,//个人签到时间集合
                }).ToList();
                model.AttendList = query;
                //返回给签到记录
                return Json(query);
            }
        }
    }
}