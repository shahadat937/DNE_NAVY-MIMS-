

namespace RMS.Model
{
    using System;
    using System.Collections.Generic;
   public partial class vwDamageMachinery:ReportHeader
    {
        public long MachineriesDescriptionIdentity { get; set; }
        public string SerialNo { get; set; }
        public Nullable<System.DateTime> ReportDate { get; set; }
        public long ShipId { get; set; }
        public string ShipName { get; set; }
       
        public string Description { get; set; }
        public string MobilityDescription { get; set; }
        public Nullable<System.DateTime> DamageDate { get; set; }
        public string RepairTime { get; set; }
        public string Remarks { get; set; }
        public string MachineSerialNo { get; set; }
        public string MachineSide { get; set; }
        public string MachineName { get; set; }
        public string Hour { get; set; }
        public string Minute { get; set; }
        public string Duration { get; set; }
        public string MachineRemarks { get; set; }
        public string FuelSerialNo { get; set; }
        public string FuelName { get; set; }
        public string StockFuelOil { get; set; }
        public string FuelConsumption { get; set; }
        public string UnitPrice { get; set; }
        public string TotalPrice { get; set; }
        public string UserId { get; set; }
        public System.DateTime LastUpdate { get; set; }
        public decimal consum { get; set; }
        public decimal UPrice { get; set; }
        public decimal TotPric { get; set; }
        public string GrandTotal { get; set; }
       

        public virtual ShipInfo ShipInfo { get; set; }
    }
}
