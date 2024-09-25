using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Model
{
    public partial class QuaterlyMainEnginesGeneratorsRunningHour
    {
        public long QuaterlyMainEnginesGeneratorsRunningHourId { get; set; }
        public long QuaterlyReturnId { get; set; }
        public long ShipId { get; set; }
        public Nullable<long> MachineGeneratorId { get; set; }
        public Nullable<long> DuringQuaterHr { get; set; }
        public Nullable<long> DuringQuaterMin { get; set; }
        public Nullable<long> SinceLastLOChangeHr { get; set; }
        public Nullable<long> SinceLastLOChangeMin { get; set; }
        public Nullable<long> SinceLastTopOverhaulHr { get; set; }
        public Nullable<long> SinceLastTopOverhaulMin { get; set; }
        public Nullable<long> SinceLastMajorOverhaulHr { get; set; }
        public Nullable<long> SinceLastMajorOverhaulMin { get; set; }
        public Nullable<long> SinceCommissionInBNHr { get; set; }
        public Nullable<long> SinceCommissionInBNMin { get; set; }
        public Nullable<long> SinceInstallationHr { get; set; }
        public Nullable<long> SinceInstallationMin { get; set; }
        public string Remarks { get; set; }

        public virtual ControlShipInfo ControlShipInfo { get; set; }
        public virtual MachineryInfo MachineryInfo { get; set; }
        public virtual QuaterlyReturn QuaterlyReturn { get; set; }
    }
}
