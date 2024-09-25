using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Model
{
    public partial class vwShipRepairInfo : ReportHeader
    {
        public long ShipRepairIdentity { get; set; }
        public long ShipInfoIdentity { get; set; }
        public string FinantialYear { get; set; }
        public Nullable<System.DateTime> DockingStart { get; set; }
        public Nullable<System.DateTime> DockingEnd { get; set; }
        public Nullable<decimal> RepairCost { get; set; }
        public string DockingPlace { get; set; }
        public string ShipName { get; set; }
        public DateTime todate { get; set; }
        public string UserId { get; set; }
        public System.DateTime LastUpdate { get; set; }

        public virtual ShipInfo ShipInfo { get; set; }
    }
}
