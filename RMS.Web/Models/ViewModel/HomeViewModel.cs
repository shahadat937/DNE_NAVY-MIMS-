using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RMS.Model;

namespace RMS.Web.Models.ViewModel
{
    public class HomeViewModel
    {
       //public List<DashboardTreeView> ChildDashbord { get; set; }
      
        public int TotalShipCount { get; set; }
       public int OperationalShipCount { get; set; }
       public int NonOperational { get; set; }
       public string ShipName { get; set; }
       public string Comand { get; set; }
       public string Comand1 { get; set; }
       public string ORG { get; set; }
       public long ORG1 { get; set; }
       public DateTime InactiveDate { get; set; }

       public string ShipAndCraftName { get; set; }
       public string RefitDocked { get; set; }
       public string ProcType { get; set; }
       public decimal Totalprice { get; set; }

       public List<ControlShipInfo> AllShipShow { get; set; }
       public List<ShipInactive> ShipInactive { get; set; }

       public List<ShipInfo> ShipInfos { get; set; }
       public List<CommonCode> CommonCodes { get; set; }
       public List<ProcurementSchedule> ProcurementSchedules { get; set; }
       public List<ShipcommOrgnaization> ShipcommOrgnaizations { get; set; }


          public List<UpdateOPlState> UpdateOPlStatesTreeViews{ get; set; }
        public List<UpdateOPlState> UpdateOPlState { get; set; }
        public List<CommonCode> AccountCategory { get; set; }
        public List<CommonCode> AccountStatus { get; set; }
       
       
       
       public HomeViewModel()
       {
           //this.ChildDashbord = new List<DashboardTreeView>();
           AllShipShow = new List<ControlShipInfo>();
           CommonCodes = new List<CommonCode>();
           ShipInactive=new List<ShipInactive>();
           ShipInfos= new List<ShipInfo>();
           ProcurementSchedules=new List<ProcurementSchedule>();
           ShipcommOrgnaizations=new List<ShipcommOrgnaization>();
           UpdateOPlState = new List<UpdateOPlState>();
           AccountCategory = new List<CommonCode>();
           AccountStatus = new List<CommonCode>();
           UpdateOPlStatesTreeViews = new List<UpdateOPlState>();
       }

       public IEnumerable<SelectListItem> CategoryListItem
       {
           get { return new SelectList(AccountCategory, "DisplayCode", "TypeValue"); }
       }
       public IEnumerable<SelectListItem> AccountStatusListItem
       {
           get { return new SelectList(AccountStatus, "DisplayCode", "TypeValue"); }
       }
    }
}