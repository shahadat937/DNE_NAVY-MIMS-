using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RMS.Model;

namespace RMS.Web.Models.ViewModel
{
    public class ShipRepairViewModel
    {
        public ShipRepairInfo ShipRepairInfo { get; set; }
        public List<ShipRepairInfo> ShipRepairInfoes { get; set; }
        public List<ControlShipInfo> ControlShipInfos { get; set; }
        public List<vwShipBranchInfo> BrControlShipInfos { get; set; }
        public List<ShipInfo> ShipInfos { get; set; }
        public ShipRepairViewModel()
        {
            ShipRepairInfo =new ShipRepairInfo();
            ShipRepairInfoes =new List<ShipRepairInfo>();
            ShipInfos =new List<ShipInfo>();
            BrControlShipInfos = new List<vwShipBranchInfo>();
            
        }
        public IEnumerable<SelectListItem> ShipSelectListItems
        {
            get { return new SelectList(ShipInfos, "ShipInfoIdentity", "ShipName"); }

        }
        public IEnumerable<SelectListItem> ControlShipSelectListItems
        {
            get { return new SelectList(BrControlShipInfos, "ControlShipInfoId", "ControlName"); }
        }
        public string SearchKey { get; set; }
    }
}