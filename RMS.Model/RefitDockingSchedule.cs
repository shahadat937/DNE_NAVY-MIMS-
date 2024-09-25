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
  public partial class RefitDockingSchedule
  {
      public long RefitDockingIdentity { get; set; }
      [Required]
      public long ControlShipInfoId { get; set; }
      public System.DateTime LastRefitFrom { get; set; }
      public System.DateTime LastRefitTo { get; set; }
      public System.DateTime LastDockingFrom { get; set; }
      public System.DateTime LastDockingTo { get; set; }
      public long Docked { get; set; }
      public Nullable<System.DateTime> PNextRefitFrom { get; set; }
      public Nullable<System.DateTime> PNextRefitTo { get; set; }
      public Nullable<System.DateTime> PNextDockingFrom { get; set; }
      public Nullable<System.DateTime> PNextDockingTo { get; set; }

      public Nullable<int> StatusId { get; set; }
      public string Reason { get; set; }

      public string UserId { get; set; }
      public System.DateTime LastUpdate { get; set; }
      public Nullable<System.DateTime> EntryDate { get; set; }
      public string UpdatedBy { get; set; }
      public Nullable<bool> IsVerified { get; set; }
      public string VerifiedBy { get; set; }
      public Nullable<System.DateTime> VerifiedDate { get; set; }

      public virtual CommonCode CommonCode { get; set; }
      public virtual ControlShipInfo ControlShipInfo { get; set; }

    }
}
