//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
// <copyright file="GoodsList.cs">
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
    /// [GoodsList]
    /// </summary>
    [Serializable]
    public partial class GoodsList 
    {
        /// <summary>
        /// [ID]
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// [Name]
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// [Type]
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// [Cost1]
        /// </summary>
        public Nullable<int> Cost1 { get; set; }
        /// <summary>
        /// [Cost2]
        /// </summary>
        public Nullable<int> Cost2 { get; set; }
        /// <summary>
        /// [Level]
        /// </summary>
        public Nullable<int> Level { get; set; }
        /// <summary>
        /// [LevelName]
        /// </summary>
        public string LevelName { get; set; }
        /// <summary>
        /// [Comm]
        /// </summary>
        public string Comm { get; set; }
        /// <summary>
        /// [IMG]
        /// </summary>
        public string IMG { get; set; }
    }
}
