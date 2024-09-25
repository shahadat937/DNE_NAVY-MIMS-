using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RMS.Model;
using RMS.Utility;
using RMS.Web.Models.ViewModel;
using Microsoft.AspNet.Identity;
using RMS.BLL.IManager;
using RMS.Web.Controllers;

namespace RMS.Web.Controllers
{
    [Authorize]
    public class LubOilConsumptionController : BaseController
    {
        private readonly ILubOilConsumptionManager _lubOilConsumptionManager;
       
        private readonly IShipInfoManager _shipInfoManager;
        private readonly ICommonCodeManager _commonCodeManager;
        private readonly IMachineryInfoManager _machineryInfoManager;
        public LubOilConsumptionController(ILubOilConsumptionManager lubOilConsumptionManager, IShipInfoManager shipInfoManager, ICommonCodeManager commonCodeManager, IMachineryInfoManager machineryInfoManager)
        {
            _lubOilConsumptionManager = lubOilConsumptionManager;
            _shipInfoManager = shipInfoManager;
            _commonCodeManager = commonCodeManager;
            _machineryInfoManager = machineryInfoManager;
        }
        public ActionResult Index(LubOilConsumptionViewModel model)
        {
            model.LubOilConsumptions = _lubOilConsumptionManager.GetAll();
            return View(model);
        }
        public ActionResult SearchByKey(LubOilConsumptionViewModel model)
        {
            string searchKey = model.SearchKey;
            model.LubOilConsumptions = searchKey != null ? _lubOilConsumptionManager.GetAll().Where(x => x.CommonCode.TypeValue.ToLower().Contains(searchKey.ToLower()) || x.ControlShipInfo.ControlName.ToLower().Contains(searchKey.ToLower())).ToList() : _lubOilConsumptionManager.GetAll().ToList();

            return View("Index", model);
        }
        [HttpGet]
        public ActionResult Edit(string id, LubOilConsumptionViewModel model)
        {
            ModelState.Clear();
            model.MachineryInfos = _machineryInfoManager.GetAll();
            model.CommonCodes = _commonCodeManager.GetCommonCodeByType("POLConsumption");
            model.ShipInfos = _shipInfoManager.GetAll();
            var costCentre = _lubOilConsumptionManager.GetShipEdit(id, model.LubOilConsumption) ?? new LubOilConsumption();
            model.LubOilConsumption = costCentre;
            return View(model);
        }
        [HttpPost]
        public ActionResult Save(LubOilConsumptionViewModel model)
        {
           // model.LubOilConsumption.UserId = User.Identity.GetUserName();
            ResponseModel saveData = _lubOilConsumptionManager.Saving(model.LubOilConsumption);
            if (saveData.Message != string.Empty)
            {
                TempData["message"] = saveData.Message;
            }
            return RedirectToAction("Edit", model);
        }
        public ActionResult Delete(string id)
        {
            var deleteIndex = _lubOilConsumptionManager.Delete(id);
            return RedirectToAction("Index");
        }
	}
}