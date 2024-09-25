using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Model
{
    public partial class vwFuelConsumption1_Result:ReportHeader
    {
        public string ShipName { get; set; }
        public string Machinery { get; set; }
        public string Fuel { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public string Unit { get; set; }
        public string ReportName { get; set; }
        
    }
}
