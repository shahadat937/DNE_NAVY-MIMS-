using System.Collections.Generic;

namespace RMS.BLL.TreeView
{
    public class ShipTreeViewBuilder
    {
        private readonly IShipTarget _chartofAccount;

        public ShipTreeViewBuilder(IShipTarget chartofAccount)
        {
            _chartofAccount = chartofAccount;
        }
        public List<ShipChartofAccount> GetChartofAccount()
        {
            return _chartofAccount.GetTreeView();
        } 
    }
}
