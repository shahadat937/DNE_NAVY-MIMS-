using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RMS.BLL.IManager;
using RMS.Model;
using RMS.Utility;
using RMS.Web.Models.ViewModel;
using iTextSharp.text.pdf.qrcode;
using Microsoft.Reporting.WebForms;

namespace RMS.Web.Controllers
{
    [Authorize]
    public class ICEController : Controller
    {
        private IShipInfoManager _shipInfoManager;
        private IControlShipInfoManager _controlShipInfoManager;
        private IICEManager _ICEManager;
        private readonly IDamageMachineriesInfoManager _damageMachineriesInfoManager;
        private readonly ICommonCodeManager _commonCodeManager;

        public ICEController(IICEManager iceManager, IShipInfoManager shipInfoManager,
            ICommonCodeManager commonCodeManager, IControlShipInfoManager controlShipInfoManager, IDamageMachineriesInfoManager damageMachineriesInfoManager)
        {
            _ICEManager = iceManager;
            _shipInfoManager = shipInfoManager;
            _commonCodeManager = commonCodeManager;
            _controlShipInfoManager = controlShipInfoManager;
            _damageMachineriesInfoManager = damageMachineriesInfoManager;
        }

        public ActionResult Index(ICEViewModel model)
        {
            model.BrControlShipInfos = _controlShipInfoManager.ShipBranchInfo();
            var ICE = _ICEManager.GetAll().OrderByDescending(x => x.Year).ToList();
            var query = (from post in ICE join meta in model.BrControlShipInfos on post.ControlShipInfoId equals meta.ControlShipInfoId select post).ToList();
            model.ICEs = query;
            return View(model);
        }
        public ActionResult ICEBYShipName(string Id, ICEViewModel model)
        {
            model.BrControlShipInfos = _controlShipInfoManager.ShipBranchInfo();
            var ICE = _ICEManager.GetAll().Where(x=>x.ControlShipInfoId==Convert.ToInt64(Id)).OrderByDescending(x => x.LastUpdate).ToList();
            var query = (from post in ICE join meta in model.BrControlShipInfos on post.ControlShipInfoId equals meta.ControlShipInfoId select post).ToList();
            model.ICEs = query;
            return View(model);
        }       
        public ActionResult Edit(string id, ICEViewModel model)
        {
            ModelState.Clear();
            //model.ControlShipInfos = _controlShipInfoManager.AllByControlLevel(0);
            model.BrControlShipInfos = _controlShipInfoManager.ShipBranchInfo();
            model.ShipInfos = _shipInfoManager.GetAll();
            model.ICE = _ICEManager.FindOne(Convert.ToInt32(id)) ?? new ICE();
            model.Years = _damageMachineriesInfoManager.returnReportYears();
            model.ICEFor = _commonCodeManager.GetCommonCodeByType("ICEFor");

            return View(model);
        }

        public ActionResult ICEReturnView(string Year, string ShipId, ICEViewModel model)
        {
            if (model.Year > 0 && model.ShipId > 0)
            {
                ICE yearlyReturn = new ICE();
                model.ICEs = _ICEManager.ICEViewByShipId( Convert.ToInt16(Year), Convert.ToInt64(ShipId));
                model.ShipName = model.ICEs.FirstOrDefault().ControlShipInfo.ControlName;
                model.ReportYear = model.ICEs.FirstOrDefault().Year;
            }

            return View(model);

        }

        [HttpPost]
        public ActionResult Save(ICEViewModel model)
        {
            ResponseModel saveData = _ICEManager.Save(model.ICE);
            if (saveData.Message != string.Empty)
            {
                TempData["message"] = saveData.Message;
            }
            return RedirectToAction("Edit", model);
        }
        public JsonResult SearchShipName(string SearchCharacter)
        {
            List<vwShipNameSearch> val = new List<vwShipNameSearch>();
            var kho = _controlShipInfoManager.GetAll().Where(x => x.ParentCode == 0 && x.ControlName.ToLower().Contains(SearchCharacter.ToLower())).ToList();
            foreach (var item in kho)
            {
                vwShipNameSearch khomeni = new vwShipNameSearch();
                khomeni.ShipName = item.ControlName;
                val.Add(khomeni);
            }
            return Json(val, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SearchByKey(ICEViewModel model)
        {
            string searchKey = "";
            if (model.SearchKey != null)
            {
                searchKey = model.SearchKey.Trim().ToLower();
                model.ICEs =
                    _ICEManager.GetAll()
                        .Where(
                            x =>
                                x.ControlShipInfo.ControlName.ToLower().Contains(searchKey) ||
                                x.EngineCoolingSys.ToLower().Contains(searchKey) ||
                                x.EngineModel.ToLower().Contains(searchKey) ||
                                x.EngineManufacturer.ToLower().Contains(searchKey))
                        .ToList();
            }
            else
            {
                model.ICEs = _ICEManager.GetAll();
            }
            return View("Index", model);
        }

        public ActionResult Search(ICEViewModel model)
        {

            string serachKey = "";


            if (model.SearchKey != null)
            {
                serachKey = model.SearchKey.Trim().ToLower();

                model.ICEs = _ICEManager.GetAll().Where(x => x.ControlShipInfo.ControlName.ToLower().Contains(serachKey.ToLower()) || x.EngineCoolingSys.ToLower().Contains(serachKey.ToLower()) || x.EngineModel.ToLower().Contains(serachKey.ToLower()) || x.EngineManufacturer.ToLower().Contains(serachKey.ToLower()) && x.LastUpdate >= model.DateFrom && x.LastUpdate <= model.DateTo).ToList();
            }

            model.ControlShipInfos = _controlShipInfoManager.AllByControlLevel(0);
            return View("Index", model);
        }

        public ActionResult Download(long id)
        {
            //DateTime fromdate = (DateTime)dateFrom;
            //DateTime ToDate = (DateTime)dateTo;
            long shipid = id;

            var localReport = new LocalReport();
            var model = new ICEViewModel();


           model.ICEs = _ICEManager.GetICEId (shipid);
            var reportDataForICE = model.ICEs.Select(item => new vwICE()
            
                {
                    IceId = item.IceId,
                    ControlShipInfoId = item.ControlShipInfoId,
                    BNS = item.ControlShipInfo.ControlName,
                    todate = DateTime.Now.Date,
                    IceDate = item.IceDate,
                    InstructionNo = item.InstructionNo,
                    EngineManufacturer = item.EngineManufacturer,
                    EngineModel = item.EngineModel,
                    EngineWork = item.EngineWork,
                    EngineSlNo = item.EngineSlNo,
                    EngineCoolingSys = item.EngineCoolingSys,
                    AfterRepairTime = item.AfterRepairTime,
                    SinceCommission = item.SinceCommission,
                    MaintenanceRemark = item.MaintenanceRemark,
                    EngineerRemark1 = item.EngineerRemark1,
                    EngineerRemark2 = item.EngineerRemark2,
                    EngineerRemark3 = item.EngineerRemark3,
                    UserId = PortalContext.CurrentUser.UserName,
                    LastUpdate = DateTime.Now

                }).ToList();
           
            var reportDataSource = new ReportDataSource { Name = "DataSet1" };
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = reportDataForICE;

            localReport.ReportPath = Server.MapPath("~/Reports/BNS/ICEReport.rdlc");
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

       

         

        public ActionResult Delete(long id)
        {
            _ICEManager.Delete(id);
            return RedirectToAction("Index");
        }
    }
}