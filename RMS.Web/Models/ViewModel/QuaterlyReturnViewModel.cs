using RMS.Model;
using RMS.Utility;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace RMS.Web.Models.ViewModel
{
    public class QuaterlyReturnViewModel
    {
        public QuaterlyReturn QuaterlyReturn { get; set; }
        public List<QuaterlyReturn> quaterlyReturns { get; set; }
        public List<ControlShipInfo> controlShipInfos { get; set; }
        public List<SelectionList> MachinaryList { get; set; }
        public List<SelectionList> MachinaryState { get; set; }
        public List<SelectionList> OilNames { get; set; }
        public List<ReturnReportNo> returnReportNos { get; set; }
        public List<ReturnReportYear> returnReportYears { get; set; }
        public List<SelectionList> ShipStatus { get; set; }
        public List<QuaterlyConsumption> quaterlyConsumptions { get; set; }
        public List<QuaterlyMainEnginesGeneratorsRunningHour> quaterlyMainEnginesGeneratorsRunningHours { get; set; }
        public List<MachineryInfo> machineryInfos { get; set; }
        public List<QuaterlyReturnEngineerOfficerRemark> QuaterlyReturnEngineerOfficerRemarks { get; set; }

        public QuaterlyReturnViewModel()
        {
            QuaterlyReturn = new QuaterlyReturn();
            quaterlyReturns = new List<QuaterlyReturn>();
            controlShipInfos = new List<ControlShipInfo>();
            MachinaryList = new List<SelectionList>();
            MachinaryState = new List<SelectionList>();
            OilNames = new List<SelectionList>();
            returnReportNos = new List<ReturnReportNo>();
            returnReportYears = new List<ReturnReportYear>();
            ShipStatus = new List<SelectionList>();
            quaterlyConsumptions = new List<QuaterlyConsumption>();
            quaterlyMainEnginesGeneratorsRunningHours = new List<QuaterlyMainEnginesGeneratorsRunningHour>();
            machineryInfos = new List<MachineryInfo>();
            QuaterlyReturnEngineerOfficerRemarks = new List<QuaterlyReturnEngineerOfficerRemark>();
        }

        public IEnumerable<SelectListItem> ShipInfoSelectListItems
        {
            get { return new SelectList(controlShipInfos, "ControlShipInfoId", "ControlName"); }

        }
        public IEnumerable<SelectListItem> MachinarySelectListItem
        {
            get { return new SelectList(MachinaryList, "Value", "Text"); }

        }
        public IEnumerable<SelectListItem> MachinaryStateSelectListItem
        {
            get { return new SelectList(MachinaryState, "Value", "Text"); }
        }
        public IEnumerable<SelectListItem> OilNameSelectListItems
        {
            get { return new SelectList(OilNames, "Value", "Text"); }
        }
        public IEnumerable<SelectListItem> ReportNoSelectListItem
        {
            get { return new SelectList(returnReportNos, "ReturnReportNoId", "ReturnSerialNo"); }
        }
        public IEnumerable<SelectListItem> ReportYearSelectListItem
        {
            get { return new SelectList(returnReportYears, "ReturnReportYearId", "Name"); }
        }
        public IEnumerable<SelectListItem> shipStatusSelectedListItem
        {
            get { return new SelectList(ShipStatus, "Value", "Text"); }
        }
        public string SearchKey { get; set; }
        public long QuaterlyReturnId { get; set; }
        public int ShipId { get; set; }
        public long ShipStatusId { get; set; }
        public Nullable<System.DateTime> ReportMonthDate { get; set; }
        public int ReturnReportNoId { get; set; }
        public int ReportYearId { get; set; }
        public string ReportYear { get; set; }
        public string ReportMonth { get; set; }
        public string ShipName { get; set; }
        public string SearchELement { get; set; }

        public string StartDate { get; set; }
        public string EndDate { get; set; }

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
        public string EngineerOfficerRemarks { get; set; }
        public string MachineryUnderTrialRemarks { get; set; }
        public string RemarksOfAdministrativeAuthority { get; set; }

        public Nullable<System.DateTime> LastUpdateDate { get; set; }
        public string LastUpdateId { get; set; }
        public System.DateTime CreateUserDate { get; set; }
        public string CreateUserId { get; set; }
        public int viewCount { get; set; }
    }
}