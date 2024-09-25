using System.Collections.Generic;
using RMS.Model;
using RMS.Utility;

namespace RMS.BLL.IManager
{
   public interface IShipSpeedTrialManager
    {
     
     
       ShipSpeedTrial GetShipSpeedById(long shipSpeedIdentity);
     
       List<ShipSpeedTrial> GetAll();
       ResponseModel Save(ShipSpeedTrial shipSpeedTrial);
       object Delete(string id);
    }
}
