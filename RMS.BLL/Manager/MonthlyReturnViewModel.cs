using System;
using System.Collections.Generic;

namespace RMS.BLL.Manager
{
    internal class MonthlyReturnViewModel
    {
        public MonthlyReturnViewModel()
        {
            DefectMachinaries = new List<DefectMachinaryViewModel>();
            MachinaryRunningHours = new List<MachinaryRunningHourViewModel>();
            POLExpenseInfoes = new List<POLExpenseInfoesViewModel>();
        }

        public long MonthlyReturnId { get; set; }
        public long? ShipId { get; set; }
        public Nullable<long> ShipStatusId { get; set; }
        public Nullable<int> ReportMonthId { get; set; }
        public Nullable<int> ReportYearId { get; set; }
        public virtual ICollection<DefectMachinaryViewModel> DefectMachinaries { get; set; }
        public virtual ICollection<MachinaryRunningHourViewModel> MachinaryRunningHours { get; set; }
        public virtual ICollection<POLExpenseInfoesViewModel> POLExpenseInfoes { get; set; }
        

    }


    public class DefectMachinaryViewModel
    {
        public long DefectMachinaryId { get; set; }
        public long? MachineId { get; set; }
        public string MachineName {get; set; }
        public Nullable<long> MachinaryStatusId { get; set; }
        public string MachinaryStatusName { get; set; }
        public long? ShipId { get; set; }
        public long? MonthlyReturnId { get; set; }
        public string DefectReasonDetail { get; set; }
        public DateTime? DefectDate { get;  set; }
        public string DefectAction { get;  set; }
        public string Remarks { get;  set; }
    }
    public class MachinaryRunningHourViewModel
    {
        public long MachinaryRunningHourId { get; set; }
        public long? NoOPSMachineId { get; set; }
        public string NoOPSMachineName { get; set; }
        public long? ShipId { get; set; }
        public long? MonthlyReturnId { get; set; }
        public int? RunningHr { get; set; }
        public int? RunningMin { get; set; }
    }
    public class POLExpenseInfoesViewModel
    {
        public long POLExpenseInfoId { get; set; }
        public long? OilName { get; set; }
        public string OilNameFull { get; set; }
        public long? MonthlyReturnId { get; set; }
        public decimal? OilPerLtRate { get; set; }
        public decimal? ReceivedQtyLtrorUnit { get; set; }
        public decimal? StockAfterUseLtrOrUnit { get; set; }
        public decimal? InitialStockLtrOrUnit { get; set; }
        public decimal? MonthlyUseltrOrUnit { get; set; }
        public decimal? HandoverLtrOrUnit { get; set; }
    }

}