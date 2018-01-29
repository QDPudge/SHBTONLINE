using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SHBTONLINE.Models.PUBG
{

    public class heibox
    {
        public string msg { get; set; }
        public string status { get; set; }
        public string version { get; set; }
        public result result { get; set; }
    }
    public class result
    {
        public List<calendar> calendar { get; set; }

        public List<career> career { get; set; }

        public List<match> matches { get; set; }

        public string download_url { get; set; }
        public string follow_state { get; set; }
        public string fpp_default { get; set; }
        public List<normal_score_detail> normal_score_detail { get; set; }
        public overview normal_score_round { get; set; }
        public player_info player_info { get; set; }
        public overview player_rank_round { get; set; }
        public overview radar_score { get; set; }
        public string rating { get; set; }
        public string rating_mode_img { get; set; }
        public string rating_name { get; set; }
        public string rating_rank { get; set; }
        public string rating_rank_name { get; set; }
    }

    public class calendar
    {
        public int match_count { get; set; }
        public string start { get; set; }
        public string tag { get; set; }
        public string time { get; set; }
    }
    public class overview
    {
        public string desc { get; set; }
        public string value { get; set; }
        public string rank { get; set; }
        public string percent { get; set; }
    }
    public class career
    {
        public List<overview> overview { get; set; }
    }
    public class match
    {
        public int d { get; set; }
        public int k { get; set; }
        public double kd { get; set; }
        public int match_count { get; set; }
        public string mode { get; set; }
        public string mode_img { get; set; }
        public string rating { get; set; }
        public string rating_diff { get; set; }
        public string rating_trend { get; set; }
        public string record_time { get; set; }
        public string region { get; set; }
        public string region_desc { get; set; }
        public string score { get; set; }
        public string season { get; set; }
        public string season_desc { get; set; }
        public string tag { get; set; }
        public string tag_desc { get; set; }
        public string time { get; set; }
    }
    public class normal_score_detailinfo
    {
        public List<normal_score_detail> normal_score_detail { get; set; }
    }
    public class normal_score_detail
    {
        public string c { get; set; }
        public string color { get; set; }
        public string desc { get; set; }
        public string mode { get; set; }
        public string value { get; set; }
    }
    public class player_info
    {
        public string nickname { get; set; }
    }
    public class stats
    {
        public List<stats_mode> modes { get; set; }
        public string this_season { get; set; }
    }
    public class stats_mode
    {
        public string match_count { get; set; }
        public string mode { get; set; }
        public string mode_desc { get; set; }
        public string mode_img { get; set; }
        public string region { get; set; }
        public string season { get; set; }
        public List<overview> values { get; set; }
    }
}