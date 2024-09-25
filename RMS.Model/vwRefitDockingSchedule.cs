using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Model
{
    using System;
    using System.Collections.Generic;
  public partial class vwRefitDockingSchedule:ReportHeader
    {
       public long RefitDockingIdentity { get; set; }
      [Required]
      public long ControlShipInfoId { get; set; }
      public Nullable<System.DateTime> LastRefitFrom { get; set; }
      public Nullable<System.DateTime> LastRefitTo { get; set; }
      public Nullable<System.DateTime> LastDockingFrom { get; set; }
      public Nullable<System.DateTime> LastDockingTo { get; set; }
      public long BranchCode { get; set; }
      public string ControlName { get; set; }
      public string Docked { get; set; }
      public Nullable<System.DateTime> PNextRefitFrom { get; set; }
      public Nullable<System.DateTime> PNextRefitTo { get; set; }
      public Nullable<System.DateTime> PNextDockingFrom { get; set; }
      public Nullable<System.DateTime> PNextDockingTo { get; set; }
      public string UserId { get; set; }
      public System.DateTime LastUpdate { get; set; }

      
      public virtual ControlShipInfo ControlShipInfo { get; set; }

        public long firstCol { get; set; }
        public long SecondCol { get; set; }
        public long ThirdCol { get; set; }
        public long forthCol { get; set; }
        public long fifthCol { get; set; }
        public long sixth { get; set; }
        public long seventh { get; set; }
        public long eighth { get; set; }
        public long ninth { get; set; }
        public long tenth { get; set; }
        public long eleventh { get; set; }
        public long twelveth { get; set; }
        public long thirteen { get; set; }
        public long forteenth { get; set; }
        public long fifteenth { get; set; }
        public long sixteenth { get; set; }
        public long seventeenth { get; set; }
        public long eighteenth { get; set; }

        public long nineteenth { get; set; }
        public long twentyth { get; set; }
        public long twentyOneth { get; set; }
        public long twentytwoth { get; set; }

        public long twentythreeth { get; set; }
        public long twentyforeth { get; set; }

        public long fromValue { get; set; }
        public long toValue { get; set; }
        public string DATEDIFF { get; set; }
      
        public DateTime dockingForm { get; set; }
        public DateTime dockingTo { get; set; }
        public long result { get; set; }
        public DateTime fromDate { get; set; }
        public DateTime ToDate { get; set; }
        public virtual BranchInfo BranchInfo { get; set; }
        public virtual ShipInfo ShipInfo { get; set; }
    }
}
