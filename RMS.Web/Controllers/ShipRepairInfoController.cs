using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using RMS.BLL.IManager;
using RMS.BLL.Manager;
using RMS.Model;
using RMS.Utility;
using RMS.Web.Models.ViewModel;

namespace RMS.Web.Controllers
{
    public class ShipRepairInfoController : BaseController
    {
        private readonly IShipRepairInfoManager _shipRepairInfoManager;
        private readonly IShipInfoManager _shipInfoManager;
        private IControlShipInfoManager _controlShipInfoManager;


        public ShipRepairInfoController(IControlShipInfoManager controlShipInfoManager)
        {
            _shipRepairInfoManager = new ShipRepairInfoManager();
            _shipInfoManager =new ShipInfoManager();
            _controlShipInfoManager = controlShipInfoManager;
        }
        public ActionResult Index(ShipRepairViewModel model)
        {
            model.BrControlShipInfos = _controlShipInfoManager.ShipBranchInfo();
            var SRInfo = _shipRepairInfoManager.GetAll();
            var query = (from post in SRInfo join meta in model.BrControlShipInfos on post.ShipInfoIdentity equals meta.ControlShipInfoId select post).ToList();
            model.ShipRepairInfoes = query;
            return View(model);
        }
        public ActionResult RepairCostDetails(string id, ShipRepairViewModel model)
        {
            var costCentre = _shipRepairInfoManager.RepairCostDetailsId(id) ?? new ShipRepairInfo();
            model.ShipRepairInfo = costCentre;
            return View(model);
        }
        public ActionResult Searchbykey(ShipRepairViewModel model)
        {
            string searchkey = "";
            if (model.SearchKey != null)
            {
                searchkey = model.SearchKey.ToLower();
                model.ShipRepairInfoes = _shipRepairInfoManager.GetAll().Where(x => x.ControlShipInfo.ControlName.ToLower().Contains(searchkey)).ToList();
            }
            else
            {
                model.ShipRepairInfoes = _shipRepairInfoManager.GetAll();
            }

            return View("Index", model);
        }

        [HttpGet]
        public ActionResult Edit(string id, ShipRepairViewModel model)
        {
            ModelState.Clear();
            model.ShipInfos = _shipInfoManager.GetAll();
            //model.ControlShipInfos = _controlShipInfoManager.AllByControlLevel(0);
            model.BrControlShipInfos = _controlShipInfoManager.ShipBranchInfo();
            var costCentre = _shipRepairInfoManager.GetShipId(id) ?? new ShipRepairInfo();
            model.ShipRepairInfo = costCentre;
            return View(model);
        }

        [HttpPost]
        public ActionResult Save(ShipRepairViewModel model)
        {
            //model.ShipPower.UserId = PortalUser.UserName;
            ResponseModel savedata = _shipRepairInfoManager.SaveData(model.ShipRepairInfo);
            if (savedata.Message != string.Empty)
            {
                TempData["message"] = savedata.Message;
            }
            return RedirectToAction("Edit", model);
        }
        public ActionResult Delete(string id)
        {
            var deleteIndex = _shipRepairInfoManager.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Download(string id)
        {
            var localReport = new LocalReport();
            var model = new ShipRepairViewModel { ShipRepairInfoes = _shipRepairInfoManager.GetAll() };
            var reportDataList = model.ShipRepairInfoes.Select(item => new vwShipRepairInfo()
            {

                OrgName = "Bangladesh Navy",
                OfficeName = PortalContext.CurrentUser.BankName,
                Parameter = "Ship's Basic Information",
                ReportName = "Machinery Parameters - ",
                 ShipName= item.ControlShipInfo.ControlName ?? "",
                DockingPlace = item.DockingPlace ?? "",
                FinantialYear = item.FinantialYear,
                DockingStart = item.DockingStart,
                DockingEnd = item.DockingEnd,
                RepairCost = item.RepairCost ?? 0,
               
            }).ToList();



            var reportDataSource = new ReportDataSource { Name = "DataSet1" };
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = reportDataList;

            localReport.ReportPath = Server.MapPath("~/Reports/BNS/ShipRepairCost.rdlc");
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