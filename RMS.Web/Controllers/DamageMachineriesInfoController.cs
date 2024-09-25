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
    public class DamageMachineriesInfoController : BaseController
    {
        private readonly IDamageMachineriesInfoManager _damageMachineriesInfo;
        private readonly IShipInfoManager _shipInfoManager;
        public readonly ICommonCodeManager _CommonCodeManager;
        private IControlShipInfoManager _controlShipInfoManager;
        private IMachineryInfoManager _machineryInfoManager;
        private IICEManager _ICEManager;

        public DamageMachineriesInfoController(IControlShipInfoManager controlShipInfoManager, IDamageMachineriesInfoManager damageMachineriesInfo, IShipInfoManager shipInfoManager, ICommonCodeManager CommonCodeManager, IMachineryInfoManager machineryInfoManager, IICEManager ICEManager)
        {
            _damageMachineriesInfo = damageMachineriesInfo;
            _shipInfoManager = shipInfoManager;
            _CommonCodeManager = CommonCodeManager;
            _controlShipInfoManager = controlShipInfoManager;
            _machineryInfoManager = machineryInfoManager;
            _ICEManager = ICEManager;
        }

        public ActionResult QuaterlyReturnIndexByShipName(string ShipId, QuaterlyReturnViewModel model)
        {
            model.quaterlyReturns = _damageMachineriesInfo.GetQuaterlyReturnByReturnByShipId(Convert.ToInt64(ShipId));
            return View(model);
        }

        public ActionResult MonthlyReturnIndex(string ShipId, MonthlyReturnViewModel model)
        {
            if (ShipId != null)
            {
                long shipId = Convert.ToInt64(ShipId);
                model.monthlyReturns = _damageMachineriesInfo.GetMonthlyReturnByShipId(shipId);
            }
            else
            {
                model.monthlyReturns = _damageMachineriesInfo.GetAllMonthlyReturn();

                var MRByCompareInGroups = from mr in model.monthlyReturns
                                          group mr by mr.ShipId into grp
                                          select grp.OrderByDescending(a => a.ShipId).First();
                model.monthlyReturns = MRByCompareInGroups.ToList();
            }
            return View(model);
        }
        public ActionResult MonthlyReturnIndexByShipName(string ShipId, MonthlyReturnViewModel model)
        {
            if (ShipId != null)
            {
                long shipId = Convert.ToInt64(ShipId);
                model.monthlyReturns = _damageMachineriesInfo.GetMonthlyReturnByShipId(shipId);
            }
            else
            {
                model.monthlyReturns = _damageMachineriesInfo.GetMonthlyReturnByShipId(PortalContext.CurrentUser.ShipId);

            }
            return View(model);
        }
        public ActionResult QuaterlyReturnIndex(string ShipId, QuaterlyReturnViewModel model)
        {
            if (ShipId != null)
            {
                long shipId = Convert.ToInt64(ShipId);
                model.quaterlyReturns = _damageMachineriesInfo.GetQuaterlyReturnByReturnByShipId(shipId);
            }
            else
            {
                model.quaterlyReturns = _damageMachineriesInfo.GetAllQuaterlyReturn();

            }

            return View(model);
        }
        public ActionResult YearlyReturnIndexByShipName(string ShipId, YearlyReturnViewModel model)
        {
            if (ShipId != null)
            {
                long shipId = Convert.ToInt64(ShipId);
                model.YearlyReturns = _damageMachineriesInfo.GetAllYearlyReturnByShipId(Convert.ToInt64(shipId)).ToList();
            }
            return View(model);

        }
        public ActionResult YearlyReturnIndex(string ShipId, YearlyReturnViewModel model)
        {


            model.YearlyReturns = _damageMachineriesInfo.GetAllYearlyReturn(1).OrderBy(x => x.ShipId).ToList();

            var YRByCompareInGroups = from mr in model.YearlyReturns
                                      group mr by mr.ShipId into grp
                                      select grp.OrderByDescending(a => a.ShipId).First();

            model.YearlyReturns = YRByCompareInGroups.ToList();
            model.ICEs = _ICEManager.GetAll().OrderByDescending(x => x.ControlShipInfoId).ToList();
            var ICEByCompareInGroups = from ice in model.ICEs
                                       group ice by ice.ControlShipInfoId into grp
                                       select grp.OrderByDescending(a => a.ControlShipInfoId).First();
            model.ICEs = ICEByCompareInGroups.ToList();


            return View(model);
        }

        public ActionResult YearlyReturnEdit(string id, YearlyReturnViewModel model)
        {
            ModelState.Clear();
            model.controlShipInfos = _controlShipInfoManager.GetAllShipList();
            model.DeskNoList = _CommonCodeManager.GetCommonCodeByType("DeckNo").Select(lg => new SelectionList()
            {
                Value = lg.CommonCodeID,
                Text = lg.TypeValue
            }).ToList();
            model.YearlyReturn = _damageMachineriesInfo.GetYearlyReturn(id, model.YearlyReturn) ?? new YearlyReturn();
            model.returnReportYears = _damageMachineriesInfo.returnReportYears();

            return View(model);
        }
        public ActionResult QuaterlyReturnEdit(string id, int viewCount, QuaterlyReturnViewModel model)
        {
            ModelState.Clear();
            model.viewCount = viewCount;
            model.controlShipInfos = _controlShipInfoManager.GetAllShipList();
            model.ShipStatus = _CommonCodeManager.GetCommonCodeByType("ShipStatus").Select(lg => new SelectionList()
            {
                Value = lg.CommonCodeID,
                Text = lg.TypeValue + " " + lg.BTypeValue
            }).ToList();
            model.QuaterlyReturn = _damageMachineriesInfo.GetQuaterlyReturn(id, model.QuaterlyReturn) ?? new QuaterlyReturn();
            if (id != null)
            {
                model.MachinaryList = _machineryInfoManager.GetAll().Where(x => x.ControlShipInfoId == model.QuaterlyReturn.ShipId).Select(lg => new SelectionList()
                {
                    Value = lg.MachineryInfoIdentity,
                    Text = lg.Machinery + "+" + lg.MachinaryBangla
                }).ToList();
            }

            model.MachinaryState = _CommonCodeManager.GetCommonCodeByType("MachinariesState").Select(x => new SelectionList()
            {
                Value = x.CommonCodeID,
                Text = x.TypeValue + x.BTypeValue
            }).ToList();
            model.OilNames = _CommonCodeManager.GetCommonCodeByType("POLType").Select(x => new SelectionList()
            {
                Value = x.CommonCodeID,
                Text = x.TypeValue
            }).ToList();

            model.returnReportNos = _damageMachineriesInfo.returnReportNoByParameter("Quaterly");
            model.returnReportYears = _damageMachineriesInfo.returnReportYears();

            return View(model);
        }


        public ActionResult MonthlyReturnEdit(string id, int viewCount, MonthlyReturnViewModel model)
        {
            ModelState.Clear();
            model.ViewCount = viewCount;
            model.ControlShipInfos = _controlShipInfoManager.GetAllShipList();
            //model.BrControlShipInfos = _controlShipInfoManager.ShipBranchInfo();
            //model.ShipInfos = _shipInfoManager.GetAll();
            model.MonthlyReturn = _damageMachineriesInfo.GetMonthlyReturn(id, model.MonthlyReturn) ?? new MonthlyReturn();
            if (id != null)
            {
                model.MachinaryList = _machineryInfoManager.GetAll().Where(x => x.MachinariesState == CommonConstant.RunningMachinaries && x.ControlShipInfoId == model.MonthlyReturn.ShipId).Select(lg => new SelectionList()
                {
                    Value = lg.MachineryInfoIdentity,
                    Text = lg.Machinery + "+" + lg.MachinaryBangla
                }).ToList();
            }
            model.returnReportNos = _damageMachineriesInfo.returnReportNoByParameter("Monthly");
            model.returnReportYears = _damageMachineriesInfo.returnReportYears();
            model.ShipStatus = _CommonCodeManager.GetCommonCodeByType("ShipStatus").Select(lg => new SelectionList()
            {
                Value = lg.CommonCodeID,
                Text = lg.TypeValue + " " + lg.BTypeValue
            }).ToList();
            model.MachinaryState = _CommonCodeManager.GetCommonCodeByType("MachinariesState").Select(x => new SelectionList()
            {
                Value = x.CommonCodeID,
                Text = x.TypeValue + x.BTypeValue
            }).ToList();
            model.OilNames = _CommonCodeManager.GetCommonCodeByType("POLType").Select(x => new SelectionList()
            {
                Value = x.CommonCodeID,
                Text = x.TypeValue
            }).ToList();

            return View(model);
        }

        public ActionResult MonthlyReturnView(string id, MonthlyReturnViewModel model)
        {
            if ((model.ReportMonthId > 0 && model.ReportYearId > 0 && model.SearchKey != null) || id != null)
            {
                model.machineryInfos = _machineryInfoManager.GetAll();
                List<MonthlyReturn> monthlyReturns = _damageMachineriesInfo.GetSearchMonthlyReturn(model.ReportMonthId, model.ReportYearId, model.SearchKey, id);
                model.defectMachinaries = _damageMachineriesInfo.GetDefectMachinary(monthlyReturns);
                model.machinaryRunningHours = _damageMachineriesInfo.GetMachinaryRunningHours(monthlyReturns);

                var viewmodel = from b in model.machinaryRunningHours
                                join c in model.machineryInfos on b.NoOPSMachineId equals c.MachineryInfoIdentity
                                group b by new { b.NoOPSMachineId, c.MachineryInfoIdentity } into g
                                select new MachinaryRunningHour()
                                {
                                    Machinery = g.FirstOrDefault().MachineryInfo.Machinery,
                                    NoOPSMachineId = g.Key.NoOPSMachineId,
                                    RunningHr = g.Sum(x => x.RunningHr),
                                    RunningMin = g.Sum(x => x.RunningMin)
                                };
                model.machinaryRunningHours = viewmodel.ToList();

                model.polExpenseInfos = _damageMachineriesInfo.GetPOLExpenseInfos(monthlyReturns);
                foreach (var item in model.polExpenseInfos)
                {
                    item.OilTotalPrice = (float)(item.MonthlyUseltrOrUnit * item.OilPerLtRate ?? 0);
                }


                foreach (var item in model.defectMachinaries)
                {
                    item.DefectReasonDetail = item.MachineryInfo.Machinery + ": " + item.DefectReasonDetail;
                }
                foreach (var item in monthlyReturns)
                {
                    model.ReportMonth = item.ReturnReportNo.ReturnSerialNo + "/" + item.ReportYearId;
                    model.ShipName = item.ControlShipInfo.ControlName;
                    model.MonthlyReturnId = item.MonthlyReturnId;
                }
                if (id != null)
                {
                    model.SearchELement = "hide";
                }

            }

            model.ControlShipInfos = _controlShipInfoManager.GetAllShipList().ToList();
            model.returnReportNos = _damageMachineriesInfo.returnReportNoByParameter("Monthly");
            model.returnReportYears = _damageMachineriesInfo.returnReportYears();
            return View(model);

        }
        public ActionResult QuaterlyReturnView(string id, QuaterlyReturnViewModel model)
        {
            if ((model.ReturnReportNoId > 0 && model.ReportYearId > 0 && model.SearchKey != null) || id != null)
            {
                model.machineryInfos = _machineryInfoManager.GetAll();
                model.QuaterlyReturn = _damageMachineriesInfo.GetSearchQuaterlyReturn(model.ReturnReportNoId, model.ReportYearId, model.SearchKey, id);
                if (model.QuaterlyReturn.ReturnReportNo.ReturnSerialNo == "3rd")
                {
                    model.StartDate = "01 July " + model.QuaterlyReturn.ReturnReportYear.Name;
                    model.EndDate = "30 Sep " + model.QuaterlyReturn.ReturnReportYear.Name;
                }

                if (model.QuaterlyReturn != null)
                {
                    model.quaterlyMainEnginesGeneratorsRunningHours = _damageMachineriesInfo.GetQuaterlyMainEnginesGeneratorsRunningHoursByQuaterlyReturnId(model.QuaterlyReturn.QuaterlyReturnId);


                    model.ReportMonth = model.QuaterlyReturn.ReturnReportNo.ReturnSerialNo + "/" + model.QuaterlyReturn.ReturnReportYear.Name;
                    model.ShipName = model.QuaterlyReturn.ControlShipInfo.ControlName;
                    model.QuaterlyReturnId = model.QuaterlyReturn.QuaterlyReturnId;
                }


                if (id != null)
                {
                    model.SearchELement = "hide";
                }

            }

            model.controlShipInfos = _controlShipInfoManager.GetAllShipList().ToList();
            model.returnReportNos = _damageMachineriesInfo.returnReportNoByParameter("Quaterly");
            model.returnReportYears = _damageMachineriesInfo.returnReportYears();
            return View(model);

        }
        public ActionResult YearlyReturnView(string YearId, string ShipId, YearlyReturnViewModel model)
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
        public ActionResult GetMonthlyReturnById(string id)
        {
            ModelState.Clear();
            var model = _damageMachineriesInfo.GetMonthlyReturn(id) ?? new MonthlyReturn();
            return Json(new { result = model }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetYearlyReturnById(string id)
        {
            ModelState.Clear();
            var model = _damageMachineriesInfo.GetYearlyReturn(id) ?? new YearlyReturn();
            return Json(new { result = model }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetQuaterlyReturnById(string id)
        {
            ModelState.Clear();
            var model = _damageMachineriesInfo.GetQuaterlyReturn(id) ?? new QuaterlyReturn();
            return Json(new { result = model }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SaveQuaterlyReturn(QuaterlyReturnViewModel model)
        {
            QuaterlyReturn quaterlyReturn = new QuaterlyReturn
            {
                ShipId = model.ShipId,
                ShipStatusId = model.ShipStatusId,
                ReturnReportNoId = model.ReturnReportNoId,
                ReportYearId = model.ReportYearId,
                QuaterlyReturnId = model.QuaterlyReturnId,
                UndergoingRefit = model.UndergoingRefit,
                TotalNoOfDay = model.TotalNoOfDay,
                Refit = model.Refit,
                InDocking = model.InDocking,
                UnDocking = model.UnDocking,
                MaintancePeriod = model.MaintancePeriod,
                FullPowerTrial = model.FullPowerTrial,
                NonOplReason = model.NonOplReason,
                AtSea = model.AtSea,
                DistanceRun = model.DistanceRun,
                AtNormalNotice = model.AtNormalNotice,
                TimeUnderway = model.TimeUnderway,
                TimeAwaitingOrder = model.TimeAwaitingOrder,
                PercentOfLubOilQuaterlyConsumption = model.PercentOfLubOilQuaterlyConsumption,
                LoadAtHarbourQuaterlyConsumption = model.LoadAtHarbourQuaterlyConsumption,
                LoadAtSeaQuaterlyConsumption = model.LoadAtSeaQuaterlyConsumption,
                MainEngineFuelNameId = model.MainEngineFuelNameId,
                MainEngineFuelQuantity = model.MainEngineFuelQuantity,
                MainEngineLubOilNameId = model.MainEngineLubOilNameId,
                MainEngineLubOilQuantity = model.MainEngineLubOilQuantity,
                DGAndOthersFuelNameId = model.DGAndOthersFuelNameId,
                DGAndOthersFuelQuantity = model.DGAndOthersFuelQuantity,
                OthersLubOilNameId = model.OthersLubOilNameId,
                OthersLubOilQuantity = model.OthersLubOilQuantity,
                DGLubOilNameId = model.DGLubOilNameId,
                DGLubOilQuantity = model.DGLubOilQuantity,
                RefrigerantQuaterlyConsumption = model.RefrigerantQuaterlyConsumption,
                FreshWaterQuaterlyConsumption = model.FreshWaterQuaterlyConsumption,
                MachineryUnderTrialRemarks = model.MachineryUnderTrialRemarks,
                RemarksOfAdministrativeAuthority = model.RemarksOfAdministrativeAuthority,
                QuaterlyMainEnginesGeneratorsRunningHours = model.quaterlyMainEnginesGeneratorsRunningHours,
                QuaterlyReturnEngineerOfficerRemarks = model.QuaterlyReturnEngineerOfficerRemarks
            };

            foreach (var item in quaterlyReturn.QuaterlyMainEnginesGeneratorsRunningHours)
            {
                item.ShipId = quaterlyReturn.ShipId;
            }

            ResponseModel saveData = _damageMachineriesInfo.SavingQuaterlyReturn(quaterlyReturn);

            return Json(new { Success = 1, SaveData = saveData.Message, ShipId = saveData.ShipId, ex = "" });
        }

        [HttpPost]
        public ActionResult SaveMonthlyReturn(MonthlyReturnViewModel model)
        {
            //model.DamageMachineriesInfo.UserId = User.Identity.GetUserName();
            MonthlyReturn monthlyReturn = new MonthlyReturn
            {
                ShipId = model.ShipId,
                ShipStatusId = model.ShipStatusId,
                ReportMonthId = model.ReportMonthId,
                ReportYearId = model.ReportYearId,
                MonthlyReturnId = model.MonthlyReturnId,
                ShipMonthlyRunningHr = model.ShipMonthlyRunningHr,
                ShipMonthlyRunningMin = model.ShipMonthlyRunningMin,
                NonOperationalDuringMonthHr = model.NonOperationalDuringMonthHr,
                NonOperationalDuringMonthMin = model.NonOperationalDuringMonthMin,
                AtSeaDuringThisMonthHr = model.AtSeaDuringThisMonthHr,
                AtSeaDuringThisMonthMin = model.AtSeaDuringThisMonthMin,
                HarbourDuringThisMonthHr = model.HarbourDuringThisMonthHr,
                HarbourDuringThisMonthMin = model.HarbourDuringThisMonthMin,
                FreshWaterTons = model.FreshWaterTons,
                RefrigerantKg = model.RefrigerantKg,
                AtSeaHr = model.AtSeaHr,
                AtSeaMin = model.AtSeaMin,
                DefectMachinaries = model.defectMachinaries,
                POLExpenseInfoes = model.polExpenseInfos,
                MachinaryRunningHours = model.machinaryRunningHours

            };
            foreach (var item in monthlyReturn.DefectMachinaries)
            {
                item.ShipId = monthlyReturn.ShipId;
            }
            foreach (var item in monthlyReturn.MachinaryRunningHours)
            {
                item.ShipId = monthlyReturn.ShipId;
            }
            foreach (var item in monthlyReturn.POLExpenseInfoes)
            {
                item.ShipId = monthlyReturn.ShipId;
            }

            ResponseModel saveData = _damageMachineriesInfo.SavingMonthlyReturn(monthlyReturn);
            return Json(new { Success = 1, SaveData = saveData.Message, ShipId = saveData.ShipId, ex = "" });
        }

        public ActionResult SaveYearlyReturn(YearlyReturnViewModel model)
        {
            YearlyReturn yearlyReturn = new YearlyReturn
            {
                YearlyReturnId = model.YearlyReturnId,
                ShipId = model.ShipId,
                YearId = model.YearId,
                DeskNoId = model.DeskNoId,
                FrameNo = model.FrameNo,
                YearlyReturnType = 1,
                YearlyReturnDetails = model.YearlyReturnDetails

            };

            ResponseModel saveData = _damageMachineriesInfo.SavingYearlyReturn(yearlyReturn);
            return Json(new { Success = 1, SaveData = saveData.Message, ShipId = saveData.ShipId, ex = "" });
        }
        public ActionResult Nabvar(DamageMachineriesInfoViewModel model)
        {
            model.BrControlShipInfos = _controlShipInfoManager.ShipBranchInfo();
            var DMInfo = _damageMachineriesInfo.GetAll();
            var query = (from post in DMInfo join meta in model.BrControlShipInfos on post.ShipId equals meta.ControlShipInfoId select post).ToList();
            model.DamageMachineriesInfos = query;
            var PCalculation = _damageMachineriesInfo.GetPol();
            var query1 = (from post in PCalculation join meta in model.BrControlShipInfos on post.ShipId equals meta.ControlShipInfoId select post).ToList();
            model.PolCalculation = query1;
            var runningH = _damageMachineriesInfo.GetMachineHour();
            var query2 = (from post in runningH join meta in model.BrControlShipInfos on post.ShipId equals meta.ControlShipInfoId select post).ToList();
            model.runninghour = query2;
            return View(model);
        }

        public JsonResult GetMachinariesInformation(long shipId)
        {
            var selectionData = _machineryInfoManager.GetMachinariesInfo(shipId);
            return Json(selectionData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Index(DamageMachineriesInfoViewModel model)
        {
            model.ControlShipInfos = _controlShipInfoManager.AllByControlLevel(0);
            model.DamageMachineriesInfos = _damageMachineriesInfo.GetAll().OrderBy(x => x.LastUpdate).ToList();
            return View(model);
        }
        public ActionResult SearchByKey(DamageMachineriesInfoViewModel model)
        {
            string searchKey = model.SearchKey;
            //model.DamageMachineriesInfos = searchKey != null ? _damageMachineriesInfo.GetAll().Where(x => x.ShipInfo.ShipName.ToLower().Contains(searchKey.ToLower()) || x.Description.Contains(searchKey) || x.MobilityDescription.Contains(searchKey)).ToList() : _damageMachineriesInfo.GetAll().ToList();
            model.DamageMachineriesInfos = searchKey != null ? _damageMachineriesInfo.GetAll().Where(x => x.ControlShipInfo.ControlName.ToLower().Contains(searchKey.ToLower()) && x.ReportDate >= model.DateFrom && x.ReportDate <= model.DateFrom).ToList() : new List<DamageMachineriesInfo>();
            return PartialView("~/Views/DamageMachineriesInfo/_DamageMachineries.cshtml", model);
        }

        public ActionResult RunningStatusUpdate(string id, DamageMachineriesInfoViewModel model)
        {
            long status = 974;
            var oldData = _damageMachineriesInfo.GetShip(id, model.DamageMachineriesInfo);
            var dStatusUpdate = _damageMachineriesInfo.UpdateStatus(id, status);
            var mStatusUpdate = _machineryInfoManager.UpdateStatus(oldData.Description, status);
            return RedirectToAction("Nabvar");
        }
        public ActionResult DamageStatusUpdate(string id, DamageMachineriesInfoViewModel model)
        {
            long status = 983;
            var oldData = _damageMachineriesInfo.GetShip(id, model.DamageMachineriesInfo);
            var dStatusUpdate = _damageMachineriesInfo.UpdateStatus(id, status);
            var mStatusUpdate = _machineryInfoManager.UpdateStatus(oldData.Description, status);
            return RedirectToAction("Nabvar");
        }
        public ActionResult Edit(string id, DamageMachineriesInfoViewModel model)
        {
            ModelState.Clear();
            //model.ControlShipInfos = _controlShipInfoManager.AllByControlLevel(0);
            model.BrControlShipInfos = _controlShipInfoManager.ShipBranchInfo();
            model.ShipInfos = _shipInfoManager.GetAll();
            model.MachinaryList = _machineryInfoManager.GetAll().Where(x => x.MachinariesState == CommonConstant.RunningMachinaries).Select(lg => new SelectionList()
            {
                Value = lg.MachineryInfoIdentity,
                Text = lg.Machinery + "+" + lg.MachinaryBangla
            }).ToList();
            model.MachinaryState = _CommonCodeManager.GetCommonCodeByType("MachinariesState").Select(x => new SelectionList()
            {
                Value = x.CommonCodeID,
                Text = x.TypeValue + x.BTypeValue
            }).ToList();
            var costCentre = _damageMachineriesInfo.GetShip(id, model.DamageMachineriesInfo) ?? new DamageMachineriesInfo();
            model.DamageMachineriesInfo = costCentre;
            return View(model);
        }

        [HttpPost]
        public ActionResult Save(DamageMachineriesInfoViewModel model)
        {
            //model.DamageMachineriesInfo.UserId = User.Identity.GetUserName();
            ResponseModel updateStatus = _machineryInfoManager.UpdateStatus(model.DamageMachineriesInfo.Description, model.DamageMachineriesInfo.MobilityDescription);
            ResponseModel saveData = _damageMachineriesInfo.Saving(model.DamageMachineriesInfo);
            if (saveData.Message != string.Empty)
            {
                TempData["message"] = saveData.Message;
            }
            return RedirectToAction("Edit", model);
        }
        public ActionResult Delete(string id)
        {
            var deleteIndex = _damageMachineriesInfo.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Download(string id1, string DateFrom2)
        {
            long id = Convert.ToInt64(id1);
            DateTime DateFrom1 = Convert.ToDateTime(DateFrom2);
            var localReport = new LocalReport();
            var model = new DamageMachineriesInfoViewModel { DamageMachineriesInfos = _damageMachineriesInfo.GetAllForReport(id, DateFrom1) };

            var reportDataList = model.DamageMachineriesInfos.Select(item => new vwDamageMachinery
            {
                OrgName = "Bangladesh Navy",
                OfficeName = PortalContext.CurrentUser.BankName,
                Parameter = "Ship's Basic Information",
                ReportName = "Machinery Parameters - ",//+ item.ShipName,
                ShipId = item.ShipId,
                ShipName = item.ControlShipInfo.ControlName,
                SerialNo = item.SerialNo,
                //   Description = item.Description ?? "",
                // MobilityDescription = item.MobilityDescription ?? "",
                DamageDate = item.DamageDate,
                ReportDate = item.ReportDate,
                RepairTime = item.RepairTime ?? "",
                Remarks = item.Remarks ?? "",
                MachineSerialNo = item.MachineSerialNo ?? "",
                MachineSide = item.MachineSide ?? "",
                //   MachineName = item.MachineName ?? "",
                Hour = item.Hour ?? "",
                Minute = item.Minute ?? "",
                Duration = item.Duration ?? "",
                MachineRemarks = item.MachineRemarks ?? "",



                //FuelOilName = item.FuelOilName ?? "",
                //StockFuelOil = item.StockFuelOil.ToString() ,
                //FuelConsumption = item.FuelConsumption.ToString(),
                //UnitPrice = item.UnitPrice.ToString(),
                //TotalPrice = item.TotalPrice ?? "",
                UserId = item.UserId,
                LastUpdate = item.LastUpdate
                //Remarks = item.Remarks


            }).ToList();



            //var reportDataList = _damageMachineriesInfo.FindOne(id);
            var reportDataSource = new ReportDataSource { Name = "DamageMachineries" };
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = reportDataList;

            var m = _damageMachineriesInfo.RunningHourReport(id, DateFrom1);
            reportDataSource = new ReportDataSource { Name = "RunningHour" };
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = m;

            var r = _damageMachineriesInfo.GetPolData(id, DateFrom1);

            List<vwDamageMachinery> damagesList = new List<vwDamageMachinery>();
            foreach (var damageMachineriesInfo in r)
            {
                vwDamageMachinery damage = new vwDamageMachinery();
                damage.FuelSerialNo = string.Concat(damageMachineriesInfo.FuelSerialNo.Select(c => (char)('\u09E6' + c - '0')));
                decimal consum = Convert.ToDecimal(damageMachineriesInfo.FuelConsumption);
                decimal UPrice = Convert.ToDecimal(damageMachineriesInfo.UnitPrice);
                decimal t = consum * UPrice;
                damage.TotalPrice = string.Concat(t.ToString().Select(c => (char)('\u09E6' + c - '0')));
                string price = damageMachineriesInfo.UnitPrice.ToString();
                damage.FuelName = _CommonCodeManager.FindOilName(damageMachineriesInfo.FuelOilId);
                damage.UnitPrice = string.Concat(price.Select(c => (char)('\u09E6' + c - '0')));
                damage.FuelConsumption = string.Concat(damageMachineriesInfo.FuelConsumption.ToString().Select(c => (char)('\u09E6' + c - '0')));
                damage.StockFuelOil = string.Concat(damageMachineriesInfo.StockFuelOil.ToString().Select(c => (char)('\u09E6' + c - '0')));
                //damage.GrandTotal = string.Concat(gt.ToString().Select(c => (char)('\u09E6' + c - '0')));
                damagesList.Add(damage);
            }

            reportDataSource = new ReportDataSource { Name = "PolCalculate" };
            localReport.DataSources.Add(reportDataSource);
            reportDataSource.Value = damagesList;

            localReport.ReportPath = Server.MapPath("~/Reports/BNS/DamageMachineriesInformation.rdlc");
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
        // verify Damage machinaries
        public ActionResult VerifyMachinariesData(DamageMachineriesInfoViewModel model)
        {
            model.VerifyType =
                _CommonCodeManager.GetCommonCodeByType("VerifyUserSearchCategory").Select(x => new SelectionList
                {
                    Value = x.CommonCodeID,
                    Text = x.TypeValue
                }).ToList();
            return View(model);
        }

        public ActionResult VerifyMachinariesSearch(int? VerifyType, DateTime? DateFrom, DateTime? DateTo, DamageMachineriesInfoViewModel model)
        {

            //string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();

            model.DamageMachineriesInfos = _damageMachineriesInfo.UserWiseData(VerifyType);
            if (model.DamageMachineriesInfos.Any())
            {
                model.DamageMachineriesInfos = DateFrom != null && DateTo != null
                    ? model.DamageMachineriesInfos.Where(x => x.EntryDate >= DateFrom && x.EntryDate <= DateTo).ToList()
                    : model.DamageMachineriesInfos;
            }
            // Bind Model
            List<vwDamageMachinaries> ListDataModel = new List<vwDamageMachinaries>();
            foreach (var item in model.DamageMachineriesInfos)
            {
                vwDamageMachinaries bindingModel = new vwDamageMachinaries();
                bindingModel.MachineriesDescriptionIdentity = item.MachineriesDescriptionIdentity;
                bindingModel.ShipId = item.MachineriesDescriptionIdentity;
                bindingModel.SerialNo = item.SerialNo;
                bindingModel.DamageDate = item.DamageDate;
                bindingModel.ReportDate = item.ReportDate;
                bindingModel.RepairTime = item.RepairTime;
                bindingModel.Remarks = item.Remarks;
                bindingModel.MachineSerialNo = item.MachineSerialNo;
                bindingModel.MachineSide = item.MachineSide;
                bindingModel.Hour = item.Hour;
                bindingModel.Minute = item.Minute;
                bindingModel.Duration = item.Duration;
                bindingModel.MachineRemarks = item.MachineRemarks;
                bindingModel.UserId = item.UserId;
                bindingModel.LastUpdate = item.LastUpdate;
                bindingModel.EntryDate = item.EntryDate;
                bindingModel.UpdatedBy = item.UpdatedBy;
                bindingModel.IsVerified = (bool)item.IsVerified;
                bindingModel.VerifiedBy = item.VerifiedBy;
                bindingModel.VerifiedDate = item.VerifiedDate;
                bindingModel.ShipName = _controlShipInfoManager.GetShipName(item.ShipId);
                if (item.Description != null)
                    bindingModel.MachinaryName = _machineryInfoManager.GetMashineName(item.Description);
                if (item.MobilityDescription != null)
                    bindingModel.Status = _CommonCodeManager.GetCommonName(item.MobilityDescription);
                if (item.MachineName != null)
                    bindingModel.MachineName2 = _machineryInfoManager.GetMashineName(item.MachineName);
                ListDataModel.Add(bindingModel);
            }
            model.VwDamageMachinarieses = ListDataModel;
            model.VerifyType =
                _CommonCodeManager.GetCommonCodeByType("VerifyUserSearchCategory").Select(x => new SelectionList
                {
                    Value = x.CommonCodeID,
                    Text = x.TypeValue
                }).ToList();

            return View("VerifyMachinariesData", model);
        }

        public ActionResult StatusUpdate(DamageMachineriesInfoViewModel model)
        {

            ResponseModel verifiedStatusUpdate =
                _damageMachineriesInfo.VerifiedStatusUpdate(model.VwDamageMachinarieses);
            if (verifiedStatusUpdate.Message != string.Empty)
            {
                model.Message = verifiedStatusUpdate.Message;
                model.IsSucceed = 1;
            }
            else
            {
                model.Message = "Status Not Updated";
            }
            model.VerifyType =
                _CommonCodeManager.GetCommonCodeByType("VerifyUserSearchCategory").Select(x => new SelectionList
                {
                    Value = x.CommonCodeID,
                    Text = x.TypeValue
                }).ToList();

            return View("VerifyMachinariesData", model);
        }

        public ActionResult MonthlyReturnAction(string id, DamageMachineriesInfoViewModel model)
        {
            TempData["message"] = _damageMachineriesInfo.MonthlyReturnActionStatus(id);
            return RedirectToAction("MonthlyReturnView", new { id = id });
        }
    }
}