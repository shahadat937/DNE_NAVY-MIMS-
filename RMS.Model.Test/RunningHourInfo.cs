//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RMS.Model.Test
{
    using System;
    using System.Collections.Generic;
    
    public partial class RunningHourInfo
    {
        public long RunningMachinariesIdentity { get; set; }
        public long MachinariesInfoIdentity { get; set; }
        public Nullable<System.DateTime> RunningDate { get; set; }
        public Nullable<decimal> RunningHour { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string CreateUserId { get; set; }
        public Nullable<System.DateTime> LastUpdateDate { get; set; }
        public string LastUpdateId { get; set; }
        public Nullable<bool> IsVerified { get; set; }
        public string VerifiedBy { get; set; }
        public Nullable<System.DateTime> VerifiedDate { get; set; }
    
        public virtual MachineryInfo MachineryInfo { get; set; }
    }
}
