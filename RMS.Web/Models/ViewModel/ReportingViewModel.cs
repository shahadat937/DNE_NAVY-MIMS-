using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RMS.Model;
using RMS.Utility;
using System.Web.Mvc;

namespace RMS.Web.Models.ViewModel
{
    public class ReportingViewModel : Reporting
    {
        public List<Reporting> ReportingTreeViews { get; set; }
        public List<BranchInfo> ExchangeCompany { get; set; }
        //public List<ControlShipInfo> ShipInfoes { get; set; }
        public List<vwShipBranchInfo> ShipInfoes { get; set; }
        public List<vwShipBankInfo> ShipNameInfoes { get; set; }
        public Dictionary<string, string> Parameters { get; set; }
        public string ReportName { get; set; }
        public List<TrainingInfo> TrainingInfos { get; set; }
        public List<CommonCode> Ranks { get; set; }
        public List<CommonCode> NameOfCourses { get; set; }
        public List<ReturnReportYear> ReturnYearList { get; set; }
        public List<ReturnReportNo> MonthsList { get; set; }
        public List<ReturnReportNo> QuarterList { get; set; }
        
        public ReportingViewModel()
        {
            ReportingTreeViews = new List<Reporting>();
            ExchangeCompany = new List<BranchInfo>();
            ShipInfoes = new List<vwShipBranchInfo>();
            ShipNameInfoes = new List<vwShipBankInfo>();
            Parameters = new Dictionary<string, string>();
            TrainingInfos = new List<TrainingInfo>();
            Ranks = new List<CommonCode>();
            NameOfCourses = new List<CommonCode>();
            ReturnYearList = new List<ReturnReportYear>();
            MonthsList = new List<ReturnReportNo>();
            QuarterList = new List<ReturnReportNo>();
        }
        public IEnumerable<SelectListItem> ExchangeCompanyListItem
        {
            get { return new SelectList(ExchangeCompany, "BranchCode", "BranchName"); }
        }
        public IEnumerable<SelectListItem> ShipListItem
        {
            get { return new SelectList(ShipInfoes, "ControlShipInfoId", "ControlName"); }
        }
        public IEnumerable<SelectListItem> YearListSelectItem
        {
            get { return new SelectList(ReturnYearList, "Name", "Name"); }
        }
        public IEnumerable<SelectListItem> MonthListSelectItem
        {
            get { return new SelectList(MonthsList, "ReturnReportNoId", "ReturnSerialNo"); }
        }
        public IEnumerable<SelectListItem> QuarterListSelectItem
        {
            get { return new SelectList(QuarterList, "ReturnReportNoId", "ReturnSerialNo"); }
        }

        public IEnumerable<SelectListItem> ShipNameListItem
        {
            get { return new SelectList(ShipNameInfoes, "ControlShipInfoId", "ControlName"); }
        }
        public IEnumerable<SelectListItem> RanksListItems
        {
            get { return new SelectList(Ranks, "CommonCodeID", "TypeValue"); }

        }
        public IEnumerable<SelectListItem> NameOfCoursesListItems
        {
            get { return new SelectList(NameOfCourses, "CommonCodeID", "TypeValue"); }

        }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }


    }
}