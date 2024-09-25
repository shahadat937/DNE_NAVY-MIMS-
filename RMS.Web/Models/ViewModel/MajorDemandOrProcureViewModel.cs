using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RMS.Model;
using RMS.Utility;

namespace RMS.Web.Models.ViewModel
{
    public class MajorDemandOrProcureViewModel
    {
        public MajorDemandOrProcure MajorDemandOrProcure { get; set; }
        public List<MajorDemandOrProcure> MajorDemandOrProcures { get; set; }
        public List<ControlShipInfo> ConShipInfos { get; set; }
        public List<vwShipBranchInfo> BrControlShipInfos { get; set; }
        public List<SelectionList> VerifyType { get; set; }
        public MajorDemandOrProcureViewModel()
        {
            MajorDemandOrProcure =new MajorDemandOrProcure();
            MajorDemandOrProcures =new List<MajorDemandOrProcure>();
            ConShipInfos = new List<ControlShipInfo>();
            BrControlShipInfos = new List<vwShipBranchInfo>();
            VerifyType = new List<SelectionList>();
        }
        public IEnumerable<SelectListItem> ShipSelectListItems
        {
            get { return new SelectList(BrControlShipInfos, "ControlShipInfoId", "ControlName"); }

        }
        public IEnumerable<SelectListItem> VerifySelectListCategory
        {
            get { return new SelectList(VerifyType, "Value", "Text"); }
        }
        public string SearchKey { get; set; }
        public Nullable<DateTime> DateFrom { get; set; }
        public Nullable<DateTime> DateTo { get; set; }
        public int IsSucceed { get; set; }
        public string Message { get; set; }
        public int VerifiedType { get; set; }
    }
}