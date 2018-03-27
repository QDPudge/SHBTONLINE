using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace NanJingMonitorDB
{
    public partial class Service : ServiceBase
    {
        private readonly System.Timers.Timer timer;
        private readonly System.Timers.Timer timer2;
        public Service()
        {
            InitializeComponent();
            timer = new System.Timers.Timer();
            timer.Interval = 1000 * 60 * Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["time"]);  //设置计时器事件间隔执行时间，为一个小时
            timer.Elapsed += new System.Timers.ElapsedEventHandler(DataSrv);//到达时间的时候执行事件；
            timer.AutoReset = true;//设置是执行一次（false）还是一直执行(true)； 
            timer2 = new System.Timers.Timer();
            timer2.Interval = 1000 * 60 * Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["time"]);  //设置计时器事件间隔执行时间，为一个小时
            timer2.Elapsed += new System.Timers.ElapsedEventHandler(DataSrv2);//到达时间的时候执行事件；
            timer2.AutoReset = true;//设置是执行一次（false）还是一直执行(true)； 
        }

        protected override void OnStart(string[] args)
        {
            timer.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件； 
            LogHelper.info("服务启动：" + DateTime.Now.ToString());
        }

        protected override void OnStop()
        {
            this.timer.Enabled = false;
        }
        private void DataSrv(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                //实例化一个能够序列化数据的类
                string RefreshUrl = System.Configuration.ConfigurationManager.AppSettings["DOTA2URL"];
                var Url = RefreshUrl;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.Method = "GET";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                string retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();

                string RefreshUrl2 = System.Configuration.ConfigurationManager.AppSettings["PUBGURL"];
                var Url2 = RefreshUrl2;
                HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create(Url);
                request2.Method = "GET";
                HttpWebResponse response2 = (HttpWebResponse)request2.GetResponse();
                Stream myResponseStream2 = response2.GetResponseStream();
                StreamReader myStreamReader2 = new StreamReader(myResponseStream2, Encoding.GetEncoding("utf-8"));
                string retString2= myStreamReader2.ReadToEnd();
                myStreamReader2.Close();
                myResponseStream.Close();

                //var ifno = js.Deserialize<heibox>(retString);

                //return Json(retString);
            }
            catch (Exception ex)
            {
                LogHelper.error("服务启动错误：" + ex.Message);
            }
        }
        private void DataSrv2(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                //实例化一个能够序列化数据的类
                string RefreshUrl2 = System.Configuration.ConfigurationManager.AppSettings["PUBGURL"];
                var Url2 = RefreshUrl2;
                HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create(Url2);
                request2.Method = "GET";
                HttpWebResponse response2 = (HttpWebResponse)request2.GetResponse();
                Stream myResponseStream2 = response2.GetResponseStream();
                StreamReader myStreamReader2 = new StreamReader(myResponseStream2, Encoding.GetEncoding("utf-8"));
                string retString2 = myStreamReader2.ReadToEnd();
                myStreamReader2.Close();
                myResponseStream2.Close();

                //var ifno = js.Deserialize<heibox>(retString);

                //return Json(retString);
            }
            catch (Exception ex)
            {
                LogHelper.error("服务启动错误：" + ex.Message);
            }
        }
    }
}
