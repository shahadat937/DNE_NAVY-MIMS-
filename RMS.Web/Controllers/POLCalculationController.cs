using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using iTextSharp.text.pdf.qrcode;
using Microsoft.AspNet.Identity;
using Microsoft.Reporting.WebForms;
using RMS.BLL.IManager;
using RMS.Model;
using RMS.Utility;
using RMS.Web.Controllers;
using RMS.Web.Models.ViewModel;

namespace RMS.Web.Controllers
{
    public class POLCalculationController : BaseController
    {
       private readonly IDamageMachineriesInfoManager _damageMachineriesInfo;
        private readonly IShipInfoManager _shipInfoManager;
        private readonly ICommonCodeManager _commonCodeManager;
        private IControlShipInfoManager _controlShipInfoManager;
        private IMachineryInfoManager _machineryInfoManager;

        public POLCalculationController(IDamageMachineriesInfoManager damageMachineriesInfo, IControlShipInfoManager controlShipInfoManager, IShipInfoManager shipInfoManager, ICommonCodeManager commonCodeManager, IMachineryInfoManager machineryInfoManager)
        {
            _damageMachineriesInfo = damageMachineriesInfo;
            _shipInfoManager = shipInfoManager;
            _commonCodeManager = commonCodeManager;
            _controlShipInfoManager = controlShipInfoManager;
            _machineryInfoManager = machineryInfoManager;
        }
        public ActionResult Index(DamageMachineriesInfoViewModel model)
        {
            model.ControlShipInfos = _controlShipInfoManager.AllByControlLevel(0);
            model.DamageMachineriesInfos = _damageMachineriesInfo.GetPol();
            return View(model);
        }

       
        public ActionResult SearchByKey(DamageMachineriesInfoViewModel model)
        {
            string searchKey = model.SearchKey;
            model.PolCalculation = searchKey != null ? _damageMachineriesInfo.GetPol().Where(x => x.ControlShipInfo.ControlName.ToLower().Contains(searchKey.ToLower())).ToList() : _damageMachineriesInfo.GetPol().ToList();
            return PartialView("~/Views/POLCalculation/_polCalculaton.cshtml", model);
        }
        public ActionResult Edit(string id, DamageMachineriesInfoViewModel model)
        {

            ModelState.Clear();
            //model.ControlShipInfos = _controlShipInfoManager.AllByControlLevel(0);
            model.BrControlShipInfos = _controlShipInfoManager.ShipBranchInfo();
            model.POLItemList = _commonCodeManager.GetCommonCodeByType("POLType");
            model.ShipInfos = _shipInfoManager.GetAll();
            var costCentre = _damageMachineriesInfo.GetShip(id, model.DamageMachineriesInfo) ?? new DamageMachineriesInfo();
            model.DamageMachineriesInfo = costCentre;
            if (costCentre.UnitPrice != null | costCentre.FuelConsumption != null | costCentre.UnitPrice != null && costCentre.FuelConsumption != null)
            {
                string unitprice = model.DamageMachineriesInfo.UnitPrice.ToString();
                string fuelconsumptuon = model.DamageMachineriesInfo.FuelConsumption.ToString();
                string stockFuel = model.DamageMachineriesInfo.StockFuelOil.ToString();
                model.BFuelConsumtionMoney =string.Concat(unitprice.Select(c => (char) ('\u09E6' + c - '0')));  // "১২৩৪৫৬৭৮৯০"
                model.BFuelConsumtion = string.Concat(fuelconsumptuon.Select(c => (char)('\u09E6' + c - '0'))); // "১২৩৪৫৬৭৮৯০"
                model.BStockFuel = string.Concat(stockFuel.Select(c => (char)('\u09E6' + c - '0'))); // "১২৩৪৫৬৭৮৯০"

            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Save(DamageMachineriesInfoViewModel model)
        {
           // model.DamageMachineriesInfo.UnitPrice = string.Concat(model.DamageMachineriesInfo.UnitPrice.ToString().Select(c => (char)('\u09E6' + c - '0'))); // "১২৩৪৫৬৭৮৯০"
           
            //model.DamageMachineriesInfo.UnitPrice = Convert.ToDecimal(string.Concat(model.BFuelConsumtionMoney.Select(c => (char)('0' + c - '\u09E6')))); // "1234567890"
            //model.DamageMachineriesInfo.FuelConsumption = Convert.ToDecimal(string.Concat(model.BFuelConsumtionMoney.Select(c => (char)('0' + c - '\u09E6')))); // "1234567890"
            //model.DamageMachineriesInfo.StockFuelOil = Convert.ToDecimal(string.Concat(model.BStockFuel.Select(c => (char)('0' + c - '\u09E6')))); // "1234567890"
            ResponseModel saveData = _damageMachineriesInfo.Saving(model.DamageMachineriesInfo);
            if (saveData.Message != string.Empty)
            {
                TempData["message"] = saveData.Message;
            }
            return RedirectToAction("Edit", model);
        }
        public ActionResult Delete(string id)
        {
            var deleteIndex = _damageMachineriesInfo.Delete(id);
            return RedirectToAction("Nabvar", "DamageMachineriesInfo");
        }

        public ActionResult Download(long id)
        {

            var localReport = new LocalReport();
            var model = new DamageMachineriesInfoViewModel { DamageMachineriesInfos = _damageMachineriesInfo.GetAll().Where(x =>x.ShipId == id).ToList() };
            

            var reportDataList = model.DamageMachineriesInfos.Select(item => new vwDamageMachinery
            {
                OrgName = "Bangladesh Navy",
                OfficeName = PortalContext.CurrentUser.BankName,
                Parameter = "Ship Information",
                ReportName = "Machinery Parameters - "+ item.ControlShipInfo.ControlName,
                ShipId = item.ShipId,
                ShipName = item.ControlShipInfo.ControlName,
                //SerialNo = item.SerialNo,
               Description = item.MachineryInfo.Machinery ?? "",
               MobilityDescription = item.CommonCode.TypeValue ?? "",
                DamageDate = item.DamageDate,
                RepairTime = item.RepairTime ?? "",
                Remarks = item.Remarks ?? "",
                //MachineSerialNo = item.MachineSerialNo ?? "",
                MachineSide = item.MachineSide ?? "",
                MachineName = item.MachineryInfo1.Machinery ?? "",
                Hour = item.Hour ?? "",
                Minute = item.Minute ?? "",
                Duration = item.Duration ?? "",
                MachineRemarks = item.MachineRemarks ?? "",
                //FuelSerialNo = item.FuelSerialNo ?? "",
              //  FuelName = item.FuelOilName ?? "",
                //StockFuelOil = item.StockFuelOil,
                //FuelConsumption = item.FuelConsumption,
                //UnitPrice = item.UnitPrice ?? "",
            
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

            //model.DamageMachineriesInfo.UnitPrice = string.Concat(model.DamageMachineriesInfo.UnitPrice.Select(c => (char)('\u09E6' + c - '0'))); // "১২৩৪৫৬৭৮৯০"
            //model.DamageMachineriesInfo.FuelConsumption = string.Concat(model.DamageMachineriesInfo.FuelConsumption.Select(c => (char)('\u09E6' + c - '0'))); // "১২৩৪৫৬৭৮৯০"

            var r = _damageMachineriesInfo.GetPol();
            reportDataSource = new ReportDataSource { Name = "PolCalculate" };
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = r;
            
            //model.DamageMachineriesInfo.FuelConsumption= string.Concat(model.DamageMachineriesInfo.FuelConsumption.Select(c => (char)('\u09E6' + c - '0'))); // "১২৩৪৫৬৭৮৯০
            
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
        public ActionResult VerifyPolData(DamageMachineriesInfoViewModel model)
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
                if (item.FuelOilId > 0)
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
                    bindingModel.StockFuelOil = item.StockFuelOil;
                    bindingModel.FuelConsumption = item.FuelConsumption;
                    bindingModel.UnitPrice = item.UnitPrice;
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
                    if (item.FuelOilId != null)
                        bindingModel.FuelOilName = _commonCodeManager.GetCommonName(item.FuelOilId);
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

            return View("VerifyPolData", model);
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
            model.VerifyType =
                _commonCodeManager.GetCommonCodeByType("VerifyUserSearchCategory").Select(x => new SelectionList
                {
                    Value = x.CommonCodeID,
                    Text = x.TypeValue
                }).ToList();
            return View("VerifyPolData", model);
        }
	}
}