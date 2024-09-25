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
    
    public partial class vwShipBranchInfo
    {
        public long BranchInfoIdentity { get; set; }
        public string Code { get; set; }
        public Nullable<long> BankCodeIdentity { get; set; }
        public string BankCode { get; set; }
        public string BankName { get; set; }
        public Nullable<long> DistrictCodeIdentity { get; set; }
        public string DistrictCode { get; set; }
        public string DistrictName { get; set; }
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public string BranchAddress { get; set; }
        public string BranchType { get; set; }
        public string BranchLevel { get; set; }
        public long ControlShipInfoId { get; set; }
        public decimal ParentCode { get; set; }
        public decimal ControlCode { get; set; }
        public string ControlName { get; set; }
        public string ControlValue { get; set; }
        public long SQD { get; set; }
        public string SQDName { get; set; }
        public Nullable<long> Organization { get; set; }
        public string ShipStatus { get; set; }
        public Nullable<long> Status { get; set; }
        public Nullable<int> ControlLevel { get; set; }
        public Nullable<int> SortOrder { get; set; }
        public bool IsActive { get; set; }
        public string Level1 { get; set; }
        public string Level2 { get; set; }
        public string Level3 { get; set; }
        public string Level4 { get; set; }
        public string Level5 { get; set; }
        public Nullable<System.DateTime> LastUpdateDate { get; set; }
        public string LastUpdateId { get; set; }
        public Nullable<System.DateTime> CreateUserDate { get; set; }
        public string CreateUserId { get; set; }
    }
}
