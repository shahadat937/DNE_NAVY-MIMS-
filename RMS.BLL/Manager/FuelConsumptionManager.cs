using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using RMS.BLL.IManager;
using RMS.DAL.IRepository;
using RMS.DAL.Repository;
using RMS.Model;
using RMS.Utility;

namespace RMS.BLL.Manager
{
   public class FuelConsumptionManager : BaseManager, IFuelConsumptionManager
    {
        private readonly IFuelConsumptionRepository _fuelConsumptionRepository;

        public FuelConsumptionManager()
        {

            _fuelConsumptionRepository = new FuelConsumptionRepository(Instance.Context);

        }

       public FuelConsumption GetShip(string id, FuelConsumption model)
       {
           var shipid = Convert.ToInt64(id);
           var costCentre = _fuelConsumptionRepository.FindOne(x => x.FuelConsumptionIdentity == shipid);
           if (costCentre != null)
           {
               model.FuelConsumptionIdentity = costCentre.FuelConsumptionIdentity;
               model.ShipId = costCentre.ShipId;
               model.Machinery = costCentre.Machinery;
               model.MachinaryOil = costCentre.MachinaryOil;
               model.CompletionDate = costCentre.CompletionDate;
             //  model.MahinaryOilType = costCentre.MahinaryOilType;
               model.LubOil = costCentre.LubOil;
               model.HSDOil = costCentre.HSDOil;
               model.GreaseOil = costCentre.GreaseOil;
               model.Water = costCentre.Water;
               model.WLubOil = costCentre.WLubOil;
               model.PercentageOfOil = costCentre.PercentageOfOil;
               model.AtSea = costCentre.AtSea;
               model.Harbour = costCentre.Harbour;
               model.LubOil = costCentre.LubOil;
               model.RefGas = costCentre.RefGas;
            //   model.LuboilType = costCentre.LuboilType;
             
               model.UserId = costCentre.UserId;
               model.LastUpdate = costCentre.LastUpdate;
           }
           return model;
       }
       public List<FuelConsumption> AllData()
       {
           return _fuelConsumptionRepository.All().Include(x =>x.ControlShipInfo).ToList();
       }
       public List<FuelConsumption> FindOne(long id)
       {
           return _fuelConsumptionRepository.Filter(x => x.ShipId == id).ToList();
       }

       public object Delete(string id)
       {

           var fuelId = Convert.ToInt64(id);
           return _fuelConsumptionRepository.Delete(x => x.FuelConsumptionIdentity == fuelId);
       }

       ResponseModel IFuelConsumptionManager.Saving(FuelConsumption fuelConsumption)
       {
           FuelConsumption oldData = _fuelConsumptionRepository.FindOne(x => x.FuelConsumptionIdentity == fuelConsumption.FuelConsumptionIdentity);
           if (oldData != null)
           {
               oldData.ShipId = fuelConsumption.ShipId;
               oldData.Machinery = fuelConsumption.Machinery;
               oldData.MachinaryOil = fuelConsumption.MachinaryOil;
               //oldData.MahinaryOilType = fuelConsumption.MahinaryOilType;
               oldData.LubOil = fuelConsumption.LubOil;
               oldData.HSDOil = fuelConsumption.HSDOil;
               oldData.GreaseOil = fuelConsumption.GreaseOil;
               oldData.Water = fuelConsumption.Water;
               oldData.WLubOil = fuelConsumption.WLubOil;
               oldData.AtSea = fuelConsumption.AtSea;
               oldData.Harbour = fuelConsumption.Harbour;
               oldData.RefGas = fuelConsumption.RefGas;
               oldData.CompletionDate = fuelConsumption.CompletionDate;
               decimal a = Convert.ToDecimal(fuelConsumption.MachinaryOil + fuelConsumption.HSDOil);
               decimal b = (decimal) (fuelConsumption.LubOil*100);
               oldData.PercentageOfOil = b/a;
              // oldData.LuboilType = fuelConsumption.LuboilType;
               oldData.UserId = PortalContext.CurrentUser.UserName;
               oldData.LastUpdate = DateTime.Now;
               _fuelConsumptionRepository.Edit(oldData);
               ResponseModel.Message = "Machinery information is updated successfully.";
           }
           else
           {
               var a = (fuelConsumption.MachinaryOil + fuelConsumption.HSDOil);
               fuelConsumption.PercentageOfOil = (fuelConsumption.LubOil * 100) / Convert.ToDecimal(a);
               fuelConsumption.UserId = PortalContext.CurrentUser.UserName;
               fuelConsumption.LastUpdate = DateTime.Now;
               _fuelConsumptionRepository.Save(fuelConsumption);
               ResponseModel.Message = "Machinery information is saved successfully.";
           }
           return ResponseModel;
       }

       public List<vwFuelConsumption> GetReportData(DateTime fromdate, DateTime toDate, long shipid)
       {
           List<vwFuelConsumption> fuelList =new List<vwFuelConsumption>();
           vwFuelConsumption fuelConsumption =new vwFuelConsumption();
           fuelConsumption.MainEngines = _fuelConsumptionRepository.Filter(
               x => x.ShipId == shipid && x.Machinery == 891 && x.LastUpdate >= fromdate && x.LastUpdate <= toDate)
               .Sum(x => x.MachinaryOil);
          

           fuelConsumption.otherOil = _fuelConsumptionRepository.Filter(
               x => x.ShipId == shipid && x.Machinery == 892 && x.LastUpdate >= fromdate && x.LastUpdate <= toDate)
               .Sum(x => x.MachinaryOil);
         

           fuelConsumption.FreshWater = _fuelConsumptionRepository.Filter(
               x => x.ShipId == shipid && x.Machinery == 892 && x.LastUpdate >= fromdate && x.LastUpdate <= toDate)
               .Sum(x => x.MachinaryOil);
         


           var value = _fuelConsumptionRepository.Filter(
               x => x.ShipId == shipid && x.Machinery == 891 && x.LastUpdate >= fromdate && x.LastUpdate <= toDate);
           fuelConsumption.MainEnginesLubOil =(decimal) (value.Any()? value.Sum(x => x.LubOil) :(decimal) 0.00);


           var value1 = _fuelConsumptionRepository.Filter(
               x => x.ShipId == shipid && x.Machinery == 892 && x.LastUpdate >= fromdate && x.LastUpdate <= toDate);
           fuelConsumption.otherOilLubOil = (decimal) (value1.Any() ? value1.Sum(x => x.LubOil) : (decimal) 0.00);


           var value2 = _fuelConsumptionRepository.Filter(
                                                x => x.ShipId == shipid && x.Machinery == 893 && x.LastUpdate >= fromdate && x.LastUpdate <= toDate);

           fuelConsumption.FreshWaterLubOil = (decimal) (value2.Any() ? value2.Sum(x => x.LubOil) : (decimal) 0.00);
             fuelList.Add(fuelConsumption);
           return fuelList;
       }
    }
}
