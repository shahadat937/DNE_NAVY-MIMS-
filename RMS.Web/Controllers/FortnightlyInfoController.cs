using Microsoft.Reporting.WinForms;
using RMS.BLL.IManager;
using RMS.Model;
using RMS.Utility;
using RMS.Web.Models.ViewModel;
using System;
using System.Linq;
using System.Web.Mvc;

namespace RMS.Web.Controllers
{
    [Authorize]
    public class FortnightlyInfoController : BaseController
    {
        //
        // GET: /FortnightlyInfo/
        private readonly IFortnightlyInfoManager _fortnightlyInfoManager;
        private ICommonCodeManager _commonCodeManager;
        private readonly IShipInfoManager _shipInfoManager;
        private readonly IRefitDockingScheduleManager _refitDockingScheduleManager;
        private IControlShipInfoManager _controlShipInfoManager;
        public FortnightlyInfoController(IFortnightlyInfoManager fortnightlyInfoManager, ICommonCodeManager commonCodeManager, IShipInfoManager shipInfoManager, IRefitDockingScheduleManager refitDockingScheduleManager, IControlShipInfoManager controlShipInfoManager)
        {
            _fortnightlyInfoManager = fortnightlyInfoManager;
            _commonCodeManager = commonCodeManager;
            _shipInfoManager = shipInfoManager;
            _refitDockingScheduleManager = refitDockingScheduleManager;
            _controlShipInfoManager = controlShipInfoManager;
        }
        public ActionResult Index(FortnightlyInfoViewModel model)
        {
            model.BrControlShipInfos = _controlShipInfoManager.ShipBranchInfo();
            var FuelConsu = _fortnightlyInfoManager.GetAll();
            var query = (from post in FuelConsu join meta in model.BrControlShipInfos on post.ControlShipIdentity equals meta.ControlShipInfoId select post).ToList();
            model.FortnightlyInfos = query;
           
          //  model.FortnightlyInfos = _fortnightlyInfoManager.GetAll();
            return View(model);
        }
        public ActionResult Create(string id, FortnightlyInfoViewModel model)
        {
            ModelState.Clear();
            //model.ControlShipInfos = _controlShipInfoManager.AllByControlLevel(0);
            model.BrControlShipInfos = _controlShipInfoManager.ShipBranchInfo();
            model.Sections = _commonCodeManager.GetCommonCodeByType("SECTION");
            model.TypeOfWork = _commonCodeManager.GetCommonCodeByType("TypeOfWork");
            model.DrTypeList = _commonCodeManager.GetCommonCodeByType("DrType").Select(x => new SelectionList()
            {
                Value = x.CommonCodeID,
                Text = x.TypeValue
            }).ToList();
            model.DocketList = _commonCodeManager.GetCommonCodeByType("Docked").Select(x => new SelectionList()
            {
                Value = x.CommonCodeID,
                Text = x.TypeValue
            }).ToList();
            model.ShipInfos = _shipInfoManager.GetAll();
            model.FortnightlyInfo = _fortnightlyInfoManager.GetFortnightlyInfoById(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult Update(FortnightlyInfoViewModel model)
        {
            // var statusUpdate = _fortnightlyInfoManager.UpdateInfo(id, TwentyFive, Fifty, SeventyFive, Complete, Remarks);
            var UpdateStatus = _fortnightlyInfoManager.UpdateStatus(model.FortnightlyInfos);
            return RedirectToAction("Index");
        }
        public ActionResult SearchFortnightlyInfo(FortnightlyInfoViewModel model)
        {
            string searchKey = "";
            if (model.SearchKey != null)
            {
                searchKey = model.SearchKey.Trim().ToLower();

            }
            model.FortnightlyInfos = searchKey != "" ? _fortnightlyInfoManager.GetAll().Where(x => x.ControlShipInfo.ControlName.ToLower().Contains(searchKey) || x.CommonCode.TypeValue.ToLower().Contains(searchKey)).ToList() : _fortnightlyInfoManager.GetAll();
            return View("Index", model);
        }

        [HttpPost]
        public ActionResult Create(FortnightlyInfoViewModel model)
        {
            ResponseModel response = _fortnightlyInfoManager.Saving(model.FortnightlyInfo);
            if (response.Message != string.Empty)
            {
                TempData["message"] = response.Message;
            }
            return RedirectToAction("Create", model);
        }
        public ActionResult Delete(string id)
        {
            var deleteIndex = _fortnightlyInfoManager.Delete(id);
            return RedirectToAction("Index");
        }
        public ActionResult Download(DateTime? startDate, DateTime? endDate)
        {

            var localReport = new LocalReport();
            var model = new FortnightlyInfoViewModel { FortnightlyInfos = _fortnightlyInfoManager.GetAll() };

            var reportDataList = model.FortnightlyInfos.Select(item => new vwFortnightlyInfo()
            {
                // CommonCodeID=item.CommonCodeID??0,
                // ShipInfoIdentity=item.ShipInfoIdentity??0,
                // TotalWorkNo = item.TotalWorkNo ?? "নাই",
                // Progress25Percent = item.Progress25Percent ?? "নাই",
                // Progress50Percent = item.Progress50Percent ?? "নাই",
                // Progress75Percent = item.Progress75Percent ?? "নাই",
                // Progress100Percent = item.Progress100Percent ?? "নাই",
                // UnfishWork = item.UnfishWork ?? "নাই",
                //Remarks=item.Remarks??""

                OrgName = "Bangladesh Navy",
                OfficeName = PortalContext.CurrentUser.BankName,
                Parameter = "Ship's Information",
                ReportName = "Machinery Parameters - ",
                //FortnightlyId = item.FortnightlyId,
                // CommonCodeID = item.CommonCodeID,
                //TypeValue = item.CommonCode.TypeValue,
                // ShipInfoIdentity = item.ShipInfoIdentity,
                // FortnightlyName = item.FortnightlyName,
                // RefitDockSelectedStartDate = item.RefitDockSelectedStartDate,
                // RefitDockSelectedEndDate = item.RefitDockSelectedEndDate,
                // RefitStartDate = item.RefitStartDate,
                // RefitEndDate = item.RefitEndDate,
                // DockingDate = item.DockingDate,
                // UnDockingDate = item.UnDockingDate,
                // ProgressTime = item.ProgressTime,
                // TotalWorkProgress = item.TotalWorkProgress,
                //todate = DateTime.Now.Date,
                //ShipName = item.ControlShipInfo.ControlName,


                // TotalWorkNo = item.TotalWorkNo,
                // Progress25Percent = item.Progress25Percent,
                // Progress50Percent = item.Progress50Percent,
                // Progress75Percent = item.Progress75Percent,
                // Progress100Percent = item.Progress100Percent,
                // UnfishWork = item.UnfishWork,


                //smProgress25Percent = (item.Progress25Percent.Split(',').Length.ToString()),
                //smProgress50Percent = (item.Progress50Percent.Split(',').Length.ToString()),
                //smProgress75Percent = (item.Progress75Percent.Split(',').Length.ToString()),
                //smProgress100Percent = (item.Progress100Percent.Split(',').Length.ToString()),
                //smUnfishWork = item.UnfishWork.Split(',').Length.ToString(),


                //sumProgress25Percent = (item.Progress25Percent.Split(',').Length.ToString()).Sum(x => Convert.ToInt32(x)),
                //sumProgress50Percent = (item.Progress50Percent.Split(',').Length.ToString()).Sum(x => Convert.ToInt32(x)),
                // sumProgress75Percent = (item.Progress75Percent.Split(',').Length.ToString()).Sum(x => Convert.ToInt32(x)),
                // sumProgress100Percent = (item.Progress100Percent.Split(',').Length.ToString()).Sum(x => Convert.ToInt32(x)),
                //sumUnfishWork = (item.UnfishWork.Split(',').Length.ToString()).Sum(x => Convert.ToInt32(x)),
                Remarks = item.Remarks,
                LastUpdate = DateTime.Now,
                UserID = PortalContext.CurrentUser.UserName

            }).ToList();

            var reportDataSource = new ReportDataSource { Name = "DataSet1" };
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = reportDataList;

            localReport.ReportPath = Server.MapPath("~/Reports/BNS/navy1.rdlc");
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

