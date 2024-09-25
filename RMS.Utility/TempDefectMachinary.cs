using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Utility
{
    public class TempDefectMachinary
    {
        public long DefectMachinaryId { get; set; }
        public Nullable<long> MachineId { get; set; }
        public Nullable<long> MachinaryStatusId { get; set; }
        public Nullable<System.DateTime> DefectDate { get; set; }
        public string DefectReasonDetail { get; set; }
        public string DefectSolution { get; set; }
        public string Remarks { get; set; }
    }
}
