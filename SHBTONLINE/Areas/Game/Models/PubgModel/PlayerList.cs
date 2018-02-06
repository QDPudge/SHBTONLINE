using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SHBTONLINE.Areas.Game.Models.PubgModel
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
        public string PubgID { get; set; }
        public string IMG { get; set; }
    }
}