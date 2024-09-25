using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RMS.Model;
using RMS.Utility;
using System.ComponentModel.DataAnnotations;

namespace RMS.Web.Models.ViewModel
{
    public partial class As_AsViewModel
    {
        public List<AsAndAsInfo> AsAsInfos { get; set; }
        public AsAndAsInfo AsAsInfo { get; set; }
        public List<ShipInfo> ShipInfos { get; set; }

        public List<ControlShipInfo> ControlShipInfos { get; set; }
        public List<vwShipBranchInfo> BrControlShipInfos { get; set; }
        public long SearchKey { get; set; }
        public List<SelectionList> VerifyType { get; set; }
        public List<SelectionList> TempDisCodes { get; set; }
        public List<SelectionList> ClassList { get; set; }
        [DataType(DataType.Upload)]
        public HttpPostedFileBase ImageUpload { get; set; }
        public As_AsViewModel()
        {
            AsAsInfos = new List<AsAndAsInfo>();
            AsAsInfo = new AsAndAsInfo();
            ShipInfos = new List<ShipInfo>();
            ControlShipInfos = new List<ControlShipInfo>();
            BrControlShipInfos = new List<vwShipBranchInfo>();
            VerifyType = new List<SelectionList>();
            TempDisCodes = new List<SelectionList>();
            ClassList = new List<SelectionList>();
        }
        public IEnumerable<SelectListItem> AsAsInfoSelectListItems
        {
            get { return new SelectList(ShipInfos, "ShipInfoIdentity", "ShipName"); }

        }
        public IEnumerable<SelectListItem> TempDisCodeSelectListItems
        {
            get { return new SelectList(TempDisCodes, "Value", "Text"); }

        }
        public IEnumerable<SelectListItem> ControlShipSelectListItems
        {
            get { return new SelectList(BrControlShipInfos, "ControlShipInfoId", "ControlName"); }
        }
        public IEnumerable<SelectListItem> ClassSelectListItems
        {
            get { return new SelectList(ClassList, "Value", "Text"); }
        }
        public IEnumerable<SelectListItem> VerifySelectListCategory
        {
            get { return new SelectList(VerifyType, "Value", "Text"); }
        }
        public Nullable<DateTime> DateFrom { get; set; }
        public Nullable<DateTime> DateTo { get; set; }
        public int IsSucceed { get; set; }
        public string Message { get; set; }
        public int VerifiedType { get; set; }
    }
}