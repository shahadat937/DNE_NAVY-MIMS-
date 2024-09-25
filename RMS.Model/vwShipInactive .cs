using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Model
{
    public partial class vwShipInactive : ReportHeader
    {
        public long SInactiveIdentity { get; set; }
        public long ControlShipInfoId { get; set; }
        public string ControlName { get; set; }
        public string CrashDetails { get; set; }
        public System.DateTime InactiveDate { get; set; }
        public string TakenStap { get; set; }
        public string Reference { get; set; }
        public string Command { get; set; }
        public DateTime todate { get; set; }
        public string UserID { get; set; }
        public System.DateTime LastUpdate { get; set; }

        public virtual ShipInfo ShipInfo { get; set; }
    }
}
