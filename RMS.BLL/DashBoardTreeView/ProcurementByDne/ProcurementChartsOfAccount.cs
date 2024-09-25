using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Model;

namespace RMS.BLL.DashBoardTreeView.ProcurementByDne
{
   public class ProcurementChartsOfAccount : ProcurementByDNE
    {
       public List<ProcurementChartsOfAccount> Nodes { get; set; }
       public ProcurementChartsOfAccount()
        {
            Nodes = new List<ProcurementChartsOfAccount>();
        }

    }
}
