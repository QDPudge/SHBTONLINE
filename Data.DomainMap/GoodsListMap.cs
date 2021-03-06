
//------------------------------------------------------------------------------

	using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Data.Domain;

//namespace TM.Data.Models.Mapping
namespace Data.DomainMap { 
    public class GoodsListMap : EntityTypeConfiguration<GoodsList>
    {
        public GoodsListMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(36);

            this.Property(t => t.Name)
                .HasMaxLength(200);

            this.Property(t => t.Comm)
                .HasMaxLength(200);

            this.Property(t => t.IMG)
                .HasMaxLength(200);

            this.Property(t => t.LevelName)
                .HasMaxLength(200);            

            this.Property(t => t.Type)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("GoodsList");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.Cost1).HasColumnName("Cost1");
            this.Property(t => t.Cost2).HasColumnName("Cost2");
            this.Property(t => t.Level).HasColumnName("Level");
            this.Property(t => t.LevelName).HasColumnName("LevelName");            
            this.Property(t => t.Comm).HasColumnName("Comm");
            this.Property(t => t.IMG).HasColumnName("IMG");
        }
    }
}
