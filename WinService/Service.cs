using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using TY.FrameWork;
using TY.FrameWork.ORM;

namespace NanJingMonitorDB
{
    public partial class Service : ServiceBase
    {
        private readonly System.Timers.Timer timer;
        public Service()
        {
            InitializeComponent();
            timer = new System.Timers.Timer();
            timer.Interval = 1000 * 60 * Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["time"]);  //设置计时器事件间隔执行时间，为一个小时
            timer.Elapsed += new System.Timers.ElapsedEventHandler(VideoSrv);//到达时间的时候执行事件；
            timer.AutoReset = true;//设置是执行一次（false）还是一直执行(true)； 
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
        private void ChkSrv(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                DateTime now = DateTime.Now;
                //每天除0-5点不更新数据
                if (now.Hour >= 6 && now.Hour < 24)
                {
                    //调用webService，数据逻辑处理
                    IList<RelTable> relList = TYDataQueryManager.GetInstance().QueryUseSql<RelTable>("select *  from  RelTable");
                    List<MonitorPointData> targetList = new List<MonitorPointData>();
                    //获取要更新数据ID
                    List<int> idArray = new List<int>();
                    //取历史的值
                    for (int i = 0; i <= Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["HistoryDays"]); i++)
                    {
                        //当前取值时间（一天）
                        DateTime dataTime = DateTime.Now.AddDays(-i);
                        string nowDayStart = dataTime.ToShortDateString() + " 00:00:00";
                        string nowDayEnd = dataTime.ToShortDateString() + " 06:00:00";
                        //循环关联表，取单个测点数据
                        foreach (RelTable relModel in relList)
                        {
                            //查找但个测点当前取值天的数据
                            IList<cjyData> sourseList = TYDataQueryManager.GetInstance().QueryUseSql<cjyData>("select * from cjyData where (IsHas is null or IsHas=0)  and time>'" + nowDayStart + "' and time<'" + nowDayEnd + "' and cjyID=" + relModel.ID + " order by data");
                            if (sourseList.Count > 0)
                            {
                                //取平均值
                                double dataAvg = 0;
                                if (sourseList.Count == 1)
                                {
                                    dataAvg = sourseList[0].data;
                                    idArray.Add(sourseList[0].ID);
                                }
                                else if (sourseList.Count == 2)
                                {
                                    dataAvg = Convert.ToSingle(Math.Round((sourseList[0].data + sourseList[1].data) / 2, 2));
                                    idArray.Add(sourseList[0].ID);
                                    idArray.Add(sourseList[1].ID);
                                }
                                else
                                {
                                    sourseList.ToList().ForEach(z =>
                                    {
                                        idArray.Add(z.ID);
                                    });
                                    double sum = 0;
                                    for (int j = 1; j < sourseList.Count - 1; j++)
                                    {
                                        sum += sourseList[j].data;
                                    }
                                    dataAvg = Convert.ToSingle(Math.Round(sum / (sourseList.Count - 2), 2));
                                }
                                //放入参数模板中
                                MonitorPointData resultModel = new MonitorPointData();
                                resultModel.MonitorValue = dataAvg;
                                resultModel.PointId = relModel.PointId;
                                resultModel.Time = dataTime.ToShortDateString() + " 06:00:00";
                                targetList.Add(resultModel);
                            }
                        }
                    }
                    if (targetList.Count > 0)
                    {
                        //序列化参数                
                        JavaScriptSerializer json = new JavaScriptSerializer();
                        string parme = json.Serialize(targetList);
                        //调用webservice
                        string Url = System.Configuration.ConfigurationManager.AppSettings["WebServiceUrl"];
                        string Ip = System.Configuration.ConfigurationManager.AppSettings["WebServiceIp"];
                        int Port = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["WebServicePort"]);
                        var args = new List<object>();
                        args.Add(parme);
                        var webServiceRequest = new WebServiceRequest
                        {
                            NameSpace = "TY.LightRail.Web.WebService",
                            Ip = Ip,
                            Port = Port,
                            Url = Url,
                            MethodName = "NanJingMonitorData",
                            Args = args.ToArray()
                        };
                        var objResponse = webServiceRequest.InvokeWebService();
                        ResultModel resultData = json.Deserialize<ResultModel>(objResponse.ToString());
                        if (resultData.s == "ok")
                        {//成功
                            string ids = string.Join(",", idArray.ToArray());
                            //更新数据源数据库
                            string strMainSql = "update cjyData set IsHas=1 where  ID in (" + ids + ")";
                            TYDataQueryManager.GetInstance().ExecuteSql(strMainSql);
                        }
                        else
                        {//失败写入日志
                            LogHelper.error("服务启动错误：调用WebService失败");
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                LogHelper.error("服务启动错误：" + ex.Message);
            }
        }
        private void VideoSrv(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                //获取页面刷新地址
                string RefreshUrl = System.Configuration.ConfigurationManager.AppSettings["SmsRefreshUrl"]; 
                if (string.IsNullOrEmpty(RefreshUrl))
                {
                    //记录错误日志
                    LogHelper.error("ConfigError：未配置通道刷新页面地址！");
                    return;
                }

                //更新通道状态
                ProcessStartInfo proc = new ProcessStartInfo();
                proc.FileName = RefreshUrl;
                proc.WindowStyle = ProcessWindowStyle.Hidden;
                Process.Start(RefreshUrl);
            }
            catch
            {
                LogHelper.error("更新监控视频在线状态失败！");
            }
        }
    }
}
