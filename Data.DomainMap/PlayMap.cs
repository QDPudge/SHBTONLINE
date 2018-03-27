
//------------------------------------------------------------------------------

	using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Data.Domain;
using Data.DomainMap;

//namespace TM.Data.Models.Mapping
namespace Data.DomainMap
{ 
    public class PlayMap : EntityTypeConfiguration<Play>
    {
        public PlayMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(36);

            this.Property(t => t.Name)
                .HasMaxLength(300);

            this.Property(t => t.ParentID)
                .IsFixedLength()
                .HasMaxLength(36);

            this.Property(t => t.Status)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Play");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Odds).HasColumnName("Odds");
            this.Property(t => t.ParentID).HasColumnName("ParentID");
            this.Property(t => t.OffTime).HasColumnName("OffTime");
            this.Property(t => t.Status).HasColumnName("Status");
        }
    }
}
