using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.BLL.DashBoardTreeView.ShipInactive
{
   public interface IShipInactiveTarget
    {
       List<ShipInactiveChartsOfAccount> GetTreeView();
    }
}
