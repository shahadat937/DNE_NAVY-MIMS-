

namespace RMS.Model
{
    using System;
    using System.Collections.Generic;
    public partial class vwProcurementSchedule : ReportHeader
    {
        public long ProcurementId { get; set; }
        public long ProcurementType { get; set; }
        public string Description { get; set; }
        public string Qty { get; set; }
        public Nullable<decimal> EstTotalPrice { get; set; }
        public string Currency { get; set; }
        public Nullable<bool> AIPReceived { get; set; }
        public Nullable<System.DateTime> SpecSentToDTS { get; set; }
        public Nullable<System.DateTime> SpecSentToNSSD { get; set; }
        public Nullable<System.DateTime> SpecSentToDGDP { get; set; }
        public Nullable<System.DateTime> TenderOpened { get; set; }
        public Nullable<System.DateTime> QuatationRec { get; set; }
        public Nullable<System.DateTime> AccepSentToDTS { get; set; }
        public Nullable<System.DateTime> AccepSentToNSSD { get; set; }
        public Nullable<System.DateTime> AccepSentToDGDP { get; set; }
        public Nullable<System.DateTime> AccepSentToAFD { get; set; }
        public string ApprovedByAFD { get; set; }
        public string ConttractSigned { get; set; }
        public Nullable<decimal> Taka { get; set; }
        public Nullable<System.DateTime> DTSReTender { get; set; }
        public Nullable<System.DateTime> DGDPReTender { get; set; }
        public Nullable<System.DateTime> NSSDReTender { get; set; }
        public Nullable<System.DateTime> TenderOpenReTender { get; set; }
        public Nullable<System.DateTime> QuotRecReTender { get; set; }
        public string Remark { get; set; }
        public string UserId { get; set; }
        public System.DateTime LastUpdate { get; set; }

        public long YearFrom { get; set; }
        public long YearTo { get; set; }
        public string proType { get; set; }

        public virtual CommonCode CommonCode { get; set; }
    }
}
