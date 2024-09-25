using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using RMS.BLL.TreeView;
using RMS.Model;

namespace RMS.Web.Models.ViewModel
{
    public class ControlAccountViewModel : ControlShipInfo
    {
        public List<ShipChartofAccount> ChartofAccounts { get; set; }
        public ControlShipInfo ControlShipInfo { get; set; }
        public List<ControlShipInfo> ControlShipInfos { get; set; }
        public List<ControlShipInfo1> ControlShipInfos1 { get; set; }
        public List<CommonCode> SqdCommonCodes { get; set; }
        public List<CommonCode> OrgCommonCodes { get; set; }
        public List<BankInfo> OrgBranch { get; set; }
        public List<CommonCode> ShipstatusCommonCodes { get; set; }
        public List<ShipcommOrgnaization> ShipcommOrgnaizations { get; set; }
        public List<CommonCode> ShipTypes { get; set; }
        public List<CommonCode> ClassTypes { get; set; }
        public List<vwPontoon> vwPontoons { get; set; }
        public ControlAccountViewModel()
        {
            ControlShipInfo=new ControlShipInfo();
            ChartofAccounts = new List<ShipChartofAccount>();
            ControlShipInfos =new List<ControlShipInfo>();
            SqdCommonCodes =new List<CommonCode>();
            OrgCommonCodes = new List<CommonCode>();
            OrgBranch = new List<BankInfo>();
            ShipstatusCommonCodes =new List<CommonCode>();
            ShipcommOrgnaizations= new List<ShipcommOrgnaization>();
            ShipTypes = new List<CommonCode>();
            ClassTypes = new List<CommonCode>();
            vwPontoons = new List<vwPontoon>();
        }
        public string SearchKey { get; set; }
        public string OrgBranchName { get; set; }
        public string[] controlnm { get; set; }
        public string[] controlvle { get; set; }
        public IEnumerable<SelectListItem> SqdSelectListItems
        {
            get { return new SelectList(SqdCommonCodes, "CommonCodeID", "TypeValue"); }
        }
        public IEnumerable<SelectListItem> OrgSelectListItems
        {
            get { return new SelectList(OrgBranch, "BranchInfoIdentity", "BranchName"); }
        }
        public IEnumerable<SelectListItem> ShipstatusSelectListItems
        {
            get { return new SelectList(ShipstatusCommonCodes, "CommonCodeID", "TypeValue"); }
        }
        public IEnumerable<SelectListItem> ShipTypeSelectListItems
        {
            get { return new SelectList(ShipTypes, "CommonCodeID", "TypeValue"); }
        }
        public IEnumerable<SelectListItem> ClassTypeSelectListItems
        {
            get { return new SelectList(ClassTypes, "CommonCodeID", "TypeValue"); }
        }


    }
}