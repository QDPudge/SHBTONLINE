	
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
// <copyright file="YwGdTaskMap.cs">
//        Copyright(c)2015   rights reserved.
//        CLR版本：4.0.30319.239
//        开发组织：李文学@上海同岩土木科技工程有限公司
//        所属工程：TY.*
//        生成时间：2016-12-15 09:54
// </copyright>
//------------------------------------------------------------------------------

	using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace TY.SHMetroPlan.Data.Domain.Models.Mapping
{
    public class YwGdTaskMap : EntityTypeConfiguration<YwGdTask>
    {
        public YwGdTaskMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(36);

            this.Property(t => t.LineId)
                .IsFixedLength()
                .HasMaxLength(36);

            this.Property(t => t.LineCode)
                .HasMaxLength(200);

            this.Property(t => t.WorkAreaId)
                .IsFixedLength()
                .HasMaxLength(36);

            this.Property(t => t.WorkAreaCode)
                .HasMaxLength(200);

            this.Property(t => t.TaskName)
                .HasMaxLength(200);

            this.Property(t => t.TaskCode)
                .HasMaxLength(200);

            this.Property(t => t.MemberConfigCode)
                .HasMaxLength(200);

            this.Property(t => t.CheckLeaderCode)
                .HasMaxLength(200);

            this.Property(t => t.CheckMemberCode)
                .HasMaxLength(400);

            this.Property(t => t.Remark)
                .IsFixedLength()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("YwGdTask");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.LineId).HasColumnName("LineId");
            this.Property(t => t.LineCode).HasColumnName("LineCode");
            this.Property(t => t.WorkAreaId).HasColumnName("WorkAreaId");
            this.Property(t => t.WorkAreaCode).HasColumnName("WorkAreaCode");
            this.Property(t => t.TaskName).HasColumnName("TaskName");
            this.Property(t => t.TaskCode).HasColumnName("TaskCode");
            this.Property(t => t.TaskState).HasColumnName("TaskState");
            this.Property(t => t.MemberConfigCode).HasColumnName("MemberConfigCode");
            this.Property(t => t.CheckLeaderCode).HasColumnName("CheckLeaderCode");
            this.Property(t => t.CheckMemberCode).HasColumnName("CheckMemberCode");
            this.Property(t => t.DiseaseNum).HasColumnName("DiseaseNum");
            this.Property(t => t.IsUrgentTask).HasColumnName("IsUrgentTask");
            this.Property(t => t.StartDate).HasColumnName("StartDate");
            this.Property(t => t.EndDate).HasColumnName("EndDate");
            this.Property(t => t.ExecStartDate).HasColumnName("ExecStartDate");
            this.Property(t => t.ExecEndDate).HasColumnName("ExecEndDate");
            this.Property(t => t.CheckTime).HasColumnName("CheckTime");
            this.Property(t => t.CheckKilometre).HasColumnName("CheckKilometre");
            this.Property(t => t.Remark).HasColumnName("Remark");
            this.Property(t => t.CreateTime).HasColumnName("CreateTime");
            this.Property(t => t.UpdateTime).HasColumnName("UpdateTime");
            this.Property(t => t.RowVersion).HasColumnName("RowVersion");
        }
    }
}
