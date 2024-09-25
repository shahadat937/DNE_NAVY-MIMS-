using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using RMS.BLL.IManager;
using RMS.Model;
using RMS.Utility;
using RMS.Web.Models.ViewModel;
using Microsoft.AspNet.Identity;

namespace RMS.Web.Controllers
{
    public class RunningHourController : BaseController
    {
           private readonly IDamageMachineriesInfoManager _damageMachineriesInfo;
        private readonly IShipInfoManager _shipInfoManager;
        private IControlShipInfoManager _controlShipInfoManager;
        private IMachineryInfoManager _machineryInfoManager;
        private ICommonCodeManager _commonCodeManager;
        public RunningHourController(IControlShipInfoManager controlShipInfoManager, IDamageMachineriesInfoManager damageMachineriesInfo, IShipInfoManager shipInfoManager, IMachineryInfoManager machineryInfoManager, ICommonCodeManager commonCodeManager)
        {
            _damageMachineriesInfo = damageMachineriesInfo;
            _shipInfoManager = shipInfoManager;
            _controlShipInfoManager = controlShipInfoManager;
            _machineryInfoManager = machineryInfoManager;
            _commonCodeManager = commonCodeManager;
        }
        public ActionResult Index(DamageMachineriesInfoViewModel model)
        {
            model.DamageMachineriesInfos = _damageMachineriesInfo.GetMachineHour();
            model.ControlShipInfos = _controlShipInfoManager.AllByControlLevel(0);
            return View(model);
        }
        public ActionResult SearchByKey(DamageMachineriesInfoViewModel model)
        {
            string searchKey = model.SearchKey;
            model.runninghour = searchKey != null ? _damageMachineriesInfo.GetMachineHour().Where(x => x.ControlShipInfo.ControlName.ToLower().Contains(searchKey.ToLower())).ToList(): _damageMachineriesInfo.GetMachineHour().ToList();
            return PartialView("~/Views/RunningHour/_runningHour.cshtml", model);
        }
        public ActionResult Edit(string id, DamageMachineriesInfoViewModel model)
        {
            ModelState.Clear();
            //model.ControlShipInfos = _controlShipInfoManager.AllByControlLevel(0);
            model.BrControlShipInfos = _controlShipInfoManager.ShipBranchInfo();
            model.ShipInfos = _shipInfoManager.GetAll();
            model.MachinaryList = _machineryInfoManager.GetAll().Where(x => x.MachinariesState == CommonConstant.RunningMachinaries).Select(lg => new SelectionList()
            {
                Value = lg.MachineryInfoIdentity,
                Text = lg.Machinery + "+" + lg.MachinaryBangla
            }).ToList();
            var costCentre = _damageMachineriesInfo.GetShip(id, model.DamageMachineriesInfo) ?? new DamageMachineriesInfo();
            model.DamageMachineriesInfo = costCentre;
            return View(model);
        }

        [HttpPost]
        public ActionResult Save(DamageMachineriesInfoViewModel model)
        {
           
            var saveData = _damageMachineriesInfo.Saving(model.DamageMachineriesInfo);
            if (saveData.Message != string.Empty)
            {
                TempData["message"] = saveData.Message;
            }
            return RedirectToAction("Edit", model);
        }
        public ActionResult Delete(string id)
        {
            var deleteIndex = _damageMachineriesInfo.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Download(long id)
        {
            var localReport = new LocalReport();
            var model = new DamageMachineriesInfoViewModel { DamageMachineriesInfos = _damageMachineriesInfo.GetAll() };

            var reportDataList = model.DamageMachineriesInfos.Select(item => new vwDamageMachinery
            {
                OrgName = "Bangladesh Navy",
                OfficeName = PortalContext.CurrentUser.BankName,
                Parameter = "Ship's Basic Information",
                ReportName = "Machinery Parameters - ",//+ item.ShipName,
                ShipId = item.ShipId,
                //ShipName = item.ShipName,
                //SerialNo = item.SerialNo,
               // Description = item.Description ?? "",
               // MobilityDescription = item.MobilityDescription ?? "",
                DamageDate = item.DamageDate,
                RepairTime = item.RepairTime ?? "",
                Remarks = item.Remarks ?? "",
                //MachineSerialNo = item.MachineSerialNo ?? "",
                MachineSide = item.MachineSide ?? "",
               // MachineName = item.MachineName ?? "",
                Hour = item.Hour ?? "",
                Minute = item.Minute ?? "",
                Duration = item.Duration ?? "",
                MachineRemarks = item.MachineRemarks ?? "",
                //FuelSerialNo = item.FuelSerialNo ?? "",
               // FuelOilName = item.FuelOilName ?? "",
                //StockFuelOil = item.StockFuelOil ?? "",
                //FuelConsumption = item.FuelConsumption ?? "",
                //UnitPrice = item.UnitPrice ?? "",
                //TotalPrice = item.TotalPrice ?? "",
                UserId = item.UserId,
                LastUpdate = item.LastUpdate
                //Remarks = item.Remarks
            }).ToList();



            //var reportDataList = _damageMachineriesInfo.FindOne(id);
            var reportDataSource = new ReportDataSource { Name = "DamageMachineries" };
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = reportDataList;

            var m = _damageMachineriesInfo.FindOne(id);
            reportDataSource = new ReportDataSource { Name = "RunningHour" };
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = m;

            var r = _damageMachineriesInfo.GetPol();
            reportDataSource = new ReportDataSource { Name = "PolCalculate" };
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = r;


            localReport.ReportPath = Server.MapPath("~/Reports/BNS/DamageMachineriesInformation.rdlc");
            string mimeType;
            string encoding;
            string fileNameExtension;
            Warning[] warnings;
            string[] streams;

            byte[] renderedBytes = localReport.Render("PDF", "", out mimeType, out encoding,
                out fileNameExtension, out streams, out warnings);
            Response.AddHeader("content-disposition", "attachment; filename=Urls." + fileNameExtension);

            return File(renderedBytes, fileNameExtension);

        }
        // Verify Running Hour Index
        public ActionResult VerifyRunningData(DamageMachineriesInfoViewModel model)
        {
            model.VerifyType =
                _commonCodeManager.GetCommonCodeByType("VerifyUserSearchCategory").Select(x => new SelectionList
                {
                    Value = x.CommonCodeID,
                    Text = x.TypeValue
                }).ToList();
            return View(model);
        }
        public ActionResult VerifyMachinariesSearch(int? VerifyType, DateTime? DateFrom, DateTime? DateTo, DamageMachineriesInfoViewModel model)
        {

            //string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();

            model.DamageMachineriesInfos = _damageMachineriesInfo.UserWiseData(VerifyType);
            if (model.DamageMachineriesInfos.Any())
            {
                model.DamageMachineriesInfos = DateFrom != null && DateTo != null
                    ? model.DamageMachineriesInfos.Where(x => x.EntryDate >= DateFrom && x.EntryDate <= DateTo).ToList()
                    : model.DamageMachineriesInfos;
            }
            // Bind Model
            List<vwDamageMachinaries> ListDataModel = new List<vwDamageMachinaries>();
            foreach (var item in model.DamageMachineriesInfos)
            {
                if (item.MachineName > 0)
                {
                    vwDamageMachinaries bindingModel = new vwDamageMachinaries();
                    bindingModel.MachineriesDescriptionIdentity = item.MachineriesDescriptionIdentity;
                    bindingModel.ShipId = item.MachineriesDescriptionIdentity;
                    bindingModel.SerialNo = item.SerialNo;
                    bindingModel.DamageDate = item.DamageDate;
                    bindingModel.ReportDate = item.ReportDate;
                    bindingModel.RepairTime = item.RepairTime;
                    bindingModel.Remarks = item.Remarks;
                    bindingModel.MachineSerialNo = item.MachineSerialNo;
                    bindingModel.MachineSide = item.MachineSide;
                    bindingModel.Hour = item.Hour;
                    bindingModel.Minute = item.Minute;
                    bindingModel.Duration = item.Duration;
                    bindingModel.MachineRemarks = item.MachineRemarks;
                    bindingModel.UserId = item.UserId;
                    bindingModel.LastUpdate = item.LastUpdate;
                    bindingModel.EntryDate = item.EntryDate;
                    bindingModel.UpdatedBy = item.UpdatedBy;
                    bindingModel.IsVerified = (bool) item.IsVerified;
                    bindingModel.VerifiedBy = item.VerifiedBy;
                    bindingModel.VerifiedDate = item.VerifiedDate;
                    bindingModel.ShipName = _controlShipInfoManager.GetShipName(item.ShipId);
                    if (item.Description != null)
                        bindingModel.MachinaryName = _machineryInfoManager.GetMashineName(item.Description);
                    if (item.MobilityDescription != null)
                        bindingModel.Status = _commonCodeManager.GetCommonName(item.MobilityDescription);
                    if (item.MachineName != null)
                        bindingModel.MachineName2 = _machineryInfoManager.GetMashineName(item.MachineName);
                    ListDataModel.Add(bindingModel);
                }

            }
            model.VwDamageMachinarieses = ListDataModel;
            model.VerifyType =
                _commonCodeManager.GetCommonCodeByType("VerifyUserSearchCategory").Select(x => new SelectionList
                {
                    Value = x.CommonCodeID,
                    Text = x.TypeValue
                }).ToList();

            return View("VerifyRunningData", model);
        }

        public ActionResult StatusUpdate(DamageMachineriesInfoViewModel model)
        {

            ResponseModel verifiedStatusUpdate =
                _damageMachineriesInfo.VerifiedStatusUpdate(model.VwDamageMachinarieses);
            if (verifiedStatusUpdate.Message != string.Empty)
            {
                model.Message = verifiedStatusUpdate.Message;
                model.IsSucceed = 1;
            }
            else
            {
                model.Message = "Status Not Updated";
            }
            model.VerifyType =_commonCodeManager.GetCommonCodeByType("VerifyUserSearchCategory").Select(x => new SelectionList
                {
                    Value = x.CommonCodeID,
                    Text = x.TypeValue
                }).ToList();
            return View("VerifyRunningData", model);
        }
	}
}