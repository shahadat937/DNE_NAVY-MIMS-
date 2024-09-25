using System.Collections.Generic;
using System.Web.Mvc;
using RMS.Model;

namespace RMS.Web.Models.ViewModel
{
    public class ShipcommOrgnaizationViewModel
    {
        public ShipcommOrgnaization ShipcommOrgnaization { get; set; }
        public List<ShipcommOrgnaization> ShipcommOrgnaizations { get; set; }
        public List<ShipInfo> ShipInfos { get; set; }
        public List<CommonCode> OrgList { get; set; }
        public ShipcommOrgnaizationViewModel()
        {
            ShipcommOrgnaization = new ShipcommOrgnaization();
            ShipcommOrgnaizations = new List<ShipcommOrgnaization>();
            ShipInfos = new List<ShipInfo>();
            OrgList = new List<CommonCode>();
        }
        public string SearchKey { get; set; }
        //public IEnumerable<SelectListItem> SelectedShipListItems
        //{
        //    get { return new SelectList(ShipInfos, "ShipInfoIdentity", "ShipName"); }
        //}
        //public IEnumerable<SelectListItem> SelectedOrg

        //{
        //    get { return new SelectList(ShipInfos, "CommonCodeId", "TypeValue"); }
        //}
    }
}