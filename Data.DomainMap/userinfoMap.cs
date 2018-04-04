
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Data.Domain;
using Data.DomainMap;

//namespace TM.Data.Models.Mapping
namespace Data.DomainMap
{

    public class userinfoMap : EntityTypeConfiguration<userinfo>
    {
        public userinfoMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(36);

            this.Property(t => t.Email)
                .HasMaxLength(200);

            this.Property(t => t.Name)
                .HasMaxLength(200);

            this.Property(t => t.LoginName)
                .HasMaxLength(200);

            this.Property(t => t.PSW)
                .HasMaxLength(200);

            this.Property(t => t.MateLoginName)
                .HasMaxLength(200);

            this.Property(t => t.MateName)
                .HasMaxLength(200);

            this.Property(t => t.Card_bg)
                .HasMaxLength(200);            

            this.Property(t => t.IMG)
                .HasMaxLength(200);
            

            this.Property(t => t.PrivateKey)
                .IsFixedLength()
                .HasMaxLength(36);

            this.Property(t => t.SteamID)
                .HasMaxLength(200);

            this.Property(t => t.PubgID)
                .HasMaxLength(200);

            this.Property(t => t.WechatID)
                .HasMaxLength(200);
            
            this.Property(t => t.DOTA2ID)
                .HasMaxLength(200);
            // Table & Column Mappings
            this.ToTable("userinfo");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.LoginName).HasColumnName("LoginName");
            this.Property(t => t.PSW).HasColumnName("PSW");
            this.Property(t => t.MateLoginName).HasColumnName("MateLoginName");
            this.Property(t => t.MateName).HasColumnName("MateName");
            this.Property(t => t.RemainRoll).HasColumnName("RemainRoll");
            this.Property(t => t.PrivateKey).HasColumnName("PrivateKey");
            this.Property(t => t.SteamID).HasColumnName("SteamID");
            this.Property(t => t.SCrrency).HasColumnName("SCrrency");
            this.Property(t => t.Points).HasColumnName("Points");
            this.Property(t => t.RMB).HasColumnName("RMB");
            this.Property(t => t.IMG).HasColumnName("IMG");            
            this.Property(t => t.PubgID).HasColumnName("PubgID");
            this.Property(t => t.DOTA2ID).HasColumnName("DOTA2ID");
            this.Property(t => t.Card_bg).HasColumnName("Card_bg");
            this.Property(t => t.WechatID).HasColumnName("WechatID");         
            ///上香次数（每个人相关次数）
            this.Property(t => t.sacrifNum).HasColumnName("sacrifNum");

        }
    }
}
