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
    
    public partial class DailyTransaction
    {
        public string VoucherNo { get; set; }
        public System.DateTime TransDate { get; set; }
        public long TransType { get; set; }
        public long ModeOfTrans { get; set; }
        public long TransNature { get; set; }
        public long AccountCode { get; set; }
        public string Description { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public long Currency { get; set; }
        public decimal ExchangeRate { get; set; }
        public decimal FDebit { get; set; }
        public decimal FCredit { get; set; }
        public long BranchCode { get; set; }
        public string BankCode { get; set; }
        public string ReferenceNo { get; set; }
        public Nullable<long> UserAgentBranch { get; set; }
        public bool Verified { get; set; }
        public string UserId { get; set; }
        public System.DateTime LastUpdate { get; set; }
        public long IdentityNo { get; set; }
    }
}
