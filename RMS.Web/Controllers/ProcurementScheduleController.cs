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
    public class ProcurementScheduleController : BaseController
    {
        private IProcurementScheduleManager _ProcurementScheduleManager;
        private IShipInfoManager _shipInfoManager;
        private ICommonCodeManager _commonCodeManager;
        //private IMachineryRunningManager _machineryRunning;
        //private IFuelConsumptionManager _fuelConsumptionManager;
        //private IFuelConsumptions1_ResultManager _fuelConsumption1_ResultManager;



        public ProcurementScheduleController(IProcurementScheduleManager ProcurementScheduleManager, IShipInfoManager shipInfoManager, ICommonCodeManager commonCodeManager)
        {
            _ProcurementScheduleManager = ProcurementScheduleManager;
            _shipInfoManager = shipInfoManager;
            _commonCodeManager = commonCodeManager;
            //_machineryRunning = machineryRunning;
            //_fuelConsumptionManager = fuelConsumptionManager;
            //_fuelConsumption1_ResultManager= new FuelConsumptions1_ResultManager();
        }
        public ActionResult Index(ProcurementScheduleViewModel model)
        {
            //model.ShipMovements = _shipMovementManager.GetAll();
            model.ProcurementSchedules = _ProcurementScheduleManager.GetAll();
            return View(model);
        }

        public ActionResult Save(ProcurementScheduleViewModel model)
        {
            ResponseModel savedata = _ProcurementScheduleManager.Save(model.ProcurementSchedule);
          if (savedata.Message != string.Empty)
          {
              TempData["message"] = savedata.Message;
          }
          return RedirectToAction("Edit", model);
        }

        public ActionResult Edit(string id, ProcurementScheduleViewModel model)
        {
            model.Common = _commonCodeManager.GetCommonCodeByType("ProcurementType");
            model.ProcurementSchedule = _ProcurementScheduleManager.GetProcurmentmentById(Convert.ToInt64(id)) ?? new ProcurementSchedule();
            if (model.ProcurementSchedule.DGDPReTender != null || model.ProcurementSchedule.DTSReTender != null || model.ProcurementSchedule.QuotRecReTender != null || model.ProcurementSchedule.NSSDReTender != null || model.ProcurementSchedule.TenderOpenReTender != null)
            {
                model.DefaultCheckboxCheck = 1;
            }
            else
            {
                model.DefaultCheckboxCheck = 0;
            }
            
            return View(model);
        }

        public ActionResult Delete(string id)
        {
            var deletedata = _ProcurementScheduleManager.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Search(ProcurementScheduleViewModel model)
        {

            string searchkey = model.SearchKey;
            model.ProcurementSchedules = searchkey != null
                ? _ProcurementScheduleManager.GetAll()
                    //.Where(x => x.sh.ShipName.ToLower().Contains(searchkey.ToLower()))
                    .ToList()
                : _ProcurementScheduleManager.GetAll();
            return View("Index", model);
        }


        public ActionResult Download(string id, string dateFrom, string dateTo)
        {

         
            dateFrom = "10 jan 2016";
            dateTo = "10 jan 2017";
           
             var localReport = new LocalReport();
             var reportData = new List<vwProcurementSchedule>();
            var list = _ProcurementScheduleManager.GetAll();
            foreach (var item in list)
            {
                var oldData = new vwProcurementSchedule
                {
                    //OrgName = PortalContext.CurrentUser.BankName,
                    //OfficeName = PortalContext.CurrentUser.BankName,
                    //Parameter = "Ship's Information",
                    //ReportName = "Machinery Parameters - ", //+ item.ShipName,
                    ProcurementId = item.ProcurementId,
                    ProcurementType = item.ProcurementType,
                    Description = item.Description ?? "",
                    Qty = item.Qty,
                    proType = item.CommonCode.TypeValue,
                    YearFrom = Convert.ToDateTime(dateFrom).Year,
                    YearTo = Convert.ToDateTime(dateTo).Year,
                    EstTotalPrice = item.EstTotalPrice,
                    Currency = item.Currency,
                    AIPReceived = item.AIPReceived,
                    SpecSentToDTS = item.SpecSentToDTS,
                    SpecSentToNSSD = item.SpecSentToNSSD,
                    SpecSentToDGDP = item.SpecSentToDGDP,
                    TenderOpened = item.TenderOpened,
                    QuatationRec = item.QuatationRec,
                    AccepSentToDTS = item.AccepSentToDTS,
                    AccepSentToNSSD = item.AccepSentToNSSD,
                    AccepSentToDGDP = item.AccepSentToDGDP,
                    AccepSentToAFD = item.AccepSentToAFD,
                    ApprovedByAFD = item.ApprovedByAFD,
                    ConttractSigned = item.ConttractSigned,
                    Taka = item.Taka,
                    DTSReTender = item.DTSReTender,
                    DGDPReTender = item.DGDPReTender,
                    NSSDReTender = item.NSSDReTender,
                    TenderOpenReTender = item.TenderOpenReTender,
                    QuotRecReTender = item.QuotRecReTender,
                    Remark = item.Remark,
                    UserId = PortalContext.CurrentUser.UserName,
                    LastUpdate = DateTime.Now,

                };
                reportData.Add(oldData);
            }
            var reportDataSource = new ReportDataSource { Name = "DataSet1" };
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = reportData;

            

            localReport.ReportPath = Server.MapPath("~/Reports/BNS/procurement.rdlc");
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