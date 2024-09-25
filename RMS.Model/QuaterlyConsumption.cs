using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Model
{
    public partial class QuaterlyConsumption
    {
        public long QuaterlyConsumptionId { get; set; }
        public long QuaterlyReturnId { get; set; }
        public long ShipId { get; set; }
        public long MachineId { get; set; }
        public long OilName { get; set; }
        public decimal Ltrs { get; set; }

        public virtual CommonCode CommonCode { get; set; }
        public virtual ControlShipInfo ControlShipInfo { get; set; }
        public virtual MachineryInfo MachineryInfo { get; set; }
        public virtual QuaterlyReturn QuaterlyReturn { get; set; }
    }
}
