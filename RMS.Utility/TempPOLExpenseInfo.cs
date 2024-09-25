using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Utility
{
    public class TempPOLExpenseInfo
    {
        public long POLExpenseInfoId { get; set; }
        public Nullable<long> OilName { get; set; }
        public Nullable<decimal> InitialStockLtrOrUnit { get; set; }
        public Nullable<decimal> ReceivedQtyLtrorUnit { get; set; }
        public Nullable<decimal> HandoverLtrOrUnit { get; set; }
        public Nullable<decimal> MonthlyUseltrOrUnit { get; set; }
        public Nullable<decimal> StockAfterUseLtrOrUnit { get; set; }
        public Nullable<decimal> OilPerLtRate { get; set; }
        
        public float? OilTotalPrice { get; set; }
    }
}
