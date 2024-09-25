using System.Collections.Generic;
using RMS.Model;
using RMS.Utility;

namespace RMS.BLL.IManager
{
   public interface IShipPowerManager
    {
       List<ShipPower> GetAll();
       ShipPower GetShipId(string id);
      
       object Delete(string id);
       ResponseModel SaveData(ShipPower shipPower);
    }
}
