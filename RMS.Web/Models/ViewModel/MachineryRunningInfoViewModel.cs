using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RMS.Model;

namespace RMS.Web.Models.ViewModel
{
    public partial class MachineryRunningInfoViewModel
    {

        public ShipMovementViewModel ShipMovementViewModel { get; set; }
        public FuelConsumptionViewModel FuelConsumptionViewModel { get; set; }
        public MachineryRunningInfo MachineryRunningInfo { get; set; }
        public List<MachineryRunningInfo> MachineryRunningInfos { get; set; }
        public List<CommonCode> CommonCodess { get; set; }
        public List<CommonCode> Type { get; set; } 
        public List<ShipInfo> ShipInfos { get; set; }
        public List<ControlShipInfo> ControlShipInfos { get; set; }
        public List<vwShipBranchInfo> BrControlShipInfos { get; set; }

        public MachineryRunningInfoViewModel()
        {
            FuelConsumptionViewModel =new FuelConsumptionViewModel();
            ShipMovementViewModel = new ShipMovementViewModel();
            MachineryRunningInfo =new MachineryRunningInfo();
            MachineryRunningInfos =new List<MachineryRunningInfo>();
            ControlShipInfos = new List<ControlShipInfo>();
            BrControlShipInfos = new List<vwShipBranchInfo>();
          
            CommonCodess= new List<CommonCode>();
            ShipInfos =new List<ShipInfo>();
            Type =new List<CommonCode>();
        }
       
        public IEnumerable<SelectListItem> CommonCodeSelectListType
        {
            get { return new SelectList(CommonCodess, "CommonCodeID", "TypeValue"); }

        }
        public IEnumerable<SelectListItem> TypeSelectListItems
        {
            get { return new SelectList(Type, "CommonCodeID", "TypeValue"); }

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
        public Nullable<DateTime> DateFrom { get; set; }
        public Nullable<DateTime> DateTo { get; set; } 
    }
}