using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Model
{
  public partial class ShipInactiveDescription
    {
        public long ParentCode { get; set; }
        public long id { get; set; }
        public string Name { get; set; }
        public int LEVEL { get; set; }
        public int firstLevel { get; set; }
        public long SecendLevel { get; set; }
        public long ThirdLevel { get; set; }
    }
}
