using System;
using System.Collections.Generic;
using RMS.Model;
using RMS.Utility;

namespace RMS.BLL.IManager
{
    public interface ISignalManager
  {
      List<signal> GetAll();

      //signal GetShipcommOrgnaizationById(long OrgnizID);
      //List<signal> FindOne(long id);
     
      //List<ShipcommOrgnaization> FindOne1(DateTime DateFrom, DateTime DateTo); 
      //object Delete(string id);
      //ResponseModel Save(ShipcommOrgnaization Orgnaiz);
  }
}
