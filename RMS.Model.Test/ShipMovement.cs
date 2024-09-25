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
    
    public partial class ShipMovement
    {
        public long ShipEmployedIdentity { get; set; }
        public long ShipIdentity { get; set; }
        public string LetterNo { get; set; }
        public Nullable<System.DateTime> CompletionDate { get; set; }
        public Nullable<System.DateTime> RefitDate { get; set; }
        public Nullable<System.DateTime> UnrefitDate { get; set; }
        public Nullable<System.DateTime> DockingDate { get; set; }
        public Nullable<System.DateTime> UndockingDate { get; set; }
        public Nullable<int> MaintenancePeriod { get; set; }
        public Nullable<System.DateTime> PowerTrial { get; set; }
        public string NonOperational { get; set; }
        public string InspectionBy { get; set; }
        public Nullable<int> AtSea { get; set; }
        public Nullable<int> DistanceRun { get; set; }
        public Nullable<int> AtNormalNotice { get; set; }
        public Nullable<int> TimeUnderWayHour { get; set; }
        public Nullable<int> TimeUnderWayMiniute { get; set; }
        public Nullable<int> TimeAwaitingOrderHour { get; set; }
        public Nullable<int> TimeAwaitingOrderMiniute { get; set; }
        public string MachineryUnderTrialRemarks { get; set; }
        public string EngineerOfficerRemarks { get; set; }
        public string AdministrativeAuthorityRemarks { get; set; }
        public string UserId { get; set; }
        public System.DateTime LastUpdate { get; set; }
    
        public virtual ControlShipInfo ControlShipInfo { get; set; }
    }
}
