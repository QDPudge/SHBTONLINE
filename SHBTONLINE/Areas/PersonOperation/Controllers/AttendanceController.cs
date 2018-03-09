using CommonData;
using Data;
using Data.Domain;
using SHBTONLINE.Areas.PersonOperation.Models.Attendance;
using SHBTONLINE.Models.SystemModel;
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
                var query = db.AttendanceInfos.Where(p => p.AD_LoginName == Name).Where(p => p.AD_AttendTime.Value.Month == DateTime.Now.Month).Select(p => new
                {
                    AttendTime = p.AD_AttendTime,//个人签到时间集合
                }).ToList();
                //返回给签到记录
                return Json(query);
            }
        }
        /// <summary>
        /// 签到保存数据
        /// </summary>
        /// <param name="LoginName"></param>
        /// <returns></returns>
        public JsonResult UpAttendanceData(string LoginName)
        {
            using (var db = new SHBTONLINEContext())
            {
                //开启事务
                using (var tran = db.Database.BeginTransaction())
                {
                    try
                    {
                        var userinfo = db.userinfoes.Where(a => a.LoginName == LoginName).FirstOrDefault();
                        var queryAttend = db.AttendanceInfos.Where(p => p.AD_LoginName == LoginName).Where(p => p.AD_AttendTime.Value.Year == DateTime.Now.Year && p.AD_AttendTime.Value.Month == DateTime.Now.Month && p.AD_AttendTime.Value.Day == DateTime.Now.Day).ToList();
                        //说明未签到
                        if (queryAttend.Count() < 1)
                        {
                            //签到数据绑定。
                            AttendanceInfo model = new AttendanceInfo()
                            {
                                ID = Guid.NewGuid().ToString(),
                                AD_Id = userinfo.ID,
                                AD_LoginName = LoginName,
                                AD_AttendTime = DateTime.Now,
                                Create_Time = DateTime.Now
                            };
                            db.AttendanceInfos.Add(model);
                            //db.Entry(model).State= System.Data.Entity.EntityState.Added;
                            //签到给S币
                            //s币数据的计算[判断方法]建议改掉S币数据类型
                            userinfo.SCrrency = userinfo.SCrrency+1;
                            db.userinfoes.Attach(userinfo);
                            db.Entry(userinfo).Property(x => x.SCrrency).IsModified = true;
                            db.SaveChanges();
                            tran.Commit();
                            ReturnJson r = new ReturnJson() { s = "ok", r = "签到完成，获得S币+1s！" };
                            return Json(r);
                        }
                        else
                        {
                            ReturnJson r = new ReturnJson() { s = "error", r = "已签到,想白嫖？" };
                            return Json(r);
                        }
                    }
                    catch (Exception e)
                    {
                        tran.Rollback();
                        ReturnJson r = new ReturnJson { r = "error", s = "删除失败！" + e.Message };
                        return Json(r);
                    }
                }
            }
        }


        /// <summary>
        /// 计数方法(保存数据)
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public JsonResult GetsacrifInfo(string Name)
        {
            using (var db = new SHBTONLINEContext())
            {
                using (var tran = db.Database.BeginTransaction())
                {
                    try
                    {
                        ///将登陆名确定下来
                        var loginname = SessionManager.Instance.UserInfoSession.LoginName;
                        ///查询人员基础信息
                        var userinfo = db.userinfoes.Where(a => a.LoginName == loginname).FirstOrDefault();
                        AttendanceInfo model = new AttendanceInfo()
                        {
                            ID = Guid.NewGuid().ToString(),
                            AD_LoginName = loginname,
                            //因为公用一张表所以考勤时间记为空
                            AD_AttendTime = null,
                            sacrificeName = Name,
                            Create_Time = DateTime.Now
                        };
                        //签到表与上香表在同一张表中[上线没有签到时间AD_AttendTime的数值为null]
                        db.AttendanceInfos.Add(model);

                        //更新人员表上香次数
                        userinfo.sacrifNum = userinfo.sacrifNum + 1;
                        db.userinfoes.Attach(userinfo);
                        db.Entry(userinfo).Property(x => x.sacrifNum).IsModified = true;
                        db.SaveChanges();
                        tran.Commit();

                        var Count = 0;
                        //计数xk+wyh（被上香次数）
                        if (Name == "xk")
                        {
                            Count = db.AttendanceInfos.Where(p => p.sacrificeName == "xk").Count();

                        }
                        else
                        {
                            Count = db.AttendanceInfos.Where(p => p.sacrificeName == "wyh").Count();
                        }
                        //返回计算出来的数值（上香次数）
                        return Json(Count);
                    }
                    catch(Exception e) {
                        tran.Rollback();
                        ReturnJson r = new ReturnJson { r = "error", s = "出现问题！" + e.Message };
                        return Json(r);
                    }
                }
            }
        }
    }
}