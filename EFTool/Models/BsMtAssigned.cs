//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
// <copyright file="BsMtAssigned.cs">
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
    /// [BsMtAssigned]
    /// </summary>
    [Serializable]
    public partial class BsMtAssigned : BaseEntity
    {
        /// <summary>
        /// [Id]
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// [AssignedCode]
        /// </summary>
        public string AssignedCode { get; set; }
        /// <summary>
        /// [ReservoirId]
        /// </summary>
        public string ReservoirId { get; set; }
        /// <summary>
        /// [ReservoirCode]
        /// </summary>
        public string ReservoirCode { get; set; }
        /// <summary>
        /// [AreaId]
        /// </summary>
        public string AreaId { get; set; }
        /// <summary>
        /// [AreaCode]
        /// </summary>
        public string AreaCode { get; set; }
        /// <summary>
        /// [DepartId]
        /// </summary>
        public string DepartId { get; set; }
        /// <summary>
        /// [DepartCode]
        /// </summary>
        public string DepartCode { get; set; }
        /// <summary>
        /// [MaterialApplyId]
        /// </summary>
        public string MaterialApplyId { get; set; }
        /// <summary>
        /// [MaterialApplyCode]
        /// </summary>
        public string MaterialApplyCode { get; set; }
        /// <summary>
        /// [AssignedType]
        /// </summary>
        public Nullable<int> AssignedType { get; set; }
        /// <summary>
        /// [SpecificationId]
        /// </summary>
        public string SpecificationId { get; set; }
        /// <summary>
        /// [SpecificationCode]
        /// </summary>
        public string SpecificationCode { get; set; }
        /// <summary>
        /// [StockQuantity]
        /// </summary>
        public Nullable<int> StockQuantity { get; set; }
        /// <summary>
        /// [Remark]
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// [SortId]
        /// </summary>
        public Nullable<int> SortId { get; set; }
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
        /// 导航属性：BsMtPurchase
        /// </summary>
        public virtual BsMtPurchase BsMtPurchase { get; set; }
    }
}
