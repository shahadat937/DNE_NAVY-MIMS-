using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Model
{
    public class DefectMachinary
    {
        public long DefectMachinaryId { get; set; }
        public Nullable<long> MachineId { get; set; }
        public Nullable<long> MachinaryStatusId { get; set; }
        public Nullable<long> ShipId { get; set; }
        public long MonthlyReturnId { get; set; }
        public Nullable<System.DateTime> DefectDate { get; set; }
        public string DefectReasonDetail { get; set; }
        public string DefectSolution { get; set; }
        public string Remarks { get; set; }

        public virtual CommonCode CommonCode { get; set; }
        public virtual MonthlyReturn MonthlyReturn { get; set; }
        public virtual MachineryInfo MachineryInfo { get; set; }
    }
}
