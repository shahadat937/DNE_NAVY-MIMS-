using System.Collections.Generic;

namespace RMS.BLL.TreeView
{
    public interface IShipTarget
    {
        List<ShipChartofAccount> GetTreeView();
    }
}
