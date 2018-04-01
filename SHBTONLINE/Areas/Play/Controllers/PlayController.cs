using CommonData;
using Data;
using Data.Domain;
using SHBTONLINE.Areas.Play.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SHBTONLINE.Areas.Play.Controllers
{
    public class PlayController : Controller
    {
        // GET: Play/Play
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 获取今日所有赛事
        /// </summary>
        /// <returns></returns>
        public JsonResult GetPlays()
        {
            ReturnJson r = new ReturnJson() {s="ok" };
            var start = DateTime.Now.Date;
            var end = start.AddDays(1);
            using (var db=new SHBTONLINEContext())
            {
                var parent = db.Plays.Where(p => p.ParentID == null&&p.OffTime>=start&&p.OffTime<end).Select(p => new PlayForm {
                    ID=p.ID,
                    Name=p.Name,
                    Odds=p.Odds,
                    OffTime=p.OffTime,
                    ParentID=p.ParentID,
                    Status=p.Status,
                    Results=p.Results
                }).OrderByDescending(p=>p.OffTime).ToList();
                parent.ForEach(w => {
                    w.child= db.Plays.Where(p => p.ParentID == w.ID).Select(p => new PlayForm
                    {
                        ID = p.ID,
                        Name = p.Name,
                        Odds = p.Odds,
                        OffTime = p.OffTime,
                        ParentID = p.ParentID,
                        Status = p.Status,
                        Results=p.Results
                    }).ToList();
                });
                r.r = parent;
            }
            return Json(r);
        }
        /// <summary>
        /// 购买
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public JsonResult BuyPlays(BuyPlayForm mode)
        {
            ReturnJson r = new ReturnJson() { s = "ok" };
                using (var db = new SHBTONLINEContext())
            {
                try
                {
                    var state = db.Plays.Where(p => p.ID == mode.PlayID).First();
                    if (state.Status != "正常")
                    {
                        r.s = "error";
                        r.r = "比赛未开始或已结束";
                    }
                    var buyitems = db.PlayItems.Where(p => p.Loginname == mode.Loginname && p.PlayID == mode.PlayID).ToList();
                    //if (buyitems.Count > 0)
                    //{
                    //    r.s = "error";
                    //    r.r = "单个条目无法重复购买";
                    //}
                    PlayItem item = new PlayItem()
                    {
                        ID = Guid.NewGuid().ToString(),
                        CreateTime = DateTime.Now,
                        Loginname = mode.Loginname,
                        PlayID = mode.PlayID,
                        State = "已购买"
                    };
                    item.Cost1 = Convert.ToInt32(mode.Cost * 3);
                    var queryuser = db.userinfoes.Where(p => p.LoginName == mode.Loginname).FirstOrDefault();
                    queryuser.SCrrency = queryuser.SCrrency - Convert.ToInt32(item.Cost1);
                    if (queryuser.SCrrency<0)
                    {
                        r.s = "error";
                        r.r = "兜里空空如也";
                        return Json(r);
                    }
                    db.PlayItems.Add(item);
                    db.userinfoes.Attach(queryuser);
                    db.Entry(queryuser).Property(p => p.SCrrency).IsModified = true;
                    db.SaveChanges();
                }
                catch(Exception ex)
                {
                    r.s = "error";
                    r.r = "下注失败，" + ex.Message;
                }
            }      
            return Json(r);
        }
        /// <summary>
        /// 获取胜利排名【生涯】
        /// </summary>
        /// <returns></returns>
        public JsonResult GetWinRank()
        {
            Dictionary<string, int> count = new Dictionary<string, int>();
            ReturnJson r = new ReturnJson() { s = "ok" };
            using (var db = new SHBTONLINEContext())
            {
                var parent = db.PlayItems.GroupBy(p => p.Loginname).ToList();
                parent.ForEach(p => {
                    var raw = p.Where(w => w.State == "成功").GroupBy(w => w.PlayID).Count();

                    var queryemp = db.userinfoes.Where(w => w.LoginName == p.Key).First().Name;
                    count.Add(queryemp, raw);
                });
                r.r = count.OrderByDescending(p=>p.Value);
            }
            return Json(r);
        }
        /// <summary>
        /// 获取营收排行【生涯】
        /// </summary>
        /// <returns></returns>
        public JsonResult GetSBRank()
        {
            Dictionary<string, int> count = new Dictionary<string, int>();
            ReturnJson r = new ReturnJson() { s = "ok" };
            using (var db = new SHBTONLINEContext())
            {
                var parent = db.PlayItems.Where(p=>p.Get!=null).GroupBy(p => p.Loginname).ToList();
                parent.ForEach(p => {
                    var sb = p.ToList();
                    int sbcount = 0;
                    for (int i=0;i<sb.Count;i++)
                    {
                        if (sb[i]!=null)
                        {
                            sbcount = sbcount + (int)sb[i].Get-(int)sb[i].Cost1;
                        }
                    }
                    var queryemp = db.userinfoes.Where(w => w.LoginName == p.Key).First().Name;
                    count.Add(queryemp, sbcount);
                });
                r.r = count.OrderByDescending(p => p.Value);
            }
            return Json(r);
        }       
        /// <summary>
                 /// 获取营收排行【今日】
                 /// </summary>
                 /// <returns></returns>
        public JsonResult GetSBRank2()
        {
            Dictionary<string, int> count = new Dictionary<string, int>();
            ReturnJson r = new ReturnJson() { s = "ok" };
            var now = DateTime.Now.Date;
            var tomorrow = now.AddDays(1);
            using (var db = new SHBTONLINEContext())
            {
                var parent = db.PlayItems.Where(p => p.Get != null&&p.CreateTime>= now&&p.CreateTime<tomorrow).GroupBy(p => p.Loginname).ToList();
                parent.ForEach(p => {
                    var sb = p.ToList();
                    int sbcount = 0;
                    for (int i = 0; i < sb.Count; i++)
                    {
                        if (sb[i] != null)
                        {
                            sbcount = sbcount + (int)sb[i].Get - (int)sb[i].Cost1;
                        }
                    }
                    var queryemp = db.userinfoes.Where(w => w.LoginName == p.Key).First().Name;
                    count.Add(queryemp, sbcount);
                });
                r.r = count.OrderByDescending(p => p.Value);
            }
            return Json(r);
        }
        /// <summary>
        /// 获取历史
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public JsonResult GetHistory(string ID)
        {
            ViewHistory his = new ViewHistory();
            ReturnJson r = new ReturnJson() { s = "ok" };
            using (var db = new SHBTONLINEContext())
            {
                var play = db.Plays.Where(p => p.ID == ID).First();
                his.Result = play.Results;
                List<History> hiss = new List<History>();
               var parent = db.PlayItems.Where(p=>p.PlayID==ID).ToList();
                parent.ForEach(p => {

                    History item = new History()
                    {
                        Cost1=p.Cost1,
                        CreateTime=p.CreateTime,
                        Get=p.Get,
                        ID=p.ID,
                        Loginname=p.Loginname,
                        PlayID=p.PlayID,
                        State=p.State
                    };
                    item.Name = db.userinfoes.Where(w => w.LoginName == item.Loginname).First().Name;
                    hiss.Add(item);
                });

                his.list = hiss;
                r.r = his;
            }
            return Json(r);
        }
        /// <summary>
        /// 比赛结束设置
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Result"></param>
        /// <returns></returns>
        public JsonResult MatchFinish(string ID,string Result)
        {
            ReturnJson r = new ReturnJson() { s = "ok" };
            using (var db = new SHBTONLINEContext()) 
            {
                try
                {
                    var play = db.Plays.Where(p => p.ID == ID).First();
                    var query = db.PlayItems.Where(p => p.PlayID == ID).ToList();
                    if (Result == "win")
                    {
                        play.Results = "中奖";
                    }
                    else
                    {
                        play.Results = "失败";
                    }
                    query.ForEach(p =>
                    {
                        if (Result == "win")
                        {
                            p.Get = (int)play.Odds * (p.Cost1 / 3);
                            p.State = "已结算";
                            var queryuser = db.userinfoes.Where(w => w.LoginName == p.Loginname).First();
                            queryuser.SCrrency += (int)p.Get;
                            db.userinfoes.Attach(queryuser);
                            db.Entry(queryuser).Property(w => w.SCrrency).IsModified = true;
                        }
                        else
                        {
                            p.Get = 0 - p.Cost1;
                            p.State = "已结算";
                        }
                        db.PlayItems.Attach(p);
                        db.Entry(p).Property(w => w.Get).IsModified = true;

                        db.Plays.Attach(play);
                        db.Entry(play).Property(w => w.Results).IsModified = true;

                    });
                    db.SaveChanges();

                }
                catch (Exception ex)
                {
                    r.s = "error";
                    r.r = ex.Message;
                }
                return Json(r);
            }
        }

        //public JsonResult AddMatch(AddPlay form)
        //{
        //    Plays play = new Plays()
        //    {

        //    };
        //}
        public ActionResult MatchManage()
        {
            return View();
        }
        [HttpGet]
        public JsonResult GetMatchManageList(int page,int limit)
        {
            uiPlayManage r = new uiPlayManage();
            r.code = 0;
            r.msg = "";
            using (var db = new SHBTONLINEContext())
            {
                var query = db.Plays.Where(p => p.Results == "未开赛" || p.Results == "进行中").Select(p => new PlayManage
                {
                    ID = p.ID,
                    Name = p.Name,
                    Odds = p.Odds,
                    OffTime = p.OffTime,
                    ParentID = p.ParentID,

                    Results = p.Results,
                    Status = p.Status
                });
                r.count = query.Count();
                r.data = query
                    .OrderBy(p=>p.OffTime)
                .Skip(page * (page - 1))
                .Take(limit).ToList();
                return Json(r,JsonRequestBehavior.AllowGet);
            }
        }
    }
}