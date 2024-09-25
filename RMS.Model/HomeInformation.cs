using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Model
{
   public partial class HomeInformation 
   {
       public List<HomeInformation> ChildDashbord { get; set; }
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
       public long id { get; set; }
       public string Name { get; set; }
       public int level { get; set; }
       public Nullable<int> firstLevel { get; set; }
       public Nullable<int> SecendLevel { get; set; }
       public Nullable<int> ThirdLevel { get; set; }

       public List<UpdateOPlState> AccountInfoTrees = new List<UpdateOPlState>();

       public HomeInformation()
       {
           AllShipShow = new List<ControlShipInfo>();
           CommonCodes = new List<CommonCode>();
           ShipInactive=new List<ShipInactive>();
           ShipInfos= new List<ShipInfo>();
           ProcurementSchedules=new List<ProcurementSchedule>();
           ShipcommOrgnaizations=new List<ShipcommOrgnaization>();
           AccountInfoTrees = new List<UpdateOPlState>();

       }


      
   }
}
