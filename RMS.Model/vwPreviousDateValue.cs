using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Model
{
  public class vwPreviousDateValue
    {
        public DateTime PNextDockingTo { get; set; }
        public DateTime PNextDockingFrom { get; set; }

        public DateTime PNextRefitFrom { get; set; }
        public DateTime PNextRefitTo { get; set; }
        public string Result { get; set; }
    }
}
