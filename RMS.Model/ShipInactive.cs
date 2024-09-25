using System;
using System.Web.Mvc;

namespace RMS.Model
{
    public partial class ShipInactive
    {
        public long SInactiveIdentity { get; set; }
        public Nullable<long> MessageInfoIdentity { get; set; }
        public long ControlShipInfoIdentity { get; set; }
        public string MechineLevel2 { get; set; }
        public string MechineLevel3 { get; set; }
        public string CrashDetails { get; set; }
        public System.DateTime InactiveDate { get; set; }
        public string TakenStap { get; set; }
        //[Remote("ReferenceCheck", "ShipInactive", HttpMethod = "POST", ErrorMessage = "Reference Number Is Not Match.")]
        public string Reference { get; set; }
        public Nullable<long> ShipStatus { get; set; }
        public Nullable<int> MachineryId { get; set; }
        public Nullable<bool> IsCompeleted { get; set; }
        public string UserID { get; set; }
        public System.DateTime LastUpdate { get; set; }

        public virtual CommonCode CommonCode { get; set; }
        public virtual ControlShipInfo ControlShipInfo { get; set; }
    }
}
