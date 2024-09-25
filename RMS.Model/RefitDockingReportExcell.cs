using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Model
{
    public class RefitDockingReportExcell
    {
        public long RefitDockingReportExcellId { get; set; }
        public Nullable<long> DockId { get; set; }
        public Nullable<int> FromYear { get; set; }
        public Nullable<int> ToYear { get; set; }
        public Nullable<long> DocumentType { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string ImageUrl { get; set; }
        public string UserId { get; set; }
        public Nullable<System.DateTime> LastUpdate { get; set; }

        public virtual CommonCode CommonCode { get; set; }

    }
}
