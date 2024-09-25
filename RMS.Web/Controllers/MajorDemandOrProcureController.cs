using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RMS.BLL.IManager;
using RMS.Model;
using RMS.Utility;
using RMS.Web.Models.ViewModel;
using System.Data.SqlClient;
using System.Data.Entity.SqlServer;

namespace RMS.Web.Controllers
{
    public class MajorDemandOrProcureController : BaseController
    {
        private IMajorDemandOrProcureManager _majorDemandOrProcure;
        private IControlShipInfoManager _controlShipInfoManager;
        private IShipInfoManager _shipInfoManager;
        private ICommonCodeManager _commonCodeManager;

        public MajorDemandOrProcureController(IMajorDemandOrProcureManager majorDemandOrProcure, IShipInfoManager shipInfoManager, IControlShipInfoManager controlShipInfoManager, ICommonCodeManager commonCodeManager)
        {
            _majorDemandOrProcure = majorDemandOrProcure;
            _shipInfoManager = shipInfoManager;
            _controlShipInfoManager = controlShipInfoManager;
            _commonCodeManager = commonCodeManager;
        }
        public ActionResult Index(MajorDemandOrProcureViewModel model)
        {
            model.BrControlShipInfos = _controlShipInfoManager.ShipBranchInfo();
            var MDemand = _majorDemandOrProcure.GetAll();
            var query = (from post in MDemand join meta in model.BrControlShipInfos on post.ShipIdentity equals meta.ControlShipInfoId select post).ToList();
            model.MajorDemandOrProcures = query;
            return View(model);
        }
        public ActionResult MejorDemanNotification(MajorDemandOrProcureViewModel model)
        {
            var ConShip = _controlShipInfoManager.ShipBranchInfo();
            var Msg = _majorDemandOrProcure.GetNotificationAll().ToList();
            var query = (from post in Msg join meta in ConShip on post.ShipIdentity equals meta.ControlShipInfoId select post).ToList();
            model.MajorDemandOrProcures = query;
            return View(model);
        }
        public ActionResult SearchByKey(MajorDemandOrProcureViewModel model)
        {
            string searchKey = model.SearchKey;
            model.MajorDemandOrProcures = searchKey != null ? _majorDemandOrProcure.GetAll().Where(x => x.ControlShipInfo.ControlName.ToLower().Contains(searchKey.ToLower())).ToList() : _majorDemandOrProcure.GetAll().ToList();
            return RedirectToAction("Index");
        }
        public ActionResult SearchByKeyNot(MajorDemandOrProcureViewModel model)
        {
            string searchKey = model.SearchKey;
            model.MajorDemandOrProcures = searchKey != null ? _majorDemandOrProcure.GetNotificationAll().Where(x => x.ControlShipInfo.ControlName.ToLower().Contains(searchKey.ToLower())).ToList() : _majorDemandOrProcure.GetNotificationAll().ToList();
            return RedirectToAction("MejorDemanNotification");
        }
        public ActionResult Edit(string id, MajorDemandOrProcureViewModel model)
        {
            ModelState.Clear();
            //model.ConShipInfos = _controlShipInfoManager.GetAll().Where(x => x.ControlLevel == 1).ToList();
            model.BrControlShipInfos = _controlShipInfoManager.ShipBranchInfo();
            var costCentre = _majorDemandOrProcure.GetMajorDemand(id, model.MajorDemandOrProcure) ?? new MajorDemandOrProcure();
            model.MajorDemandOrProcure = costCentre;
            return View(model);
        }

        [HttpPost]
        public ActionResult Save(MajorDemandOrProcureViewModel model)
        {

            ResponseModel saveData = _majorDemandOrProcure.Saving(model.MajorDemandOrProcure);
            if (saveData.Message != string.Empty)
            {
                TempData["message"] = saveData.Message;
            }
            return RedirectToAction("Edit", model);
        }
        public ActionResult Delete(string id)
        {
            var deleteIndex = _majorDemandOrProcure.Delete(id);
            return RedirectToAction("Index");
        }
        public ActionResult VerifyMajorDemandData(MajorDemandOrProcureViewModel model)
        {
            model.VerifyType = _commonCodeManager.GetCommonCodeByType("VerifyUserSearchCategory").Select(x => new SelectionList()
            {
                Value = x.CommonCodeID,
                Text = x.TypeValue
            }).ToList();
            return View(model);
        }

        public ActionResult VerifyMajorDemandSearch(int? VerifyType, DateTime? DateFrom, DateTime? DateTo, MajorDemandOrProcureViewModel model)
        {
            model.MajorDemandOrProcures = _majorDemandOrProcure.UserWiseData(VerifyType);
            if (model.MajorDemandOrProcures.Any())
            {
                model.MajorDemandOrProcures = DateFrom != null && DateTo != null ? model.MajorDemandOrProcures.Where(x => x.EntryDate >= DateFrom && x.EntryDate <= DateTo).ToList() : model.MajorDemandOrProcures;
            }
            model.VerifyType = _commonCodeManager.GetCommonCodeByType("VerifyUserSearchCategory").Select(x => new SelectionList()
            {
                Value = x.CommonCodeID,
                Text = x.TypeValue
            }).ToList();
            return View("VerifyMajorDemandData", model);
        }

        public ActionResult StatusUpdate(MajorDemandOrProcureViewModel model)
        {
            ResponseModel verifiedStatusUpdate = _majorDemandOrProcure.VerifiedStatusUpdate(model.MajorDemandOrProcures);
            if (verifiedStatusUpdate.Message != string.Empty)
            {
                model.Message = verifiedStatusUpdate.Message;
                model.IsSucceed = 1;
            }
            else
            {
                model.Message = "Status Not Updated";
            }
            model.VerifyType = _commonCodeManager.GetCommonCodeByType("VerifyUserSearchCategory").Select(x => new SelectionList()
            {
                Value = x.CommonCodeID,
                Text = x.TypeValue
            }).ToList();
            return View("VerifyMajorDemandData", model);
        }
	}
}