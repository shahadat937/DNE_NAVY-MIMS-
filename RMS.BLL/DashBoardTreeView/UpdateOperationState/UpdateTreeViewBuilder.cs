using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.BLL.DashBoardTreeView.UpdateOperationState
{
   public class UpdateTreeViewBuilder
    {
             private readonly IUpdateTarget _chartofAccount;

             public UpdateTreeViewBuilder(IUpdateTarget chartofAccount)
        {
            _chartofAccount = chartofAccount;
        }
        public List<UpdateChartsOfAccount> GetChartofAccount()
        {
            return _chartofAccount.GetTreeView();
        } 
    }
}
