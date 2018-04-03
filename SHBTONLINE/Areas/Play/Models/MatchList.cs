using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SHBTONLINE.Areas.Play.Models
{
    /// <summary>
    /// 比赛列表
    /// </summary>
    public class MatchList
    {
        public string msg { get; set; }
        public string status { get; set; }
        public string version { get; set; }
        public List<Match> Result { get; set; }
    }
    public class Match
    {
        public string title { get; set; }
        public string progress_desc
        {
            get; set;
        }
        public decimal end_bid_time { get; set; }
        public Team team1_info { get; set; }
        public Team team2_info { get; set; }
        
    }
    public class Team
    {
        public string tag { get; set; }
        public logo logo { get; set; }
    }
    public class logo
    {
        public string url { get; set; }
    }

    public class MatchForm
    {
        /// <summary>
        /// [ID]
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// [Name]
        /// </summary>
        public string Name { get; set; }
        public string Player1 { get; set; }
        public string Player2 { get; set; }
        /// <summary>
        /// [ParentID]
        /// </summary>
        public string ParentID { get; set; }
        /// <summary>
        /// [OffTime]
        /// </summary>
        public Nullable<System.DateTime> OffTime { get; set; }

        /// <summary>
        /// [OffTime]
        /// </summary>
        public string OffTimeDis { get { return OffTime.HasValue ? OffTime.Value.ToString("yyyy/MM/dd HH:mm") : ""; } }
        /// <summary>
        /// [Status]
        /// </summary>
        public string Status { get; set; }
        public string Results { get; set; }
        public string OP
        {
            get
            {
                return string.Format("<a href='javascript:void(0)' onClick =\"GO2('{0}','{1}','{2}','{3}');\" class=\"btn btn-primary datagrid-row-btn\" >开始</a>", this.Name,this.Player1,this.Player2,this.OffTimeDis);


            }
        }
    }

    public class AddMatchForm
    {
        public string Name { get; set; }
        public string P1 { get; set; }
        public string P2 { get; set; }
        public string Sec { get; set; }
        public DateTime Time { get; set; }
    }
}