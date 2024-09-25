using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Model;

namespace RMS.BLL.DashBoardTreeView.RefitDocking
{
   public class RefitDockingChartsOfAccount :ApprovedRefitDockingSchedule
    {
              public List<RefitDockingChartsOfAccount> Nodes { get; set; }
              public RefitDockingChartsOfAccount()
        {
            Nodes = new List<RefitDockingChartsOfAccount>();
        }
    }
}
