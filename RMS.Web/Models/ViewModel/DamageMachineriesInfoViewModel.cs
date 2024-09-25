using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using RMS.Model;
using RMS.Utility;

namespace RMS.Web.Models.ViewModel
{
    public partial class DamageMachineriesInfoViewModel
    {
        public DamageMachineriesInfo DamageMachineriesInfo { get; set; }
        public MonthlyReturn MonthlyReturn { get; set; }
        public List<DamageMachineriesInfo> DamageMachineriesInfos { get; set; }
        public List<DamageMachineriesInfo> PolCalculation { get; set; }
        public List<DamageMachineriesInfo> runninghour { get; set; }
        public List<ShipInfo> ShipInfos { get; set; }
        public List<CommonCode> POLItemList { get; set; }
        public List<ControlShipInfo> ControlShipInfos { get; set; }
        public List<vwShipBranchInfo> BrControlShipInfos { get; set; }
        public List<SelectionList> MachinaryList { get; set; }
        public List<SelectionList> MachinaryState { get; set; }
        public List<vwDamageMachinaries> VwDamageMachinarieses { get; set; }
        public List<SelectionList> VerifyType { get; set; }
        public List<MonthlyReturn> monthlyReturns { get; set; }
        public List<DefectMachinary> defectMachinaries { get; set; }
        public List<MachinaryRunningHour> machinaryRunningHours { get; set; }
        public List<MachineryInfo> machineryInfos { get; set; }
        public List<POLExpenseInfo> polExpenseInfos { get; set; }
        public List<ReturnReportNo> returnReportNos { get; set; }
        public List<ReturnReportYear> returnReportYears { get; set; }
        public DamageMachineriesInfoViewModel()
        {
            DamageMachineriesInfo = new DamageMachineriesInfo();
            MonthlyReturn = new MonthlyReturn();
            DamageMachineriesInfos = new List<DamageMachineriesInfo>();
            PolCalculation = new List<DamageMachineriesInfo>();
            runninghour = new List<DamageMachineriesInfo>();
            ShipInfos = new List<ShipInfo>();
            POLItemList = new List<CommonCode>();
            ControlShipInfos = new List<ControlShipInfo>();
            BrControlShipInfos = new List<vwShipBranchInfo>();
            MachinaryList = new List<SelectionList>();
            MachinaryState = new List<SelectionList>();
            VwDamageMachinarieses = new List<vwDamageMachinaries>();
            VerifyType = new List<SelectionList>();
            monthlyReturns = new List<MonthlyReturn>();
            defectMachinaries = new List<DefectMachinary>();
            machinaryRunningHours = new List<MachinaryRunningHour>();
            machineryInfos = new List<MachineryInfo>();
            returnReportNos = new List<ReturnReportNo>();
            returnReportYears = new List<ReturnReportYear>();
        }

        public IEnumerable<SelectListItem> ReportMonthSelectListItem
        {
            get { return new SelectList(returnReportNos, "ReturnReportNoId", "ReturnSerialNo"); }
        }
        public IEnumerable<SelectListItem> ReportYearSelectListItem
        {
            get { return new SelectList(returnReportYears, "ReturnReportYearId", "Name"); }
        }
        public IEnumerable<SelectListItem> MachinaryStateSelectListItem
        {
            get { return new SelectList(MachinaryState, "Value", "Text"); }
        }
        public IEnumerable<SelectListItem> MachinarySelectListItem
        {
            get { return new SelectList(MachinaryList, "Value", "Text"); }

        }
        public IEnumerable<SelectListItem> ShipSelectListItems
        {
            get { return new SelectList(ShipInfos, "ControlShipInfoId", "ShipName"); }

        }

        public IEnumerable<SelectListItem> ShipInfoSelectListItems
        {
            get { return new SelectList(ControlShipInfos, "ControlShipInfoId", "ControlName"); }

        }

        public IEnumerable<SelectListItem> PolsSelectListItems
        {
            get { return new SelectList(POLItemList, "CommonCodeID", "BTypeValue"); }

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
        public Nullable<DateTime> DateFrom { get; set; }
        public Nullable<DateTime> DateTo { get; set; }
        public Nullable<int> ReportMonthId { get; set; }
        public Nullable<int> ReportYearId { get; set; }
        public long MonthlyReturnId { get; set; }
        public string ReportYear { get; set; }
        public string ReportMonth { get; set; }
        public string SearchELement { get; set; }
        public string BStockFuel { get; set; }
        public string BFuelConsumtion { get; set; }
        public string BFuelConsumtionMoney { get; set; }
        public string FuelSerialNo { get; set; }
        public string FuelName { get; set; }
        public string ShipName { get; set; }
        public string StockFuelOil { get; set; }
        public string FuelConsumption { get; set; }
        public string UnitPrice { get; set; }
        public string TotalPrice { get; set; }
        public int IsSucceed { get; set; }
        public string Message { get; set; }
        public int VerifiedType { get; set; }

    }
}