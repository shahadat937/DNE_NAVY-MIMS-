using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RMS.Model;

namespace RMS.Web.Models.ViewModel
{
    public class RefitDockingScheduleViewModel
    {
        public RefitDockingSchedule RefitDockingSchedule { get; set; }
        public RefitDockingReportExcell RefitDockingReportExcell { get; set; }
        public List<RefitDockingReportExcell> RefitDockingReportExcellList { get; set; }
        public List<RefitDockingSchedule> RefitDockingSchedules { get; set; }
        public List<ShipInfo> ShipInfos { get; set; }
        public List<ControlShipInfo> ControlShipInfos { get; set; }
        public List<vwShipBranchInfo> BrControlShipInfos { get; set; }
        public List<BranchInfo> BranchInfos { get; set; }
        public List<CommonCode> Docked { get; set; } 
        public List<CommonCode> Status { get; set; }
        public List<ReturnReportYear> FromYearList { get; set; }
        public List<ReturnReportYear> ToYearList { get; set; }

        public RefitDockingScheduleViewModel()
        {
            RefitDockingSchedule =new RefitDockingSchedule();
            RefitDockingReportExcell = new RefitDockingReportExcell();
            RefitDockingSchedules =new List<RefitDockingSchedule>();
            ShipInfos =new List<ShipInfo>();
            BranchInfos = new List<BranchInfo>();
            Docked = new List<CommonCode>();
            BrControlShipInfos = new List<vwShipBranchInfo>();
            Status = new List<CommonCode>();
            FromYearList = new List<ReturnReportYear>();
            ToYearList = new List<ReturnReportYear>();
            RefitDockingReportExcellList = new List<RefitDockingReportExcell>();
        }
        public IEnumerable<SelectListItem> ShipSelectListItems
        {
            get { return new SelectList(ShipInfos, "ShipInfoIdentity", "ShipName"); }

        }
        public IEnumerable<SelectListItem> ControlShipSelectListItems
        {
            get { return new SelectList(BrControlShipInfos, "ControlShipInfoId", "ControlName"); }
        }


        public IEnumerable<SelectListItem> ToYearSelectListItems
        {
            get { return new SelectList(ToYearList, "Name", "Name"); }

        }
        public IEnumerable<SelectListItem> FromYearSelectListItems
        {
            get { return new SelectList(FromYearList, "Name", "Name"); }
        }



        public IEnumerable<SelectListItem> BranchSelectList
        {
            get { return new SelectList(BranchInfos, "BranchInfoIdentity", "BranchName"); }

        }
        public IEnumerable<SelectListItem> DockedSelectList
        {
            get { return new SelectList(Docked, "CommonCodeID", "TypeValue"); }

        }
        public IEnumerable<SelectListItem> StatusSelectList
        {
            get { return new SelectList(Status, "CommonCodeID", "TypeValue"); }

        }
        public string SearchKey { get; set; }
        public int FromYear { get; set; }
        public int ToYear { get; set; }
        public string DockName { get; set; }

        [DataType(DataType.Upload)]
        public HttpPostedFileBase ImageUpload { get; set; }
    }
}