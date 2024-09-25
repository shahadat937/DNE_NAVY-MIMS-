using System;
using System.Collections.Generic;
using RMS.Model;
using RMS.Utility;

namespace RMS.BLL.IManager
{
  public interface IShipMovementManager
  {
      List<ShipMovement> GetAll();
    
      ShipMovement GetShipMovementById(long shipMovementIdentity);
      List<ShipMovement> FindOne(long id);
      object Delete(string id);
      ResponseModel Save(ShipMovement shipMovement);

      List<ShipMovement> GetDataForReport(DateTime fromdate, DateTime toDate, long shipid);
  }
}
