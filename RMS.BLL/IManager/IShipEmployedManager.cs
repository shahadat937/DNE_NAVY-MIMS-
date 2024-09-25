using System;
using System.Collections;
using System.Collections.Generic;
using RMS.Model;
using RMS.Utility;

namespace RMS.BLL.IManager
{
    public interface IShipEmployedManager
    {
        List<ShipEmployed> GetAll();

        ShipEmployed FindOne(int p);

        ResponseModel Save(ShipEmployed shipEmployed);

        int Delete(long id);
       
    }

   
}
