using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Model
{
    public class MachinaryRunningHour
    {
        public long MachinaryRunningHourId { get; set; }
        public long MonthlyReturnId { get; set; }
        public Nullable<long> NoOPSMachineId { get; set; }
        public Nullable<long> ShipId { get; set; }
        public Nullable<int> RunningHr { get; set; }
        public Nullable<int> RunningMin { get; set; }

        public virtual MachineryInfo MachineryInfo { get; set; }
        public virtual MonthlyReturn MonthlyReturn { get; set; }
        public string Machinery { get; set; }
    }
}
