using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Model
{
    public partial class YearlyReturn
    {
        public YearlyReturn()
        {
            this.YearlyReturnDetails = new HashSet<YearlyReturnDetail>();
        }

        public long YearlyReturnId { get; set; }
        public Nullable<int> YearId { get; set; }
        public Nullable<long> DeskNoId { get; set; }
        public string FrameNo { get; set; }
        public Nullable<long> ShipId { get; set; }
        public Nullable<int> YearlyReturnType { get; set; }

        public virtual CommonCode CommonCode { get; set; }
        public virtual ControlShipInfo ControlShipInfo { get; set; }
        public virtual ICollection<YearlyReturnDetail> YearlyReturnDetails { get; set; }
    }
}
