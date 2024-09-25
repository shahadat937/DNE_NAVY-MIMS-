using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Model
{
    public class vwFuelConsumption
    {
        //public long FuelConsumptionIdentity { get; set; }
        //public long ShipId { get; set; }
        public decimal? MainEngines { get; set; }
        public decimal? otherOil { get; set; }
        public decimal? FreshWater { get; set; }
        public decimal MainEnginesLubOil { get; set; }
        public decimal otherOilLubOil { get; set; }
        public decimal FreshWaterLubOil { get; set; }
       
        //public Nullable<DateTime> DateFrom { get; set; }
        //public Nullable<DateTime> DateTo { get; set; }
        //public string Location { get; set; }
        //public Nullable<decimal> RunningHour { get; set; }
        //public Nullable<decimal> Price { get; set; }
        //public string Unit { get; set; }
        //public decimal Quantity { get; set; }
        //public string UserId { get; set; }
        //public DateTime LastUpdate { get; set; }
        //public string FuelTypeHead { get; set; }
        //public string ReportName { get; set; }
    }
}
