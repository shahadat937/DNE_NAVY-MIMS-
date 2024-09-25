using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.BLL.DashBoardTreeView.RefitDocking
{
   public class RefitDockingTreeViewBuilder
    {
         private readonly IRefitDockingTarget _chartofAccount;

         public RefitDockingTreeViewBuilder(IRefitDockingTarget chartofAccount)
        {
            _chartofAccount = chartofAccount;
        }
             public List<RefitDockingChartsOfAccount> GetChartofAccount()
        {
            return _chartofAccount.GetTreeView();
        } 
    }
}
