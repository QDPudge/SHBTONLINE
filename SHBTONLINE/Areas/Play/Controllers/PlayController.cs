using CommonData;
using Data;
using Data.Domain;
using Newtonsoft.Json;
using SHBTONLINE.Areas.Play.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

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
                            p.Get = 0;
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
            GridData<PlayManage> r = new GridData<PlayManage>();
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

        [HttpGet]
        public JsonResult GetNewMatchInfo(int page, int limit)
        {
            //实例化一个能够序列化数据的类
            JavaScriptSerializer js = new JavaScriptSerializer();
            var start = 0;
            var end = 30;
            for (var i=1;i<page;i++)
            {
                start = +30;
                end = +30;
            }
            
            var Url = "https://api.maxjia.com/api/bets/get_all_category/3/?&offset="+start+"&limit="+end+"&bet_type=all&max_id=8789444&game_type=dota2&imei=356156077945624&os_type=Android&os_version=7.0&version=4.2.1&lang=zh-cn";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "GET";
            request.Referer = " http://api.maxjia.com/";
            request.UserAgent = " Mozilla/5.0 AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2272.118 Safari/537.36 ApiMaxJia/1.0";
            request.Headers.Add("Cookie: phone_num=0006070300040101040800;pkey=MTUyMTkwOTU4My43MTE3NjIxNTAwNTkxXzFhbWhzZXp1Z2RwY3lybG93;maxid=8789444; Connection: Keep - Alive  Accept - Encoding: gzip");
            request.Host = "api.maxjia.com";



            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            List<MatchForm> list = new List<MatchForm>();
            var ifno = js.Deserialize<MatchList>(retString);
            ifno.Result.ForEach(p => {
                MatchForm form = new MatchForm()
                {
                    Name=p.team1_info.tag+" VS "+p.team2_info.tag+"【"+p.title+"】",
                    Status=p.progress_desc,
                    Player1=p.team1_info.tag,
                    Player2= p.team2_info.tag
                };
                long jsTimeStamp =Convert.ToInt64( p.end_bid_time);
                System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区
                form.OffTime  = startTime.AddMilliseconds(jsTimeStamp*1000);

                list.Add(form);
            });
            GridData<MatchForm> r = new GridData<MatchForm>();
            r.count = list.Count();
            r.data = list;
            //var result= JsonConvert.SerializeObject(ifno.Result);

            return Json(r, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddMatch(string Name,string P1,string P2,DateTime Time)
        {
            AddMatchForm form = new AddMatchForm()
            {
                Name = Name,
                P1 = P1,
                P2 = P2,
                Time=Time
            };
            return View(form);
        }
        public JsonResult SaveAddMatch(AddMatchForm form)
        {
            ReturnJson r = new ReturnJson();
            using (var db=new SHBTONLINEContext()) {
                Data.Domain.Play play = new Data.Domain.Play()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = form.Name,
                    OffTime = form.Time.AddMinutes(10),
                    Status = "正常",
                };
                db.Plays.Add(play);
                Data.Domain.Play item1 = new Data.Domain.Play() {
                    ID = Guid.NewGuid().ToString(),
                    Name=form.P1 + "胜利",
                    Odds =form.Odds1,
                    OffTime = form.Time.AddMinutes(10),
                    Results="进行中",
                    Status="正常",
                    ParentID=play.ID
                };
                db.Plays.Add(item1);
                Data.Domain.Play item2 = new Data.Domain.Play()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = form.P2 + "胜利",
                    Odds = form.Odds2,
                    OffTime = form.Time.AddMinutes(10),
                    Results = "进行中",
                    Status = "正常",
                    ParentID = play.ID
                };
                db.Plays.Add(item2);
                switch (form.Sec)
                {
                    case "NBA大小分":
                        Data.Domain.Play item3 = new Data.Domain.Play()
                        {
                            ID = Guid.NewGuid().ToString(),
                            Name = "总分大于225",
                            Odds = form.Odds1,
                            OffTime = form.Time.AddMinutes(10),
                            Results = "进行中",
                            Status = "正常",
                            ParentID = play.ID
                        };
                        db.Plays.Add(item3);
                        Data.Domain.Play item4 = new Data.Domain.Play()
                        {
                            ID = Guid.NewGuid().ToString(),
                            Name = "总分小于225",
                            Odds = form.Odds2,
                            OffTime = form.Time.AddMinutes(10),
                            Results = "进行中",
                            Status = "正常",
                            ParentID = play.ID
                        };
                        db.Plays.Add(item4);
                        break;
                    case "足球大小球":
                        Data.Domain.Play item13 = new Data.Domain.Play()
                        {
                            ID = Guid.NewGuid().ToString(),
                            Name = "总得分大于3",
                            Odds = form.Odds1,
                            OffTime = form.Time.AddMinutes(10),
                            Results = "进行中",
                            Status = "正常",
                            ParentID = play.ID
                        };
                        db.Plays.Add(item13);
                        Data.Domain.Play item14 = new Data.Domain.Play()
                        {
                            ID = Guid.NewGuid().ToString(),
                            Name = "总得分小于于3",
                            Odds = form.Odds2,
                            OffTime = form.Time.AddMinutes(10),
                            Results = "进行中",
                            Status = "正常",
                            ParentID = play.ID
                        };
                        db.Plays.Add(item14);
                        break;
                        
                    case "十杀":
                        Data.Domain.Play item5 = new Data.Domain.Play()
                        {
                            ID = Guid.NewGuid().ToString(),
                            Name = form.P1 + "十杀",
                            Odds = form.Odds3,
                            OffTime = form.Time.AddMinutes(10),
                            Results = "进行中",
                            Status = "正常",
                            ParentID = play.ID
                        };
                        db.Plays.Add(item5);
                        Data.Domain.Play item6 = new Data.Domain.Play()
                        {
                            ID = Guid.NewGuid().ToString(),
                            Name = form.P2 + "十杀",
                            Odds = form.Odds4,
                            OffTime = form.Time.AddMinutes(10),
                            Results = "进行中",
                            Status = "正常",
                            ParentID = play.ID
                        };
                        db.Plays.Add(item6);
                        break;
                    case "一血":
                        Data.Domain.Play item11 = new Data.Domain.Play()
                        {
                            ID = Guid.NewGuid().ToString(),
                            Name = form.P1 + "一血",
                            Odds = form.Odds3,
                            OffTime = form.Time.AddMinutes(10),
                            Results = "进行中",
                            Status = "正常",
                            ParentID = play.ID
                        };
                        db.Plays.Add(item11);
                        Data.Domain.Play item12 = new Data.Domain.Play()
                        {
                            ID = Guid.NewGuid().ToString(),
                            Name = form.P2 + "一血",
                            Odds = form.Odds4,
                            OffTime = form.Time.AddMinutes(10),
                            Results = "进行中",
                            Status = "正常",
                            ParentID = play.ID
                        };
                        db.Plays.Add(item12);
                        break;
                    case "领先4分":
                        Data.Domain.Play item7 = new Data.Domain.Play()
                        {
                            ID = Guid.NewGuid().ToString(),
                            Name = form.P1 + "胜利",
                            Odds = form.Odds3,
                            OffTime = form.Time.AddMinutes(10),
                            Results = "进行中",
                            Status = "正常",
                            ParentID = play.ID
                        };
                        db.Plays.Add(item7);
                        Data.Domain.Play item8 = new Data.Domain.Play()
                        {
                            ID = Guid.NewGuid().ToString(),
                            Name = form.P2 + "胜利",
                            Odds = form.Odds4,
                            OffTime = form.Time.AddMinutes(10),
                            Results = "进行中",
                            Status = "正常",
                            ParentID = play.ID
                        };
                        db.Plays.Add(item8);
                        break;
                }
                db.SaveChanges();
            }

            return Json(r);
        }
    }
}