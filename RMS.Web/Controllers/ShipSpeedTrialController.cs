using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;
using RMS.BLL.IManager;
using RMS.Model;
using RMS.Utility;
using RMS.Web.Controllers;
using RMS.Web.Models.ViewModel;

namespace RMS.Web.Controllers
{
    public class ShipSpeedTrialController : BaseController
    {
        private IShipSpeedTrialManager _shipSpeedTrialManager;
        private IShipInfoManager _shipInfoManager;
        private readonly IControlShipInfoManager _controlShipInfoManager;
        public ShipSpeedTrialController(IShipSpeedTrialManager shipSpeedTrialManager, IShipInfoManager shipInfoManager, IControlShipInfoManager controlShipInfoManager)
        {
            _shipSpeedTrialManager = shipSpeedTrialManager;
            _shipInfoManager = shipInfoManager;
            _controlShipInfoManager = controlShipInfoManager;
        }
       
        public ActionResult Index( ShipSpeedTrialViewModel model)
        {
            var ConShip = _controlShipInfoManager.ShipBranchInfo();
            var SihpST = _shipSpeedTrialManager.GetAll();
            var query = (from post in SihpST join meta in ConShip on post.ShipId equals meta.ControlShipInfoId select post).ToList();
            model.ShipSpeedTrials = query;
            return View(model);
        }

        public ActionResult Save(ShipSpeedTrialViewModel model)
        {
            ResponseModel response = _shipSpeedTrialManager.Save(model.ShipSpeedTrial);
           if (response.Message != string.Empty)
           {
               TempData["message"] = response.Message;
           }
           return RedirectToAction("Edit", model);
        }

        public ActionResult Edit(string id,ShipSpeedTrialViewModel model)
        {
            //model.ShipInfos = _shipInfoManager.GetAll();
            model.ShipInfos = _controlShipInfoManager.ShipBranchInfo();
            // string ass=model.CommonCodes.ToString();
            model.ShipSpeedTrial = _shipSpeedTrialManager.GetShipSpeedById(Convert.ToInt32(id))??new ShipSpeedTrial();

            return View(model);
        }
        public ActionResult Delete(String Id)
        {
           var deletedata = _shipSpeedTrialManager.Delete(Id);
           return RedirectToAction("Index");
        }

        public ActionResult ShipControlIndex(ShipSpeedTrialViewModel model)
        {

            return RedirectToAction("Index", "ControlShipInfo");
        }

        public ActionResult Search(ShipSpeedTrialViewModel model)
        {
            string searchKey = "";
            if (model.SearchKey != null)
            {
                searchKey = model.SearchKey.Trim().ToLower();
                model.ShipSpeedTrials =
                    _shipSpeedTrialManager.GetAll().Where(x => x.ControlShipInfo.ControlName.Contains(searchKey) || x.Description.Contains(searchKey)).ToList();

            }
            else
            {
                model.ShipSpeedTrials =
                    _shipSpeedTrialManager.GetAll();
             }
            return View("index", model);
        }
       
	}
}