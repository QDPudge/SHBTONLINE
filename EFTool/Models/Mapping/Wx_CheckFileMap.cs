	
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
// <copyright file="Wx_CheckFileMap.cs">
//        Copyright(c)2015   rights reserved.
//        CLR版本：4.0.30319.239
//        开发组织：李文学@上海同岩土木科技工程有限公司
//        所属工程：TY.*
//        生成时间：2016-12-15 09:53
// </copyright>
//------------------------------------------------------------------------------

	using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace TY.SHMetroPlan.Data.Domain.Models.Mapping
{
    public class Wx_CheckFileMap : EntityTypeConfiguration<Wx_CheckFile>
    {
        public Wx_CheckFileMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(36);

            this.Property(t => t.CheckArrangeId)
                .IsFixedLength()
                .HasMaxLength(36);

            this.Property(t => t.CheckArrangeCode)
                .HasMaxLength(200);

            this.Property(t => t.FileCode)
                .HasMaxLength(200);

            this.Property(t => t.FileName)
                .HasMaxLength(200);

            this.Property(t => t.FilePath)
                .HasMaxLength(200);

            this.Property(t => t.FileType)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("Wx_CheckFile");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CheckArrangeId).HasColumnName("CheckArrangeId");
            this.Property(t => t.CheckArrangeCode).HasColumnName("CheckArrangeCode");
            this.Property(t => t.FileCode).HasColumnName("FileCode");
            this.Property(t => t.FileName).HasColumnName("FileName");
            this.Property(t => t.FilePath).HasColumnName("FilePath");
            this.Property(t => t.FileType).HasColumnName("FileType");
            this.Property(t => t.CreateTime).HasColumnName("CreateTime");
            this.Property(t => t.UpdateTime).HasColumnName("UpdateTime");
            this.Property(t => t.RowVersion).HasColumnName("RowVersion");

            // Relationships
            this.HasOptional(t => t.Wx_CheckArrange)
                .WithMany(t => t.Wx_CheckFile)
                .HasForeignKey(d => d.CheckArrangeId);

        }
    }
}
