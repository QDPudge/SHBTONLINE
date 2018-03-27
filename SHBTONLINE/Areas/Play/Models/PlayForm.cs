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
        /// <summary>
        /// 子选项
        /// </summary>
        public virtual List<PlayForm> child { get; set; }
    }

    public class BuyPlayForm
    {
        public string PlayID { get; set; }
        public string Loginname { get; set; }
        public int Cost { get; set; }
    }
}