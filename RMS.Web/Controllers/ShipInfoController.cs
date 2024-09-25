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
    public class ShipInfoController : BaseController
    {
        private readonly IShipInfoManager _shipInfoManager;
        private IControlShipInfoManager _controlShipInfoManager;
        private ICommonCodeManager _commonCodeManager;
        private IMachineryInfoManager _machineryInfoManager;
        public ShipInfoController(IShipInfoManager shipInfoManager, IControlShipInfoManager controlShipInfoManager, ICommonCodeManager commonCodeManager, IMachineryInfoManager machineryInfoManager)
        {
            _shipInfoManager = shipInfoManager;
            _controlShipInfoManager = controlShipInfoManager;
            _commonCodeManager = commonCodeManager;
            _machineryInfoManager = machineryInfoManager;
        }
        public ActionResult Index(ShipInfoViewModel model)
        {
            model.ControlShipInfos = _controlShipInfoManager.AllByControlLevel(0);
            model.ShipInfoes = _shipInfoManager.GetAll().OrderBy(x => x.SQD).ToList();
            return View(model);
        }

        public ActionResult RefitDockingIndex(ShipInfoViewModel model)
        {
            model.BrControlShipInfos = _controlShipInfoManager.ShipBranchInfo();
            var ShipInfo = _shipInfoManager.GetAll().OrderBy(x => x.LastUpdate).ToList();
            var query = (from post in ShipInfo join meta in model.BrControlShipInfos on post.ControlShipInfoId equals meta.ControlShipInfoId select post).ToList();
            model.ShipInfoes = query;
            return View(model);
        }
        [HttpPost]
        public JsonResult CallSignCheck(string sign)
        {
            int result = _shipInfoManager.FindCallSign(sign);
            return Json(result);
        }
        public ActionResult SearchByKey(ShipInfoViewModel model)
        {
            string searchKey = "";
            if (model.SearchKey != null)
            {
                searchKey = model.SearchKey.Trim().ToLower();

            }
            model.ShipInfoes = searchKey != "" ? _shipInfoManager.GetAll().Where(x => x.ShipId.ToLower().Contains(searchKey) || x.ShipName.ToLower().Contains(searchKey) || x.CommonCode.TypeValue.ToLower().Contains(searchKey)).ToList() : _shipInfoManager.GetAll();
            return View("Index", model);
        }
        public ActionResult SearchRefitAndDocking(ShipInfoViewModel model)
        {
            //string searchKey = "";
            //if (model.SearchKey != null)
            //{
            //    searchKey = model.SearchKey.Trim().ToLower();

            //}
            //model.ShipInfoes = searchKey != "" ? _shipInfoManager.GetAll().Where(x => x.ShipId.ToLower().Contains(searchKey) || x.ShipName.ToLower().Contains(searchKey) || x.CommonCode.TypeValue.ToLower().Contains(searchKey)).ToList() : _shipInfoManager.GetAll();
            //return View("RefitDockingIndex", model);

            long Search = model.Search;
            model.ShipInfoes = Search != null
                ? _shipInfoManager.GetAll()
                    .Where(x => x.ControlShipInfoId == Search).ToList() : _shipInfoManager.GetAll();
           
            model.ControlShipInfos = _controlShipInfoManager.AllByControlLevel(0);

            return View("RefitDockingIndex", model);
        }
        [HttpGet]
        public ActionResult Edit(string id, ShipInfoViewModel model)
        {
            ModelState.Clear();
            var costCentre = _shipInfoManager.GetShipById(id) ?? new ShipInfo();
            model.ShipInfo = costCentre;
            //model.ControlShipInfos = _controlShipInfoManager.AllByControlLevel(0);
            model.BrControlShipInfos = _controlShipInfoManager.ShipBranchInfo();
            model.CommonCodes = _commonCodeManager.GetCommonCodeByType("SQD");
           
            return View(model);
        }
        public ActionResult EditRefitDocking(string id, ShipInfoViewModel model)
        {
            ModelState.Clear();
            //model.ControlShipInfos = _controlShipInfoManager.AllByControlLevel(0);
            model.BrControlShipInfos = _controlShipInfoManager.ShipBranchInfo();
            model.ShipInfoes = _shipInfoManager.GetAll();
            model.ShipInfo  = _shipInfoManager.GetShipById(id) ?? new ShipInfo();
            model.TimeDuration = _commonCodeManager.GetCommonCodeByType("TimeDuration");
            return View(model);
        }
        [HttpPost]
        public ActionResult Save(ShipInfoViewModel model)
        {
          ResponseModel response =_shipInfoManager.Saving(model.ShipInfo);
          if (response.Message != string.Empty)
           {
               TempData["message"] = response.Message;
           }
            return RedirectToAction("Edit",model);
        }
        public ActionResult SaveRefitDocking(ShipInfoViewModel model)
        {
           
          
           ResponseModel response= _shipInfoManager.SaveRefitDocking(model.ShipInfo);
            if (response.Message != string.Empty)
            {
                TempData["message"] = response.Message;
            }
            return RedirectToAction("EditRefitDocking", model);
        }
        public ActionResult Nabvar(ShipInfoViewModel model)
        {
            return RedirectToAction("Nabvar", "MachineryInfo");
        }

        public ActionResult Search(ShipInfoViewModel model)
        {
            long Search = model.Search;
            model.ShipInfoes = Search != null
                ? _shipInfoManager.GetAll()
                    .Where(x => x.ShipInfoIdentity == Search).ToList() : _shipInfoManager.GetAll();
            //model.OrgCommonCodes = _commonCodeManager.GetCommonCodeByType("organization");
            model.ControlShipInfos = _controlShipInfoManager.AllByControlLevel(0);

            return View("Index", model);
        }
        public FileResult ExportTo(string reportType, string searchKey, string id)
        {
            var localReport = new LocalReport();
            var model = new ShipInfoViewModel();

            if (searchKey != null)
            {
                searchKey = searchKey.Trim().ToLower();
                model.ShipInfoes = _shipInfoManager.GetAll()
                      .Where(
                          x =>
                              x.ShipId.ToLower().Contains(searchKey) || x.ShipName.ToLower().Contains(searchKey) ||
                              x.CommonCode.TypeValue.ToLower().Contains(searchKey) ||
                              x.ShipId.ToLower().Contains(searchKey))
                      .ToList();

            }
            else
            {
                model.ShipInfoes = _shipInfoManager.GetAll();
            }
            var reportDataList = model.ShipInfoes.Select(item => new vwShipInfo()
            {
               
                ShipId = item.ShipId,
               ShipName = item.ShipName,
                ControlShipInfoId = item.ControlShipInfoId,
                ControlName = item.ControlShipInfo.ControlName,
                
                RefitInterval = item.RefitInterval ?? 0,
                RefitIntervalType = item.CommonCode2.TypeValue ,
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
            Response.AddHeader("content-disposition", "attachment; filename=Urls." + fileNameExtension);

            return File(renderedBytes, fileNameExtension);

        }
        public ActionResult Download(long id)
        {
            var localReport = new LocalReport();
            var model = new ShipInfoViewModel { ShipInfoes = _shipInfoManager.FindOne(id) };

            var reportDataList = model.ShipInfoes.Select(item => new vwShipInfo()
            {
                
                ShipId = item.ShipId,
                ShipName = item.ShipName,
                ControlShipInfoId = item.ControlShipInfoId,
                ControlName = item.ControlShipInfo.ControlName,
                
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
    }
}