using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RMS.Model;

namespace RMS.Web.Models.ViewModel
{
    public class LubOilConsumptionViewModel
    {  
        public LubOilConsumption LubOilConsumption { get; set; }
        public List<LubOilConsumption> LubOilConsumptions { get; set; }
     
        public List<CommonCode> CommonCodes { get; set; }
        public List<ShipInfo> ShipInfos { get; set; }
        public List<MachineryInfo> MachineryInfos { get; set; }
        public List<ControlShipInfo> ControlShipInfos { get; set; }
        public LubOilConsumptionViewModel()
        {
            LubOilConsumptions = new List<LubOilConsumption>();
            LubOilConsumption = new LubOilConsumption();
            CommonCodes =new List<CommonCode>();
            ControlShipInfos = new List<ControlShipInfo>();
        }
        public IEnumerable<SelectListItem> ShipSelectListItems
        {
            get { return new SelectList(ShipInfos, "ShipInfoIdentity", "ShipName"); }

        }
        public IEnumerable<SelectListItem> CommonCodeSelectList
        {
            get { return new SelectList(CommonCodes, "CommonCodeID", "Description"); }

        }
        public IEnumerable<SelectListItem> MachineryInfoSelectListItems
        {
            get { return new SelectList(MachineryInfos, "MachineryInfoIdentity", "Machinery"); }

        }
        public IEnumerable<SelectListItem> ControlShipSelectListItems
        {
            get { return new SelectList(ControlShipInfos, "ControlShipInfoId", "ControlName"); }
        }
        public string SearchKey { get; set; }
    }
}