using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RMS.BLL.IManager;
using RMS.Model;
using RMS.Utility;

namespace RMS.Web.Models.ViewModel
{
    public partial class ShipInactiveViewModel
    {
        public List<ShipInactive> ShipInactives { get; set; }
        public ShipInactive ShipInactive { get; set; }
        public List<ControlShipInfo> ShipInfos { get; set; }
        public List<vwShipBranchInfo> BrControlShipInfoes { get; set; }
        public List<ControlShipInfo> ShipSeclevel { get; set; }
        public List<ControlShipInfo> ShipThirdlevel { get; set; }
        public List<CommonCode> Command { get; set; }
        public List<CommonCode> OrgCommonCodes { get; set; }
        public long SearchKey { get; set; }
        public string shipCode { get; set; }
        public string DistrictCode { get; set; }
        public List<SelectionList> ShipStatus { get; set; }
        public List<MachineryInfo> Machineries { get; set; }
        public ShipInactiveViewModel()
        {
            ShipInactives = new List<ShipInactive>();
            ShipInactive = new ShipInactive();
            ShipInfos = new List<ControlShipInfo>();
            BrControlShipInfoes = new List<vwShipBranchInfo>();
            Command = new List<CommonCode>();
            OrgCommonCodes = new List<CommonCode>();
            ShipSeclevel = new List<ControlShipInfo>();
            ShipThirdlevel = new List<ControlShipInfo>();
            ShipStatus = new List<SelectionList>();
            Machineries = new List<MachineryInfo>();
        }
        public IEnumerable<SelectListItem> shipStatusSelectedListItem
        {
            get { return new SelectList(ShipStatus, "Value", "Text"); }
        }
        public IEnumerable<SelectListItem> MachinerySelectListItems
        {
            get { return new SelectList(Machineries, "MachineryInfoIdentity", "Machinery"); }

        }
        public IEnumerable<SelectListItem> OrgSelectListItems
        {
            get { return new SelectList(OrgCommonCodes, "CommonCodeID", "TypeValue"); }
        }
        public IEnumerable<SelectListItem> ShipInactiveSelectListItems
        {
            get { return new SelectList(BrControlShipInfoes, "ControlShipInfoId", "ControlName"); }

        }
        public IEnumerable<SelectListItem> CommandShipInactiveSelectListItems
        {
            get { return new SelectList(Command, "CommonCodeID", "TypeValue"); }

        }
        public IEnumerable<SelectListItem> ShipSecSelectListItems
        {
            get { return new SelectList(ShipSeclevel, "Level2", "ControlName"); }

        }

        public IEnumerable<SelectListItem> ShipThirdSelectListItems
        {
            get { return new SelectList(ShipThirdlevel, "Level3", "ControlName"); }

        }
        public long S { get; set; }      
        public long R { get; set; }

    }
}