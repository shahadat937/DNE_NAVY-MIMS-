using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Model
{
   public class RefitDockingDefectList
    {
        public long DefectListIdentity { get; set; }
        public string WRNumber { get; set; }
        public string DRNumber { get; set; }
        public Nullable<System.DateTime> DRDate { get; set; }
        public string WorkDescription { get; set; }
        public string Dshop { get; set; }
        public long WorkCategory { get; set; }
        public Nullable<bool> TwentyFivePercent { get; set; }
        public Nullable<bool> FiftyPercent { get; set; }
        public Nullable<bool> SeventyFivePersecnt { get; set; }
        public Nullable<bool> HundredPercent { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string CreateId { get; set; }
        public string LastUpdateId { get; set; }
        public Nullable<System.DateTime> LastUpdateDate { get; set; }
        public Nullable<bool> IsVerified { get; set; }
        public string VerifiedBy { get; set; }
        public Nullable<System.DateTime> VerifiedDate { get; set; }
    }
}
