using Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SHBTONLINE.Areas.PersonOperation.Models.Attendance
{
    public class AttendModel
    {
        public List<AttendList> AttendList { get; set; }
    }
    /// <summary>
    /// 玩家信息
    /// </summary>
    public class AttendList
        {
        public string ID { get; set;}
        public string Name { get; set; }
         public Nullable<System.DateTime> AttendTime { get; set; }
    }
}