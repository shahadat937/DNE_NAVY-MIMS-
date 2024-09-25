using System;
using System.IO;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using RMS.BLL.IManager;
using RMS.BLL.Manager;
using RMS.Model;
using RMS.Utility;
using RMS.Web.Models.ViewModel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections.Generic;

namespace RMS.Web.Controllers
{
    public class ShipMovementController : BaseController
    {
        private IShipMovementManager _shipMovementManager;
        private IShipInfoManager _shipInfoManager;
        private IMachineryRunningManager _machineryRunning;
        private IFuelConsumptionManager _fuelConsumptionManager;
        private IControlShipInfoManager _controlShipInfoManager;
        private IFuelConsumptions1_ResultManager _fuelConsumption1_ResultManager;
      


        public ShipMovementController(IShipMovementManager shipMovementManager, IShipInfoManager shipInfoManager, IMachineryRunningManager machineryRunning, IFuelConsumptionManager fuelConsumptionManager, IControlShipInfoManager controlShipInfoManager)
        {
            _shipMovementManager = shipMovementManager;
            _shipInfoManager = shipInfoManager;
            _machineryRunning = machineryRunning;
            _fuelConsumptionManager = fuelConsumptionManager;
            _controlShipInfoManager = controlShipInfoManager;
            _fuelConsumption1_ResultManager= new FuelConsumptions1_ResultManager();
        }
        public ActionResult Index(ShipMovementViewModel model)
        {
            //model.ShipMovements = _shipMovementManager.GetAll();
            model.ShipMovements = _shipMovementManager.GetAll();
            return View(model);
        }

        public ActionResult Save(ShipMovementViewModel model)
        {
          ResponseModel savedata =  _shipMovementManager.Save(model.ShipMovement);
          if (savedata.Message != string.Empty)
          {
              TempData["message"] = savedata.Message;
          }
          return RedirectToAction("Edit", model);
        }

        public ActionResult Edit( string id,ShipMovementViewModel model)
        {
            //model.ShipInfos = _shipInfoManager.GetAll();
            model.BrControlShipInfoes = _controlShipInfoManager.ShipBranchInfo();
            model.ShipMovement = _shipMovementManager.GetShipMovementById(Convert.ToInt64(id)) ?? new ShipMovement();
            return View(model);
        }

        public ActionResult Delete(string id)
        {
           var deletedata = _shipMovementManager.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Search(ShipMovementViewModel model)
        {
         
            string searchkey = model.SearchKey;
            model.ShipMovements = searchkey != null
                ? _shipMovementManager.GetAll()
                    .Where(x => x.ControlShipInfo.ControlName.ToLower().Contains(searchkey.ToLower()))
                    .ToList()
                : _shipMovementManager.GetAll();
            return View("~/Views/ShipMovement/_shipMovement.cshtml", model);
        }


        public ActionResult Download(long id)
        {
            var localReport = new LocalReport();
            var model = new ShipMovementViewModel { ShipMovements = _shipMovementManager.FindOne(id) };

            var reportDataList = model.ShipMovements.Select(item => new vwShipMovement()
            {
                OrgName = "Bangladesh Navy",
                OfficeName = PortalContext.CurrentUser.BankName,
                Parameter = "Ship's Information",
                ReportName = "Machinery Parameters - ", //+ item.ShipName,
                //ShipId = item.ShipId,
                //Location = item.Location??"",
                //DateFrom = item.DateFrom,
                //DateTo = item.DateTo,
                //CompletionDes = item.CompletionDes ?? "",
                CompletionDate = item.CompletionDate,
                UserId = item.UserId
                
            }).ToList();


            var reportDataSource = new ReportDataSource { Name = "ShipMovement" };
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = reportDataList;

            var FuelConsumption = _fuelConsumptionManager.FindOne(id);
            var reportDataSource1 = new ReportDataSource { Name = "DataSet2" };
            localReport.DataSources.Add(reportDataSource1);
            reportDataSource1.Value = FuelConsumption;

            var Machinery = _machineryRunning.FindOne(id);
            var reportDataSource2 = new ReportDataSource { Name = "MachineriesRunning" };
            localReport.DataSources.Add(reportDataSource1);
            reportDataSource2.Value = Machinery;


            localReport.ReportPath = Server.MapPath("~/Reports/BNS/ShipInformation.rdlc");
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
	}
}