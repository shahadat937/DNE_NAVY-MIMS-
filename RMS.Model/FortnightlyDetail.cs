using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Model
{
   public partial class FortnightlyDetail
    {
        public int FortnightlyDeatailsId { get; set; }
        public int FortnightlyId { get; set; }
        public Nullable<System.DateTime> RefitDockSelectedStartDate { get; set; }
        public Nullable<System.DateTime> RefitDockSelectedEndDate { get; set; }
        public Nullable<System.DateTime> RefitStartDate { get; set; }
        public Nullable<System.DateTime> RefitEndDate { get; set; }
        public Nullable<System.DateTime> DockingDate { get; set; }
        public Nullable<System.DateTime> UnDockingDate { get; set; }
        public Nullable<int> ProgressTime { get; set; }
        public string TotalWorkProgress { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> CreateUserId { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public Nullable<int> UpdateUserId { get; set; }

        public virtual FortnightlyInfo FortnightlyInfo { get; set; }
        public CommonCode CommonCode { get; set; }
    }
}
