using System;
using System.Collections.Generic;
using RMS.Model;
using RMS.Utility;

namespace RMS.BLL.IManager
{
  public interface IShipInactiveOrgManager
  {
      List<ShipInactiveOrg> GetAll();

      ShipInactiveOrg GetShipInactiveOrgById(long shipInactiveOrgIdentity);
      List<ShipInactiveOrg> FindOne(long id);
      object Delete(string id);
      ResponseModel Save(ShipInactiveOrg shipInactiveOrg);

      //List<ShipInactive> GetDataForReport(DateTime fromdate, DateTime toDate, long shipid);
  }
}
