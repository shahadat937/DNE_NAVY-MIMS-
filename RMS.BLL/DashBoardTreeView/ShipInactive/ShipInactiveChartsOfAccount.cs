using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Model;

namespace RMS.BLL.DashBoardTreeView.ShipInactive
{
  public partial class ShipInactiveChartsOfAccount :ShipInactiveDescription
    {
       public List<ShipInactiveChartsOfAccount> Nodes { get; set; }
       public ShipInactiveChartsOfAccount()
        {
            Nodes = new List<ShipInactiveChartsOfAccount>();
        }
    }
}
