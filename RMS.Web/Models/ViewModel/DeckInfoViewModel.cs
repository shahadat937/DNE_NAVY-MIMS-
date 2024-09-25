using System;
using System.Collections.Generic;
using System.Web.Mvc;
using RMS.Model;

namespace RMS.Web.Models.ViewModel
{
    public partial class DeckInfoViewModel
    {
        public DeckInfo DeckInfo { get; set; }
        public List<DeckInfo> DeckInfos { get; set; }
        public List<ShipInfo> ShipInfos { get; set; }
        public List<ControlShipInfo> ControlShipInfos { get; set; }
        public List<vwShipBranchInfo> BrControlShipInfos { get; set; }
        public List<CommonCode> CommonCodes { get; set; }
        public DeckInfoViewModel()
        {
            DeckInfo =new DeckInfo();
            DeckInfos=new List<DeckInfo>();
            ShipInfos = new List<ShipInfo>();
            CommonCodes=new List<CommonCode>();
            ControlShipInfos = new List<ControlShipInfo>();
            BrControlShipInfos = new List<vwShipBranchInfo>();
        }
        public string SearchKey { get; set; }
        public string ShipName { get; set; }
        public long S { get; set; }
        public Nullable<DateTime> DateFrom { get; set; }
        public Nullable<DateTime> DateTo { get; set; }
        public IEnumerable<SelectListItem> ShipinSelectListItems
        {
            get { return new SelectList(ShipInfos, "ShipInfoIdentity", "ShipName"); }
        }

        public IEnumerable<SelectListItem> ControlShipSelectListItems
        {
            get { return new SelectList(BrControlShipInfos, "ControlShipInfoId", "ControlName"); }
        }

        public IEnumerable<SelectListItem> HullSelectListItems
        {
            get { return new SelectList(CommonCodes, "CommonCodeID", "TypeValue"); }
        }
    }
}