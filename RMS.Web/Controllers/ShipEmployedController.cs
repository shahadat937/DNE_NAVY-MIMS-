using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RMS.BLL.IManager;
using RMS.Model;
using RMS.Utility;
using RMS.Web.Models.ViewModel;

namespace RMS.Web.Controllers
{
    public class ShipEmployedController : Controller
    {
        private IShipInfoManager _shipInfoManager;
        private IShipEmployedManager _shipEmployedManager;
        public ShipEmployedController(IShipInfoManager shipInfoManager, IShipEmployedManager shipEmployedManager)
        {
            _shipInfoManager = shipInfoManager;
           _shipEmployedManager = shipEmployedManager;

        }
        public ActionResult Index(ShipEmployedViewModel model)
        {
            model.ShipEmployeds = _shipEmployedManager.GetAll();
            return View(model);
        }
        public ActionResult SearchByKey(ShipEmployedViewModel model)
        {
            string searchKey = "";
            if (model.SearchKey != null)
            {
                searchKey = model.SearchKey.ToLower();
                model.ShipEmployeds =
                    _shipEmployedManager.GetAll().Where(x => x.ShipInfo.ShipName.Contains(searchKey)).ToList();
            }
            else
            {
                model.ShipEmployeds = _shipEmployedManager.GetAll();
            }
            return View("Index", model);
        }
        [HttpGet]
        public ActionResult Edit(string id, ShipEmployedViewModel model)
        {
            ModelState.Clear();
            model.ShipInfos = _shipInfoManager.GetAll();
            model.ShipEmployed = _shipEmployedManager.FindOne(Convert.ToInt32(id)) ?? new ShipEmployed();
            return View(model);
        }
        [HttpPost]
        public ActionResult Save(ShipEmployedViewModel model)
        {
            ResponseModel response = _shipEmployedManager.Save(model.ShipEmployed);
            if (response.Message != string.Empty)
            {
                TempData["message"] = response.Message;
            }
            model.ShipInfos = _shipInfoManager.GetAll();
            return RedirectToAction("Edit", model);
        }
        public ActionResult Delete(long id)
        {
           int isDeleted = _shipEmployedManager.Delete(id);
           return RedirectToAction("Index");
        }
	}
}