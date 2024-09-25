using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Model
{
    public class ShipInactiveView
    {
        public long SInactiveIdentity { get; set; }
        public Nullable<long> MessageInfoIdentity { get; set; }
        public long ControlShipInfoIdentity { get; set; }
        public string ControlName { get; set; }
        public string BranchName { get; set; }

        public virtual CommonCode CommonCode { get; set; }
        public virtual ControlShipInfo ControlShipInfo { get; set; }
    }
}
