

using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Data.Domain;
using Data.DomainMap;

namespace Data
{
    public partial class SHBTONLINEContext : DbContext
    {
        static SHBTONLINEContext()
        {
            Database.SetInitializer<SHBTONLINEContext>(null);
        }

        public SHBTONLINEContext()
            : base("Name=SHBTONLINEContext")
        {
        }

        /// <summary>
        /// [userinfo]
        /// </summary>
        public DbSet<userinfo> userinfoes { get; set; }


        /// <summary>
        /// [DOTA2Info]
        /// </summary>
        public DbSet<DOTA2Info> DOTA2Info { get; set; }
        /// <summary>
        /// [PUBGInfo]
        /// </summary>
        public DbSet<PUBGInfo> PUBGInfoes { get; set; }
        /// <summary>
        /// [Goodsinfo]
        /// </summary>
        public DbSet<Goodsinfo> Goodsinfoes { get; set; }
        /// <summary>
        /// [Play]
        /// </summary>
        public DbSet<Play> Plays { get; set; }
        /// <summary>
        /// [GoodsList]
        /// </summary>
        public DbSet<GoodsList> GoodsLists { get; set; }

        /// <summary>
        /// 签到记录
        /// </summary>
        public DbSet<AttendanceInfo> AttendanceInfos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new userinfoMap());
            modelBuilder.Configurations.Add(new AttendanceInfoMap());
            modelBuilder.Configurations.Add(new GoodsinfoMap());
            modelBuilder.Configurations.Add(new GoodsListMap());
            modelBuilder.Configurations.Add(new PUBGInfoMap());
            modelBuilder.Configurations.Add(new DOTA2InfoMap());
            modelBuilder.Configurations.Add(new PlayMap());
        }
    }
}
