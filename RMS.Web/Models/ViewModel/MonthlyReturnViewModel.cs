using RMS.Model;
using RMS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RMS.Web.Models.ViewModel
{
    public partial class MonthlyReturnViewModel
    {
        public MonthlyReturn  MonthlyReturn{ get; set; }
        public List<MonthlyReturn> monthlyReturns { get; set; }
        public MonthlyReturnView monthlyReturnView { get; set; }
        public List<DefectMachinary> defectMachinaries { get; set; }
        public List<MachinaryRunningHour> machinaryRunningHours { get; set; }
        public List<POLExpenseInfo> polExpenseInfos { get; set; }
        public List<DamageMachineriesInfo> DamageMachineriesInfos { get; set; }
        public List<DamageMachineriesInfo> PolCalculation { get; set; }
        public List<DamageMachineriesInfo> runninghour { get; set; }
        public List<ShipInfo> ShipInfos { get; set; }
        public List<CommonCode> POLItemList { get; set; }
        public List<ControlShipInfo> ControlShipInfos { get; set; }
        public List<vwShipBranchInfo> BrControlShipInfos { get; set; }
        public List<SelectionList> MachinaryList { get; set; }
        public List<SelectionList> MachinaryState { get; set; }
        public List<SelectionList> OilNames { get; set; }
        public List<vwDamageMachinaries> VwDamageMachinarieses { get; set; }
        public List<SelectionList> VerifyType { get; set; }
        public List<ReturnReportNo> returnReportNos { get; set; }
        public List<ReturnReportYear> returnReportYears { get; set; }
        public List<SelectionList> ShipStatus { get; set; }
        public List<MachineryInfo> machineryInfos { get; set; }
        public MonthlyReturnViewModel()
        {
            MonthlyReturn = new MonthlyReturn();
            monthlyReturns = new List<MonthlyReturn>();
            monthlyReturnView = new MonthlyReturnView();
            defectMachinaries = new List<DefectMachinary>();
            machinaryRunningHours = new List<MachinaryRunningHour>();
            polExpenseInfos = new List<POLExpenseInfo>();
            DamageMachineriesInfos = new List<DamageMachineriesInfo>();
            PolCalculation = new List<DamageMachineriesInfo>();
            runninghour = new List<DamageMachineriesInfo>();
            POLItemList = new List<CommonCode>();
            ControlShipInfos = new List<ControlShipInfo>();
            BrControlShipInfos = new List<vwShipBranchInfo>();
            MachinaryList = new List<SelectionList>();
            MachinaryState = new List<SelectionList>();
            OilNames = new List<SelectionList>();
            VwDamageMachinarieses = new List<vwDamageMachinaries>();
            VerifyType = new List<SelectionList>();
            returnReportNos = new List<ReturnReportNo>();
            returnReportYears = new List<ReturnReportYear>();
            ShipStatus = new List<SelectionList>();
            machineryInfos = new List<MachineryInfo>();
        }
        public IEnumerable<SelectListItem> shipStatusSelectedListItem
        {
            get { return new SelectList(ShipStatus, "Value", "Text"); }
        }
        public IEnumerable<SelectListItem> ReportMonthSelectListItem
        {
            get { return new SelectList(returnReportNos, "ReturnReportNoId", "ReturnSerialNo"); }
        }
        public IEnumerable<SelectListItem> ReportYearSelectListItem
        {
            get { return new SelectList(returnReportYears, "Name", "Name"); }
        }
        public IEnumerable<SelectListItem> MachinaryStateSelectListItem
        {
            get { return new SelectList(MachinaryState, "Value", "Text"); }
        }
        public IEnumerable<SelectListItem> MachinarySelectListItem
        {
            get { return new SelectList(MachinaryList, "Value", "Text"); }

        }
        public IEnumerable<SelectListItem> ShipInfoSelectListItems
        {
            get { return new SelectList(ControlShipInfos, "ControlShipInfoId", "ControlName"); }

        }
        public IEnumerable<SelectListItem> PolsSelectListItems
        {
            get { return new SelectList(POLItemList, "CommonCodeID", "BTypeValue"); }

        }
        public IEnumerable<SelectListItem> OilNameSelectListCategory
        {
            get { return new SelectList(OilNames, "Value", "Text"); }
        }
        public IEnumerable<SelectListItem> ControlShipSelectListItems
        {
            get { return new SelectList(BrControlShipInfos, "ControlShipInfoId", "ControlName"); }
        }
        public IEnumerable<SelectListItem> VerifySelectListCategory
        {
            get { return new SelectList(VerifyType, "Value", "Text"); }
        }
        public string SearchKey { get; set; }
        public long MonthlyReturnId { get; set; }
        public int ShipId { get; set; }
        public Nullable<long> ShipStatusId { get; set; }
        public Nullable<System.DateTime> ReportMonthDate { get; set; }
        public Nullable<int> ReportMonthId { get; set; }
        public Nullable<int> ReportYearId { get; set; }
        public string ReportYear { get; set; }
        public string ReportMonth { get; set; }
        public string ShipName { get; set; }
        public string SearchELement { get; set; }
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

        public  Nullable< int> OilRate {get; set; }
        public Nullable<DateTime> DateFrom { get; set; }
        public Nullable<DateTime> DateTo { get; set; }
        public string BStockFuel { get; set; }
        public string BFuelConsumtion { get; set; }
        public string BFuelConsumtionMoney { get; set; }
        public string FuelSerialNo { get; set; }
        public string FuelName { get; set; }
        public string StockFuelOil { get; set; }
        public string FuelConsumption { get; set; }
        public string UnitPrice { get; set; }
        public string TotalPrice { get; set; }
        public int IsSucceed { get; set; }
        public string Message { get; set; }
        public int VerifiedType { get; set; }
        public int ViewCount { get; set; }

    }
}