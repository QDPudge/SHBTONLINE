using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SHBTONLINE.Areas.Game.Models.DOTAModel
{
    public class PlayerListForm
    {
        public List<PlayerList> PlayerList { get; set; }
    }
    /// <summary>
    /// 玩家信息
    /// </summary>
    public class PlayerList
    {
        public string Name { get; set; }
        public string DOTA2ID { get; set; }
        public string IMG { get; set; }
    }
    public class RankList
    {
        public RankList()
        {
            list = new List<Rank>();
        }
        public string Name { get; set; }
        public List<Rank> list { get; set; }
    }
    public class Rank
    {
        public string Value { get; set; }
        public string Owner { get; set; }
        public int Sort { get; set; }
    }
}