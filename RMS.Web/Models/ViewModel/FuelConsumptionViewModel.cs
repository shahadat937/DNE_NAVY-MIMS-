using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Antlr.Runtime.Misc;
using RMS.Model;


namespace RMS.Web.Models.ViewModel
{
    public partial class FuelConsumptionViewModel
    {
         public FuelConsumption FuelConsumption { get; set; }
        public List<FuelConsumption> FuelConsumptions { get; set; }
        public List<ShipInfo> ShipInfos { get; set; }
        public List<CommonCode> CommonCodes { get; set; }
        public List<CommonCode> OilTypeList { get; set; }
        public List<MachineryInfo> MachineryInfos { get; set; }
        public string MachinaryOilType { get; set; }
        public string LubOilType { get; set; }
        public List<ControlShipInfo> ControlShipInfos { get; set; }
        public List<vwShipBranchInfo> BrControlShipInfos { get; set; }

        public FuelConsumptionViewModel()
        {
            FuelConsumptions = new List<FuelConsumption>();
            FuelConsumption = new FuelConsumption();
            CommonCodes =new List<CommonCode>();
            ShipInfos =new List<ShipInfo>();
            MachineryInfos =new List<MachineryInfo>();
            OilTypeList = new List<CommonCode>();
            ControlShipInfos = new List<ControlShipInfo>();
            BrControlShipInfos = new List<vwShipBranchInfo>();
        }
        public IEnumerable<SelectListItem> ShipSelectListItems
        {
            get { return new SelectList(ShipInfos, "ShipInfoIdentity", "ShipName"); }

        }
        public IEnumerable<SelectListItem> CommonCodeSelectList
        {
            get { return new SelectList(CommonCodes, "CommonCodeID", "TypeValue"); }

        }
        public IEnumerable<SelectListItem> OilTypeSelectListItems
        {
            get { return new SelectList(OilTypeList, "TypeValue", "TypeValue"); }

        }

        public IEnumerable<SelectListItem> ControlShipSelectListItems
        {
            get { return new SelectList(BrControlShipInfos, "ControlShipInfoId", "ControlName"); }
        }
        public string SearchKey { get; set; }
    }
}