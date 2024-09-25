using Microsoft.Reporting.WebForms;
using RMS.BLL.IManager;
using RMS.Model;
using RMS.Utility;
using RMS.Web.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace RMS.Web.Controllers
{

    public class DeckInfoController : BaseController
    {
        private IDeckInfoManager _deckInfoManager;
        private IShipInfoManager _shipInfoManager;
        private IControlShipInfoManager _controlShipInfoManager;
        private ICommonCodeManager _commonCodeManager;
        private IDamageMachineriesInfoManager _damageMachineriesInfo;

        public DeckInfoController(IShipInfoManager shipInfoManager, IDeckInfoManager deckInfoManager, IControlShipInfoManager controlShipInfoManager, ICommonCodeManager commonCodeManager, IDamageMachineriesInfoManager damageMachineriesInfoManager)
        {
            _shipInfoManager = shipInfoManager;
            _deckInfoManager = deckInfoManager;
            _controlShipInfoManager = controlShipInfoManager;
            _commonCodeManager = commonCodeManager;
            _damageMachineriesInfo = damageMachineriesInfoManager;
        }
        public ActionResult Index(string ShipId, YearlyReturnViewModel model)
        {

            model.YearlyReturns = _damageMachineriesInfo.GetAllYearlyReturn(2).OrderBy(x => x.ShipId).ToList();

            var YRByCompareInGroups = from mr in model.YearlyReturns
                                      group mr by mr.ShipId into grp
                                      select grp.OrderByDescending(a => a.ShipId).First();

            model.YearlyReturns = YRByCompareInGroups.ToList();


            return View(model);
        }
        public ActionResult HalfYearlyReturnView(string YearId, string ShipId, YearlyReturnViewModel model)
        {
            if (model.YearId > 0 && model.ShipId > 0)
            {
                YearlyReturn yearlyReturn = new YearlyReturn
                {
                    ShipId = Convert.ToInt64(ShipId),
                    YearId = Convert.ToInt32(YearId)
                };
                model.YearlyReturns = _damageMachineriesInfo.GetYearlyReturnByYearAndShipId(yearlyReturn);
                model.ShipName = model.YearlyReturns.FirstOrDefault().ControlShipInfo.ControlName;
                model.ReportYear = Convert.ToString(model.YearlyReturns.FirstOrDefault().YearId);
            }

            return View(model);

        }
        public ActionResult SaveHalfYearlyReturn(YearlyReturnViewModel model)
        {
            YearlyReturn yearlyReturn = new YearlyReturn
            {
                YearlyReturnId = model.YearlyReturnId,
                ShipId = model.ShipId,
                YearId = model.YearId,
                DeskNoId = model.DeskNoId,
                FrameNo = model.FrameNo,
                YearlyReturnType = 2,
                YearlyReturnDetails = model.YearlyReturnDetails

            };

            ResponseModel saveData = _damageMachineriesInfo.SavingYearlyReturn(yearlyReturn);
            return Json(new { Success = 1, SaveData = saveData.Message, ShipId = saveData.ShipId, ex = "" });
        }
        public ActionResult HalfYearlyIndexByShipName(string ShipId, YearlyReturnViewModel model)
        {
            if (ShipId != null)
            {
                long shipId = Convert.ToInt64(ShipId);
                model.YearlyReturns = _damageMachineriesInfo.GetAllYearlyReturnByShipId(Convert.ToInt64(shipId)).ToList();
            }
            return View(model);

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

        public ActionResult AddNew(DeckInfoViewModel model)
        {
            model.ShipInfos = _shipInfoManager.GetAll();
            return View(model);
        }
        [HttpPost]
        public ActionResult Save(DeckInfoViewModel model)
        {
            ResponseModel savedata = _deckInfoManager.Save(model.DeckInfo);
            if (savedata.Message != string.Empty)
            {
                TempData["message"] = savedata.Message;
            }
            return RedirectToAction("Edit", model);
        }

        //public ActionResult SearchByPartial(DeckInfoViewModel model)
        //{
        //    string serachKey = "";
        //    if (model.SearchKey != null)
        //    {
        //        serachKey= model.SearchKey.Trim().ToLower();
        //        model.DeckInfos = _deckInfoManager.GetAll().Where(x=>x.ControlShipInfo.ControlName.ToLower().Contains(serachKey)||x.DechNo.Contains(serachKey)||x.Compartment.Contains(serachKey)||x.Location.Contains(serachKey)).ToList();
        //    }
        //    else
        //    {
        //        model.DeckInfos = _deckInfoManager.GetAll();
        //    }
        //    return View("Index", model);
        //}

        public ActionResult Search(DeckInfoViewModel model)
        {

            string serachKey = "";
            long search = model.S;

            if (model.SearchKey != null)
            {
                serachKey = model.SearchKey.Trim().ToLower();

                model.DeckInfos = _deckInfoManager.GetAll().Where(x => x.ControlShipInfo.ControlName.ToLower().Contains(serachKey.ToLower()) || x.DechNo.ToLower().Contains(serachKey.ToLower()) || x.Compartment.ToLower().Contains(serachKey.ToLower()) || x.Location.ToLower().Contains(serachKey.ToLower()) && x.ReportDateFrom >= model.DateFrom && x.ReportDateTo <= model.DateTo).ToList();
            }
            if (model.S != null)
            {

                model.DeckInfos = search != null
                   ? _deckInfoManager.GetAll()
                       .Where(x => x.CommonCode.CommonCodeID == search).ToList() : _deckInfoManager.GetAll();
            }
            if (model.SearchKey != null && model.S != null)
            {
                serachKey = model.SearchKey.Trim().ToLower();

                model.DeckInfos = _deckInfoManager.GetAll().Where(x => x.ControlShipInfo.ControlName.ToLower().Contains(serachKey.ToLower()) || x.DechNo.ToLower().Contains(serachKey.ToLower()) || x.Compartment.ToLower().Contains(serachKey.ToLower()) || x.Location.ToLower().Contains(serachKey.ToLower()) && x.ReportDateFrom >= model.DateFrom && x.ReportDateTo <= model.DateTo && x.CommonCode.CommonCodeID == search).ToList();
            }
            model.CommonCodes = _commonCodeManager.GetCommonCodeByType("HallStatus");
            return View("Index", model);
        }

        public ActionResult Edit(string id, YearlyReturnViewModel model)
        {
            ModelState.Clear();
            model.controlShipInfos = _controlShipInfoManager.GetAllShipList();
            model.DeskNoList = _commonCodeManager.GetCommonCodeByType("DeckNo").Select(lg => new SelectionList()
            {
                Value = lg.CommonCodeID,
                Text = lg.TypeValue
            }).ToList();
            model.YearlyReturn = _damageMachineriesInfo.GetYearlyReturn(id, model.YearlyReturn) ?? new YearlyReturn();
            model.returnReportYears = _damageMachineriesInfo.returnReportYears();

            return View(model);
        }


        public ActionResult Delete(string id)
        {
            var deleteIndex = _deckInfoManager.Delete(id);
            return RedirectToAction("Index");

        }
        public ActionResult Download(string id, DeckInfoViewModel deckInfo)
        {
            var localReport = new LocalReport();
            var model = new DeckInfoViewModel();

            model.DeckInfos = _deckInfoManager.GetAllByShipId(id);
            var reportDataList = model.DeckInfos.Select(item => new DeckInfo()
            {
                OrgName = PortalContext.CurrentUser.BankName,
                OfficeName = PortalContext.CurrentUser.BranchName,
                Parameter = "",
                ReportName = "Record of Examination of Accessible Compartments of Hull and Structure and Watertight Compartments " + item.ControlShipInfo.ControlName,
                DeckInfoIdentity = item.DeckInfoIdentity,
                HullStatus = item.HullStatus,
                ShipName = item.ControlShipInfo.ControlName,
                ReportDateFrom = item.ReportDateFrom,
                ReportDateTo = item.ReportDateTo,
                DechNo = item.DechNo,
                Compartment = item.Compartment ?? "",
                Location = item.Location ?? "",
                CheckDate = item.CheckDate,
                StatePlates = item.StatePlates ?? "",
                StateFrames = item.StateFrames ?? "",
                StateCement = item.StateCement ?? "",
                StateRivets = item.StateRivets ?? "",
                PaintDescriptio = item.PaintDescriptio ?? "",
                PaintState = item.PaintState ?? "",
                SanctionDisLineS = item.SanctionDisLineS ?? "",
                SanctDisNonRS = item.SanctDisNonRS ?? "",
                SanctDisNonRW = item.SanctDisNonRW ?? "",
                SanctionDisLineW = item.SanctionDisLineW ?? "",
                HeadCoverWT = item.HeadCoverWT ?? "",
                HeadCoverS = item.HeadCoverS ?? "",
                DefectDes = item.DefectDes ?? "",
                DefectActionTaken = item.DefectActionTaken ?? "",
                UserID = PortalContext.CurrentUser.UserName,
                LastUpdate = DateTime.Now
            }).ToList();


            var reportDataSource = new ReportDataSource { Name = "Report" };
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = reportDataList;

            localReport.ReportPath = Server.MapPath("~/Reports/BNS/RecordOfExamination.rdlc");
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