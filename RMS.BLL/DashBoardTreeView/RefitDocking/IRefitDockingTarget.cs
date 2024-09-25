using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.BLL.DashBoardTreeView.RefitDocking
{
   public interface IRefitDockingTarget
    {
       List<RefitDockingChartsOfAccount> GetTreeView();
    }
}
