using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SHBTONLINE.Models.DOTA2
{
    public class maxjia
    {
        public string msg { get; set; }
        public result result { get; set; }
        public string status { get; set; }
        public string version { get; set; }
    }
    public class result
    {
        public power_statslist power_stats { get; set; }
    }
    public class power_statslist
    {
        public power_stats all { get; set; }
        public power_stats r100 { get; set; }
        public power_stats r20 { get; set; }
    }
    public class power_stats
    {
        public decimal d { get; set; }
        public decimal damage { get; set; }
        public decimal gpm { get; set; }
        public decimal kda { get; set; }
        public decimal last_hits { get; set; }
        public int match_count { get; set; }
        public decimal pv_all { get; set; }
        public decimal pv_damage { get; set; }
        public decimal pv_deatch { get; set; }
        public decimal pv_growth { get; set; }
        public decimal pv_kda { get; set; }
        public decimal pv_tower { get; set; }
        public decimal win_rate { get; set; }
        public decimal xpm { get; set; }
    }
}