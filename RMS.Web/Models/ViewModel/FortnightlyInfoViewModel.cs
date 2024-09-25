using System;
using System.Linq;
using RMS.Model;
using System.Collections.Generic;
using System.Web.Mvc;
using RMS.Utility;

namespace RMS.Web.Models.ViewModel
{
    public class FortnightlyInfoViewModel
    {
        public FortNightly FortnightlyInfo { get; set; }
        public List<FortNightly> FortnightlyInfos { get; set; }
        public List<CommonCode> Sections { get; set; }
        public List<CommonCode> TypeOfWork { get; set; }
        public List<ShipInfo> ShipInfos { get; set; }
        public List<ControlShipInfo> ControlShipInfos { get; set; }
        public List<vwShipBranchInfo> BrControlShipInfos { get; set; }
        public List<SelectionList> DocketList { get; set; }
        public List<SelectionList> DrTypeList { get; set; }
        public FortnightlyInfoViewModel()
        {
            FortnightlyInfo = new FortNightly();
            FortnightlyInfos = new List<FortNightly>();
            Sections = new List<CommonCode>();
            TypeOfWork = new List<CommonCode>();
            ShipInfos = new List<ShipInfo>();
            BrControlShipInfos = new List<vwShipBranchInfo>();
            DocketList = new List<SelectionList>();
            DrTypeList = new List<SelectionList>();
        }

        public IEnumerable<SelectListItem> DrSelectListItems
        {
            get { return new SelectList(DrTypeList, "Value", "Text"); }
        }
        public IEnumerable<SelectListItem> DocketListItems
        {
            get { return new SelectList(DocketList, "Value", "Text"); }
        }
        public IEnumerable<SelectListItem> ControlShipSelectListItems
        {
            get { return new SelectList(BrControlShipInfos, "ControlShipInfoId", "ControlName"); }
        }
        public IEnumerable<SelectListItem> ShipSelectListItems
        {
            get { return new SelectList(ShipInfos, "ShipInfoIdentity", "ShipName"); }

        }
        public IEnumerable<SelectListItem> SectionsListItems
        {
            get { return new SelectList(Sections, "CommonCodeID", "TypeValue"); }

        }
        public IEnumerable<SelectListItem> TypeOfWorkListItems
        {
            get { return new SelectList(TypeOfWork, "CommonCodeID", "TypeValue"); }

        }
        public string SearchKey { get; set; }
        public string SelectValue { get; set; }
    }
}