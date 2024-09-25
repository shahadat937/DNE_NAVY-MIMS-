using System.Collections.Generic;
using RMS.Model;
using RMS.Utility;

namespace RMS.BLL.IManager
{
    public interface IShipCraftManager 
    {
        List<ShipInfo> GetAll();
        ShipInfo GetShipById(string id);
        int Delete(string id);
        ResponseModel Saving(ShipInfo shipInfo);

        List<ShipInfo> GetControlShipAll();


        ResponseModel SaveRefitDocking(ShipInfo shipInfo);

        List<ShipInfo> FindOne(long id);

        int FindCallSign(string sign);
    }
}
