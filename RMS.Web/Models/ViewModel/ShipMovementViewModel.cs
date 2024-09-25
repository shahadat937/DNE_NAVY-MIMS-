using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RMS.Model;

namespace RMS.Web.Models.ViewModel
{
    public partial class ShipMovementViewModel
    {
        public List<ShipMovement> ShipMovements { get; set; }
        public ShipMovement ShipMovement { get; set; }
        public List<ShipInfo> ShipInfos { get; set; }
        public List<vwShipBranchInfo> BrControlShipInfoes { get; set; }
        public string SearchKey { get; set; }
        public ShipMovementViewModel()
        {
            ShipMovements = new List<ShipMovement>();
            ShipMovement=new ShipMovement();
            ShipInfos = new List<ShipInfo>();
            BrControlShipInfoes = new List<vwShipBranchInfo>();
        }
        public IEnumerable<SelectListItem> ShipMovementSelectListItems
        {
            get { return new SelectList(BrControlShipInfoes, "ControlShipInfoId", "ControlName"); }

        }
    }
}