using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RMS.Model;

namespace RMS.Web.Models.ViewModel
{
    public partial class ShipInactiveOrgViewModel
    {
        public List<ShipInactiveOrg> ShipInactiveOrgs { get; set; }
        public ShipInactiveOrg ShipInactiveOrg { get; set; }
        public List<ShipInfo> ShipInfos { get; set; }
        public List<CommonCode> CommonCodes { get; set; }
        public string SearchKey { get; set; }
        public ShipInactiveOrgViewModel()
        {
            ShipInactiveOrgs = new List<ShipInactiveOrg>();
            ShipInactiveOrg = new ShipInactiveOrg();
            ShipInfos = new List<ShipInfo>();
            CommonCodes = new List<CommonCode>();
        }
        public IEnumerable<SelectListItem> ShipInactiveOrgSelectListItems
        {
            get { return new SelectList(ShipInfos, "ShipInfoIdentity", "ShipName"); }

        }

        public IEnumerable<SelectListItem> CommonCodeSelectList
        {
            get { return new SelectList(CommonCodes, "CommonCodeID", "TypeValue"); }

        }
    }
}