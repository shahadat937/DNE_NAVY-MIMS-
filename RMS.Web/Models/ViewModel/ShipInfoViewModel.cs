using System.Collections.Generic;
using System.Web.Mvc;
using RMS.Model;

namespace RMS.Web.Models.ViewModel
{
    public partial class ShipInfoViewModel
    {
        public ShipInfo ShipInfo { get; set; }
        public List<ShipInfo> ShipInfoes { get; set; }
        public List<vwShipInfo> VwShipInfos { get; set; }
        public List<CommonCode> CommonCodes { get; set; }
        public List<CommonCode> TimeDuration { get; set; }
        public List<ControlShipInfo> ControlShipInfos { get; set; }
        public List<vwShipBranchInfo> BrControlShipInfos { get; set; }
        public ControlShipInfo ControlShipInfo { get; set; }
        public List<MachineryInfo> MachineryInfos { get; set; } 

        public ShipInfoViewModel()
        {
            ShipInfoes = new List<ShipInfo>();
            ShipInfo = new ShipInfo();
            VwShipInfos=new List<vwShipInfo>();
            ControlShipInfos = new List<ControlShipInfo>();
            BrControlShipInfos = new List<vwShipBranchInfo>();
            CommonCodes = new List<CommonCode>();
            TimeDuration =new List<CommonCode>();
            ControlShipInfo =new ControlShipInfo();
        }
        public IEnumerable<SelectListItem> ShipSelectListItems
        {
            get { return new SelectList(ShipInfoes, "ControlShipInfoId", "ShipName"); }

        }

        public IEnumerable<SelectListItem> ControlShipSelectListItems
        {
            get { return new SelectList(BrControlShipInfos, "ControlShipInfoId", "ControlName"); }
        }

        public IEnumerable<SelectListItem> SqdSelectListItems
        {
            get { return new SelectList(CommonCodes,"CommonCodeID","TypeValue");}
        }
        public IEnumerable<SelectListItem> TimeDurationItemList
        {
            get { return new SelectList(TimeDuration, "CommonCodeID", "TypeValue"); }
        } 
        public string SearchKey { get; set; }
        public long Search { get; set; }
    }
}