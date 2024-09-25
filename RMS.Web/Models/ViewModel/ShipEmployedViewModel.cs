using System.Collections.Generic;
using System.Web.Mvc;
using RMS.Model;

namespace RMS.Web.Models.ViewModel
{
    public class ShipEmployedViewModel
    {
        public ShipEmployed ShipEmployed { get; set; }
        public List<ShipEmployed> ShipEmployeds { get; set; }
        public List<ShipInfo> ShipInfos { get; set; }
        public List<CommonCode> EmploymentList { get; set; } 
        public ShipEmployedViewModel()
        {
            ShipEmployed = new ShipEmployed();
            ShipEmployeds = new List<ShipEmployed>();
            EmploymentList = new List<CommonCode>();
        }
        public string SearchKey { get; set; }
        public IEnumerable<SelectListItem> SelectedShipListItems
        {
            get { return new SelectList(ShipInfos, "ShipInfoIdentity", "ShipName"); }
        }
        public IEnumerable<SelectListItem> SelectedEmployment
        {
            get { return new SelectList(ShipInfos, "CommonCodeId", "TypeValue"); }
        }
    }
}