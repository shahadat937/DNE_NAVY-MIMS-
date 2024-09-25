using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Model
{
    public partial class QuaterlyReturnEngineerOfficerRemark
    {
        public long QuaterlyReturnEngineerOfficerRemarkId { get; set; }
        public string RemarkTitle { get; set; }
        public string RemarkDetails { get; set; }
        public Nullable<long> QuaterlyReturnId { get; set; }

        public virtual QuaterlyReturn QuaterlyReturn { get; set; }
    }
}
