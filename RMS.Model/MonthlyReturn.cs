using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Model
{
    public partial class MonthlyReturn
    {

        public MonthlyReturn()
        {
            this.DefectMachinaries = new HashSet<DefectMachinary>();
            this.MachinaryRunningHours = new HashSet<MachinaryRunningHour>();
            this.POLExpenseInfoes = new HashSet<POLExpenseInfo>();
        }

        public long MonthlyReturnId { get; set; }
        public Nullable<long> ShipId { get; set; }
        public Nullable<long> ShipStatusId { get; set; }
        public Nullable<int> AtSeaHr { get; set; }
        public Nullable<int> AtSeaMin { get; set; }
        public Nullable<int> ShipMonthlyRunningHr { get; set; }
        public Nullable<int> ShipMonthlyRunningMin { get; set; }
        public string RefrigerantKg { get; set; }
        public string FreshWaterTons { get; set; }
        public Nullable<int> NonOperationalDuringMonthHr { get; set; }
        public Nullable<int> NonOperationalDuringMonthMin { get; set; }
        public Nullable<int> AtSeaDuringThisMonthHr { get; set; }
        public Nullable<int> AtSeaDuringThisMonthMin { get; set; }
        public Nullable<int> HarbourDuringThisMonthHr { get; set; }
        public Nullable<int> HarbourDuringThisMonthMin { get; set; }
        public Nullable<int> ReportMonthId { get; set; }
        public Nullable<int> ReportYearId { get; set; }
        public Nullable<bool> MonthlyReturnStatus { get; set; }
        public Nullable<bool> MonthlyReturnSendStatus { get; set; }
        public Nullable<System.DateTime> LastUpdateDate { get; set; }
        public string LastUpdateId { get; set; }
        public Nullable<System.DateTime> CreateUserDate { get; set; }
        public string CreateUserId { get; set; }
        public virtual CommonCode CommonCode { get; set; }
        public virtual ControlShipInfo ControlShipInfo { get; set; }

        public virtual ICollection<DefectMachinary> DefectMachinaries { get; set; }
        public virtual ICollection<MachinaryRunningHour> MachinaryRunningHours { get; set; }
        public virtual ReturnReportNo ReturnReportNo { get; set; }
        public virtual ReturnReportYear ReturnReportYear { get; set; }
        public virtual ICollection<POLExpenseInfo> POLExpenseInfoes { get; set; }
    }
}
