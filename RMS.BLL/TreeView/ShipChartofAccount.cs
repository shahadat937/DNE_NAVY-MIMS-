using System.Collections.Generic;
using RMS.Model;

namespace RMS.BLL.TreeView
{
    public class ShipChartofAccount : ControlShipInfo
    {
        public List<ShipChartofAccount> Nodes { get; set; }
        public ShipChartofAccount()
        {
            Nodes = new List<ShipChartofAccount>();
        }
    }
}
