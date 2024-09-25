using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Utility
{
    public class TempMachinaryRunningHour
    {
        public long MachinaryRunningHourId { get; set; }
        public Nullable<long> NoOPSMachineId { get; set; }
        public Nullable<int> RunningHr { get; set; }
        public Nullable<int> RunningMin { get; set; }
        
        public string Machinery { get; set; }
    }
}
