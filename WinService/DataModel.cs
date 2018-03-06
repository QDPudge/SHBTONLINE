using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanJingMonitorDB
{
    public class cjyData
    {
        public int ID { get; set; }
        public DateTime time { get; set; }
        public int cjyID { get; set; }
        public double data { get; set; }
        public int IsHas { get; set; }

    }
    public class RelTable
    {
        public int ID { get; set; }
        public Guid PointId { get; set; }

    }
    public class MonitorPointData
    {
        public int ID { get; set; }
        public string Time { get; set; }
        public Guid PointId { get; set; }
        public double MonitorValue { get; set; }

    }
    public class ResultModel
    {
        public string s { get; set; }
        public string v { get; set; }
    }

}
