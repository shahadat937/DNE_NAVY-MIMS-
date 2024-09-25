using System;
using System.Collections.Generic;
using System.Linq;
using RMS.Model;
using RMS.Utility;



namespace RMS.BLL.IManager
{
    public interface  IFuelConsumptions1_ResultManager
    {
        List<FuelConsumptions1_Result> GetAll();
        List<FuelConsumptions1_Result> GetShipId(string id,DateTime fromDate,DateTime todate);
        List<FuelConsumptions1_Result> FindOne(string id);
    }
}
