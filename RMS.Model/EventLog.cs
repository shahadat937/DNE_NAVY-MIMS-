//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RMS.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class EventLog
    {
        public string UserId { get; set; }
        public string EventName { get; set; }
        public System.DateTime EventTime { get; set; }
        public string PreviousInfo { get; set; }
        public string CurrentInfo { get; set; }
        public string Workstation { get; set; }
        public int SLNo { get; set; }
        public string BranchCode { get; set; }
    }
}
