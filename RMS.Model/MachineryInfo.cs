//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RMS.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class MachineryInfo
    {
        public MachineryInfo()
        {
            this.DamageMachineriesInfoes = new HashSet<DamageMachineriesInfo>();
            this.DamageMachineriesInfoes1 = new HashSet<DamageMachineriesInfo>();
            this.DefectMachinaries = new HashSet<DefectMachinary>();
            this.MachinaryRunningHours = new HashSet<MachinaryRunningHour>();
            this.QuaterlyConsumptions = new HashSet<QuaterlyConsumption>();
            this.QuaterlyMainEnginesGeneratorsRunningHours = new HashSet<QuaterlyMainEnginesGeneratorsRunningHour>();
            this.RunningHourInfoes = new HashSet<RunningHourInfo>();
        }

        public long MachineryInfoIdentity { get; set; }
        public long ControlShipInfoId { get; set; }
        public string Machinery { get; set; }
        public string MachinaryBangla { get; set; }
        public Nullable<long> MachineryCategoryId { get; set; }
        public Nullable<long> EquipmentTypeId { get; set; }
        public Nullable<long> Quantity { get; set; }
        public string Location { get; set; }
        public string Model { get; set; }
        public string Power { get; set; }
        public string PowerUnit { get; set; }
        public string RPM { get; set; }
        public string Pressure { get; set; }
        public string MakerAddress { get; set; }
        public string InstallationDate { get; set; }
        public Nullable<long> TOH { get; set; }
        public Nullable<long> MOH { get; set; }
        public Nullable<decimal> ConsumptionFuel { get; set; }
        public string ConsumptionFuelUnit { get; set; }
        public string LubOilGrade { get; set; }
        public Nullable<decimal> ConsuptionLuboil { get; set; }
        public string ConsumptionLuboilUnit { get; set; }
        public Nullable<long> MachinariesState { get; set; }
        public Nullable<long> LifeTimeType { get; set; }
        public Nullable<decimal> DueTOHTime { get; set; }
        public Nullable<System.DateTime> ExpiredLifeDate { get; set; }
        public Nullable<decimal> DueMOHTime { get; set; }
        public Nullable<System.DateTime> ExpiredLifeDateAlerm { get; set; }
        public string jpFiveConsumtion { get; set; }
        public string Remarks { get; set; }
        public string UserId { get; set; }
        public string UpdateBy { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public System.DateTime LastUpdate { get; set; }
        public Nullable<bool> IsVerified { get; set; }
        public string VerifiedBy { get; set; }
        public Nullable<System.DateTime> VerifiedDate { get; set; }
        public Nullable<double> TotalRunningHr { get; set; }

        public virtual CommonCode CommonCode { get; set; }
        public virtual CommonCode CommonCode1 { get; set; }
        public virtual ControlShipInfo ControlShipInfo { get; set; }

        public virtual ICollection<DamageMachineriesInfo> DamageMachineriesInfoes { get; set; }

        public virtual ICollection<DamageMachineriesInfo> DamageMachineriesInfoes1 { get; set; }

        public virtual ICollection<DefectMachinary> DefectMachinaries { get; set; }

        public virtual ICollection<MachinaryRunningHour> MachinaryRunningHours { get; set; }

        public virtual ICollection<QuaterlyConsumption> QuaterlyConsumptions { get; set; }

        public virtual ICollection<QuaterlyMainEnginesGeneratorsRunningHour> QuaterlyMainEnginesGeneratorsRunningHours { get; set; }

        public virtual ICollection<RunningHourInfo> RunningHourInfoes { get; set; }
    }
}
