

namespace RMS.Model
{
    using System;
    using System.Collections.Generic;
    public partial class vwControlShipInfo : ReportHeader
    {
        public long ControlShipInfoId { get; set; }
        
        public decimal ParentCode { get; set; }
    
        public decimal ControlCode { get; set; }
        public string ControlName { get; set; }
        public string ControlValue { get; set; }
        public string AdditionalValue { get; set; }
        public string SQD { get; set; }
        public Nullable<long> Organization { get; set; }
        public string Organazation { get; set; }
        public Nullable<long> Status { get; set; }
        public string Remarks { get; set; }
        public Nullable<int> ControlLevel { get; set; }
        public Nullable<int> SortOrder { get; set; }
        public bool IsActive { get; set; }
        public string Level1 { get; set; }
        public string Level2 { get; set; }
        public string Level3 { get; set; }
        public string Level4 { get; set; }
        public string Level5 { get; set; }
        public long TotalShip { get; set; }
        public long NonOpl { get; set; }
        public long Opl { get; set; }
        public long SqdId { get; set; }

        public System.DateTime todate { get; set; }
        public virtual CommonCode Common { get; set; }
    }
}
