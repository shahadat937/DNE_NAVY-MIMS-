using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RMS.Web.Models.ViewModel
{
    public partial class vwShipEmployed
    {
        public DateTime fromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string ShipName { get; set; }
        public DateTime CompletionDate { get; set; }
        public DateTime RefitDate { get; set; }
        public DateTime UnrefitDate { get; set; }
        public DateTime DockingDate { get; set; }
        public DateTime UndockingDate { get; set; }
        public int MaintenancePeriod { get; set; }
        public DateTime PowerTrial { get; set; }
        public string NonOperational { get; set; }
        public string InspectionBy { get; set; }
        public int AtSea { get; set; }
        public Int64 DistanceRun { get; set; }
        public int AtNormalNotice { get; set; }
        public int TimeUnderWayHour { get; set; }
        public int TimeUnderWayMiniute { get; set; }
        public string MachineryUnderTrialRemarks { get; set; }
        public string EngineerOfficerRemarks { get; set; }
        public string AdministrativeAuthorityRemarks { get; set; }

    }
}