//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
// <copyright file="AttendanceInfo.cs">
//        Copyright(c)2015   rights reserved.
//        CLR版本：4.0.30319.239
//        开发组织：李文学@上海同岩土木科技工程有限公司
//        所属工程：TY.*
//        生成时间：2018-02-07 15:01
// </copyright>
//------------------------------------------------------------------------------


using System;
using System.Collections.Generic;
using Data.Domain;

namespace Data.Domain
{
    /// <summary>
    /// [AttendanceInfo]
    /// </summary>
    [Serializable]
    public partial class AttendanceInfo 
    {
        /// <summary>
        /// [ID]
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 人员ID
        /// </summary>
        public string AD_Id { get; set; }
        /// <summary>
        /// [AD_AttendTime]
        /// </summary>
        public Nullable<System.DateTime> AD_AttendTime { get; set; }
        /// <summary>
        /// [AD_LoginName]
        /// </summary>
        public string AD_LoginName { get; set; }
        /// <summary>
        /// [Create_Time]
        /// </summary>
        public Nullable<System.DateTime> Create_Time { get; set; }
        /// <summary>
        /// 上香对象名称
        /// </summary>
        public string sacrificeName { get; set; }
    }
}
