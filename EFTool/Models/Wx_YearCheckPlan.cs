//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
// <copyright file="Wx_YearCheckPlan.cs">
//        Copyright(c)2015   rights reserved.
//        CLR版本：4.0.30319.239
//        开发组织：李文学@上海同岩土木科技工程有限公司
//        所属工程：TY.*
//        生成时间：2016-12-15 09:53
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using TY.Framework.EF;
using TY.Framework.EF.Base;
using TY.Framework.ToolT4;


namespace TY.SHMetroPlan.Data.Domain.Models
{
    /// <summary>
    /// [Wx_YearCheckPlan]
    /// </summary>
    [Serializable]
    public partial class Wx_YearCheckPlan : BaseEntity
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public Wx_YearCheckPlan()
        {
            this.Wx_CheckProject = new List<Wx_CheckProject>();
        }

        /// <summary>
        /// [Id]
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// [YearPlanId]
        /// </summary>
        public string YearPlanId { get; set; }
        /// <summary>
        /// [TaskName]
        /// </summary>
        public string TaskName { get; set; }
        /// <summary>
        /// [TaskCode]
        /// </summary>
        public string TaskCode { get; set; }
        /// <summary>
        /// [TaskState]
        /// </summary>
        public Nullable<int> TaskState { get; set; }
        /// <summary>
        /// [PlanStartDate]
        /// </summary>
        public Nullable<System.DateTime> PlanStartDate { get; set; }
        /// <summary>
        /// [PlanEndDate]
        /// </summary>
        public Nullable<System.DateTime> PlanEndDate { get; set; }
        /// <summary>
        /// [IsFinished]
        /// </summary>
        public Nullable<int> IsFinished { get; set; }
        /// <summary>
        /// [Remark]
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// [CreateTime]
        /// </summary>
        public Nullable<System.DateTime> CreateTime { get; set; }
        /// <summary>
        /// [UpdateTime]
        /// </summary>
        public Nullable<System.DateTime> UpdateTime { get; set; }
        /// <summary>
        /// [RowVersion]
        /// </summary>
        public Nullable<int> RowVersion { get; set; }
        /// <summary>
        /// 导航集合：Wx_CheckProject
        /// </summary>
        public virtual ICollection<Wx_CheckProject> Wx_CheckProject { get; set; }
        /// <summary>
        /// 导航属性：Wx_RepairYearPlan
        /// </summary>
        public virtual Wx_RepairYearPlan Wx_RepairYearPlan { get; set; }
    }
}
