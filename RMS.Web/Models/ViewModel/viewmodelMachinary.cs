using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RMS.Model;

namespace RMS.Web.Models.ViewModel
{
    public partial class viewmodelMachinary
    {
        public MachineryInfo MachineryInfo { get; set; }
        public List<MachineryInfo> MachineryInfos { get; set; }
        public List<ShipInfo> ShipInfos { get; set; }
        public ShipInfo ShipInfo { get; set; }
        public List<ShipInfo> ShipInfoes { get; set; }
        public List<vwShipInfo> VwShipInfos { get; set; }
        public List<CommonCode> CommonCodes { get; set; }
        public List<CommonCode> TimeDuration { get; set; } 
        public List<ControlShipInfo> ControlShipInfos { get; set; }
        public ControlShipInfo ControlShipInfo { get; set; }

        public viewmodelMachinary()
        {
             MachineryInfo =new MachineryInfo();
            MachineryInfos =new List<MachineryInfo>();
            ShipInfos =new List<ShipInfo>();
             ShipInfoes = new List<ShipInfo>();
            ShipInfo = new ShipInfo();
            VwShipInfos=new List<vwShipInfo>();
            ControlShipInfos = new List<ControlShipInfo>();
            CommonCodes = new List<CommonCode>();
            TimeDuration =new List<CommonCode>();
            ControlShipInfo =new ControlShipInfo();
            
        }

        public IEnumerable<SelectListItem> ShipSelectListItemsmachinary
        {
            get { return new SelectList(ShipInfos, "ShipInfoIdentity", "ShipName"); }

        }
        public string SearchKey { get; set; }
        public string MSearchKey { get; set; }



    

    
        public IEnumerable<SelectListItem> ShipSelectListItems
        {
            get { return new SelectList(ShipInfoes, "ControlShipInfoId", "ShipName"); }

        }

        public IEnumerable<SelectListItem> ControlShipSelectListItems
        {
            get { return new SelectList(ControlShipInfos, "ControlShipInfoId", "ControlName"); }
        }

        public IEnumerable<SelectListItem> SqdSelectListItems
        {
            get { return new SelectList(CommonCodes,"CommonCodeID","TypeValue");}
        }
        public IEnumerable<SelectListItem> TimeDurationItemList
        {
            get { return new SelectList(TimeDuration, "CommonCodeID", "TypeValue"); }
        } 
      
}
}