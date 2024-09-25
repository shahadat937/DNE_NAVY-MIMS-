using System.Collections.Generic;
using RMS.Model;
using RMS.Utility;

namespace RMS.BLL.IManager
{
    public interface ILubOilConsumptionManager
    {
        LubOilConsumption GetShipEdit(string id, LubOilConsumption lubOilConsumption);
      
        List<LubOilConsumption> GetAll();
        object Delete(string id);
        ResponseModel Saving(LubOilConsumption lubOilConsumption);
    }
}
