using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Model;

namespace RMS.Utility
{
   public class DashboardTreeView
    {
       public List<DashboardTreeView> ChildDashbord { get; set; }
      
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
        public DashboardTreeView()
       {
           this.ChildDashbord = new List<DashboardTreeView>();
           AllShipShow = new List<ControlShipInfo>();
           CommonCodes = new List<CommonCode>();
           ShipInactive=new List<ShipInactive>();
           ShipInfos= new List<ShipInfo>();
           ProcurementSchedules=new List<ProcurementSchedule>();
           ShipcommOrgnaizations=new List<ShipcommOrgnaization>();
       }

    }
}
