using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using RMS.BLL.IManager;
using RMS.Model;
using RMS.Utility;
using RMS.Web.Models.ViewModel;

namespace RMS.Web.Controllers
{
    [Authorize]
    public class ShipCraftController : BaseController
    {
        private readonly IShipCraftManager _shipCraftManager;
        private readonly IControlShipInfoManager _controlShipInfoManager;
        private readonly ICommonCodeManager _commonCodeManager;
        private readonly IMachineryInfoManager _machineryInfoManager;
        public ShipCraftController(IShipCraftManager shipCraftManager, IControlShipInfoManager controlShipInfoManager, ICommonCodeManager commonCodeManager, IMachineryInfoManager machineryInfoManager)
        {
            _shipCraftManager = shipCraftManager;
            _controlShipInfoManager = controlShipInfoManager;
            _commonCodeManager = commonCodeManager;
            _machineryInfoManager = machineryInfoManager;
        }
        public ActionResult Index(ShipInfoViewModel model)
        {
            model.ShipInfoes = _shipCraftManager.GetAll().OrderBy(x => x.SQD).ToList();
            return View(model);
        }

        public ActionResult RefitDockingIndex(ShipInfoViewModel model)
        {
            model.ShipInfoes = _shipCraftManager.GetAll();
            return View(model);
        }

        public ActionResult ShipCraftEdit(ShipInfoViewModel model)
        {
            return RedirectToAction("StateofShipIndex", "ControlShipInfo");
        }
        [HttpPost]
        public JsonResult CallSignCheck(string sign)
        {
            int result = _shipCraftManager.FindCallSign(sign);
            return Json(result);
        }
        public ActionResult SearchByKey(ShipInfoViewModel model)
        {
            string searchKey = "";
            if (model.SearchKey != null)
            {
                searchKey = model.SearchKey.Trim().ToLower();

            }
            model.ShipInfoes = searchKey != "" ? _shipCraftManager.GetAll().Where(x => x.ShipId.ToLower().Contains(searchKey) || x.ShipName.ToLower().Contains(searchKey) || x.CommonCode.TypeValue.ToLower().Contains(searchKey)).ToList() : _shipCraftManager.GetAll();
            return View("Index", model);
        }
        public ActionResult SearchRefitAndDocking(ShipInfoViewModel model)
        {
            string searchKey = "";
            if (model.SearchKey != null)
            {
                searchKey = model.SearchKey.Trim().ToLower();

            }
            model.ShipInfoes = searchKey != "" ? _shipCraftManager.GetAll().Where(x => x.ShipId.ToLower().Contains(searchKey) || x.ShipName.ToLower().Contains(searchKey) || x.CommonCode.TypeValue.ToLower().Contains(searchKey)).ToList() : _shipCraftManager.GetAll();
            return View("RefitDockingIndex", model);
        }
        [HttpGet]
        public ActionResult Edit(string id, ShipInfoViewModel model)
        {
            ModelState.Clear();
            var costCentre = _shipCraftManager.GetShipById(id) ?? new ShipInfo();
            model.ShipInfo = costCentre;
            model.ControlShipInfos = _controlShipInfoManager.AllByControlLevel(1);
            model.CommonCodes = _commonCodeManager.GetCommonCodeByType("SQD");
           
            return View(model);
        }
        public ActionResult EditRefitDocking(string id, ShipInfoViewModel model)
        {
            ModelState.Clear();
            model.ShipInfoes = _shipCraftManager.GetAll();
            model.ShipInfo = _shipCraftManager.GetShipById(id) ?? new ShipInfo();
            model.TimeDuration = _commonCodeManager.GetCommonCodeByType("TimeDuration");
            return View(model);
        }
        [HttpPost]
        public ActionResult Save(ShipInfoViewModel model)
        {
            ResponseModel response = _shipCraftManager.Saving(model.ShipInfo);
          if (response.Message != string.Empty)
           {
               TempData["message"] = response.Message;
           }
            return RedirectToAction("Edit",model);
        }
        public ActionResult SaveRefitDocking(ShipInfoViewModel model)
        {


            ResponseModel response = _shipCraftManager.SaveRefitDocking(model.ShipInfo);
            if (response.Message != string.Empty)
            {
                TempData["message"] = response.Message;
            }
            return RedirectToAction("EditRefitDocking", model);
        }
        public FileResult ExportTo(string reportType, string searchKey, string id)
        {
            var localReport = new LocalReport();
            var model = new ShipInfoViewModel();

            if (searchKey != null)
            {
                searchKey = searchKey.Trim().ToLower();
                model.ShipInfoes = _shipCraftManager.GetAll()
                      .Where(
                          x =>
                              x.ShipId.ToLower().Contains(searchKey) || x.ShipName.ToLower().Contains(searchKey) ||
                              x.CommonCode.TypeValue.ToLower().Contains(searchKey) ||
                              x.ShipId.ToLower().Contains(searchKey))
                      .ToList();

            }
            else
            {
                model.ShipInfoes = _shipCraftManager.GetAll();
            }
            var reportDataList = model.ShipInfoes.Select(item => new vwShipInfo()
            {
                OrgName = PortalContext.CurrentUser.BankName,
                OfficeName = PortalContext.CurrentUser.BranchName,
                Parameter = "STATE OF SHIPS AND CRAFTS",
                ReportName = "Refit Docking Interval And Duration Of BN Ships/Craft",
                ShipId = item.ShipId,
                ShipName = item.ShipName,
                SQD = item.CommonCode.TypeValue,
                RefitInterval = item.RefitInterval ?? 0,
                RefitIntervalType = item.CommonCode2.TypeValue,
                RefitDuration = item.RefitDuration ?? 0,
                RefitDurationType = item.CommonCode1.TypeValue,
                DockingDuration = item.DockingDuration ?? 0,
                DockingDurationType = item.CommonCode.TypeValue,
                LastRefitDate = item.LastRefitDate,
                LastDockingDate = item.LastDockingDate,
                NextRefitDate = item.NextRefitDate,
                NextDockingDate = item.NextDockingDate,
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
            if (id == "Basic")
            {
                localReport.ReportPath = Server.MapPath("~/Reports/BNS/ShipBasicInfo.rdlc");
            }
            else if (id == "RefitDockingDuration")
            {
                localReport.ReportPath = Server.MapPath("~/Reports/BNS/RefitDockinIntervalAndDuration.rdlc");
            }

            var reportDataSource = new ReportDataSource { Name = "Report" };
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = reportDataList;

            string mimeType;
            string encoding;
            string fileNameExtension;
            Warning[] warnings;
            string[] streams;

            byte[] renderedBytes = localReport.Render(reportType, "", out mimeType, out encoding,
                out fileNameExtension, out streams, out warnings);
            Response.AddHeader("content-disposition", "inline; filename=Urls." + fileNameExtension);

            return File(renderedBytes, fileNameExtension);

        }
        public ActionResult Download(long id)
        {
            var localReport = new LocalReport();
            var model = new ShipInfoViewModel { ShipInfoes = _shipCraftManager.FindOne(id) };

            var reportDataList = model.ShipInfoes.Select(item => new vwShipInfo()
            {
                OrgName = PortalContext.CurrentUser.BankName,
                OfficeName = PortalContext.CurrentUser.BranchName,
                Parameter = "Ship's Basic Information",
                ReportName = "Machinery Parameters - " + item.ShipName,
                ShipId = item.ShipId,
                ShipName = item.ShipName,
                SQD = item.CommonCode.TypeValue,
                RefitInterval = item.RefitInterval ?? 0,
                RefitIntervalType = item.CommonCode2.TypeValue,
                RefitDuration = item.RefitDuration ?? 0,
                RefitDurationType = item.CommonCode1.TypeValue,
                DockingDuration = item.DockingDuration ?? 0,
                DockingDurationType = item.CommonCode.TypeValue,
                LastRefitDate = item.LastRefitDate,
                LastDockingDate = item.LastDockingDate,
                NextRefitDate = item.NextRefitDate,
                NextDockingDate = item.NextDockingDate,
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
            var reportDataSource1 = new ReportDataSource { Name = "MachineryParticulars" };
            localReport.DataSources.Add(reportDataSource1);
            reportDataSource1.Value = machineryInfo;


            localReport.ReportPath = Server.MapPath("~/Reports/BNS/ShipParameter.rdlc");
            string mimeType;
            string encoding;
            string fileNameExtension;
            Warning[] warnings;
            string[] streams;

            byte[] renderedBytes = localReport.Render("PDF", "", out mimeType, out encoding,
                out fileNameExtension, out streams, out warnings);
            Response.AddHeader("content-disposition", "inline; filename=Urls." + fileNameExtension);
            return File(renderedBytes, fileNameExtension);

        }
    }
}