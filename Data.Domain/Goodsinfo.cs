//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
// <copyright file="Goodsinfo.cs">
//        Copyright(c)2015   rights reserved.
//        CLR版本：4.0.30319.239
//        开发组织：李文学@上海同岩土木科技工程有限公司
//        所属工程：TY.*
//        生成时间：2018-03-09 16:40
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using Data.Domain;


//namespace TM.Data.Models
namespace  Data.Domain
{
    /// <summary>
    /// [Goodsinfo]
    /// </summary>
    [Serializable]
    public partial class Goodsinfo 
    {
        /// <summary>
        /// [ID]
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// [GoodsID]
        /// </summary>
        public string GoodsID { get; set; }
        /// <summary>
        /// [GoodsName]
        /// </summary>
        public string GoodsName { get; set; }
        /// <summary>
        /// [LoginName]
        /// </summary>
        public string LoginName { get; set; }
        /// <summary>
        /// [Type]
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// [Name]
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// [RewardDate]
        /// </summary>
        public Nullable<System.DateTime> RewardDate { get; set; }
        /// <summary>
        /// [Spend1]
        /// </summary>
        public Nullable<int> Spend1 { get; set; }
        /// <summary>
        /// [Spend2]
        /// </summary>
        public Nullable<int> Spend2 { get; set; }
    }
}
