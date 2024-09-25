using System;
using System.Collections.Generic;
using RMS.Model;
using RMS.Utility;

namespace RMS.BLL.IManager
{
    public interface IShipcommOrgnaizationManager
  {
      List<ShipcommOrgnaization> GetAll();

      ShipcommOrgnaization GetShipcommOrgnaizationById(long OrgnizID);
      List<ShipcommOrgnaization> FindOne(long id);
     
      //List<ShipcommOrgnaization> FindOne1(DateTime DateFrom, DateTime DateTo); 
      //object Delete(string id);
      //ResponseModel Save(ShipcommOrgnaization Orgnaiz);
  }
}
