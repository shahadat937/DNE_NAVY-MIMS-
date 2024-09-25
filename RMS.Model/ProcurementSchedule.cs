using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Model
{
   public partial class ProcurementSchedule
    {
        public long ProcurementId { get; set; }
        public long ProcurementType { get; set; }
        public string Description { get; set; }
        public string Qty { get; set; }
        public decimal? EstTotalPrice { get; set; }
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
        public Nullable<System.DateTime> EntryDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<bool> IsVerified { get; set; }
        public string VerifiedBy { get; set; }
        public Nullable<System.DateTime> VerifiedDate { get; set; }

        public virtual CommonCode CommonCode { get; set; }
    }
}
