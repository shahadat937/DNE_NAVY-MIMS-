using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Model
{
   public partial class AsAndAsInfo
    {

        public long AsAndAsIdentity { get; set; }
        public long ControlShipInfoId { get; set; }
        public Nullable<System.DateTime> AsAndAsDate { get; set; }
        public Nullable<System.DateTime> ApprovalDate { get; set; }
        public string DescriptionOfAsAs { get; set; }
        public Nullable<long> ClassId { get; set; }
        public string LWLM { get; set; }
        public string DeltaTon { get; set; }
        public string KMTM { get; set; }
        public string GMTM { get; set; }
        public string KGM { get; set; }
        public string LCFM { get; set; }
        public string LCGM { get; set; }
        public string TPCTonCm { get; set; }
        public string MCTM { get; set; }
        public string DFM { get; set; }
        public string DAM { get; set; }
        public string TRIMM { get; set; }
        public string THETADeg { get; set; }
        public Nullable<System.DateTime> RaisingDate { get; set; }
        public Nullable<int> TempDisCodeId { get; set; }
        public string TempDisCode { get; set; }
        public string AsAndAsClassification { get; set; }
        public string DescriptionOfProp { get; set; }
        public string FightingEfficiency { get; set; }
        public string SeaGoingEfficiency { get; set; }
        public string EffectOnHabitability { get; set; }
        public string EffectOnStabilityTrim { get; set; }
        public string DockingPlace { get; set; }
        public string AuthorityRemark { get; set; }
        public string UserId { get; set; }
        public System.DateTime LastUpdate { get; set; }
        public Nullable<System.DateTime> EntryDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<bool> IsVerified { get; set; }
        public string VerifiedBy { get; set; }
        public Nullable<System.DateTime> VerifiedDate { get; set; }

        public virtual CommonCode CommonCode { get; set; }
        public virtual ControlShipInfo ControlShipInfo { get; set; }

    }
}
