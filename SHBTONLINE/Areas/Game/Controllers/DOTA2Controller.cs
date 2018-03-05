using Data;
using Data.Domain;
using SHBTONLINE.Areas.Game.Models.DOTAModel;
using SHBTONLINE.Models.DOTA2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SHBTONLINE.Areas.Game.Controllers
{
    public class DOTA2Controller : Controller
    {
        // GET: Game/DOTA2
        public ActionResult DOTA2Index()
        {
            PlayerListForm mode = new PlayerListForm();
            using (var db = new SHBTONLINEContext())
            {
                var query = db.userinfoes.Where(p => !string.IsNullOrEmpty(p.DOTA2ID)).Select(p => new PlayerList
                {
                    Name = p.Name,
                    DOTA2ID = p.DOTA2ID,
                    IMG = p.IMG
                }).ToList();
                mode.PlayerList = query;
            }
            return View(mode);
        }
        public JsonResult GetUserInfo(string ID)
        {
            //实例化一个能够序列化数据的类
            JavaScriptSerializer js = new JavaScriptSerializer();
            var Url = "https://api.maxjia.com/api/player/summary/?steam_id=" + ID + "&game_type=dota2&max_id=1496154&imei=356156077945624&os_type=Android&os_version=7.0&version=4.1.5&lang=zh-cn";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "GET";
            request.Referer = " http://api.maxjia.com/";
            request.UserAgent = " Mozilla/5.0 AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2272.118 Safari/537.36 ApiMaxJia/1.0";
            //request.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);
            //CookieContainer cookies = new CookieContainer();
            //Cookie cookie = new Cookie ();
            //cookie.Domain= "hkey=f9c7e56369a53a415e65b11140ec192a";
            //Cookie cookie2 = new Cookie();
            //cookie2.Domain = "pkey=MTUwMzUwNTIzNS4yNF83NjcwNDVwYXlkcWN2YXpucGh6d2Nj";
            //cookies.Add(cookie);
            //request.CookieContainer = cookies;
            request.Headers.Add("Cookie: phone_num=0004060705030200070604;pkey=MTQ3Mjc0NzE5Ny45MzE1NzY0MjMxNjc1XzFxY2R3cnVtb3FobXJjcmxm;maxid=1496154 Connection: Keep - Alive  Accept - Encoding: gzip");
            request.Host = "api.maxjia.com";



            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            //var ifno = js.Deserialize<heibox>(retString);

            return Json(retString);
        }

        #region 服务调用接口
        [HttpGet]
        public void UpdateDota2Info()
        {
            List<string> IDS = new List<string>();
            using (var db=new SHBTONLINEContext())
            {
                IDS = db.userinfoes.Where(p => !string.IsNullOrEmpty(p.DOTA2ID)).Select(p => p.DOTA2ID).ToList();
            IDS.ForEach(p => {

                //实例化一个能够序列化数据的类
                JavaScriptSerializer js = new JavaScriptSerializer();
                var Url = "https://api.maxjia.com/api/player/summary/?steam_id=" + p + "&game_type=dota2&max_id=&imei=356156077945624&os_type=Android&os_version=7.0&version=4.1.5&lang=zh-cn";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.Method = "GET";
                request.Referer = " http://api.maxjia.com/";
                request.UserAgent = " Mozilla/5.0 AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2272.118 Safari/537.36 ApiMaxJia/1.0";
                //request.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);
                //CookieContainer cookies = new CookieContainer();
                //Cookie cookie = new Cookie ();
                //cookie.Domain= "hkey=f9c7e56369a53a415e65b11140ec192a";
                //Cookie cookie2 = new Cookie();
                //cookie2.Domain = "pkey=MTUwMzUwNTIzNS4yNF83NjcwNDVwYXlkcWN2YXpucGh6d2Nj";
                //cookies.Add(cookie);
                //request.CookieContainer = cookies;
                request.Headers.Add("Cookie: phone_num=0004060705030200070604;pkey=MTQ3Mjc0NzE5Ny45MzE1NzY0MjMxNjc1XzFxY2R3cnVtb3FobXJjcmxm; Connection: Keep - Alive  Accept - Encoding: gzip");
                request.Host = "api.maxjia.com";



                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                string retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();

                var ifno = js.Deserialize<maxjia>(retString);
                if (ifno.result.power_stats.r20 != null)
                {
                var querydota = db.DOTA2Info.Where(w => w.DOTA2ID == p).FirstOrDefault();
                if (querydota==null)
                {

                    DOTA2Info info = new DOTA2Info()
                    {
                        ID=Guid.NewGuid().ToString(),
                        DOTA2ID=p,
                        pv_all=ifno.result.power_stats.r20.pv_all,
                        d= ifno.result.power_stats.r20.d,
                        damage= ifno.result.power_stats.r20.damage,
                        gpm= ifno.result.power_stats.r20.gpm,
                        kda= ifno.result.power_stats.r20.kda,
                        last_hits= ifno.result.power_stats.r20.last_hits,
                        match_count= ifno.result.power_stats.r20.match_count,
                        pv_damage= ifno.result.power_stats.r20.pv_damage,
                        pv_deatch= ifno.result.power_stats.r20.pv_deatch,
                        pv_growth= ifno.result.power_stats.r20.pv_growth,
                        pv_kda= ifno.result.power_stats.r20.pv_kda,
                        pv_tower= ifno.result.power_stats.r20.pv_tower,
                        win_rate= ifno.result.power_stats.r20.win_rate,
                        xpm= ifno.result.power_stats.r20.xpm
                    };
                    db.DOTA2Info.Add(info);
                }
                else
                {
                    querydota.pv_all = ifno.result.power_stats.r20.pv_all;
                    querydota.d = ifno.result.power_stats.r20.d;
                    querydota.kda = ifno.result.power_stats.r20.kda;
                    querydota.damage = ifno.result.power_stats.r20.damage;
                    querydota.gpm = ifno.result.power_stats.r20.gpm;
                    querydota.match_count = ifno.result.power_stats.r20.match_count;
                    querydota.pv_damage = ifno.result.power_stats.r20.pv_damage;
                    querydota.pv_deatch = ifno.result.power_stats.r20.pv_deatch;
                    querydota.pv_growth = ifno.result.power_stats.r20.pv_growth;
                    querydota.pv_kda = ifno.result.power_stats.r20.pv_kda;
                    querydota.pv_tower = ifno.result.power_stats.r20.pv_tower;
                    querydota.win_rate = ifno.result.power_stats.r20.win_rate;
                    querydota.xpm = ifno.result.power_stats.r20.xpm;
                    db.Entry<DOTA2Info>(querydota).State = System.Data.Entity.EntityState.Modified;
                }

                db.SaveChanges();

                }

            });

            }
        }
        #endregion
    }
}