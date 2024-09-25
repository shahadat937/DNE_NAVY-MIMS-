using RMS.Model;
using RMS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RMS.Web.Models.ViewModel
{
    public class YearlyReturnViewModel
    {
        public  YearlyReturn YearlyReturn { get; set; }
        public List<YearlyReturn> YearlyReturns { get; set; }
        public List<ICE> ICEs { get; set; }
        public List<ControlShipInfo> controlShipInfos { get; set; }
        public List<ReturnReportYear> returnReportYears { get; set; }
        public List<YearlyReturnDetail> YearlyReturnDetails { get; set; }
        public List<SelectionList> DeskNoList { get; set; }

        public YearlyReturnViewModel()
        {
            YearlyReturn = new YearlyReturn();
            YearlyReturns = new List<YearlyReturn>();
            ICEs = new List<ICE>();
            controlShipInfos = new List<ControlShipInfo>();
            returnReportYears = new List<ReturnReportYear>();
            YearlyReturnDetails = new List<YearlyReturnDetail>();
            DeskNoList = new List<SelectionList>();
        }

        public IEnumerable<SelectListItem> ShipInfoSelectListItems
        {
            get { return new SelectList(controlShipInfos, "ControlShipInfoId", "ControlName"); }

        }
       
        public IEnumerable<SelectListItem> ReportYearSelectListItem
        {
            get { return new SelectList(returnReportYears, "Name", "Name"); }
        }
        public IEnumerable<SelectListItem> DeskNoSelectListItem
        {
            get { return new SelectList(DeskNoList, "Value", "Text"); }
        }
        public string SearchKey { get; set; }
        public int ShipId { get; set; }
        public long ShipStatusId { get; set; }
        public Nullable<System.DateTime> ReportMonthDate { get; set; }
        public int ReturnReportNoId { get; set; }
        public string ReportYear { get; set; }
        public string ReportMonth { get; set; }
        public string ShipName { get; set; }
        public string SearchELement { get; set; }

        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public long YearlyReturnId { get; set; }
        public Nullable<int> YearId { get; set; }
        public Nullable<int> DeskNoId { get; set; }
        public string FrameNo { get; set; }


        public Nullable<System.DateTime> LastUpdateDate { get; set; }
        public string LastUpdateId { get; set; }
        public System.DateTime CreateUserDate { get; set; }
        public string CreateUserId { get; set; }

    }
}