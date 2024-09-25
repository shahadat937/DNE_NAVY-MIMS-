using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.BLL.DashBoardTreeView.UpdateOperationState;

namespace RMS.BLL.DashBoardTreeView.ProcurementByDne
{
   public class ProcurementTreeViewBuilder
    {
             private readonly IProcurementTarget _chartofAccount;

             public ProcurementTreeViewBuilder(IProcurementTarget chartofAccount)
        {
            _chartofAccount = chartofAccount;
        }
             public List<ProcurementChartsOfAccount> GetChartofAccount()
        {
            return _chartofAccount.GetTreeView();
        } 
    }
}
