using System;
using System.Collections.Generic;

namespace RMS.BLL.Manager
{
    internal class QuaterlyReturnViewModel
    {
        public QuaterlyReturnViewModel()
        {
            QuaterlyReturnEngineerOfficerRemarks = new List<QuaterlyReturnEngineerOfficerRemarklViewModel>();
            QuaterlyMainEnginesGeneratorsRunningHours = new List<QuaterlyMainEnginesGeneratorsRunningHourViewModel>();
        }
        public long QuaterlyReturnId { get; set; }
        public long ShipId { get; set; }
        public long ShipStatusId { get; set; }
        public int ReturnReportNoId { get; set; }
        public int ReportYearId { get; set; }
        public Nullable<bool> QuaterlyReturnStatus { get; set; }
        public Nullable<bool> QuaterlyReturnSendStatus { get; set; }
        public string UndergoingRefit { get; set; }
        public string Refit { get; set; }
        public string InDocking { get; set; }
        public string UnDocking { get; set; }
        public string MaintancePeriod { get; set; }
        public string FullPowerTrial { get; set; }
        public string NonOplReason { get; set; }
        public string AtSea { get; set; }
        public string DistanceRun { get; set; }
        public string AtNormalNotice { get; set; }
        public string TimeUnderway { get; set; }
        public string TimeAwaitingOrder { get; set; }
        public string PercentOfLubOilQuaterlyConsumption { get; set; }
        public string LoadAtSeaQuaterlyConsumption { get; set; }
        public string LoadAtHarbourQuaterlyConsumption { get; set; }
        public string RefrigerantQuaterlyConsumption { get; set; }
        public string FreshWaterQuaterlyConsumption { get; set; }
        public Nullable<System.DateTime> LastUpdateDate { get; set; }
        public string LastUpdateId { get; set; }
        public System.DateTime CreateUserDate { get; set; }
        public string CreateUserId { get; set; }


        public virtual ICollection<QuaterlyReturnEngineerOfficerRemarklViewModel> QuaterlyReturnEngineerOfficerRemarks { get; set; }

        public virtual ICollection<QuaterlyMainEnginesGeneratorsRunningHourViewModel> QuaterlyMainEnginesGeneratorsRunningHours { get; set; }
    }

    public class QuaterlyReturnEngineerOfficerRemarklViewModel
    {
        public long QuaterlyReturnEngineerOfficerRemarkId { get; set; }
        public string RemarkTitle { get; set; }
        public string RemarkDetails { get; set; }
        public Nullable<long> QuaterlyReturnId { get; set; }
    }
    public class QuaterlyMainEnginesGeneratorsRunningHourViewModel
    {
        public long QuaterlyMainEnginesGeneratorsRunningHourId { get; set; }
        public long QuaterlyReturnId { get; set; }
        public long ShipId { get; set; }
        public Nullable<long> MachineGeneratorId { get; set; }
        public Nullable<long> DuringQuaterHr { get; set; }
        public Nullable<long> DuringQuaterMin { get; set; }
        public Nullable<long> SinceLastLOChangeHr { get; set; }
        public Nullable<long> SinceLastLOChangeMin { get; set; }
        public Nullable<long> SinceLastTopOverhaulHr { get; set; }
        public Nullable<long> SinceLastTopOverhaulMin { get; set; }
        public Nullable<long> SinceLastMajorOverhaulHr { get; set; }
        public Nullable<long> SinceLastMajorOverhaulMin { get; set; }
        public Nullable<long> SinceCommissionInBNHr { get; set; }
        public Nullable<long> SinceCommissionInBNMin { get; set; }
        public Nullable<long> SinceInstallationHr { get; set; }
        public Nullable<long> SinceInstallationMin { get; set; }
        public string Remarks { get; set; }
        public string MachineGeneratorName { get; set; }
    }

}

