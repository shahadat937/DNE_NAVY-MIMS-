using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.BLL.DashBoardTreeView.ShipInactive
{
   public class ShipInactiveTreeViewBuilder
    {
        private readonly IShipInactiveTarget _chartofAccount;

        public ShipInactiveTreeViewBuilder(IShipInactiveTarget chartofAccount)
        {
        _chartofAccount = chartofAccount;
        }
        public List<ShipInactiveChartsOfAccount> GetChartofAccount()
        {
        return _chartofAccount.GetTreeView();
        } 
    }
}
