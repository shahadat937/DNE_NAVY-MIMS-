using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Model
{
    public partial class ReturnReportYear
    {
        public ReturnReportYear()
        {
            this.MonthlyReturns = new HashSet<MonthlyReturn>();
            this.QuaterlyReturns = new HashSet<QuaterlyReturn>();
        }

        public int ReturnReportYearId { get; set; }
        public Nullable<long> Name { get; set; }


        public virtual ICollection<MonthlyReturn> MonthlyReturns { get; set; }

        public virtual ICollection<QuaterlyReturn> QuaterlyReturns { get; set; }
    }
}
