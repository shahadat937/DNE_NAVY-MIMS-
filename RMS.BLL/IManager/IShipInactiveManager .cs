using System;
using System.Collections.Generic;
using RMS.Model;
using RMS.Utility;

namespace RMS.BLL.IManager
{
  public interface IShipInactiveManager
  {
      List<ShipInactive> GetAll();

      ShipInactive GetShipInactiveById(long shipInactiveIdentity);
      List<ShipInactive> FindOne(long id);
      object Delete(string id);
      ResponseModel Save(ShipInactive shipMovement);
      string ShipBranchName(long? ShipId);
      int ShipStatusUpdate(long? ShipInactiveIdentity);
      List<ShipInactiveView> GetNonOpsShip();

        //List<ShipInactive> GetDataForReport(DateTime fromdate, DateTime toDate, long shipid);
    }
}
