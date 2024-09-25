using System.Collections.Generic;
using System.Web.Mvc;

using RMS.Model;

namespace RMS.Web.Models.ViewModel
{
    public partial class ShipSpeedTrialViewModel
    {
       public ShipSpeedTrial ShipSpeedTrial { get; set; }
       public List<ShipSpeedTrial> ShipSpeedTrials { get; set; }
       //public List<ShipInfo> ShipInfos { get; set; }
       public List<vwShipBranchInfo> ShipInfos { get; set; }
       public string SearchKey { get; set; }
        public ShipSpeedTrialViewModel()
        {
            ShipSpeedTrial=new ShipSpeedTrial();
            ShipSpeedTrials=new List<ShipSpeedTrial>();
            ShipInfos = new List<vwShipBranchInfo>();
        }
        public IEnumerable<SelectListItem> ShipSpeedSelectListItems
        {
            get { return new SelectList(ShipInfos, "ControlShipInfoId", "ControlName"); }

        }
    }
}