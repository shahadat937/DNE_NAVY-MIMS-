using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Model
{
 public  partial class RunningHourInfo
    {
        public long RunningMachinariesIdentity { get; set; }
        public long MachinariesInfoIdentity { get; set; }
        public Nullable<System.DateTime> RunningDate { get; set; }
        public Nullable<decimal> RunningHour { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string CreateUserId { get; set; }
        public Nullable<System.DateTime> LastUpdateDate { get; set; }
        public string LastUpdateId { get; set; }
        public bool IsVerified { get; set; }
        public string VerifiedBy { get; set; }
        public Nullable<System.DateTime> VerifiedDate { get; set; }


        public virtual MachineryInfo MachineryInfo { get; set; }
    }
}
