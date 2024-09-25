using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using RMS.BLL.IManager;
using RMS.Model;
using RMS.Utility;
using RMS.Web.Models.ViewModel;

namespace RMS.Web.Controllers
{
    [Authorize]
    public class FuelConsumptionController : BaseController
    {  
        private readonly IFuelConsumptionManager _fuelConsumption;
        private readonly IShipInfoManager _shipInfoManager;
        private readonly ICommonCodeManager _commonCodeManager;
        private readonly IMachineryInfoManager _machineryInfoManager;
        private IControlShipInfoManager _controlShipInfoManager;


        public FuelConsumptionController(IFuelConsumptionManager fuelConsumption,IControlShipInfoManager controlShipInfoManager, IShipInfoManager shipInfoManager, ICommonCodeManager commonCodeManager, IMachineryInfoManager machineryInfoManager)
        {
            _fuelConsumption = fuelConsumption;
           
            _shipInfoManager = shipInfoManager;
            _commonCodeManager = commonCodeManager;
            _machineryInfoManager = machineryInfoManager;
            _controlShipInfoManager = controlShipInfoManager;

        }
        public ActionResult Index(FuelConsumptionViewModel model)
        {
            model.ControlShipInfos = _controlShipInfoManager.AllByControlLevel(0);
            model.FuelConsumptions = _fuelConsumption.AllData();
            return View(model);
        }
        public ActionResult SearchByKey(FuelConsumptionViewModel model)
        {
            string searchKey = model.SearchKey;
           // model.FuelConsumptions = searchKey != null ? _fuelConsumption.AllData().Where(x => x.ShipInfo.ShipName.ToLower().Contains(searchKey.ToLower()) || x.CommonCode.TypeValue.ToLower().Contains(searchKey.ToLower())).ToList() : new List<FuelConsumption>();
            model.FuelConsumptions = searchKey != null ? _fuelConsumption.AllData().Where(x => x.ControlShipInfo.ControlName.ToLower().Contains(searchKey.ToLower())).ToList() : new List<FuelConsumption>();
            return View("~/Views/FuelConsumption/_fuelConsumption.cshtml", model);
        }
        [HttpGet]
        public ActionResult Edit(string id, FuelConsumptionViewModel model)
        {
            //model.ControlShipInfos = _controlShipInfoManager.AllByControlLevel(0);
            model.BrControlShipInfos = _controlShipInfoManager.ShipBranchInfo();
            ModelState.Clear();
            model.ShipInfos = _shipInfoManager.GetAll();
            model.CommonCodes = _commonCodeManager.GetCommonCodeByType("EngineForOil");
            model.OilTypeList = _commonCodeManager.GetCommonCodeByType("OilType");
            var costCentre = _fuelConsumption.GetShip(id, model.FuelConsumption) ?? new FuelConsumption();
            model.FuelConsumption = costCentre;
            model.LubOilType = "Ltr's";
            model.MachinaryOilType = "Ltr's";
            return View(model);
        }
        [HttpPost]
        public ActionResult Save(FuelConsumptionViewModel model)
        {

            model.FuelConsumption.MachinaryOil = model.MachinaryOilType == "Ltr's" ?
                model.FuelConsumption.MachinaryOil : model.MachinaryOilType == "Tons" ? model.FuelConsumption.MachinaryOil * 1000 : model.MachinaryOilType == "Gallons" ? model.FuelConsumption.MachinaryOil * (decimal)3.785 : 0;
            model.FuelConsumption.LubOil = model.LubOilType == "Ltr's" ?
             model.FuelConsumption.LubOil : model.LubOilType == "Tons" ? model.FuelConsumption.LubOil * 1000 : model.LubOilType == "Gallons" ? model.FuelConsumption.LubOil * (decimal)3.785 : 0;
            ResponseModel response =_fuelConsumption.Saving(model.FuelConsumption);
            if (response.Message != string.Empty)
            {
                TempData["message"] = response.Message;
            }
            return RedirectToAction("Edit", model);
        }
        public ActionResult Delete(string id)
        {
            var deleteIndex = _fuelConsumption.Delete(id);
            return RedirectToAction("Index");
        }
	}
}