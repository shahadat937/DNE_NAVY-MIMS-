using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Model;
using RMS.Utility;

namespace RMS.BLL.IManager
{
  public interface IShipRepairInfoManager
    {
      List<ShipRepairInfo> GetAll();
      ShipRepairInfo GetShipId(string id);
      ShipRepairInfo RepairCostDetailsId(string id);
      ResponseModel SaveData(ShipRepairInfo shipRepairInfo);
      object Delete(string id);
    }
}
