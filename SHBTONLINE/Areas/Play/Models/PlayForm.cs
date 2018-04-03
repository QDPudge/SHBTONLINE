using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SHBTONLINE.Areas.Play.Models
{
    public class PlayForm
    {
        /// <summary>
        /// [ID]
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// [Name]
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// [Odds]
        /// </summary>
        public Nullable<double> Odds { get; set; }
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
        /// <summary>
        /// 子选项
        /// </summary>
        public virtual List<PlayForm> child { get; set; }
    }

    public class BuyPlayForm
    {
        public string PlayID { get; set; }
        public string Loginname { get; set; }
        public string Name { get; set; }
        public int Cost { get; set; }
    }
    public class ViewHistory
    {
        public string Result { get; set; }
        public List<History> list { get; set; }
    }
    public class History
    {
        /// <summary>
        /// [ID]
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// [PlayID]
        /// </summary>
        public string PlayID { get; set; }
        /// <summary>
        /// [Loginname]
        /// </summary>
        public string Loginname { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// [Cost1]
        /// </summary>
        public Nullable<int> Cost1 { get; set; }
        /// <summary>
        /// [Get]
        /// </summary>
        public Nullable<int> Get { get; set; }

        /// <summary>
        /// [CreateTime]
        /// </summary>
        public Nullable<System.DateTime> CreateTime { get; set; }
        /// <summary>
        /// [State]
        /// </summary>
        public string State { get; set; }
    }
    public class AddPlay
    {
        public string Name { get; set; }
        public List<AddPlayItem> Items { get; set; } 
    }
    public class AddPlayItem
    {
        public string Name { get; set; }
        public float Odds { get; set; }
        public DateTime OffTime { get; set; }
    }
    public class GridData<T>
    {
        public int code { get; set; }
        public string msg { get; set; }
        public int count { get; set; }
        public List<T> data { get; set; }
    }
    public class PlayManage
    {
        /// <summary>
        /// [ID]
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// [Name]
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// [Odds]
        /// </summary>
        public Nullable<double> Odds { get; set; }
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
                    return string.Format("<a href='javascript:void(0)' onClick =\"GO('{0}','win');\" class=\"btn btn-primary datagrid-row-btn\" >win</a>", this.ID) + string.Format("<a href='javascript:void(0)' onClick =\"GO('{0}','fail');\" class=\"btn btn-primary datagrid-row-btn\" >fail</a>", this.ID);


            }
        }
    }
}