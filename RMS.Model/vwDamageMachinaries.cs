using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Model
{
   public class vwDamageMachinaries
    {
        public long MachineriesDescriptionIdentity { get; set; }
        public long ShipId { get; set; }
        public string SerialNo { get; set; }
        public Nullable<long> Description { get; set; }
        public Nullable<long> MobilityDescription { get; set; }
        public Nullable<System.DateTime> DamageDate { get; set; }
        public Nullable<System.DateTime> ReportDate { get; set; }
        public string RepairTime { get; set; }
        public string Remarks { get; set; }
        public string MachineSerialNo { get; set; }
        public string MachineSide { get; set; }
        public Nullable<long> MachineName { get; set; }
        public string Hour { get; set; }
        public string Minute { get; set; }
        public string Duration { get; set; }
        public string MachineRemarks { get; set; }
        public string FuelSerialNo { get; set; }
        public Nullable<long> FuelOilId { get; set; }
        public Nullable<decimal> StockFuelOil { get; set; }
        public Nullable<decimal> FuelConsumption { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public string UserId { get; set; }
        public System.DateTime LastUpdate { get; set; }
        public Nullable<System.DateTime> EntryDate { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsVerified { get; set; }
        public string VerifiedBy { get; set; }
        public Nullable<System.DateTime> VerifiedDate { get; set; }
        public string ShipName { get; set; }
        public string MachinaryName { get; set; }
        public string Status { get; set; }
        public string MachineName2 { get; set; }
        public string FuelOilName { get; set; }
    }
}
