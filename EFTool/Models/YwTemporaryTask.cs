//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
// <copyright file="YwTemporaryTask.cs">
//        Copyright(c)2015   rights reserved.
//        CLR版本：4.0.30319.239
//        开发组织：李文学@上海同岩土木科技工程有限公司
//        所属工程：TY.*
//        生成时间：2016-12-15 09:54
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
    /// [YwTemporaryTask]
    /// </summary>
    [Serializable]
    public partial class YwTemporaryTask : BaseEntity
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public YwTemporaryTask()
        {
            this.YwTemporaryDeatils = new List<YwTemporaryDeatil>();
        }

        /// <summary>
        /// [Id]
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// [LineId]
        /// </summary>
        public string LineId { get; set; }
        /// <summary>
        /// [LineCode]
        /// </summary>
        public string LineCode { get; set; }
        /// <summary>
        /// [WorkAreaId]
        /// </summary>
        public string WorkAreaId { get; set; }
        /// <summary>
        /// [WorkAreaCode]
        /// </summary>
        public string WorkAreaCode { get; set; }
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
        /// [CheckLeaderCode]
        /// </summary>
        public string CheckLeaderCode { get; set; }
        /// <summary>
        /// [CheckMemberCode]
        /// </summary>
        public string CheckMemberCode { get; set; }
        /// <summary>
        /// [TempTaskType]
        /// </summary>
        public Nullable<int> TempTaskType { get; set; }
        /// <summary>
        /// [PlanStartDate]
        /// </summary>
        public Nullable<System.DateTime> PlanStartDate { get; set; }
        /// <summary>
        /// [PlanEndDate]
        /// </summary>
        public Nullable<System.DateTime> PlanEndDate { get; set; }
        /// <summary>
        /// [ExecStartDate]
        /// </summary>
        public Nullable<System.DateTime> ExecStartDate { get; set; }
        /// <summary>
        /// [ExecEndDate]
        /// </summary>
        public Nullable<System.DateTime> ExecEndDate { get; set; }
        /// <summary>
        /// [CheckTime]
        /// </summary>
        public Nullable<decimal> CheckTime { get; set; }
        /// <summary>
        /// [CheckKilometre]
        /// </summary>
        public Nullable<decimal> CheckKilometre { get; set; }
        /// <summary>
        /// [DiseaseNum]
        /// </summary>
        public Nullable<int> DiseaseNum { get; set; }
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
        /// 导航集合：YwTemporaryDeatils
        /// </summary>
        public virtual ICollection<YwTemporaryDeatil> YwTemporaryDeatils { get; set; }
    }
}
