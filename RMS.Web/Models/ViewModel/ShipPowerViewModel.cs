using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RMS.Model;
using Antlr.Runtime.Misc;
using RMS.Model;

namespace RMS.Web.Models.ViewModel
{
    public class ShipPowerViewModel
    {
       public ShipPower ShipPower { get; set; }
        public List<ShipPower> ShipPowers { get; set; }
        //public List<ShipInfo> ShipInfos { get; set; }
        public List<vwShipBranchInfo> ShipInfos { get; set; } 
        public ShipPowerViewModel()
        {
            ShipPowers = new List<ShipPower>();
            ShipPower = new ShipPower();
            ShipInfos = new List<vwShipBranchInfo>();
        }
        public IEnumerable<SelectListItem> ShipSelectListItems
        {
            get { return new SelectList(ShipInfos, "ControlShipInfoId", "ControlName"); }

        }
        public string SearchKey { get; set; }
    }
}