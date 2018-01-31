	
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
// <copyright file="BsEquipmentConfigMap.cs">
//        Copyright(c)2015   rights reserved.
//        CLR版本：4.0.30319.239
//        开发组织：李文学@上海同岩土木科技工程有限公司
//        所属工程：TY.*
//        生成时间：2016-12-15 09:52
// </copyright>
//------------------------------------------------------------------------------

	using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace TY.SHMetroPlan.Data.Domain.Models.Mapping
{
    public class BsEquipmentConfigMap : EntityTypeConfiguration<BsEquipmentConfig>
    {
        public BsEquipmentConfigMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(36);

            this.Property(t => t.FacilityConfigCode)
                .HasMaxLength(200);

            this.Property(t => t.EquipmentId)
                .IsFixedLength()
                .HasMaxLength(36);

            this.Property(t => t.EquipmentCode)
                .HasMaxLength(200);

            this.Property(t => t.RouteId)
                .IsFixedLength()
                .HasMaxLength(36);

            this.Property(t => t.RouteCode)
                .HasMaxLength(200);

            this.Property(t => t.Remark)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("BsEquipmentConfig");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.FacilityConfigCode).HasColumnName("FacilityConfigCode");
            this.Property(t => t.EquipmentId).HasColumnName("EquipmentId");
            this.Property(t => t.EquipmentCode).HasColumnName("EquipmentCode");
            this.Property(t => t.RouteId).HasColumnName("RouteId");
            this.Property(t => t.RouteCode).HasColumnName("RouteCode");
            this.Property(t => t.IsEnabled).HasColumnName("IsEnabled");
            this.Property(t => t.StartMileage).HasColumnName("StartMileage");
            this.Property(t => t.EndMileage).HasColumnName("EndMileage");
            this.Property(t => t.Remark).HasColumnName("Remark");
            this.Property(t => t.CreateTime).HasColumnName("CreateTime");
            this.Property(t => t.UpdateTime).HasColumnName("UpdateTime");
            this.Property(t => t.RowVersion).HasColumnName("RowVersion");

            // Relationships
            this.HasOptional(t => t.BsFacilityEquipment)
                .WithMany(t => t.BsEquipmentConfigs)
                .HasForeignKey(d => d.EquipmentId);
            this.HasOptional(t => t.BsSectionRoute)
                .WithMany(t => t.BsEquipmentConfigs)
                .HasForeignKey(d => d.RouteId);

        }
    }
}
