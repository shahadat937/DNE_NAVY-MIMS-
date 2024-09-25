using System;
using System.Collections.Generic;
using RMS.Model;
using RMS.Utility;

namespace RMS.BLL.IManager
{
   public interface IFuelConsumptionManager
    {
       FuelConsumption GetShip(string id, FuelConsumption model);
       List<FuelConsumption> FindOne(long id);
       List<FuelConsumption> AllData();
       object Delete(string id);
       ResponseModel Saving(FuelConsumption fuelConsumption);
       List<vwFuelConsumption> GetReportData(DateTime fromdate, DateTime toDate, long shipid);
    }
}
