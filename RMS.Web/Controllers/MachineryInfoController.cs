using Microsoft.Reporting.WebForms;
using RMS.BLL.IManager;
using RMS.Model;
using RMS.Utility;
using RMS.Web.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace RMS.Web.Controllers
{
    [Authorize]
    public class MachineryInfoController : BaseController
    {
        private IMachineryInfoManager _machineryInfoManager;
        private IShipInfoManager _shipInfoManager;
        private IMachinaryEquipmentInformationManager _machinaryEquipmentInformationManager;
        private IControlShipInfoManager _controlShipInfoManager;
        private ICommonCodeManager _commonCodeManager;
        private IRunningHourInfoManager _runningHourInfoManager;
        public MachineryInfoController(IMachineryInfoManager machineryInfoManager, IShipInfoManager shipInfoManager, IMachinaryEquipmentInformationManager machinaryEquipmentInformationManager, IControlShipInfoManager controlShipInfoManager, ICommonCodeManager commonCodeManager, IRunningHourInfoManager runningHourInfoManager)
        {
            _machineryInfoManager = machineryInfoManager;
            _shipInfoManager = shipInfoManager;
            _machinaryEquipmentInformationManager = machinaryEquipmentInformationManager;
            _controlShipInfoManager = controlShipInfoManager;
            _commonCodeManager = commonCodeManager;
            _runningHourInfoManager = runningHourInfoManager;

        }

        public ActionResult RunningHourIndex(MachineryInfoViewModel model)
        {
            var ConShip = _controlShipInfoManager.ShipBranchInfo();
            var machinaryInfo = _machineryInfoManager.GetAll();
            var query = (from post in machinaryInfo join meta in ConShip on post.ControlShipInfoId equals meta.ControlShipInfoId select post).ToList();
            var RHour = _runningHourInfoManager.GetAll();
            var query1 = (from post in RHour join meta in query on post.MachinariesInfoIdentity equals meta.MachineryInfoIdentity select post).ToList();
            model.RunningHourInfos = query1;
            return View(model);
        }

        public ActionResult MachineryRepairNotification(MachineryInfoViewModel model)
        {
            model.MachineryInfos = _machineryInfoManager.GetAllNextTohMohMachinery();
            model.UserName = PortalContext.CurrentUser.UserRole;
            return View(model);
        }

        public ActionResult ResetTOHMOHTime(decimal rh, long MachineryId)
        {
           bool isUpdate = _machineryInfoManager.ResetTOHMOHTime(rh, MachineryId);
            return RedirectToAction("MachineryRepairNotification"); 
        }

        public ActionResult MachinaryInfoByName(MachineryInfoViewModel model)
        {
            List<MachineryInfo> machinaryInfo = _machineryInfoManager.GetMachinaryInfoByMachineName(model.SearchKey);
            model.MachineryInfos = machinaryInfo;
            return View(model);
        }
        [HttpGet]
        public ActionResult RunningHourEdit(long? id, MachineryInfoViewModel model)
        {
            ModelState.Clear();
            //model.ControlShipInfos = _controlShipInfoManager.AllByControlLevel(0);
            model.BrControlShipInfos = _controlShipInfoManager.ShipBranchInfo();
            model.Machinery = _machineryInfoManager.GetAll();
            var costCentre = _runningHourInfoManager.GetMachine(id) ?? new RunningHourInfo();
            if (costCentre.RunningMachinariesIdentity > 0)
            {
                var machineIno = _machineryInfoManager.GetOne(costCentre.MachinariesInfoIdentity);
                model.ShipName = machineIno.ControlShipInfoId;
                model.RunningHourType = _commonCodeManager.GetCommonName(machineIno.LifeTimeType);
            }

            model.RunningHourInfo = costCentre;
            return View(model);
        }
        public ActionResult RunningHourSearch(MachineryInfoViewModel model)
        {
            string searchKey = "";
            if (model.SearchKey != null)
            {
                searchKey = model.SearchKey.ToLower();
                model.RunningHourInfos = _runningHourInfoManager.GetAll().Where(x => x.MachineryInfo.ControlShipInfo.ControlName.ToLower().Contains(searchKey) || x.MachineryInfo.Machinery.ToLower().Contains(searchKey)).ToList();
                //model.MachineryInfos = _machineryInfoManager.GetAll().Where(x => x.ControlShipInfo.ControlName == null ? "".ToLower().Contains(searchKey) : x.ControlShipInfo.ControlName.ToLower().Contains(searchKey) || x.Machinery == null ? "".ToLower().Contains(searchKey) : x.Machinery.ToLower().Contains(searchKey) || x.Model == null ? "".ToLower().Contains(searchKey) : x.Model.ToLower().Contains(searchKey)).OrderByDescending(x => x.LastUpdate).ToList();
            }
            return View("RunningHourIndex", model);
        }
        public JsonResult GetMachinariesInformation(string bankCode)
        {
            var districtList = _machineryInfoManager.GetFindValue(bankCode);
            List<vwMachinarieInfo> result = new List<vwMachinarieInfo>();
            foreach (var machineryInfo in districtList)
            {
                vwMachinarieInfo machine = new vwMachinarieInfo();
                machine.MachinariesId = machineryInfo.MachineryInfoIdentity;
                machine.MachineName = machineryInfo.Machinery;
                machine.ModelNo = machineryInfo.Model;
                result.Add(machine);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLifeTimeType(long machineId)
        {
            string type = "";
            var lifeTimeType = _machineryInfoManager.GetLifeTimeType(machineId);
            if (lifeTimeType.LifeTimeType != null)
                type = _commonCodeManager.GetCommonName(lifeTimeType.LifeTimeType);
            return Json(type, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Index(MachineryInfoViewModel model)
        {
            if (model.ShipName > 0)
            {
                model.MachineryInfos = _machineryInfoManager.GetAll().Where(x => x.ControlShipInfoId == model.ShipName).OrderBy(x => x.LastUpdate).ToList();
            }
            model.ControlShipInfos = _controlShipInfoManager.GetAllShipList();
            return View(model);
        }
        public ActionResult Nabvar(viewmodelMachinary model)
        {
            var ConShip = _controlShipInfoManager.ShipBranchInfo();
            var MInfo = _machineryInfoManager.GetAll().OrderByDescending(x => x.LastUpdate).ToList();
            var SInfo = _shipInfoManager.GetAll().OrderByDescending(x => x.LastUpdate).ToList();
            var query = (from post in MInfo join meta in ConShip on post.ControlShipInfoId equals meta.ControlShipInfoId select post).ToList();
            model.MachineryInfos = query;
            var query1 = (from post in SInfo join meta in ConShip on post.ControlShipInfoId equals meta.ControlShipInfoId select post).ToList();
            model.ShipInfoes = query1;
            return View(model);
        }
        public ActionResult SearchByKey(viewmodelMachinary model)
        {
            string searchKey = "";
            if (model.SearchKey != null)
            {
                searchKey = model.SearchKey.ToLower();
                model.MachineryInfos = _machineryInfoManager.GetAll().Where(x => (x.ControlShipInfo.ControlName == null ? "" : x.ControlShipInfo.ControlName).ToLower().Contains(searchKey) || (x.Machinery == null ? "" : x.Machinery).ToLower().Contains(searchKey) || (x.Model == null ? "" : x.Model).ToLower().Contains(searchKey)).OrderByDescending(x => x.LastUpdate).ToList();
                //model.MachineryInfos = _machineryInfoManager.GetAll().Where(x => x.ControlShipInfo.ControlName == null ? "".ToLower().Contains(searchKey) : x.ControlShipInfo.ControlName.ToLower().Contains(searchKey) || x.Machinery == null ? "".ToLower().Contains(searchKey) : x.Machinery.ToLower().Contains(searchKey) || x.Model == null ? "".ToLower().Contains(searchKey) : x.Model.ToLower().Contains(searchKey)).OrderByDescending(x => x.LastUpdate).ToList();
            }
            else
            {
                model.MachineryInfos = _machineryInfoManager.GetAll().OrderByDescending(x => x.LastUpdate).ToList(); ;
            }
            return View("Nabvar", model);
        }
        [HttpGet]
        public ActionResult Edit(string id, MachineryInfoViewModel model)
        {
            ModelState.Clear();
            model.BrControlShipInfos = _controlShipInfoManager.ShipBranchInfo();
            model.MachineryCategory = _commonCodeManager.GetCommonCodeByType("MachineryCategory");
            model.EquipmentType = _commonCodeManager.GetCommonCodeByType("EquipmentType");
            model.Lifetimetype = _commonCodeManager.GetCommonCodeByType("LifetimeType");
            model.MachinariesState = _commonCodeManager.GetCommonCodeByType("MachinariesState");
            var costCentre = _machineryInfoManager.GetShip(id, model.MachineryInfo) ?? new MachineryInfo();
            model.MachineryInfo = costCentre;
            return View(model);
        }
        [HttpGet]
        public ActionResult MachineryDetails(string id, MachineryInfoViewModel model)
        {
            ModelState.Clear();
            var costCentre = _machineryInfoManager.GetMachineryById(id, model.MachineryInfo) ?? new MachineryInfo();
            model.MachineryInfo = costCentre;
            return View(model);
        }

        [HttpPost]
        public ActionResult Save(MachineryInfoViewModel model)
        {
            ResponseModel response = _machineryInfoManager.Saving(model.MachineryInfo);
            if (response.Message != string.Empty)
            {
                model.ShipName = model.MachineryInfo.ControlShipInfoId;
            }
            return RedirectToAction("Index", model);
        }
        public ActionResult Delete(long id)
        {
            var deletedata = _machineryInfoManager.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult RunningHourSave(MachineryInfoViewModel model)
        {
            ResponseModel response = _runningHourInfoManager.Saving(model.RunningHourInfo);
            if (response.Message != string.Empty)
            {
                TempData["message"] = response.Message;
            }
            return RedirectToAction("RunningHourEdit", model);
        }

        public ActionResult RunningHourDelete(long id)
        {
            var deletedata = _runningHourInfoManager.Delete(id);
            return RedirectToAction("RunningHourIndex");
        }
        public ActionResult Download(long id)
        {
            var localReport = new LocalReport();
            var model = new ShipInfoViewModel { ShipInfoes = _shipInfoManager.FindOne(id) };

            var reportDataList = model.ShipInfoes.Select(item => new vwShipInfo()
            {


                ShipName = item.ShipName,
                ControlShipInfoId = item.ControlShipInfoId,
                ControlName = item.ControlShipInfo.ControlName,

                Length = item.Length,
                Breadth = item.Breadth,
                DraughtAFT = item.DraughtAFT,
                DraughtFWD = item.DraughtFWD,
                Displacement = item.Displacement,
                PropellerQty = item.PropellerQty,
                RudderQty = item.RudderQty,
                SpeedMax = item.SpeedMax,
                SpeedCont = item.SpeedCont,
                SpeedEcon = item.SpeedEcon,
                SpeedMin = item.SpeedMin,
                TankCapacityFW = item.TankCapacityFW,
                TankCapacityFuel = item.TankCapacityFuel,
                TankCapacityLuboil = item.TankCapacityLuboil,
                Remarks = item.Remarks
            }).ToList();



            var reportDataSource = new ReportDataSource { Name = "Parameters" };
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = reportDataList;

            var machineryInfo = _machineryInfoManager.FindOne(id);
            reportDataSource = new ReportDataSource { Name = "MachineryParticulars" };
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = machineryInfo;


            localReport.ReportPath = Server.MapPath("~/Reports/BNS/ShipParameter.rdlc");
            string mimeType;
            string encoding;
            string fileNameExtension;
            Warning[] warnings;
            string[] streams;

            byte[] renderedBytes = localReport.Render("PDF", "", out mimeType, out encoding,
                out fileNameExtension, out streams, out warnings);
            Response.AddHeader("content-disposition", "Inline; filename=Urls." + fileNameExtension);
            return File(renderedBytes, fileNameExtension);

        }

        public ActionResult RptGenerate(viewmodelMachinary model)
        {
            var localReport = new LocalReport();
            string pModel = (model.MSearchKey == null ? "" : model.MSearchKey).ToLower();
            var DList = _machineryInfoManager.GetAll().Where(x => (x.Model == null ? "" : x.Model).ToLower() == pModel).Select(x => new { ShipName = _controlShipInfoManager.FindOne(x.ControlShipInfoId.ToString()).ControlName.ToString(), Machinery = x.Machinery, Model = x.Model, Quantity = x.Quantity, Location = x.Location, MakerAddress = x.MakerAddress, Power = x.Power, PowerUnit = x.PowerUnit, RPM = x.RPM }).ToList();



            var reportDataSource1 = new ReportDataSource { Name = "ShipInfo" };
            localReport.DataSources.Add(reportDataSource1);
            reportDataSource1.Value = DList;

            localReport.ReportPath = Server.MapPath("~/Reports/BNS/MachineryInfoModelWise.rdlc");
            string mimeType;
            string encoding;
            string fileNameExtension;
            Warning[] warnings;
            string[] streams;

            byte[] renderedBytes = localReport.Render("PDF", "", out mimeType, out encoding,
                out fileNameExtension, out streams, out warnings);
            Response.AddHeader("content-disposition", "inline; filename=ModelWiseMachineryInfo." + fileNameExtension);
            return File(renderedBytes, fileNameExtension);

        }
        public ActionResult VerifyMachinariesData(MachineryInfoViewModel model)
        {
            model.VerifyType = _commonCodeManager.GetCommonCodeByType("VerifyUserSearchCategory").Select(x => new SelectionList()
            {
                Value = x.CommonCodeID,
                Text = x.TypeValue
            }).ToList();
            return View(model);
        }

        public ActionResult VerifyMachinariesSearch(int? VerifyType, DateTime? DateFrom, DateTime? DateTo, MachineryInfoViewModel model)
        {
            model.MachineryInfos = _machineryInfoManager.UserWiseData(VerifyType);
            if (model.MachineryInfos.Any())
            {
                model.MachineryInfos = DateFrom != null && DateTo != null ? model.MachineryInfos.Where(x => x.CreateDate >= DateFrom && x.CreateDate <= DateTo).ToList() : model.MachineryInfos;
            }
            model.VerifyType = _commonCodeManager.GetCommonCodeByType("VerifyUserSearchCategory").Select(x => new SelectionList()
            {
                Value = x.CommonCodeID,
                Text = x.TypeValue
            }).ToList();
            return View("VerifyMachinariesData", model);
        }

        public ActionResult StatusUpdate(MachineryInfoViewModel model)
        {
            ResponseModel verifiedStatusUpdate = _machineryInfoManager.VerifiedStatusUpdate(model.MachineryInfos);
            if (verifiedStatusUpdate.Message != string.Empty)
            {
                model.Message = verifiedStatusUpdate.Message;
                model.IsSucceed = 1;
            }
            else
            {
                model.Message = "Status Not Updated";
            }
            model.VerifyType =
                _commonCodeManager.GetCommonCodeByType("VerifyUserSearchCategory").Select(x => new SelectionList()
                {
                    Value = x.CommonCodeID,
                    Text = x.TypeValue
                }).ToList();
            return View("VerifyMachinariesData", model);
        }

        //  Verify Running Hour
        public ActionResult VerifyRunningData(MachineryInfoViewModel model)
        {
            model.VerifyType = _commonCodeManager.GetCommonCodeByType("VerifyUserSearchCategory").Select(x => new SelectionList()
            {
                Value = x.CommonCodeID,
                Text = x.TypeValue
            }).ToList();
            return View(model);
        }

        public ActionResult VerifyRunningSearch(int? VerifyType, DateTime? DateFrom, DateTime? DateTo, MachineryInfoViewModel model)
        {
            model.RunningHourInfos = _runningHourInfoManager.UserWiseData(VerifyType);
            if (model.RunningHourInfos.Any())
            {
                model.RunningHourInfos = DateFrom != null && DateTo != null ? model.RunningHourInfos.Where(x => x.CreateDate >= DateFrom && x.CreateDate <= DateTo).ToList() : model.RunningHourInfos;
            }
            model.VerifyType = _commonCodeManager.GetCommonCodeByType("VerifyUserSearchCategory").Select(x => new SelectionList()
            {
                Value = x.CommonCodeID,
                Text = x.TypeValue
            }).ToList();
            return View("VerifyRunningData", model);
        }

        public ActionResult RunningStatusUpdate(MachineryInfoViewModel model)
        {
            ResponseModel verifiedStatusUpdate = _runningHourInfoManager.VerifiedStatusUpdate(model.RunningHourInfos);
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
            return View("VerifyRunningData", model);
        }
    }
}