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
    
    public partial class AccountInfo
    {
        public int AccountInfoId { get; set; }
        public decimal ParentCode { get; set; }
        public decimal ControlCode { get; set; }
        public string ControlName { get; set; }
        public int ControlLevel { get; set; }
        public int SortOrder { get; set; }
        public bool IsActive { get; set; }
        public Nullable<long> AccountCategory { get; set; }
        public Nullable<long> AccountStatus { get; set; }
        public string Level1 { get; set; }
        public string Level2 { get; set; }
        public string Level3 { get; set; }
        public string Level4 { get; set; }
        public string Level5 { get; set; }
        public string UserID { get; set; }
        public System.DateTime LastUpdate { get; set; }
    }
}
