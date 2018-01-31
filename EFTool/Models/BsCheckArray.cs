//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
// <copyright file="BsCheckArray.cs">
//        Copyright(c)2015   rights reserved.
//        CLR版本：4.0.30319.239
//        开发组织：李文学@上海同岩土木科技工程有限公司
//        所属工程：TY.*
//        生成时间：2016-12-15 09:52
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
    /// [BsCheckArray]
    /// </summary>
    [Serializable]
    public partial class BsCheckArray : BaseEntity
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public BsCheckArray()
        {
            this.BsCheckConfigs = new List<BsCheckConfig>();
            this.BsCheckRanges = new List<BsCheckRange>();
        }

        /// <summary>
        /// [Id]
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// [CheckArrayName]
        /// </summary>
        public string CheckArrayName { get; set; }
        /// <summary>
        /// [CheckArrayCode]
        /// </summary>
        public string CheckArrayCode { get; set; }
        /// <summary>
        /// [WorkAreaId]
        /// </summary>
        public string WorkAreaId { get; set; }
        /// <summary>
        /// [WorkAreaCode]
        /// </summary>
        public string WorkAreaCode { get; set; }
        /// <summary>
        /// [DirectionMark]
        /// </summary>
        public Nullable<int> DirectionMark { get; set; }
        /// <summary>
        /// [StartMileage]
        /// </summary>
        public Nullable<decimal> StartMileage { get; set; }
        /// <summary>
        /// [EndMileage]
        /// </summary>
        public Nullable<decimal> EndMileage { get; set; }
        /// <summary>
        /// [IsEnabled]
        /// </summary>
        public Nullable<int> IsEnabled { get; set; }
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
        /// 导航集合：BsCheckConfigs
        /// </summary>
        public virtual ICollection<BsCheckConfig> BsCheckConfigs { get; set; }
        /// <summary>
        /// 导航属性：BsWorkArea
        /// </summary>
        public virtual BsWorkArea BsWorkArea { get; set; }
        /// <summary>
        /// 导航集合：BsCheckRanges
        /// </summary>
        public virtual ICollection<BsCheckRange> BsCheckRanges { get; set; }
    }
}
