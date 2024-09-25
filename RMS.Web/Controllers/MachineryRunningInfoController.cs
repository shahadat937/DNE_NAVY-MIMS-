using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iTextSharp.text.pdf.qrcode;
using iTextSharp.tool.xml.html;
using Microsoft.Reporting.WebForms;
using RMS.BLL.IManager;
using RMS.BLL.Manager;
using RMS.Model;
using RMS.Utility;
using RMS.Web.Models.ViewModel;
using Microsoft.AspNet.Identity;

namespace RMS.Web.Controllers
{
     [Authorize]
    public class MachineryRunningInfoController : BaseController
     {
         
        private readonly IFuelConsumptionManager _fuelConsumption;
         private readonly IShipMovementManager _shipMovementManager;
        private readonly IMachineryRunningManager _machineryRunningManager;
        private readonly ICommonCodeManager _commonCodeManager;
        private readonly IShipInfoManager _shipInfoManager;
         private IShipEmployedManager _shipEmployedManager;
         private IFuelConsumptionManager _fuelConsumptionManager;
         private IControlShipInfoManager _controlShipInfoManager;
         public MachineryRunningInfoController(IMachineryRunningManager machineryRunningManager, IControlShipInfoManager controlShipInfoManager, ICommonCodeManager commonCodeManager, IShipInfoManager shipInfoManager, IShipEmployedManager shipEmployedManager, IFuelConsumptionManager fuelConsumptionManager, IShipMovementManager shipMovementManager, IFuelConsumptionManager fuelConsumption)
        {
            _machineryRunningManager = machineryRunningManager;
             _fuelConsumptionManager = fuelConsumptionManager;
            _commonCodeManager = commonCodeManager;
            _shipInfoManager = shipInfoManager;
             _shipEmployedManager = shipEmployedManager;
             _shipMovementManager = shipMovementManager;
             _fuelConsumption = fuelConsumption;
             _controlShipInfoManager = controlShipInfoManager;


        }

         public ActionResult NavTab(MachineryRunningInfoViewModel model)
         {
             model.BrControlShipInfos = _controlShipInfoManager.ShipBranchInfo();
             var FuelConsu = _fuelConsumption.AllData();
             var query = (from post in FuelConsu join meta in model.BrControlShipInfos on post.ShipId equals meta.ControlShipInfoId select post).ToList();
             model.FuelConsumptionViewModel.FuelConsumptions = query;
             var MachineryRI = _machineryRunningManager.GetAll();
             var query1 = (from post in MachineryRI join meta in model.BrControlShipInfos on post.ShipId equals meta.ControlShipInfoId select post).ToList();
             model.MachineryRunningInfos = query1;
             var ShipMove = _shipMovementManager.GetAll();
             var query2 = (from post in ShipMove join meta in model.BrControlShipInfos on post.ShipIdentity equals meta.ControlShipInfoId select post).ToList();
             model.ShipMovementViewModel.ShipMovements = query2;
             return View(model);
         }
        public ActionResult Index(MachineryRunningInfoViewModel model)
        {
            model.ControlShipInfos = _controlShipInfoManager.AllByControlLevel(0);
            model.MachineryRunningInfos = _machineryRunningManager.GetAll();
            return View(model);
        }
        public ActionResult SearchByKey(MachineryRunningInfoViewModel model)
        {
            string searchKey = model.SearchKey;
            model.MachineryRunningInfos = searchKey != null ? _machineryRunningManager.GetAll().Where(x => x.ControlShipInfo.ControlName.ToLower().Contains(searchKey.ToLower()) && x.LastUpdate >= model.DateFrom && x.LastUpdate <= model.DateTo).ToList() : new List<MachineryRunningInfo>();
            //Session["DateFrom"] = model.DateFrom;
            //Session["DateTo"] = model.DateTo;
            //Session["ShipId"] = _machineryRunningManager.GetShipId(searchKey).ShipId;
            return PartialView("~/Views/MachineryRunningInfo/_damageMachinaries.cshtml", model);
        }
        [HttpGet]
        public ActionResult Edit(string id, MachineryRunningInfoViewModel model)
        {
            ModelState.Clear();
            model.ShipInfos = _shipInfoManager.GetAll();
            //model.ControlShipInfos = _controlShipInfoManager.AllByControlLevel(0);
            model.BrControlShipInfos = _controlShipInfoManager.ShipBranchInfo();
            model.Type = _commonCodeManager.GetCommonCodeByType("EngineType");
            model.CommonCodess = _commonCodeManager.GetCommonCodeByType("HourMinType");
            var costCentre = _machineryRunningManager.GetShip(id, model.MachineryRunningInfo) ?? new MachineryRunningInfo();
            model.MachineryRunningInfo = costCentre;
            return View(model);
        }
        [HttpPost]
        public ActionResult Save(MachineryRunningInfoViewModel model)
        {
            ResponseModel saveData = _machineryRunningManager.Saving(model.MachineryRunningInfo);
            if (saveData.Message != string.Empty)
            {
                TempData["message"] = saveData.Message;
            }
            return RedirectToAction("Edit", model);
        }
        public ActionResult Delete(string id)
        {
            var deleteIndex = _machineryRunningManager.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Download(long id, DateTime dateFrom, DateTime dateTo)
        {

            DateTime fromdate = dateFrom;
            DateTime ToDate = dateTo;
            long shipid = id;
            var localReport = new LocalReport();
            var reportDataForShipEmployed = new List<ShipMovement>();
            List<ShipMovement> list1 = _shipMovementManager.GetDataForReport(fromdate, ToDate, shipid);



            foreach (var model in list1)
            {
                var oldData = new ShipMovement
                {
                    fromDate = fromdate,
                    ToDate = ToDate,
                    ShipName = model.ControlShipInfo.ControlName,
                    CompletionDate = model.CompletionDate,
                    DockingDate = model.DockingDate,
                    UndockingDate = model.UndockingDate,
                    RefitDate = model.RefitDate,
                    UnrefitDate = model.UnrefitDate,
                    MaintenancePeriod = model.MaintenancePeriod,
                    PowerTrial = model.PowerTrial,
                    NonOperational = model.NonOperational,
                    InspectionBy = model.InspectionBy,
                    AtSea = model.AtSea,
                    DistanceRun = model.DistanceRun,
                    AtNormalNotice = model.AtNormalNotice,
                    TimeUnderWayHour = model.TimeUnderWayHour,
                    TimeUnderWayMiniute = model.TimeUnderWayMiniute,
                    MachineryUnderTrialRemarks = model.MachineryUnderTrialRemarks,
                    EngineerOfficerRemarks = model.EngineerOfficerRemarks,
                    AdministrativeAuthorityRemarks = model.AdministrativeAuthorityRemarks,
                    RDay = (int)(ToDate - fromdate).TotalDays,
                    Reportname ="SHIP RETURNING RETURN",
                    SubName="Reportname,'E.R.FLY SHEET FOR I.C.E PROPELLED VESSELS",
                    LetterNo ="02.0158.65.1.88.031.16.18",
                    Quarter = ((fromdate.Month + 2) / 3 +"th")
                };
                reportDataForShipEmployed.Add(oldData);
            }

            var reportDataSource = new ReportDataSource { Name = "DataSet1" };
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = reportDataForShipEmployed;

            var reportDataForConsumption = new List<vwFuelConsumption>();
            List<vwFuelConsumption> list2 = _fuelConsumptionManager.GetReportData(fromdate, ToDate, shipid);
            foreach (var fuelConsumption in list2)
            {
                var oldData = new vwFuelConsumption
                {
                    MainEngines = fuelConsumption.MainEngines,
                    otherOil = fuelConsumption.otherOil,
                    FreshWater = fuelConsumption.FreshWater,
                    MainEnginesLubOil = fuelConsumption.MainEnginesLubOil,
                    otherOilLubOil = fuelConsumption.otherOilLubOil,
                    FreshWaterLubOil = fuelConsumption.FreshWaterLubOil


                };
                reportDataForConsumption.Add(oldData);
            }

            var reportDataSource2 = new ReportDataSource { Name = "DataSet2" };
            localReport.DataSources.Add(reportDataSource2);
            reportDataSource2.Value = reportDataForConsumption;

            var reportDataForMachinaryRunning = new List<vwMachineryRunningInfo>();

            List<MachineryRunningInfo> list3 = _machineryRunningManager.GetReportData(fromdate, ToDate, shipid);
            
            foreach (var model in list3)
            {
                var oldData = new vwMachineryRunningInfo
                {
                    Hour =(long) model.Hour,
                    TypeValue =model.CommonCode.TypeValue,
                    Minute =(long) model.Minute,
                    EngineName =model.CommonCode1.TypeValue,
                    ShipName =model.ControlShipInfo.ControlName
           
                };
                reportDataForMachinaryRunning.Add(oldData);
            }

            var reportDataSource3 = new ReportDataSource { Name = "DataSet3" };
            localReport.DataSources.Add(reportDataSource3);
            reportDataSource3.Value = reportDataForMachinaryRunning;

            localReport.ReportPath = Server.MapPath("~/Reports/BNS/shipReturn.rdlc");
            string mimeType;
            string encoding;
            string fileNameExtension;
            Warning[] warnings;
            string[] streams;

            byte[] renderedBytes = localReport.Render("PDF", "", out mimeType, out encoding,
                out fileNameExtension, out streams, out warnings);
            Response.AddHeader("content-disposition", "attachment; filename=Urls." + fileNameExtension);

            return File(renderedBytes, fileNameExtension);

            //localReport.ReportPath = Server.MapPath("~/Reports/BNS/shipReturn.rdlc");
            //string mimeType;
            //string encoding;
            //string fileNameExtension;
            //Warning[] warnings;
            //string[] streams;
            //byte[] renderedBytes = localReport.Render("PDF", "", out mimeType, out encoding,
            //    out fileNameExtension, out streams, out warnings);
            //Response.AddHeader("content-disposition", "attachment; filename=Urls." + fileNameExtension);
            //return File(renderedBytes, fileNameExtension);
            //return View();
        }
	}
}