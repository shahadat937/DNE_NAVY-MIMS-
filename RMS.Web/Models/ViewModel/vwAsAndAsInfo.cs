using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Model
{
   public partial class vwAsAndAsInfo:ReportHeader
    {
        public long AsAndAsIdentity { get; set; }
        [Required]
        public long ShipId { get; set; }
        public Nullable<System.DateTime> AsAndAsDate { get; set; }
        public int TempDisCodeId { get; set; }
        public string AsAndAsClassification { get; set; }
        public string DescriptionOfProp { get; set; }
        public string FightingEfficiency { get; set; }
        public string SeaGoingEfficiency { get; set; }
        public string EffectOnHabitability { get; set; }
        public string EffectOnStabilityTrim { get; set; }
        public string DockingPlace { get; set; }
        public string AuthorityRemark { get; set; }
        public string UserId { get; set; }
        public string ShipName { get; set; }
        public System.DateTime LastUpdate { get; set; }
        public virtual ShipInfo ShipInfo { get; set; }
    }
}
