using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Model
{
   public partial class ShipInactiveOrg
    {
        public long SInactiveOrgIdentity { get; set; }
        public long ShipInfoIdentity { get; set; }
        public long OrgId { get; set; }
        public string TotalShip { get; set; }
        public string Operational { get; set; }
        public string NonOperational { get; set; }
        public string sDescription { get; set; }
        public string Remark { get; set; }
        public Nullable<System.DateTime> DockingDateFrom { get; set; }
        public Nullable<System.DateTime> DockingDateTo { get; set; }
        public Nullable<System.DateTime> RefitDateFrom { get; set; }
        public Nullable<System.DateTime> RefitDateTo { get; set; }
        public string UserID { get; set; }
        public System.DateTime LastUpdate { get; set; }

        public virtual CommonCode CommonCode { get; set; }
        public virtual ShipInfo ShipInfo { get; set; }
    }
}
