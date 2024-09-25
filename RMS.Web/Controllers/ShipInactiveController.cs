using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web.Helpers;
using System.Web.Mvc;
using DocumentFormat.OpenXml.Office.CustomUI;
using Microsoft.Reporting.WebForms;
using RMS.BLL.IManager;
using RMS.BLL.Manager;
using RMS.Model;
using RMS.Utility;
using RMS.Web.Models.ViewModel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections.Generic;
using Microsoft.Ajax.Utilities;

namespace RMS.Web.Controllers
{
    public class ShipInactiveController : BaseController
    {
        private IShipInactiveManager _shipInactiveManager;
        private IShipInfoManager _shipInfoManager;
        private IControlShipInfoManager _controlShipInfoManager;
        private ICommonCodeManager _commonCodeManager;
        private IMessageInfoManager _messageInfoManager;
        private readonly IMachineryInfoManager _machineryInfoManager;




        public ShipInactiveController(IShipInactiveManager ShipInactiveManager, IShipInfoManager shipInfoManager, IControlShipInfoManager controlShipInfoManager, ICommonCodeManager commonCodeManager,IMessageInfoManager messageInfoManager, IMachineryInfoManager machineryInfoManager)
        {
            _shipInactiveManager = ShipInactiveManager;
            _shipInfoManager = shipInfoManager;
            _controlShipInfoManager = controlShipInfoManager;
            _commonCodeManager = commonCodeManager;
            _messageInfoManager = messageInfoManager;
            _machineryInfoManager = machineryInfoManager;
        }
        public ActionResult Index(string id, ShipInactiveViewModel model)
        {
            //model.ShipMovements = _shipMovementManager.GetAll();
            //model.OrgCommonCodes = _commonCodeManager.GetCommonCodeByType("organization");
            List<ShipInactive> shipI = new List<ShipInactive>();
            model.BrControlShipInfoes = _controlShipInfoManager.ShipBranchInfo();
            if (id != null)
            {
                long ShipId = Convert.ToInt64(id);
                shipI = _shipInactiveManager.GetAll().Where(x=>x.ControlShipInfoIdentity==ShipId).OrderBy(x => x.IsCompeleted).ToList();
            }
            else
            {
                shipI = _shipInactiveManager.GetAll().OrderBy(x => x.IsCompeleted).ToList();
            }

            var query = (from post in shipI join meta in model.BrControlShipInfoes on post.ControlShipInfoIdentity equals meta.ControlShipInfoId select post).ToList();
            model.ShipInactives = query;
            return View(model);
        }
        public ActionResult ShipInactiveUpdate(string id, ShipInactiveViewModel model)
        {
            model.ShipInactive = _shipInactiveManager.GetShipInactiveById(Convert.ToInt64(id));
            model.ShipInactive.IsCompeleted = true;
            ResponseModel StatusUpdate =_controlShipInfoManager.ShipStatusUpdate(model.ShipInactive.ControlShipInfoIdentity, 941);
            ResponseModel savedata = null;
            savedata = _shipInactiveManager.Save(model.ShipInactive);
            int updateStatus = _shipInactiveManager.ShipStatusUpdate(Convert.ToInt64(id));
            if (updateStatus > 0)
            {
                TempData["message"] = "Updated successfully";
            }

            return RedirectToAction("Index");
        }
        public ActionResult ReferenceCheck(ShipInactiveViewModel model)
        {

            var value = model.ShipInactive.Reference;

            //var Checked = _messageInfoManager.CheckReference(value);

            //bool result = Checked.Any() ? true : false;

            return Json(true);

        }
        public ActionResult Save(ShipInactiveViewModel model)
        {
            ResponseModel MachineStatusUpdate = _controlShipInfoManager.ShipStatusUpdate(model.ShipInactive.ControlShipInfoIdentity, model.ShipInactive.ShipStatus);

            ResponseModel savedata = _shipInactiveManager.Save(model.ShipInactive);
          if (savedata.Message != string.Empty)
          {
              TempData["message"] = savedata.Message;
          }
          return View("Edit", model);
        }
        public JsonResult GetDistrictByBankCode(string bankCode)
        {
            var districtList = _controlShipInfoManager.GetDistrictList1(bankCode);
            return Json(districtList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetBranchNameByDistrict(string districtCode)
        {
            var brnchList = _controlShipInfoManager.GetBranchList(districtCode);
            return Json(brnchList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(string id, ShipInactiveViewModel model)
        {
            model.Command = _commonCodeManager.GetCommonCodeByType("organization");
            model.Machineries = _machineryInfoManager.GetAll().Select(lg => new MachineryInfo()
            {
                MachineryInfoIdentity = lg.MachineryInfoIdentity,
                Machinery = lg.Machinery
            }).DistinctBy(x=>x.Machinery).ToList();
            model.ShipSeclevel = _controlShipInfoManager.GetBankList();
            model.ShipStatus = _commonCodeManager.GetCommonCodeByType("ShipStatus").Select(lg => new SelectionList()
            {
                Value = lg.CommonCodeID,
                Text = lg.TypeValue + " " + lg.BTypeValue
            }).ToList();
            //model.ShipInfos = _controlShipInfoManager.AllByControlLevel(0);
            model.BrControlShipInfoes = _controlShipInfoManager.ShipBranchInfo();
            model.ShipInactive = _shipInactiveManager.GetShipInactiveById(Convert.ToInt64(id)) ?? new ShipInactive();
            return View(model);
        }

        public ActionResult Delete(string id)
        {
            var deletedata = _shipInactiveManager.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Search(ShipInactiveViewModel model)
        {
         
            long searchkey = model.SearchKey;
            model.ShipInactives = searchkey != null
                ? _shipInactiveManager.GetAll()
                    .Where(x => x.ControlShipInfoIdentity == searchkey).ToList() : _shipInactiveManager.GetAll();
            //model.OrgCommonCodes = _commonCodeManager.GetCommonCodeByType("organization");
            model.ShipInfos = _controlShipInfoManager.AllByControlLevel(0);
            model.BrControlShipInfoes = _controlShipInfoManager.ShipBranchInfo();
            return View("Index",model);
        }


        public ActionResult Download(long? id)
        {
            var localReport = new LocalReport();
            var model = new ShipInactiveViewModel { ShipInactives = _shipInactiveManager.GetAll().Where(x =>x.IsCompeleted == true).ToList()};

            var reportDataList = model.ShipInactives.Select(item => new vwShipInactive()
            {
                OrgName = "Bangladesh Navy",
                OfficeName = PortalContext.CurrentUser.BankName,
                Parameter = "Ship's Information",
                ReportName = "Work Status Done",
                SInactiveIdentity = item.SInactiveIdentity,
                ControlName = item.ControlShipInfo.ControlName,
                todate = DateTime.Now.Date,
                CrashDetails=item.CrashDetails,
                InactiveDate=item.InactiveDate,
                TakenStap=item.TakenStap,
                Reference=item.Reference,
               // Command = _commonCodeManager.GetTypeValue(item.Command).TypeValue,
                UserID = PortalContext.CurrentUser.UserName,
                LastUpdate = DateTime.Now
                
            }).ToList();


            var reportDataSource = new ReportDataSource { Name = "DataSet1" };
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = reportDataList;

            //var FuelConsumption = _shipInactiveManager.FindOne(id);
            //var reportDataSource1 = new ReportDataSource { Name = "DataSet1" };
            //localReport.DataSources.Add(reportDataSource1);
            //reportDataSource1.Value = FuelConsumption;


            localReport.ReportPath = Server.MapPath("~/Reports/BNS/ShipInactive(done).rdlc");
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
        public ActionResult NotDone(long? id)
        {
            var localReport = new LocalReport();
            var model = new ShipInactiveViewModel { ShipInactives = _shipInactiveManager.GetAll().Where(x => x.IsCompeleted != true).ToList() };

            var reportDataList = model.ShipInactives.Select(item => new vwShipInactive()
            {
                OrgName = "Bangladesh Navy",
                OfficeName = PortalContext.CurrentUser.BankName,
                Parameter = "Ship's Information",
                ReportName = "Work Status Not Done",
                SInactiveIdentity = item.SInactiveIdentity,
                ControlName = item.ControlShipInfo.ControlName,
                ControlShipInfoId =item.ControlShipInfo.ControlShipInfoId,
                todate = DateTime.Now.Date,
                CrashDetails = item.CrashDetails,
                InactiveDate = item.InactiveDate,
                TakenStap = item.TakenStap,
                Reference = item.Reference,
               // Command = _commonCodeManager.GetTypeValue(item.Command).TypeValue,
                UserID = PortalContext.CurrentUser.UserName,
                LastUpdate = DateTime.Now

            }).ToList();


            var reportDataSource = new ReportDataSource { Name = "DataSet1" };
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = reportDataList;


            localReport.ReportPath = Server.MapPath("~/Reports/BNS/ShipInactive(Undone).rdlc");
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