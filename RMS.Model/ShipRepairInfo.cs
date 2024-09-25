using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Model
{
  public  partial class ShipRepairInfo
    {
        public long ShipRepairIdentity { get; set; }
        public long ShipInfoIdentity { get; set; }
        public string FinantialYear { get; set; }
        public System.DateTime DockingStart { get; set; }
        public System.DateTime DockingEnd { get; set; }
        public Nullable<decimal> RepairCost { get; set; }
        public string DockingPlace { get; set; }
        public string UserId { get; set; }
        public System.DateTime LastUpdate { get; set; }

        public virtual ControlShipInfo ControlShipInfo { get; set; }
    }
}
