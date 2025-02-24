//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RMS.Model.Test
{
    using System;
    using System.Collections.Generic;
    
    public partial class ControlShipInfo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ControlShipInfo()
        {
            this.AsAndAsInfoes = new HashSet<AsAndAsInfo>();
            this.DamageMachineriesInfoes = new HashSet<DamageMachineriesInfo>();
            this.DeckInfoes = new HashSet<DeckInfo>();
            this.Documentations = new HashSet<Documentation>();
            this.FortNightlies = new HashSet<FortNightly>();
            this.FortnightlyInfoes = new HashSet<FortnightlyInfo>();
            this.FuelConsumptions = new HashSet<FuelConsumption>();
            this.LubOilConsumptions = new HashSet<LubOilConsumption>();
            this.MachineryRunningInfoes = new HashSet<MachineryRunningInfo>();
            this.MajorDemandOrProcures = new HashSet<MajorDemandOrProcure>();
            this.QuaterlyMainEnginesGeneratorsRunningHours = new HashSet<QuaterlyMainEnginesGeneratorsRunningHour>();
            this.QuaterlyReturns = new HashSet<QuaterlyReturn>();
            this.RefitDockingSchedules = new HashSet<RefitDockingSchedule>();
            this.ShipAndDockings = new HashSet<ShipAndDocking>();
            this.ShipDetails = new HashSet<ShipDetail>();
            this.ShipInactives = new HashSet<ShipInactive>();
            this.ShipInfoes = new HashSet<ShipInfo>();
            this.ShipMovements = new HashSet<ShipMovement>();
            this.ShipPowers = new HashSet<ShipPower>();
            this.ShipRepairInfoes = new HashSet<ShipRepairInfo>();
            this.ShipSpeedTrials = new HashSet<ShipSpeedTrial>();
            this.YearlyReturns = new HashSet<YearlyReturn>();
            this.ICE = new HashSet<ICE>();
            this.MonthlyReturns = new HashSet<MonthlyReturn>();
            this.MachineryInfoes = new HashSet<MachineryInfo>();
        }
    
        public long ControlShipInfoId { get; set; }
        public decimal ParentCode { get; set; }
        public decimal ControlCode { get; set; }
        public string ControlName { get; set; }
        public string ControlValue { get; set; }
        public string AdditionalValue { get; set; }
        public string Sqdn { get; set; }
        public string ContactNo { get; set; }
        public long SQD { get; set; }
        public Nullable<long> Organization { get; set; }
        public Nullable<long> ShipType { get; set; }
        public Nullable<long> Status { get; set; }
        public string Remarks { get; set; }
        public string Authority { get; set; }
        public Nullable<int> ControlLevel { get; set; }
        public Nullable<int> SortOrder { get; set; }
        public bool IsActive { get; set; }
        public string Level1 { get; set; }
        public string Level2 { get; set; }
        public string Level3 { get; set; }
        public string Level4 { get; set; }
        public string Level5 { get; set; }
        public Nullable<System.DateTime> CommotionDate { get; set; }
        public string NickName { get; set; }
        public Nullable<long> ClassId { get; set; }
        public string ShipsPackFile { get; set; }
        public string DisplacementFullLoad { get; set; }
        public string DisplacementLightWeight { get; set; }
        public string Manufacturer { get; set; }
        public string LengthMTR { get; set; }
        public string DateOfCommission { get; set; }
        public string Breadth { get; set; }
        public string HeightOfMast { get; set; }
        public string DraftFwd { get; set; }
        public string Depth { get; set; }
        public string Depth1 { get; set; }
        public string DraftAft { get; set; }
        public string MaximumSpeed { get; set; }
        public string FreshWaterCapacity { get; set; }
        public string MaximumContinuousSpeed { get; set; }
        public string FualCapacity { get; set; }
        public string CruisingSpeed { get; set; }
        public string LuboilCapcity { get; set; }
        public string EconomicSpeed { get; set; }
        public string Address { get; set; }
        public Nullable<System.DateTime> LastUpdateDate { get; set; }
        public string LastUpdateId { get; set; }
        public Nullable<System.DateTime> CreateUserDate { get; set; }
        public string CreateUserId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AsAndAsInfo> AsAndAsInfoes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DamageMachineriesInfo> DamageMachineriesInfoes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DeckInfo> DeckInfoes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Documentation> Documentations { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FortNightly> FortNightlies { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FortnightlyInfo> FortnightlyInfoes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FuelConsumption> FuelConsumptions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LubOilConsumption> LubOilConsumptions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MachineryRunningInfo> MachineryRunningInfoes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MajorDemandOrProcure> MajorDemandOrProcures { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuaterlyMainEnginesGeneratorsRunningHour> QuaterlyMainEnginesGeneratorsRunningHours { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuaterlyReturn> QuaterlyReturns { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RefitDockingSchedule> RefitDockingSchedules { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShipAndDocking> ShipAndDockings { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShipDetail> ShipDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShipInactive> ShipInactives { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShipInfo> ShipInfoes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShipMovement> ShipMovements { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShipPower> ShipPowers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShipRepairInfo> ShipRepairInfoes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShipSpeedTrial> ShipSpeedTrials { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<YearlyReturn> YearlyReturns { get; set; }
        public virtual CommonCode CommonCode { get; set; }
        public virtual CommonCode CommonCode1 { get; set; }
        public virtual CommonCode CommonCode2 { get; set; }
        public virtual CommonCode CommonCode3 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ICE> ICE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MonthlyReturn> MonthlyReturns { get; set; }
        public virtual BranchInfo BranchInfo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MachineryInfo> MachineryInfoes { get; set; }
    }
}
