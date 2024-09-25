using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Model
{
    public partial class vwFortnightlyInfo : ReportHeader
    {
        public vwFortnightlyInfo()
        {
            this.FortnightlyDetails = new HashSet<FortnightlyDetail>();
        }

        public int FortnightlyId { get; set; }
        public Nullable<long> CommonCodeID { get; set; }
        public Nullable<long> ShipInfoIdentity { get; set; }
        public string FortnightlyName { get; set; }
        public string TypeValue { get; set; }
        public string smProgress25Percent { get; set; }
        public string smProgress50Percent { get; set; }
        public string smProgress75Percent { get; set; }
        public string smProgress100Percent { get; set; }
        public string smUnfishWork { get; set; }

        public int sumProgress25Percent { get; set; }
        public int sumProgress50Percent { get; set; }
        public int sumProgress75Percent { get; set; }
        public int sumProgress100Percent { get; set; }
        public int sumUnfishWork { get; set; }
        public DateTime todate { get; set; }
        public string ShipName { get; set; }
        public Nullable<System.DateTime> RefitDockSelectedStartDate { get; set; }
        public Nullable<System.DateTime> RefitDockSelectedEndDate { get; set; }
        public Nullable<System.DateTime> RefitStartDate { get; set; }
        public Nullable<System.DateTime> RefitEndDate { get; set; }
        public Nullable<System.DateTime> DockingDate { get; set; }
        public Nullable<System.DateTime> UnDockingDate { get; set; }
        public Nullable<int> ProgressTime { get; set; }
        public string TotalWorkProgress { get; set; }
        public Nullable<long> TypeOfWork { get; set; }

        public string TotalWorkNo { get; set; }
        public string Progress25Percent { get; set; }
        public string Progress25PercentTotal { get; set; }
        public string Progress50Percent { get; set; }
        public string Progress75Percent { get; set; }
        public string Progress100Percent { get; set; }
        public string UnfishWork { get; set; }
        public string Remarks { get; set; }
        public Nullable<System.DateTime> LastUpdate { get; set; }
        public string UserID { get; set; }
       

        public virtual CommonCode CommonCode { get; set; }
        public virtual CommonCode CommonCode1 { get; set; }
        public virtual ICollection<FortnightlyDetail> FortnightlyDetails { get; set; }
        public virtual ShipInfo ShipInfo { get; set; }
    }
}
