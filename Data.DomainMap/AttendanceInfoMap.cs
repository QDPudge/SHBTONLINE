
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Data.Domain;
using Data.DomainMap;

//namespace TM.Data.Models.Mapping
namespace Data.DomainMap
{
    public class AttendanceInfoMap : EntityTypeConfiguration<AttendanceInfo>
    {
        public AttendanceInfoMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .IsRequired()
                .HasMaxLength(36);

            this.Property(t => t.AD_Id)
                .HasMaxLength(36);

            this.Property(t => t.AD_LoginName)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("AttendanceInfo");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.AD_Id).HasColumnName("AD_Id");
            this.Property(t => t.AD_AttendTime).HasColumnName("AD_AttendTime");
            this.Property(t => t.AD_LoginName).HasColumnName("AD_LoginName");
            //上香对象名称
            this.Property(t => t.sacrificeName).HasColumnName("sacrificeName");
            this.Property(t => t.Create_Time).HasColumnName("Create_Time");

        }
    }
}
