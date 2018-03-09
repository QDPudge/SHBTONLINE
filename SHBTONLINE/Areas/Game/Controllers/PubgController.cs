using SHBTONLINE.Models.PUBG;
using Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using SHBTONLINE.Areas.Game.Models.PubgModel;
using Data.Domain;
using CommonData;
using SHBTONLINE.Areas.Game.Models.DOTAModel;

namespace SHBTONLINE.Areas.Game.Controllers
{
    public class PubgController : Controller
    {
        //public SHBTONLINEContext db;
        // GET: Game/Pubg
        public ActionResult PubgIndex()
        {
            Models.PubgModel.PlayerListForm mode = new Models.PubgModel.PlayerListForm();
            using (var db = new SHBTONLINEContext() )
            {
                var query = db.userinfoes.Where(p => !string.IsNullOrEmpty(p.PubgID)).Select(p=>new Models.PubgModel.PlayerList
                {
                    Name=p.Name,
                    PubgID=p.PubgID,
                    IMG=p.IMG
                }).ToList();
                mode.PlayerList = query;
            }
            return View(mode);
        }
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="time"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        public JsonResult GetUserInfo(string time,string Name)
        {
            //实例化一个能够序列化数据的类
            JavaScriptSerializer js = new JavaScriptSerializer();
            var Url = "https://api.xiaoheihe.cn/game/pubg/get_player_overview/?nickname="+ Name+"&region=&season=&heybox_id=767045&imei=356156077945624&os_type=Android&os_version=7.0&version=1.1.14&_time=" + time;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "GET";
            request.UserAgent = " Mozilla/5.0 AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2272.118 Safari/537.36 ApiMaxJia/1.0";
            //request.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);
            //CookieContainer cookies = new CookieContainer();
            //Cookie cookie = new Cookie ();
            //cookie.Domain= "hkey=f9c7e56369a53a415e65b11140ec192a";
            //Cookie cookie2 = new Cookie();
            //cookie2.Domain = "pkey=MTUwMzUwNTIzNS4yNF83NjcwNDVwYXlkcWN2YXpucGh6d2Nj";
            //cookies.Add(cookie);
            //request.CookieContainer = cookies;
            request.Headers.Add("Cookie: hkey = f9c7e56369a53a415e65b11140ec192a; pkey = MTUwMzUwNTIzNS4yNF83NjcwNDVwYXlkcWN2YXpucGh6d2Nj  Connection: Keep - Alive  Accept - Encoding: gzip");
            request.Host = "api.xiaoheihe.cn";

            

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            var ifno = js.Deserialize<heibox>(retString);
            using (var db=new SHBTONLINEContext())
            {
                var query = db.userinfoes.Where(p => p.PubgID == Name).FirstOrDefault();
                if (query!=null&&query.IMG!= ifno.result.player_info.avatar)
                {
                    query.IMG = ifno.result.player_info.avatar;
                    db.userinfoes.Attach(query);
                    db.Entry(query).Property(x => x.IMG).IsModified = true;
                    db.SaveChanges();
                }
            }
                return Json(retString);
        }

        /// <summary>
        /// 获取排名
        /// </summary>
        /// <returns></returns>
        public JsonResult GetRanking()
        {
            ReturnJson r = new ReturnJson() { s = "ok" };
            List<RankList> result = new List<RankList>();
            var list = new List<string> { "KD","吃鸡数", "场均击杀", "吃鸡率", "前十率", "爆头率","场均伤害","存活时间", "场均移动距离"};
            int i = 1;
            using (var db = new SHBTONLINEContext())
            {
                var queryall = db.PUBGInfoes.ToList();
                var player = queryall.Select(p => p.PUBGID).ToList();
                var userinfo = db.userinfoes.Where(p => player.Contains(p.PubgID)).ToList();
                list.ForEach(p => {
                    switch (p)
                    {
                        #region KD
                        case "KD":
                            var items = queryall.OrderByDescending(w => w.KD).ToList();
                            var kdalist = new RankList();
                            kdalist.Name = "KD";
                            items.ForEach(pp => {
                                Rank rank = new Rank()
                                {
                                    Value = pp.KD.ToString(),
                                    Sort = i
                                };
                                i++;
                                var user = userinfo.Find(w => w.PubgID == pp.PUBGID);
                                rank.Owner = user.Name;
                                rank.Avatar = user.IMG;
                                kdalist.list.Add(rank);
                            });
                            result.Add(kdalist);
                            break;
                        #endregion\
                        #region 吃鸡数
                        case "吃鸡数":
                            i = 1;
                            var items2 = queryall.OrderByDescending(w => w.chicken_count).ToList();
                            var damagelist = new RankList();
                            damagelist.Name = "吃鸡数";
                            items2.ForEach(pp => {
                                Rank rank = new Rank()
                                {
                                    Value = pp.chicken_count.ToString(),
                                    Sort = i
                                };
                                i++;
                                var user = userinfo.Find(w => w.PubgID == pp.PUBGID);
                                rank.Owner = user.Name;
                                rank.Avatar = user.IMG;
                                damagelist.list.Add(rank);
                            });
                            result.Add(damagelist);
                            break;
                        #endregion
                        #region 场均击杀
                        case "场均击杀":
                            i = 1;
                            var items3 = queryall.OrderByDescending(w => w.kill_count).ToList();
                            var goldlist = new RankList();
                            goldlist.Name = "场均击杀";
                            items3.ForEach(pp => {
                                Rank rank = new Rank()
                                {
                                    Value = pp.kill_count.ToString(),
                                    Sort = i
                                };
                                i++;
                                var user = userinfo.Find(w => w.PubgID == pp.PUBGID);
                                rank.Owner = user.Name;
                                rank.Avatar = user.IMG;
                                goldlist.list.Add(rank);
                            });
                            result.Add(goldlist);
                            break;
                        #endregion
                        #region 吃鸡率
                        case "吃鸡率":
                            i = 1;
                            var items4 = queryall.OrderByDescending(w => w.chicken_rate).ToList();
                            var xpmist = new RankList();
                            xpmist.Name = "吃鸡率";
                            items4.ForEach(pp => {
                                Rank rank = new Rank()
                                {
                                    Value = pp.chicken_rate.ToString(),
                                    Sort = i
                                };
                                i++;
                                var user = userinfo.Find(w => w.PubgID == pp.PUBGID);
                                rank.Owner = user.Name;
                                rank.Avatar = user.IMG;
                                xpmist.list.Add(rank);
                            });
                            result.Add(xpmist);
                            break;
                        #endregion
                        #region 前十率
                        case "前十率":
                            i = 1;
                            var items5 = queryall.OrderByDescending(w => w.ten_rate).ToList();
                            var winlist = new RankList();
                            winlist.Name = "前十率";
                            items5.ForEach(pp => {
                                Rank rank = new Rank()
                                {
                                    Value = pp.ten_rate.ToString(),
                                    Sort = i
                                };
                                i++;
                                var user = userinfo.Find(w => w.PubgID == pp.PUBGID);
                                rank.Owner = user.Name;
                                rank.Avatar = user.IMG;
                                winlist.list.Add(rank);
                            });
                            result.Add(winlist);
                            break;
                        #endregion
                        #region 爆头率
                        case "爆头率":
                            i = 1;
                            var items6 = queryall.OrderByDescending(w => w.headshoot_rate).ToList();
                            var headlist = new RankList();
                            headlist.Name = "爆头率";
                            items6.ForEach(pp => {
                                Rank rank = new Rank()
                                {
                                    Value = pp.headshoot_rate.ToString(),
                                    Sort = i
                                };
                                i++;
                                var user = userinfo.Find(w => w.PubgID == pp.PUBGID);
                                rank.Owner = user.Name;
                                rank.Avatar = user.IMG;
                                headlist.list.Add(rank);
                            });
                            result.Add(headlist);
                            break;
                        #endregion
                        #region 场均伤害
                        case "场均伤害":
                            i = 1;
                            var items7 = queryall.OrderByDescending(w => w.damage).ToList();
                            var damagelist1 = new RankList();
                            damagelist1.Name = "场均伤害";
                            items7.ForEach(pp => {
                                Rank rank = new Rank()
                                {
                                    Value = pp.damage.ToString(),
                                    Sort = i
                                };
                                i++;
                                var user = userinfo.Find(w => w.PubgID == pp.PUBGID);
                                rank.Owner = user.Name;
                                rank.Avatar = user.IMG;
                                damagelist1.list.Add(rank);
                            });
                            result.Add(damagelist1);
                            break;
                        #endregion
                        #region 存活时间
                        case "存活时间":
                            i = 1;
                            var items8 = queryall.OrderByDescending(w => w.livetime).ToList();
                            var timelist = new RankList();
                            timelist.Name = "存活时间";
                            items8.ForEach(pp => {
                                Rank rank = new Rank()
                                {
                                    Value = pp.livetime.ToString(),
                                    Sort = i
                                };
                                i++;
                                var user = userinfo.Find(w => w.PubgID == pp.PUBGID);
                                rank.Owner = user.Name;
                                rank.Avatar = user.IMG;
                                timelist.list.Add(rank);
                            });
                            result.Add(timelist);
                            break;
                        #endregion
                        #region 场均移动距离
                        case "场均移动距离":
                            i = 1;
                            var items9 = queryall.OrderByDescending(w => w.run).ToList();
                            var runlist = new RankList();
                            runlist.Name = "场均移动距离";
                            items9.ForEach(pp => {
                                Rank rank = new Rank()
                                {
                                    Value = pp.run.ToString(),
                                    Sort = i
                                };
                                i++;
                                var user = userinfo.Find(w => w.PubgID == pp.PUBGID);
                                rank.Owner = user.Name;
                                rank.Avatar = user.IMG;
                                runlist.list.Add(rank);
                            });
                            result.Add(runlist);
                            break;
                        #endregion
                        default: break;
                    }

                });

            }
            r.r = result;
            return Json(r);
        }

        #region 服务调用接口
        [HttpGet]
        public void UpdatePubgInfo()
        {

            var ts = Convert.ToInt64((DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds/1000).ToString();
            List<string> IDS = new List<string>();
            using (var db = new SHBTONLINEContext())
            {
                IDS = db.userinfoes.Where(p => !string.IsNullOrEmpty(p.PubgID)).Select(p => p.PubgID).ToList();
                IDS.ForEach(p => {

                    //实例化一个能够序列化数据的类
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    var Url = "https://api.xiaoheihe.cn/game/pubg/get_player_overview/?nickname=" + p + "&region=&season=&heybox_id=767045&imei=356156077945624&os_type=Android&os_version=7.0&version=1.1.14&_time="+ ts;
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                    request.Method = "GET";
                    request.UserAgent = " Mozilla/5.0 AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2272.118 Safari/537.36 ApiMaxJia/1.0";
                    //request.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);
                    //CookieContainer cookies = new CookieContainer();
                    //Cookie cookie = new Cookie ();
                    //cookie.Domain= "hkey=f9c7e56369a53a415e65b11140ec192a";
                    //Cookie cookie2 = new Cookie();
                    //cookie2.Domain = "pkey=MTUwMzUwNTIzNS4yNF83NjcwNDVwYXlkcWN2YXpucGh6d2Nj";
                    //cookies.Add(cookie);
                    //request.CookieContainer = cookies;
                    request.Headers.Add("Cookie: hkey = f9c7e56369a53a415e65b11140ec192a; pkey = MTUwMzUwNTIzNS4yNF83NjcwNDVwYXlkcWN2YXpucGh6d2Nj  Connection: Keep - Alive  Accept - Encoding: gzip");
                    request.Host = "api.xiaoheihe.cn";



                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    Stream myResponseStream = response.GetResponseStream();
                    StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                    string retString = myStreamReader.ReadToEnd();
                    myStreamReader.Close();
                    myResponseStream.Close();

                    var ifno = js.Deserialize<heibox>(retString);
                    if (ifno.result.career != null)
                    {
                        var querypubg = db.PUBGInfoes.Where(w => w.PUBGID == p).FirstOrDefault();
                        if (querypubg == null)
                        {

                            PUBGInfo info = new PUBGInfo()
                            {
                                ID = Guid.NewGuid().ToString(),
                                PUBGID = p,
                            };
                            info.chicken_count = Convert.ToInt32(ifno.result.career.overview[1].value);
                            info.KD = Convert.ToDecimal(ifno.result.career.overview[3].value);
                            info.kill_count = Convert.ToDecimal(ifno.result.career.overview[4].value);
                            info.chicken_rate = Convert.ToDecimal(ifno.result.career.overview[5].value.Replace("%",""));
                            info.ten_rate = Convert.ToDecimal(ifno.result.career.overview[6].value.Replace("%", ""));
                            info.headshoot_rate = Convert.ToDecimal(ifno.result.career.overview[7].value.Replace("%", ""));
                            info.damage = Convert.ToDecimal(ifno.result.career.overview[10].value);
                            info.livetime = Convert.ToDecimal(ifno.result.career.overview[11].value.Replace(" min", ""));
                            info.run = Convert.ToDecimal(ifno.result.career.overview[12].value.Replace(" km", ""));
                            db.PUBGInfoes.Add(info);
                        }
                        else
                        {
                            querypubg.chicken_count = Convert.ToInt32(ifno.result.career.overview[1].value);
                            querypubg.KD = Convert.ToDecimal(ifno.result.career.overview[3].value);
                            querypubg.kill_count = Convert.ToDecimal(ifno.result.career.overview[4].value);
                            querypubg.chicken_rate = Convert.ToDecimal(ifno.result.career.overview[5].value.Replace("%", ""));
                            querypubg.ten_rate = Convert.ToDecimal(ifno.result.career.overview[6].value.Replace("%", ""));
                            querypubg.headshoot_rate = Convert.ToDecimal(ifno.result.career.overview[7].value.Replace("%", ""));
                            querypubg.damage = Convert.ToDecimal(ifno.result.career.overview[10].value);
                            querypubg.livetime = Convert.ToDecimal(ifno.result.career.overview[11].value.Replace(" min", ""));
                            querypubg.run = Convert.ToDecimal(ifno.result.career.overview[12].value.Replace(" km", ""));
                            db.Entry<PUBGInfo>(querypubg).State = System.Data.Entity.EntityState.Modified;
                        }

                        db.SaveChanges();

                    }

                });

            }
        }
        #endregion
    }
}