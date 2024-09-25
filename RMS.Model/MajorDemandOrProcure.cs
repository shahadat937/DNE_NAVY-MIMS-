using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Model
{
   public partial class MajorDemandOrProcure
    {
        public long MajorDemandIdentity { get; set; }
        public long ShipIdentity { get; set; }
        public string MajorDemandName { get; set; }
        public string MajorDemandModel { get; set; }
        public string AuthorityNumber { get; set; }
        public string StockedNumber { get; set; }
        public string EthicalApprovalNumber { get; set; }
        public string DeficitOrExtraNumber { get; set; }
        public string ProposalBuyNumber { get; set; }
        public string BeforePrice { get; set; }
        public string TotalPrice { get; set; }
        public string Remarks { get; set; }
        public System.DateTime LastUpdate { get; set; }
        public string UserId { get; set; }
        public Nullable<System.DateTime> EntryDate { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsVerified { get; set; }
        public string VerifiedBy { get; set; }
        public Nullable<System.DateTime> VerifiedDate { get; set; }

        public virtual ControlShipInfo ControlShipInfo { get; set; }
    }
}
