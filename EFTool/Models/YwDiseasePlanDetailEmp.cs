//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
// <copyright file="YwDiseasePlanDetailEmp.cs">
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
    /// [YwDiseasePlanDetailEmp]
    /// </summary>
    [Serializable]
    public partial class YwDiseasePlanDetailEmp : BaseEntity
    {
        /// <summary>
        /// [Id]
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// [PlanDetailId]
        /// </summary>
        public string PlanDetailId { get; set; }
        /// <summary>
        /// [PlanDetailCode]
        /// </summary>
        public string PlanDetailCode { get; set; }
        /// <summary>
        /// [WorkAreaId]
        /// </summary>
        public string WorkAreaId { get; set; }
        /// <summary>
        /// [WorkAreaCode]
        /// </summary>
        public string WorkAreaCode { get; set; }
        /// <summary>
        /// [LineId]
        /// </summary>
        public string LineId { get; set; }
        /// <summary>
        /// [LineCode]
        /// </summary>
        public string LineCode { get; set; }
        /// <summary>
        /// [EmpId]
        /// </summary>
        public string EmpId { get; set; }
        /// <summary>
        /// [EmpCode]
        /// </summary>
        public string EmpCode { get; set; }
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
        /// 导航属性：YwDiseasePlanDetail
        /// </summary>
        public virtual YwDiseasePlanDetail YwDiseasePlanDetail { get; set; }
    }
}
