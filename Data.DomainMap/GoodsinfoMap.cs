
	using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Data.Domain;
using Data.DomainMap;

//namespace TM.Data.Models.Mapping
namespace Data.DomainMap { 
    public class GoodsinfoMap : EntityTypeConfiguration<Goodsinfo>
    {
        public GoodsinfoMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(36);

            this.Property(t => t.GoodsID)
                .IsFixedLength()
                .HasMaxLength(36);

            this.Property(t => t.GoodsName)
                .HasMaxLength(200);

            this.Property(t => t.LoginName)
                .HasMaxLength(200);

            this.Property(t => t.Type)
                .HasMaxLength(200);            

            this.Property(t => t.Name)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("Goodsinfo");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.GoodsID).HasColumnName("GoodsID");
            this.Property(t => t.GoodsName).HasColumnName("GoodsName");
            this.Property(t => t.Type).HasColumnName("Type");            
            this.Property(t => t.LoginName).HasColumnName("LoginName");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.RewardDate).HasColumnName("RewardDate");
            this.Property(t => t.Spend1).HasColumnName("Spend1");
            this.Property(t => t.Spend2).HasColumnName("Spend2");
        }
    }
}
