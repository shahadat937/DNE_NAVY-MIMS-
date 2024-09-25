using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Model
{
   public partial class ProcurementByDNE
    {
        public long ParentCode { get; set; }
        public long id { get; set; }
        public string Name { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<decimal> EstTotal { get; set; }
        public string Currency { get; set; }
        public int LEVEL { get; set; }
        public int firstLevel { get; set; }
        public long SecendLevel { get; set; }
        public long ThirdLevel { get; set; }
    }
}
