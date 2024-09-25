using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RMS.Model;
using Antlr.Runtime.Misc;

namespace RMS.Web.Models.ViewModel
{
    public class ICEViewModel
    {
        public List<ICE> ICEs { get; set; }
        public ICE ICE { get; set; }
        public List<ShipInfo> ShipInfos { get; set; }
        public List<ControlShipInfo> ControlShipInfos { get; set; }
        public List<vwShipBranchInfo> BrControlShipInfos { get; set; }
        public List<ReturnReportYear> Years { get; set; }
        public List<CommonCode> ICEFor { get; set; }
        //public List<CommonCode> CommonCodes { get; set; }

        public ICEViewModel()
        {
            ICE = new ICE();
            ICEs = new List<ICE>();
            ShipInfos= new List<ShipInfo>();
            ControlShipInfos = new List<ControlShipInfo>();
            BrControlShipInfos = new List<vwShipBranchInfo>();
            Years = new List<ReturnReportYear>();
            ICEFor = new List<CommonCode>();
            //CommonCodes = new List<CommonCode>();
        }
        public string SearchKey { get; set; }
        [DataType(DataType.Upload)]
        public HttpPostedFileBase ImageUpload { get; set; }

        //public IEnumerable<SelectListItem> CommonCodeListItems
        //{
        //    get { return new SelectList(CommonCodes, "CommonCodeID", "TypeValue"); }
        //}

        public IEnumerable<SelectListItem> ControlShipSelectListItems
        {
            get { return new SelectList(BrControlShipInfos, "ControlShipInfoId", "ControlName"); }
        }
        public IEnumerable<SelectListItem> YearSelectListItems
        {
            get { return new SelectList(Years, "Name", "Name"); }
        }
        public IEnumerable<SelectListItem> ICEForSelectListItems
        {
            get { return new SelectList(ICEFor, "CommonCodeID", "TypeValue"); }
        }
        public Nullable<DateTime> DateFrom { get; set; }
        public Nullable<DateTime> DateTo { get; set; } 
        public int Year { get; set; }
        public long ShipId { get; set; }
        public string ShipName { get;  set; }
        public int? ReportYear { get;  set; }
    }
}