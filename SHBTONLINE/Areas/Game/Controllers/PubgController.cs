using SHBTONLINE.Models.PUBG;
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
    public class PubgController : Controller
    {
        // GET: Game/Pubg
        public ActionResult PubgIndex()
        {
            return View();
        }
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
       
            return Json(retString);
        }
    }
}