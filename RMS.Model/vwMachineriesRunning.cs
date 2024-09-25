using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Model
{
   public partial class vwMachineriesRunning:ReportHeader
    {
        public long MachineryRunningIdentity { get; set; }
        
        public long ShipId { get; set; }
       
        public long HourRunTime { get; set; }
        public long Type { get; set; }
        public Nullable<decimal> Hour { get; set; }
        public Nullable<decimal> Minute { get; set; }
        public string UserId { get; set; }
        public System.DateTime LastUpdate { get; set; }

        public string ReportName { get; set; }
    }
}
