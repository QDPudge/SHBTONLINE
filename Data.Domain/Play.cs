
using System;
using System.Collections.Generic;
using Data.Domain;


//namespace TM.Data.Models
namespace  Data.Domain
{
    /// <summary>
    /// [Play]
    /// </summary>
    [Serializable]
    public partial class Play 
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
        /// [Odds]
        /// </summary>
        public Nullable<double> Odds { get; set; }
        /// <summary>
        /// [ParentID]
        /// </summary>
        public string ParentID { get; set; }
        /// <summary>
        /// [OffTime]
        /// </summary>
        public Nullable<System.DateTime> OffTime { get; set; }
        /// <summary>
        /// [Status]
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// Results
        /// </summary>
        public string Results { get; set; }
    }
}
