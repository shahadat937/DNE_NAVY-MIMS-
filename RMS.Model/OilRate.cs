using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Model
{
    public class OilRate
    {
        public long OilRateId { get; set; }
        public Nullable<long> MonthlyReturnId { get; set; }
        public Nullable<int> OilNameId { get; set; }
        public Nullable<decimal> Expense { get; set; }
        public Nullable<decimal> PerLtRate { get; set; }
        public Nullable<decimal> TotalExpense { get; set; }

    }
}
