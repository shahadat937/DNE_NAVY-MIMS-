using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Model
{
    public partial class QuaterlyReturn
    {
        public QuaterlyReturn()
        {
            this.QuaterlyMainEnginesGeneratorsRunningHours = new HashSet<QuaterlyMainEnginesGeneratorsRunningHour>();
            this.QuaterlyReturnEngineerOfficerRemarks = new HashSet<QuaterlyReturnEngineerOfficerRemark>();
        }

        public long QuaterlyReturnId { get; set; }
        public long ShipId { get; set; }
        public long ShipStatusId { get; set; }
        public int ReturnReportNoId { get; set; }
        public int ReportYearId { get; set; }
        public Nullable<int> TotalNoOfDay { get; set; }
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
        public Nullable<long> MainEngineFuelNameId { get; set; }
        public Nullable<decimal> MainEngineFuelQuantity { get; set; }
        public Nullable<long> MainEngineLubOilNameId { get; set; }
        public Nullable<decimal> MainEngineLubOilQuantity { get; set; }
        public Nullable<long> DGLubOilNameId { get; set; }
        public Nullable<decimal> DGLubOilQuantity { get; set; }
        public Nullable<long> DGAndOthersFuelNameId { get; set; }
        public Nullable<decimal> DGAndOthersFuelQuantity { get; set; }
        public Nullable<long> OthersLubOilNameId { get; set; }
        public Nullable<decimal> OthersLubOilQuantity { get; set; }
        public string PercentOfLubOilQuaterlyConsumption { get; set; }
        public string LoadAtSeaQuaterlyConsumption { get; set; }
        public string LoadAtHarbourQuaterlyConsumption { get; set; }
        public string RefrigerantQuaterlyConsumption { get; set; }
        public string FreshWaterQuaterlyConsumption { get; set; }
        public string MachineryUnderTrialRemarks { get; set; }
        public string RemarksOfAdministrativeAuthority { get; set; }
        public Nullable<System.DateTime> LastUpdateDate { get; set; }
        public string LastUpdateId { get; set; }
        public System.DateTime CreateUserDate { get; set; }
        public string CreateUserId { get; set; }

        public virtual CommonCode CommonCode { get; set; }
        public virtual CommonCode CommonCode1 { get; set; }
        public virtual CommonCode CommonCode2 { get; set; }
        public virtual CommonCode CommonCode3 { get; set; }
        public virtual CommonCode CommonCode4 { get; set; }
        public virtual CommonCode CommonCode5 { get; set; }
        public virtual ControlShipInfo ControlShipInfo { get; set; }
        public virtual ICollection<QuaterlyMainEnginesGeneratorsRunningHour> QuaterlyMainEnginesGeneratorsRunningHours { get; set; }
        public virtual ReturnReportNo ReturnReportNo { get; set; }
        public virtual ReturnReportYear ReturnReportYear { get; set; }
        public virtual ICollection<QuaterlyReturnEngineerOfficerRemark> QuaterlyReturnEngineerOfficerRemarks { get; set; }
    }
}
