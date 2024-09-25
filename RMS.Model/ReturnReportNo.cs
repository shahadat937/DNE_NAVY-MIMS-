using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Model
{
    public partial class ReturnReportNo
    {
        public ReturnReportNo()
        {
            this.MonthlyReturns = new HashSet<MonthlyReturn>();
            this.QuaterlyReturns = new HashSet<QuaterlyReturn>();
        }

        public int ReturnReportNoId { get; set; }
        public string ReturnName { get; set; }
        public string ReturnSerialNo { get; set; }


        public virtual ICollection<MonthlyReturn> MonthlyReturns { get; set; }

        public virtual ICollection<QuaterlyReturn> QuaterlyReturns { get; set; }
    }
}
