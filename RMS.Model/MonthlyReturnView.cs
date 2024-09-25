using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Model
{
    public class MonthlyReturnView
    {
        public MonthlyReturnView()
        {
            this.DefectMachinaries = new HashSet<DefectMachinary>();
            this.MachinaryRunningHours = new HashSet<MachinaryRunningHour>();
            this.POLExpenseInfoes = new HashSet<POLExpenseInfo>();
        }

        public long MonthlyReturnId { get; set; }
        public Nullable<long> ShipId { get; set; }
        public Nullable<long> MachineryId { get; set; }
        public Nullable<long> MechineryStatusId { get; set; }
        public Nullable<System.DateTime> ReportMonthDate { get; set; }
        public Nullable<System.DateTime> DefectDate { get; set; }
        public Nullable<System.DateTime> PossibleRectificationDate { get; set; }
        public string OngoingActionDetail { get; set; }
        public string DefectReasonDetail { get; set; }
        public Nullable<int> RunningTimeHr { get; set; }
        public Nullable<int> RunningTimeMin { get; set; }
        public Nullable<int> DistanceCoveredHr { get; set; }
        public Nullable<int> DistanceCoveredMin { get; set; }
        public Nullable<int> AtSeaHr { get; set; }
        public Nullable<int> AtSeaMin { get; set; }
        public Nullable<int> POLNameId { get; set; }
        public Nullable<decimal> InitialStockLtrOrUnit { get; set; }
        public Nullable<decimal> ReceivedQtyLtrorUnit { get; set; }
        public Nullable<decimal> HandoverLtrOrUnit { get; set; }
        public Nullable<decimal> MonthlyUseltrOrUnit { get; set; }
        public Nullable<decimal> StockAfterUseLtrOrUnit { get; set; }
        public Nullable<int> ShipMonthlyRunningHr { get; set; }
        public Nullable<int> ShipMonthlyRunningMin { get; set; }
        public Nullable<decimal> RefrigerantKg { get; set; }
        public Nullable<decimal> FreshWaterTons { get; set; }
        public Nullable<int> NonOperationalDuringMonthHr { get; set; }
        public Nullable<int> NonOperationalDuringMonthMin { get; set; }
        public Nullable<int> AtSeaDuringThisMonthHr { get; set; }
        public Nullable<int> AtSeaDuringThisMonthMin { get; set; }
        public Nullable<int> HarbourDuringThisMonthHr { get; set; }
        public Nullable<int> HarbourDuringThisMonthMin { get; set; }
        public Nullable<int> ReportMonthId { get; set; }
        public Nullable<int> ReportYearId { get; set; }
        public Nullable<System.DateTime> LastUpdateDate { get; set; }
        public string LastUpdateId { get; set; }
        public Nullable<System.DateTime> CreateUserDate { get; set; }
        public string CreateUserId { get; set; }

        public virtual CommonCode CommonCode { get; set; }
        public virtual ControlShipInfo ControlShipInfo { get; set; }

        public virtual ICollection<DefectMachinary> DefectMachinaries { get; set; }

        public virtual ICollection<MachinaryRunningHour> MachinaryRunningHours { get; set; }
        public virtual MachineryInfo MachineryInfo { get; set; }
        public virtual ReturnReportNo ReturnReportNo { get; set; }

        public virtual ICollection<POLExpenseInfo> POLExpenseInfoes { get; set; }
        public virtual ReturnReportYear ReturnReportYear { get; set; }
    }
}
