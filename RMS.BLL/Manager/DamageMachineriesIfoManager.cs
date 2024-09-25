using RMS.BLL.IManager;
using RMS.DAL;
using RMS.DAL.IRepository;
using RMS.DAL.Repository;
using RMS.Model;
using RMS.Utility;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace RMS.BLL.Manager
{
    public class DamageMachineriesIfoManager : BaseManager, IDamageMachineriesInfoManager
    {
        private readonly IDamageMachineriesInfoRepository _damageMachineriesInfoRepository;
        private readonly IMonthlyReturnRepository _monthlyReturnRepository;
        private readonly IDefectMachinaryRepository _defectMachinaryRepository;
        private readonly IControlShipInfoRepository _controlShipInfoRepository;
        private readonly IMachinaryRunningHourRepository _machinaryRunningHourRepository;
        private readonly IMachineryInfoRepository _machineryInfoRepository;
        private readonly IPOLExpenseInfoRepository _polExpenseInfoRepository;
        private readonly IQuaterlyReturnRepository _quaterlyReturnRepository;
        private readonly IQuaterlyConsumptionRepository _quaterlyConsumptionRepository;
        private readonly IQuaterlyMainEnginesGeneratorsRunningHourRepository _quaterlyMainEnginesGeneratorsRunningHourRepository;
        private readonly IQuaterlyReturnEngineerOfficerRemarksRepository _quaterlyReturnEngineerOfficerRemarksRepository;
        private readonly IYearlyReturnRepository _yearlyReturnRepository;
        private readonly IYearlyReturnDetailRepository _yearlyReturnDetailRepository;
        RM_AGBEntities db = new RM_AGBEntities();


        public DamageMachineriesIfoManager()
        {
            _damageMachineriesInfoRepository = new DamageMachineriesInfoRepository(Instance.Context);
            _monthlyReturnRepository = new MonthlyReturnRepository(Instance.Context);
            _defectMachinaryRepository = new DefectMachinaryRepository(Instance.Context);
            _controlShipInfoRepository = new ControlShipInfoRepository(Instance.Context);
            _machinaryRunningHourRepository = new MachinaryRunningHourRepository(Instance.Context);
            _machineryInfoRepository = new MachineryInfoRepository(Instance.Context);
            _polExpenseInfoRepository = new POLExpenseInfoRepository(Instance.Context);
            _quaterlyReturnRepository = new QuaterlyReturnRepository(Instance.Context);
            _quaterlyConsumptionRepository = new QuaterlyConsumptionRepository(Instance.Context);
            _quaterlyMainEnginesGeneratorsRunningHourRepository = new QuaterlyMainEnginesGeneratorsRunningHourRepository(Instance.Context);
            _quaterlyReturnEngineerOfficerRemarksRepository = new QuaterlyReturnEngineerOfficerRemarksRepository(Instance.Context);
            _yearlyReturnRepository = new YearlyReturnRepository(Instance.Context);
            _yearlyReturnDetailRepository = new YearlyReturnDetailRepository(Instance.Context);
        }

        public List<DamageMachineriesInfo> GetAll()
        {
            var value = _damageMachineriesInfoRepository.All().Where(x => x.Description != null && (x.MobilityDescription == 981 || x.MobilityDescription == 982)).Include(x => x.ControlShipInfo).Include(x => x.MachineryInfo).Include(x => x.CommonCode).Include(x => x.MachineryInfo1).ToList();

            return value;
        }



        public DamageMachineriesInfo GetShip(string id, DamageMachineriesInfo damageMachineriesInfo)
        {
            var shipid = Convert.ToInt64(id);
            return _damageMachineriesInfoRepository.FindOne(x => x.MachineriesDescriptionIdentity == shipid);
        }
        public MonthlyReturn GetMonthlyReturn(string id, MonthlyReturn monthlyReturn)
        {
            var monthlyReturnId = Convert.ToInt64(id);
            MonthlyReturn model = new MonthlyReturn();
            model = _monthlyReturnRepository.Filter(x => x.MonthlyReturnId == monthlyReturnId).Include(x => x.DefectMachinaries).Include(x => x.POLExpenseInfoes).Include(x => x.MachinaryRunningHours).SingleOrDefault();
            return model;
        }
        public QuaterlyReturn GetQuaterlyReturn(string id, QuaterlyReturn model)
        {
            var quaterlyReturnId = Convert.ToInt64(id);
            return _quaterlyReturnRepository.Filter(x => x.QuaterlyReturnId == quaterlyReturnId).SingleOrDefault();
        }
        public YearlyReturn GetYearlyReturn(string id, YearlyReturn model)
        {
            var yearlyReturnId = Convert.ToInt64(id);
            return _yearlyReturnRepository.Filter(x => x.YearlyReturnId == yearlyReturnId).Include(x => x.YearlyReturnDetails).SingleOrDefault();
        }
        public List<YearlyReturn> GetYearlyReturnByYearAndShipId(YearlyReturn model)
        {
            return _yearlyReturnRepository.Filter(x => x.YearId == model.YearId && x.ShipId == model.ShipId).Include(x => x.YearlyReturnDetails).Include(x => x.ControlShipInfo).Include(x => x.CommonCode).OrderBy(x => x.DeskNoId).ToList();
        }
        public object GetMonthlyReturn(string id)
        {
            if (id != "undefined")
            {
                var monthlyReturnId = Convert.ToInt64(id);
                var model = (from mr in db.MonthlyReturns
                             where mr.MonthlyReturnId == monthlyReturnId
                             select new MonthlyReturnViewModel()
                             {
                                 MonthlyReturnId = mr.MonthlyReturnId,
                                 ShipId = mr.ShipId,
                                 ShipStatusId = mr.ShipStatusId,
                                 ReportMonthId = mr.ReportMonthId,
                                 ReportYearId = mr.ReportYearId
                             }).SingleOrDefault();

                if (model != null)
                {
                    model.DefectMachinaries = (from df in db.DefectMachinaries
                                               where df.MonthlyReturnId == model.MonthlyReturnId
                                               select new DefectMachinaryViewModel()
                                               {
                                                   DefectMachinaryId = df.DefectMachinaryId,
                                                   MachineId = df.MachineId,
                                                   MachineName = df.MachineryInfo.Machinery,
                                                   MachinaryStatusName = df.CommonCode.TypeValue,
                                                   MachinaryStatusId = df.MachinaryStatusId,
                                                   MonthlyReturnId = df.MonthlyReturnId,
                                                   DefectDate = df.DefectDate,
                                                   DefectReasonDetail = df.DefectReasonDetail,
                                                   DefectAction = df.DefectSolution,
                                                   Remarks = df.Remarks
                                               }).ToList();
                    model.MachinaryRunningHours = (from df in db.MachinaryRunningHours
                                                   where df.MonthlyReturnId == model.MonthlyReturnId
                                                   select new MachinaryRunningHourViewModel()
                                                   {
                                                       MachinaryRunningHourId = df.MachinaryRunningHourId,
                                                       NoOPSMachineId = df.NoOPSMachineId,
                                                       NoOPSMachineName = df.MachineryInfo.Machinery,
                                                       MonthlyReturnId = df.MonthlyReturnId,
                                                       RunningHr = df.RunningHr,
                                                       RunningMin = df.RunningMin
                                                   }).ToList();
                    model.POLExpenseInfoes = (from df in db.POLExpenseInfoes
                                              where df.MonthlyReturnId == model.MonthlyReturnId
                                              select new POLExpenseInfoesViewModel()
                                              {
                                                  POLExpenseInfoId = df.POLExpenseInfoId,
                                                  OilName = df.OilName,
                                                  OilNameFull = df.CommonCode.TypeValue,
                                                  MonthlyReturnId = df.MonthlyReturnId,
                                                  OilPerLtRate = df.OilPerLtRate,
                                                  ReceivedQtyLtrorUnit = df.ReceivedQtyLtrorUnit,
                                                  StockAfterUseLtrOrUnit = df.StockAfterUseLtrOrUnit,
                                                  InitialStockLtrOrUnit = df.InitialStockLtrOrUnit,
                                                  HandoverLtrOrUnit = df.HandoverLtrOrUnit,
                                                  MonthlyUseltrOrUnit = df.MonthlyUseltrOrUnit
                                              }).ToList();
                }


                return model;
            }
            else
            {
                return id;
            }

        }
        public object GetYearlyReturn(string id)
        {
            if (id != "undefined")
            {
                var yearlyReturnId = Convert.ToInt64(id);
                var model = (from yr in db.YearlyReturns
                             where yr.YearlyReturnId == yearlyReturnId
                             select new YearlyReturnViewModel()
                             {
                                 YearlyReturnId = yr.YearlyReturnId,
                                 ShipId = yr.ShipId,
                                 DeskNoId = yr.DeskNoId,
                                 FrameNo = yr.FrameNo,
                             }).SingleOrDefault();

                if (model != null)
                {
                    model.YearlyReturnDetails = (from yd in db.YearlyReturnDetails
                                                 where yd.YearlyReturnId == model.YearlyReturnId
                                                 select new YearlyReturnDetailViewModel()
                                                 {
                                                     YearlyReturnId = yd.YearlyReturnId,
                                                     YearlyReturnDetailsId = yd.YearlyReturnDetailsId,

                                                     CompartmentAndLocation = yd.CompartmentAndLocation,
                                                     ExmAndAirPreTestDate = yd.ExmAndAirPreTestDate,
                                                     Plates = yd.Plates,
                                                     Frames = yd.Frames,
                                                     Rivets = yd.Rivets,
                                                     Cements = yd.Cements,
                                                     PaintsDescription = yd.PaintsDescription,
                                                     PaintsState = yd.PaintsState,
                                                     SuctionAndDischargeLineWhetherClear = yd.SuctionAndDischargeLineWhetherClear,
                                                     SuctionAndDischargeLineState = yd.SuctionAndDischargeLineState,
                                                     SuctionAndDischargeNonReturnValvesWhetherState = yd.SuctionAndDischargeNonReturnValvesWhetherState,
                                                     SuctionAndDischargeNonReturnValvesState = yd.SuctionAndDischargeNonReturnValvesState,
                                                     WatertightDoorWhetherTested = yd.WatertightDoorWhetherTested,
                                                     WatertightDoorState = yd.WatertightDoorState,
                                                     DefectsDescription = yd.DefectsDescription,
                                                     DefectsActionTaken = yd.DefectsActionTaken,

                                                 }).ToList();
                }


                return model;
            }
            else
            {
                return id;
            }

        }

        public object GetQuaterlyReturn(string id)
        {
            var quaterlyReturnId = Convert.ToInt64(id);


            var model = (from qr in db.QuaterlyReturns
                         where qr.QuaterlyReturnId == quaterlyReturnId
                         select new QuaterlyReturnViewModel()
                         {
                             QuaterlyReturnId = qr.QuaterlyReturnId,
                             ShipId = qr.ShipId,
                             ShipStatusId = qr.ShipStatusId,
                             ReturnReportNoId = qr.ReturnReportNoId,
                             ReportYearId = qr.ReportYearId,
                             LoadAtHarbourQuaterlyConsumption = qr.LoadAtHarbourQuaterlyConsumption,
                             AtNormalNotice = qr.AtNormalNotice,
                             AtSea = qr.AtSea,
                             LoadAtSeaQuaterlyConsumption = qr.LoadAtSeaQuaterlyConsumption,
                             DistanceRun = qr.DistanceRun,
                             FreshWaterQuaterlyConsumption = qr.FreshWaterQuaterlyConsumption,
                             FullPowerTrial = qr.FullPowerTrial,
                             InDocking = qr.InDocking,
                             MaintancePeriod = qr.MaintancePeriod,
                             NonOplReason = qr.NonOplReason,
                             PercentOfLubOilQuaterlyConsumption = qr.PercentOfLubOilQuaterlyConsumption,
                             QuaterlyReturnSendStatus = qr.QuaterlyReturnSendStatus,
                             Refit = qr.Refit,
                             RefrigerantQuaterlyConsumption = qr.RefrigerantQuaterlyConsumption,
                             TimeAwaitingOrder = qr.TimeAwaitingOrder,
                             QuaterlyReturnStatus = qr.QuaterlyReturnStatus,
                             TimeUnderway = qr.TimeUnderway,
                             UndergoingRefit = qr.UndergoingRefit,
                             UnDocking = qr.UnDocking
                         }).SingleOrDefault();

            if (model != null)
            {
                model.QuaterlyMainEnginesGeneratorsRunningHours = (from qmgrh in db.QuaterlyMainEnginesGeneratorsRunningHours
                                                                   join m in db.MachineryInfoes on qmgrh.MachineGeneratorId equals m.MachineryInfoIdentity
                                                                   where qmgrh.QuaterlyReturnId == model.QuaterlyReturnId
                                                                   select new QuaterlyMainEnginesGeneratorsRunningHourViewModel()
                                                                   {
                                                                       MachineGeneratorId = qmgrh.MachineGeneratorId,
                                                                       MachineGeneratorName = m.Machinery,
                                                                       DuringQuaterHr = qmgrh.DuringQuaterHr,
                                                                       DuringQuaterMin = qmgrh.DuringQuaterMin,
                                                                       SinceCommissionInBNHr = qmgrh.SinceCommissionInBNHr,
                                                                       SinceCommissionInBNMin = qmgrh.SinceCommissionInBNMin,
                                                                       SinceLastTopOverhaulHr = qmgrh.SinceLastTopOverhaulHr,
                                                                       SinceLastTopOverhaulMin = qmgrh.SinceLastTopOverhaulMin,
                                                                       SinceLastLOChangeHr = qmgrh.SinceLastLOChangeHr,
                                                                       SinceInstallationMin = qmgrh.SinceInstallationMin,
                                                                       SinceInstallationHr = qmgrh.SinceInstallationHr,
                                                                       SinceLastLOChangeMin = qmgrh.SinceLastLOChangeMin,
                                                                       SinceLastMajorOverhaulHr = qmgrh.SinceLastMajorOverhaulHr,
                                                                       SinceLastMajorOverhaulMin = qmgrh.SinceLastMajorOverhaulMin,
                                                                       Remarks = qmgrh.Remarks

                                                                   }).ToList();
                model.QuaterlyReturnEngineerOfficerRemarks = (from qmgrh in db.QuaterlyReturnEngineerOfficerRemarks
                                                              where qmgrh.QuaterlyReturnId == model.QuaterlyReturnId
                                                              select new QuaterlyReturnEngineerOfficerRemarklViewModel()
                                                              {
                                                                  QuaterlyReturnEngineerOfficerRemarkId = qmgrh.QuaterlyReturnEngineerOfficerRemarkId,
                                                                  QuaterlyReturnId = qmgrh.QuaterlyReturnId,
                                                                  RemarkTitle = qmgrh.RemarkTitle,
                                                                  RemarkDetails = qmgrh.RemarkDetails
                                                              }).ToList();
            }


            return model;
        }
        public List<DamageMachineriesInfo> FindOne(long id)
        {
            var a = _damageMachineriesInfoRepository.Filter(x => x.ShipId == id).ToList();
            return a;
        }

        public List<DamageMachineriesInfo> GetMachineHour()
        {
            return _damageMachineriesInfoRepository.All().Where(x => x.MachineName != null).Include(x => x.ControlShipInfo).Include(x => x.MachineryInfo1).ToList();
        }


        public object Delete(string id)
        {
            var machineid = Convert.ToInt64(id);
            return _damageMachineriesInfoRepository.Delete(x => x.MachineriesDescriptionIdentity == machineid);

        }

        public ResponseModel Saving(DamageMachineriesInfo damageMachineriesInfo)
        {
            DamageMachineriesInfo oldData = _damageMachineriesInfoRepository.FindOne(x => x.MachineriesDescriptionIdentity == damageMachineriesInfo.MachineriesDescriptionIdentity);
            if (oldData != null)
            {
                oldData.MachineriesDescriptionIdentity = damageMachineriesInfo.MachineriesDescriptionIdentity;
                oldData.SerialNo = damageMachineriesInfo.SerialNo;
                oldData.ShipId = damageMachineriesInfo.ShipId;
                oldData.Description = damageMachineriesInfo.Description;
                oldData.MobilityDescription = damageMachineriesInfo.MobilityDescription;
                oldData.DamageDate = damageMachineriesInfo.DamageDate;
                oldData.ReportDate = damageMachineriesInfo.ReportDate;
                oldData.RepairTime = damageMachineriesInfo.RepairTime;
                oldData.Remarks = damageMachineriesInfo.Remarks;
                oldData.MachineSerialNo = damageMachineriesInfo.MachineSerialNo;
                oldData.MachineSide = damageMachineriesInfo.MachineSide;
                oldData.MachineName = damageMachineriesInfo.MachineName;
                oldData.Hour = damageMachineriesInfo.Hour;
                oldData.Minute = damageMachineriesInfo.Minute;
                oldData.Duration = damageMachineriesInfo.Duration;
                oldData.MachineRemarks = damageMachineriesInfo.MachineRemarks;
                oldData.ReportDate = damageMachineriesInfo.ReportDate;
                oldData.FuelOilName = damageMachineriesInfo.FuelSerialNo;
                oldData.FuelOilId = damageMachineriesInfo.FuelOilId;
                oldData.StockFuelOil = damageMachineriesInfo.StockFuelOil;
                oldData.FuelConsumption = damageMachineriesInfo.FuelConsumption;
                oldData.UnitPrice = damageMachineriesInfo.UnitPrice;
                oldData.ReportDate = damageMachineriesInfo.ReportDate;
                // oldData.TotalPrice = damageMachineriesInfo.TotalPrice;
                oldData.ReceivedOil = damageMachineriesInfo.ReceivedOil;
                oldData.DonateOil = damageMachineriesInfo.DonateOil;
                oldData.UpdatedBy = PortalContext.CurrentUser.UserName;
                oldData.LastUpdate = DateTime.Now;
                _damageMachineriesInfoRepository.Edit(oldData);
                ResponseModel.Message = "Machinery Information Is Updated Successfully.";
            }
            else
            {

                damageMachineriesInfo.EntryDate = DateTime.Now;
                damageMachineriesInfo.LastUpdate = DateTime.Now;
                damageMachineriesInfo.UserId = PortalContext.CurrentUser.UserName;
                damageMachineriesInfo.UpdatedBy = PortalContext.CurrentUser.UserName;
                damageMachineriesInfo.IsVerified = false;
                _damageMachineriesInfoRepository.Save(damageMachineriesInfo);
                ResponseModel.Message = "Machinery Information Is Saved Successfully.";
            }
            return ResponseModel;
        }

        public List<DamageMachineriesInfo> GetPol()
        {
            // damageMachineriesInfo.UnitPrice = string.Concat(model.DamageMachineriesInfo.UnitPrice.Select(c => (char)('\u09E6' + c - '0'))); // "১২৩৪৫৬৭৮৯০"

            return _damageMachineriesInfoRepository.All().Where(x => x.FuelOilId != null).Include(x => x.ControlShipInfo).ToList();
        }

        public List<DamageMachineriesInfo> GetAllForReport(long id, DateTime dateFrom)
        {
            var startDate = new DateTime(dateFrom.Year, dateFrom.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            return _damageMachineriesInfoRepository.Filter(x => x.ShipId == id && x.ReportDate >= startDate && x.ReportDate <= endDate && x.Description != null).Include(x => x.ControlShipInfo).ToList();
        }

        public List<DamageMachineriesInfo> RunningHourReport(long id, DateTime dateFrom)
        {
            var startDate = new DateTime(dateFrom.Year, dateFrom.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);
            return _damageMachineriesInfoRepository.Filter(x => x.ShipId == id && x.ReportDate >= startDate && x.ReportDate <= endDate && x.MachineName != null).Include(x => x.ControlShipInfo).ToList();
        }

        public List<DamageMachineriesInfo> GetPolData(long id, DateTime dateFrom)
        {
            var startDate = new DateTime(dateFrom.Year, dateFrom.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);
            return _damageMachineriesInfoRepository.Filter(x => x.ShipId == id && x.ReportDate >= startDate && x.ReportDate <= endDate && x.FuelOilId > 0).Include(x => x.ControlShipInfo).ToList();
        }

        public object UpdateStatus(string id, long status)
        {
            var shipid = Convert.ToInt64(id);
            var old = _damageMachineriesInfoRepository.FindOne(x => x.MachineriesDescriptionIdentity == shipid);
            if (old != null)
            {
                old.MobilityDescription = status;

                _damageMachineriesInfoRepository.Edit(old);
            }
            return old;
        }

        public List<DamageMachineriesInfo> UserWiseData(int? verifyType)
        {
            var allData = _damageMachineriesInfoRepository.Filter(x => x.ShipId == PortalContext.CurrentUser.ShipId).ToList();
            return verifyType == 1006
                ? allData.Where(x => x.IsVerified == true).ToList()
                : allData.Where(x => x.IsVerified == false).ToList();
        }

        public ResponseModel VerifiedStatusUpdate(List<vwDamageMachinaries> vwDamageMachinarieses)
        {
            foreach (var item in vwDamageMachinarieses)
            {
                var oldData = _damageMachineriesInfoRepository.FindOne(x => x.MachineriesDescriptionIdentity == item.MachineriesDescriptionIdentity);
                if (oldData != null && oldData.IsVerified != item.IsVerified)
                {
                    oldData.IsVerified = item.IsVerified;
                    oldData.VerifiedBy = PortalContext.CurrentUser.UserName;
                    oldData.VerifiedDate = DateTime.Now.Date;
                    _damageMachineriesInfoRepository.Edit(oldData);
                    ResponseModel.Message = "Status Updated Successfully !";
                }
            }
            return ResponseModel;
        }

        public List<MonthlyReturn> GetAllMonthlyReturn()
        {
            if (PortalContext.CurrentUser.RoleId == 4)
            {
                return _monthlyReturnRepository.Filter(x=>x.ControlShipInfo.Organization== PortalContext.CurrentUser.BranchInfoIdentity).Include(x => x.ControlShipInfo).Include(x => x.ControlShipInfo).Include(x => x.CommonCode).Include(x => x.DefectMachinaries).Include(x => x.POLExpenseInfoes).Include(x => x.MachinaryRunningHours).Include(x => x.ReturnReportNo).ToList();
            }
            else if (PortalContext.CurrentUser.RoleId == 1)
            {
                return _monthlyReturnRepository.Filter(x => x.ShipId == PortalContext.CurrentUser.ShipId).Include(x => x.ControlShipInfo).Include(x => x.ControlShipInfo).Include(x => x.CommonCode).Include(x => x.DefectMachinaries).Include(x => x.POLExpenseInfoes).Include(x => x.MachinaryRunningHours).Include(x => x.ReturnReportNo).ToList();
            }
            else
            {
                return _monthlyReturnRepository.All().Include(x => x.ControlShipInfo).Include(x => x.ControlShipInfo).Include(x => x.CommonCode).Include(x => x.DefectMachinaries).Include(x => x.POLExpenseInfoes).Include(x => x.MachinaryRunningHours).Include(x => x.ReturnReportNo).ToList();

            }

        }
        public List<QuaterlyReturn> GetAllQuaterlyReturn()
        {
            if (PortalContext.CurrentUser.RoleId == 4)
            {
                return _quaterlyReturnRepository.Filter(x=>x.ControlShipInfo.Organization== PortalContext.CurrentUser.BranchInfoIdentity).Include(x => x.ControlShipInfo).Include(x => x.ControlShipInfo).Include(x => x.CommonCode).Include(x => x.QuaterlyMainEnginesGeneratorsRunningHours).Include(x => x.ReturnReportNo).Include(x => x.ReturnReportYear).ToList();
            }
            else if (PortalContext.CurrentUser.RoleId == 1)
            {
                return _quaterlyReturnRepository.Filter(x=>x.ShipId== PortalContext.CurrentUser.ShipId).Include(x => x.ControlShipInfo).Include(x => x.ControlShipInfo).Include(x => x.CommonCode).Include(x => x.QuaterlyMainEnginesGeneratorsRunningHours).Include(x => x.ReturnReportNo).Include(x => x.ReturnReportYear).ToList();
            }
            else
            {
                return _quaterlyReturnRepository.All().Include(x => x.ControlShipInfo).Include(x => x.ControlShipInfo).Include(x => x.CommonCode).Include(x => x.QuaterlyMainEnginesGeneratorsRunningHours).Include(x => x.ReturnReportNo).Include(x => x.ReturnReportYear).ToList();

            }
        }
        public List<MonthlyReturn> GetSearchMonthlyReturn(int? monthId, int? yearId, string shipId, string monthlyReturnId)
        {
            var ShipId = Convert.ToInt64(shipId);
            var MonthlyReturnId = Convert.ToInt64(monthlyReturnId);
            List<MonthlyReturn> value = _monthlyReturnRepository.Filter(x => (x.ShipId == ShipId && x.ReportMonthId >= monthId && x.ReportYearId <= yearId) || x.MonthlyReturnId == MonthlyReturnId).Include(x => x.ReturnReportNo).Include(x => x.ControlShipInfo).ToList();
            return value;
        }
        public QuaterlyReturn GetSearchQuaterlyReturn(int? quaterlyReturnNoId, int? yearId, string shipId, string quaterlyReturnId)
        {
            var ShipId = Convert.ToInt64(shipId);
            var QuaterlyReturnId = Convert.ToInt64(quaterlyReturnId);
            QuaterlyReturn value = _quaterlyReturnRepository.Filter(x => (x.ShipId == ShipId && x.ReturnReportNoId == quaterlyReturnNoId && x.ReportYearId == yearId) || x.QuaterlyReturnId == QuaterlyReturnId).Include(x => x.ReturnReportNo).Include(x => x.ReturnReportYear).Include(x => x.QuaterlyReturnEngineerOfficerRemarks).Include(x => x.ControlShipInfo).Include(x => x.CommonCode).Include(x => x.CommonCode1).Include(x => x.CommonCode2).Include(x => x.CommonCode3).Include(x => x.CommonCode4).Include(x => x.CommonCode5).FirstOrDefault();
            return value;
        }
        public List<MonthlyReturn> GetMonthlyReturnByReturnStatus()
        {
            List<MonthlyReturn> value = _monthlyReturnRepository.Filter(x => x.MonthlyReturnStatus == false).Include(x => x.ReturnReportNo).ToList();
            return value;
        }
        public List<MonthlyReturn> GetMonthlyReturnByShipId(long shipId)
        {
            if (PortalContext.CurrentUser.RoleId == 4)
            {
                return _monthlyReturnRepository.Filter(x => x.ControlShipInfo.Organization == PortalContext.CurrentUser.BranchInfoIdentity && x.ShipId==shipId).Include(x => x.ControlShipInfo).Include(x => x.ControlShipInfo).Include(x => x.CommonCode).Include(x => x.DefectMachinaries).Include(x => x.POLExpenseInfoes).Include(x => x.MachinaryRunningHours).Include(x => x.ReturnReportNo).OrderByDescending(x => x.MonthlyReturnId).ToList();
            }
            else if (PortalContext.CurrentUser.RoleId == 1)
            {
                return _monthlyReturnRepository.Filter(x => x.ShipId == PortalContext.CurrentUser.ShipId).Include(x => x.ControlShipInfo).Include(x => x.ControlShipInfo).Include(x => x.CommonCode).Include(x => x.DefectMachinaries).Include(x => x.POLExpenseInfoes).Include(x => x.MachinaryRunningHours).Include(x => x.ReturnReportNo).OrderByDescending(x => x.MonthlyReturnId).ToList();
            }
            else
            {
                return _monthlyReturnRepository.Filter(x => x.ShipId == shipId).Include(x => x.ControlShipInfo).Include(x => x.ControlShipInfo).Include(x => x.CommonCode).Include(x => x.DefectMachinaries).Include(x => x.POLExpenseInfoes).Include(x => x.MachinaryRunningHours).Include(x => x.ReturnReportNo).OrderByDescending(x=>x.MonthlyReturnId).ToList();

            }


        }
        public List<QuaterlyReturn> GetQuaterlyReturnByReturnStatus()
        {
            List<QuaterlyReturn> value = _quaterlyReturnRepository.Filter(x => x.QuaterlyReturnStatus == false).ToList();
            return value;
        }
        public List<QuaterlyReturn> GetQuaterlyReturnByReturnByShipId(long ShipId)
        {
            if (PortalContext.CurrentUser.RoleId == 4)
            {
                return _quaterlyReturnRepository.Filter(x => x.ShipId == ShipId && x.ControlShipInfo.Organization==PortalContext.CurrentUser.BranchInfoIdentity).Include(x => x.ControlShipInfo).Include(x => x.QuaterlyMainEnginesGeneratorsRunningHours).Include(x => x.ReturnReportYear).Include(x => x.ReturnReportNo).OrderByDescending(x=>x.QuaterlyReturnId).ToList();
            }
            else if (PortalContext.CurrentUser.RoleId == 1)
            {
                return _quaterlyReturnRepository.Filter(x => x.ShipId == PortalContext.CurrentUser.ShipId).Include(x => x.ControlShipInfo).Include(x => x.QuaterlyMainEnginesGeneratorsRunningHours).Include(x => x.ReturnReportYear).Include(x => x.ReturnReportNo).OrderByDescending(x => x.QuaterlyReturnId).ToList();
            }
            else
            {
                return _quaterlyReturnRepository.Filter(x => x.ShipId == ShipId).Include(x => x.ControlShipInfo).Include(x => x.QuaterlyMainEnginesGeneratorsRunningHours).Include(x => x.ReturnReportYear).Include(x => x.ReturnReportNo).OrderByDescending(x => x.QuaterlyReturnId).ToList();

            }

        }
        public ResponseModel SavingMonthlyReturn(MonthlyReturn monthlyReturn)
        {
            MonthlyReturn oldData = _monthlyReturnRepository.Filter(x => x.MonthlyReturnId == monthlyReturn.MonthlyReturnId).Include(x => x.DefectMachinaries).Include(x => x.POLExpenseInfoes).Include(x => x.MachinaryRunningHours).SingleOrDefault();
            if (oldData != null)
            {
                oldData.ShipId = monthlyReturn.ShipId;
                oldData.ShipStatusId = monthlyReturn.ShipStatusId;
                oldData.ReportMonthId = monthlyReturn.ReportMonthId;
                oldData.ReportYearId = monthlyReturn.ReportYearId;
                oldData.AtSeaHr = monthlyReturn.AtSeaHr;
                oldData.AtSeaDuringThisMonthHr = monthlyReturn.AtSeaDuringThisMonthHr;
                oldData.AtSeaDuringThisMonthMin = monthlyReturn.AtSeaDuringThisMonthMin;
                oldData.HarbourDuringThisMonthHr = monthlyReturn.HarbourDuringThisMonthHr;
                oldData.HarbourDuringThisMonthMin = monthlyReturn.HarbourDuringThisMonthMin;
                oldData.RefrigerantKg = monthlyReturn.RefrigerantKg;
                oldData.FreshWaterTons = monthlyReturn.FreshWaterTons;
                oldData.ShipMonthlyRunningHr = monthlyReturn.ShipMonthlyRunningHr;
                oldData.ShipMonthlyRunningMin = monthlyReturn.ShipMonthlyRunningMin;
                oldData.NonOperationalDuringMonthHr = monthlyReturn.NonOperationalDuringMonthHr;
                oldData.NonOperationalDuringMonthMin = monthlyReturn.NonOperationalDuringMonthMin;
                oldData.MonthlyReturnSendStatus = false;
                oldData.MonthlyReturnStatus = false;

                //DefectMachinaries Mastering
                foreach (var defectMachinary in oldData.DefectMachinaries.ToList())
                {
                    if (!monthlyReturn.DefectMachinaries.Any(x => x.DefectMachinaryId == defectMachinary.DefectMachinaryId))
                    {
                        _defectMachinaryRepository.Delete(defectMachinary);
                        db.SaveChanges();
                    }

                }

                foreach (DefectMachinary defectMachinary in monthlyReturn.DefectMachinaries)
                {
                    DefectMachinary defectMachinaryM = new DefectMachinary();
                    var existingDefectMachinary = oldData.DefectMachinaries
                        .Where(c => c.DefectMachinaryId == defectMachinary.DefectMachinaryId)
                        .SingleOrDefault();

                    if (existingDefectMachinary != null)
                    {
                        existingDefectMachinary.MachineId = defectMachinary.MachineId;
                        existingDefectMachinary.ShipId = defectMachinary.ShipId;
                        existingDefectMachinary.MachinaryStatusId = defectMachinary.MachinaryStatusId;
                        existingDefectMachinary.DefectDate = defectMachinary.DefectDate;
                        existingDefectMachinary.DefectSolution = defectMachinary.DefectSolution;
                        existingDefectMachinary.DefectReasonDetail = defectMachinary.DefectReasonDetail;
                        existingDefectMachinary.Remarks = defectMachinary.Remarks;

                    }
                    else
                    {
                        oldData.DefectMachinaries.Add(defectMachinary);
                    }
                    db.SaveChanges();

                }


                //MachinaryRunningHours Mastering
                foreach (var machinaryRunningHour in oldData.MachinaryRunningHours.ToList())
                {
                    if (!monthlyReturn.MachinaryRunningHours.Any(x => x.MachinaryRunningHourId == machinaryRunningHour.MachinaryRunningHourId))
                    {
                        _machinaryRunningHourRepository.Delete(machinaryRunningHour);
                        db.SaveChanges();
                    }

                }

                foreach (MachinaryRunningHour machinaryRunningHour in monthlyReturn.MachinaryRunningHours)
                {
                    var existingMachinaryRunningHour = oldData.MachinaryRunningHours
                        .Where(c => c.MachinaryRunningHourId == machinaryRunningHour.MachinaryRunningHourId && machinaryRunningHour.MachinaryRunningHourId>0)
                        .SingleOrDefault();

                    if (existingMachinaryRunningHour != null)
                    {
                        // Update MachinaryRunningHour
                        var query = $"UPDATE MachinaryRunningHour SET NoOPSMachineId={machinaryRunningHour.NoOPSMachineId},ShipId={machinaryRunningHour.ShipId}, RunningHr={machinaryRunningHour.RunningHr},RunningMin={machinaryRunningHour.RunningMin} where MachinaryRunningHourId={machinaryRunningHour.MachinaryRunningHourId}";
                        int noOfRowUpdated = db.Database.ExecuteSqlCommand(query);

                    }
                    else
                    {
                        oldData.MachinaryRunningHours.Add(machinaryRunningHour);
                    }
                    db.SaveChanges();
                }



                //POLExpenseInfoes Mastering
                foreach (var polExpenseInfo in oldData.POLExpenseInfoes.ToList())
                {
                    if (!monthlyReturn.POLExpenseInfoes.Any(x => x.POLExpenseInfoId == polExpenseInfo.POLExpenseInfoId))
                    {
                        _polExpenseInfoRepository.Delete(polExpenseInfo);
                        db.SaveChanges();
                    }

                }

                foreach (POLExpenseInfo polExpenseInfo in monthlyReturn.POLExpenseInfoes)
                {
                    var existingPOLExpenseInfo = oldData.POLExpenseInfoes
                        .Where(c => c.POLExpenseInfoId == polExpenseInfo.POLExpenseInfoId)
                        .SingleOrDefault();

                    if (existingPOLExpenseInfo != null)
                    {
                        // Update POLExpenseInfo
                        var query = $"UPDATE POLExpenseInfo SET OilName={polExpenseInfo.OilName},ShipId={polExpenseInfo.ShipId}, InitialStockLtrOrUnit={polExpenseInfo.InitialStockLtrOrUnit},ReceivedQtyLtrorUnit={polExpenseInfo.ReceivedQtyLtrorUnit},HandoverLtrOrUnit={polExpenseInfo.HandoverLtrOrUnit},MonthlyUseltrOrUnit={polExpenseInfo.MonthlyUseltrOrUnit},StockAfterUseLtrOrUnit={polExpenseInfo.StockAfterUseLtrOrUnit}, OilPerLtRate={polExpenseInfo.OilPerLtRate} where POLExpenseInfoId={polExpenseInfo.POLExpenseInfoId}";
                        int noOfRowUpdated = db.Database.ExecuteSqlCommand(query);
                    }
                    else
                    {
                        var query = $"INSERT INTO POLExpenseInfo(MonthlyReturnId, ShipId, OilName, InitialStockLtrOrUnit, ReceivedQtyLtrorUnit, HandoverLtrOrUnit, MonthlyUseltrOrUnit, StockAfterUseLtrOrUnit, OilPerLtRate) VALUES({ oldData.MonthlyReturnId},{ oldData.ShipId},{ polExpenseInfo.OilName},{ polExpenseInfo.InitialStockLtrOrUnit}, { polExpenseInfo.ReceivedQtyLtrorUnit}, {polExpenseInfo.HandoverLtrOrUnit}, {polExpenseInfo.MonthlyUseltrOrUnit}, {polExpenseInfo.StockAfterUseLtrOrUnit}, {polExpenseInfo.OilPerLtRate})";
                        int noOfRowUpdated = db.Database.ExecuteSqlCommand(query);
                    }
                    db.SaveChanges();
                }
                _monthlyReturnRepository.Edit(oldData);

                ResponseModel.Message = "Monthly Return Information Is Updated Successfully";
                ResponseModel.ShipId = oldData.ShipId;
            }
            else if (_monthlyReturnRepository.Exists(x=>x.ReportYearId==monthlyReturn.ReportYearId && x.ReportMonthId==monthlyReturn.ReportMonthId && x.ShipId==monthlyReturn.ShipId && x.MonthlyReturnId!= monthlyReturn.MonthlyReturnId))
            {
                ResponseModel.Message = "Monthly Return Information Is Already Insert";
            }
            else
            {
                monthlyReturn.MonthlyReturnSendStatus = false;
                monthlyReturn.MonthlyReturnStatus = false;
                _monthlyReturnRepository.Save(monthlyReturn);
                ResponseModel.PrimaryKey = monthlyReturn.MonthlyReturnId;

                ResponseModel.Message = "Monthly Return Information Is Insert Successfully";
                ResponseModel.ShipId = monthlyReturn.ShipId;

            }
            return ResponseModel;
        }
        public ResponseModel SavingYearlyReturn(YearlyReturn yearlyReturn)
        {
            YearlyReturn oldData = _yearlyReturnRepository.Filter(x => x.YearlyReturnId == yearlyReturn.YearlyReturnId).Include(x => x.YearlyReturnDetails).Include(x => x.ControlShipInfo).SingleOrDefault();
            if (oldData != null)
            {
                oldData.ShipId = yearlyReturn.ShipId;
                oldData.DeskNoId = yearlyReturn.DeskNoId;
                oldData.YearId = yearlyReturn.YearId;
                oldData.FrameNo = yearlyReturn.FrameNo;

                //YearlyReturnDetails Mastering
                foreach (var yearlyReturnDetail in oldData.YearlyReturnDetails.ToList())
                {
                    if (!yearlyReturn.YearlyReturnDetails.Any(x => x.YearlyReturnDetailsId == yearlyReturnDetail.YearlyReturnDetailsId))
                    {
                        _yearlyReturnDetailRepository.Delete(yearlyReturnDetail);
                        db.SaveChanges();
                    }

                }

                foreach (YearlyReturnDetail yearlyReturnDetail in yearlyReturn.YearlyReturnDetails)
                {
                    var existingyEarlyReturnDetail = oldData.YearlyReturnDetails
                        .Where(c => c.YearlyReturnDetailsId == yearlyReturnDetail.YearlyReturnDetailsId && yearlyReturnDetail.YearlyReturnDetailsId > 0)
                        .SingleOrDefault();

                    if (existingyEarlyReturnDetail != null)
                    {
                        DateTime myDateTime = yearlyReturnDetail.ExmAndAirPreTestDate ?? default(DateTime);
                        string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
                        var query = $"UPDATE YearlyReturnDetails SET CompartmentAndLocation='{yearlyReturnDetail.CompartmentAndLocation}',ExmAndAirPreTestDate='{sqlFormattedDate}', Plates='{yearlyReturnDetail.Plates}',Frames='{yearlyReturnDetail.Frames}'," +
                            $"Rivets=N'{yearlyReturnDetail.Rivets}',Cements=N'{yearlyReturnDetail.Cements}',PaintsDescription=N'{yearlyReturnDetail.PaintsDescription}'," +
                            $"PaintsState=N'{yearlyReturnDetail.PaintsState}',SuctionAndDischargeLineWhetherClear=N'{yearlyReturnDetail.SuctionAndDischargeLineWhetherClear}',SuctionAndDischargeLineState=N'{yearlyReturnDetail.SuctionAndDischargeLineState}'," +
                            $"SuctionAndDischargeNonReturnValvesWhetherState=N'{yearlyReturnDetail.SuctionAndDischargeNonReturnValvesWhetherState}',SuctionAndDischargeNonReturnValvesState=N'{yearlyReturnDetail.SuctionAndDischargeNonReturnValvesState}',WatertightDoorWhetherTested=N'{yearlyReturnDetail.WatertightDoorWhetherTested}'," +
                            $"WatertightDoorState=N'{yearlyReturnDetail.WatertightDoorState}',DefectsDescription=N'{yearlyReturnDetail.DefectsDescription}',DefectsActionTaken=N'{yearlyReturnDetail.DefectsActionTaken}'" +
                            $" where YearlyReturnDetailsId={yearlyReturnDetail.YearlyReturnDetailsId}";
                        int noOfRowUpdated = db.Database.ExecuteSqlCommand(query);
                    }
                    else
                    {
                        oldData.YearlyReturnDetails.Add(yearlyReturnDetail);
                        db.SaveChanges();
                    }

                }
                _yearlyReturnRepository.Edit(oldData);
                ResponseModel.ShipId = oldData.ShipId;
                ResponseModel.Message = "Yearly Return Information Is Updated Successfully";
            }
            else if (_yearlyReturnRepository.Exists(x => x.YearId == yearlyReturn.YearId && x.DeskNoId == yearlyReturn.DeskNoId && x.ShipId == yearlyReturn.ShipId && x.YearlyReturnType==yearlyReturn.YearlyReturnType))
            {
                if (yearlyReturn.YearlyReturnType == 1)
                {
                    ResponseModel.Message = "Yearly Return Information Is Already Insert";
                }
                else
                {
                    ResponseModel.Message = "Half Yearly Return Information Is Already Insert";
                }
            }
            else
            {

                _yearlyReturnRepository.Save(yearlyReturn);
                ResponseModel.PrimaryKey = yearlyReturn.YearlyReturnId;
                ResponseModel.ShipId = yearlyReturn.ShipId;
                if (yearlyReturn.YearlyReturnType == 1)
                {
                    ResponseModel.Message = "Yearly Return Information Is Insert Successfully";
                }
                else
                {
                    ResponseModel.Message = "Half Yearly Return Information Is Insert Successfully";
                }
            }
            return ResponseModel;
        }


        public ResponseModel SavingQuaterlyReturn(QuaterlyReturn model)
        {
            QuaterlyReturn oldData = _quaterlyReturnRepository.Filter(x => x.QuaterlyReturnId == model.QuaterlyReturnId && x.ReportYearId == model.ReportYearId && x.ReturnReportNoId == model.ReturnReportNoId).Include(x => x.QuaterlyMainEnginesGeneratorsRunningHours).Include(x => x.QuaterlyReturnEngineerOfficerRemarks).SingleOrDefault();
            if (oldData != null)
            {
                oldData.ShipId = model.ShipId;
                oldData.ShipStatusId = model.ShipStatusId;
                oldData.ReturnReportNoId = model.ReturnReportNoId;
                oldData.ReportYearId = model.ReportYearId;
                oldData.QuaterlyReturnId = model.QuaterlyReturnId;
                oldData.UndergoingRefit = model.UndergoingRefit;
                oldData.Refit = model.Refit;
                oldData.InDocking = model.InDocking;
                oldData.UnDocking = model.UnDocking;
                oldData.MaintancePeriod = model.MaintancePeriod;
                oldData.FullPowerTrial = model.FullPowerTrial;
                oldData.NonOplReason = model.NonOplReason;
                oldData.AtSea = model.AtSea;
                oldData.TotalNoOfDay = model.TotalNoOfDay;
                oldData.DistanceRun = model.DistanceRun;
                oldData.AtNormalNotice = model.AtNormalNotice;
                oldData.TimeUnderway = model.TimeUnderway;
                oldData.TimeAwaitingOrder = model.TimeAwaitingOrder;
                oldData.PercentOfLubOilQuaterlyConsumption = model.PercentOfLubOilQuaterlyConsumption;
                oldData.LoadAtHarbourQuaterlyConsumption = model.LoadAtHarbourQuaterlyConsumption;
                oldData.LoadAtSeaQuaterlyConsumption = model.LoadAtSeaQuaterlyConsumption;
                oldData.RefrigerantQuaterlyConsumption = model.RefrigerantQuaterlyConsumption;
                oldData.FreshWaterQuaterlyConsumption = model.FreshWaterQuaterlyConsumption;
                oldData.MainEngineFuelNameId = model.MainEngineFuelNameId;
                oldData.MainEngineFuelQuantity = model.MainEngineFuelQuantity;
                oldData.MainEngineLubOilNameId = model.MainEngineLubOilNameId;
                oldData.MainEngineLubOilQuantity = model.MainEngineLubOilQuantity;
                oldData.DGAndOthersFuelNameId = model.DGAndOthersFuelNameId;
                oldData.DGAndOthersFuelQuantity = model.DGAndOthersFuelQuantity;
                oldData.OthersLubOilNameId = model.OthersLubOilNameId;
                oldData.OthersLubOilQuantity = model.OthersLubOilQuantity;
                oldData.DGLubOilNameId = model.DGLubOilNameId;
                oldData.DGLubOilQuantity = model.DGLubOilQuantity;
                oldData.MachineryUnderTrialRemarks = model.MachineryUnderTrialRemarks;
                oldData.RemarksOfAdministrativeAuthority = model.RemarksOfAdministrativeAuthority;
                oldData.QuaterlyReturnSendStatus = false;
                oldData.QuaterlyReturnStatus = false;


                //QuaterlyMainEnginesGeneratorsRunningHours Mastering
                foreach (var quaterlyMainEnginesGeneratorsRunningHour in oldData.QuaterlyMainEnginesGeneratorsRunningHours.ToList())
                {
                    if (!model.QuaterlyMainEnginesGeneratorsRunningHours.Any(x => x.QuaterlyMainEnginesGeneratorsRunningHourId == quaterlyMainEnginesGeneratorsRunningHour.QuaterlyMainEnginesGeneratorsRunningHourId))
                    {
                        _quaterlyMainEnginesGeneratorsRunningHourRepository.Delete(quaterlyMainEnginesGeneratorsRunningHour);
                        db.SaveChanges();
                    }

                }

                foreach (QuaterlyMainEnginesGeneratorsRunningHour quaterlyMainEnginesGeneratorsRunningHour in model.QuaterlyMainEnginesGeneratorsRunningHours)
                {
                    var existingQuaterlyMainEnginesGeneratorsRunningHour = oldData.QuaterlyMainEnginesGeneratorsRunningHours
                        .Where(c => c.QuaterlyMainEnginesGeneratorsRunningHourId == quaterlyMainEnginesGeneratorsRunningHour.QuaterlyMainEnginesGeneratorsRunningHourId && quaterlyMainEnginesGeneratorsRunningHour.QuaterlyMainEnginesGeneratorsRunningHourId > 0)
                        .SingleOrDefault();

                    if (existingQuaterlyMainEnginesGeneratorsRunningHour != null)
                    {
                        // Update QuaterlyMainEnginesGeneratorsRunningHour
                        var query = $"UPDATE MachinaryRunningHour SET MachineGeneratorId={quaterlyMainEnginesGeneratorsRunningHour.MachineGeneratorId},ShipId={quaterlyMainEnginesGeneratorsRunningHour.ShipId}, DuringQuaterHr={quaterlyMainEnginesGeneratorsRunningHour.DuringQuaterHr}," +
                            $"DuringQuaterMin={quaterlyMainEnginesGeneratorsRunningHour.DuringQuaterMin}, SinceLastLOChangeHr={quaterlyMainEnginesGeneratorsRunningHour.SinceLastLOChangeHr},SinceLastLOChangeMin={quaterlyMainEnginesGeneratorsRunningHour.SinceLastLOChangeMin}," +
                            $" SinceLastTopOverhaulHr={quaterlyMainEnginesGeneratorsRunningHour.SinceLastTopOverhaulHr},SinceLastTopOverhaulMin={quaterlyMainEnginesGeneratorsRunningHour.SinceLastTopOverhaulMin}, SinceLastMajorOverhaulHr={quaterlyMainEnginesGeneratorsRunningHour.SinceLastMajorOverhaulHr}," +
                            $" SinceLastMajorOverhaulMin={quaterlyMainEnginesGeneratorsRunningHour.SinceLastMajorOverhaulMin},SinceCommissionInBNHr={quaterlyMainEnginesGeneratorsRunningHour.SinceCommissionInBNHr}, SinceCommissionInBNMin={quaterlyMainEnginesGeneratorsRunningHour.SinceCommissionInBNMin}," +
                            $" SinceInstallationHr={quaterlyMainEnginesGeneratorsRunningHour.SinceInstallationHr},SinceInstallationMin={quaterlyMainEnginesGeneratorsRunningHour.SinceInstallationMin}, Remarks='{quaterlyMainEnginesGeneratorsRunningHour.Remarks}'" +
                            $"where QuaterlyMainEnginesGeneratorsRunningHourId={quaterlyMainEnginesGeneratorsRunningHour.QuaterlyMainEnginesGeneratorsRunningHourId}";
                        int noOfRowUpdated = db.Database.ExecuteSqlCommand(query);
                    }
                    else
                    {
                        oldData.QuaterlyMainEnginesGeneratorsRunningHours.Add(quaterlyMainEnginesGeneratorsRunningHour);
                    }
                    db.SaveChanges();
                }


                //QuaterlyReturnEngineerOfficerRemarks Mastering
                foreach (var quaterlyReturnEngineerOfficerRemark in oldData.QuaterlyReturnEngineerOfficerRemarks.ToList())
                {
                    if (!model.QuaterlyReturnEngineerOfficerRemarks.Any(x => x.QuaterlyReturnEngineerOfficerRemarkId == quaterlyReturnEngineerOfficerRemark.QuaterlyReturnEngineerOfficerRemarkId))
                    {
                        _quaterlyReturnEngineerOfficerRemarksRepository.Delete(quaterlyReturnEngineerOfficerRemark);
                        db.SaveChanges();
                    }

                }

                foreach (QuaterlyReturnEngineerOfficerRemark quaterlyReturnEngineerOfficerRemark in model.QuaterlyReturnEngineerOfficerRemarks)
                {
                    var existingQuaterlyReturnEngineerOfficerRemark = oldData.QuaterlyReturnEngineerOfficerRemarks
                        .Where(c => c.QuaterlyReturnEngineerOfficerRemarkId == quaterlyReturnEngineerOfficerRemark.QuaterlyReturnEngineerOfficerRemarkId && quaterlyReturnEngineerOfficerRemark.QuaterlyReturnEngineerOfficerRemarkId > 0)
                        .SingleOrDefault();

                    if (existingQuaterlyReturnEngineerOfficerRemark != null)
                    {
                        // Update QuaterlyReturnEngineerOfficerRemark
                        existingQuaterlyReturnEngineerOfficerRemark.RemarkTitle = quaterlyReturnEngineerOfficerRemark.RemarkTitle;
                        existingQuaterlyReturnEngineerOfficerRemark.RemarkDetails = quaterlyReturnEngineerOfficerRemark.RemarkDetails;


                        //var query = $"UPDATE QuaterlyReturnEngineerOfficerRemark SET RemarkTitle=N'{quaterlyReturnEngineerOfficerRemark.RemarkTitle}',RemarkDetails=N'{quaterlyReturnEngineerOfficerRemark.RemarkDetails}'" +
                        //    $"where QuaterlyReturnEngineerOfficerRemarkId={quaterlyReturnEngineerOfficerRemark.QuaterlyReturnEngineerOfficerRemarkId}";
                        //int noOfRowUpdated = db.Database.ExecuteSqlCommand(query);
                    }
                    else
                    {
                        oldData.QuaterlyReturnEngineerOfficerRemarks.Add(quaterlyReturnEngineerOfficerRemark);
                    }
                    db.SaveChanges();

                }



                _quaterlyReturnRepository.Edit(oldData);
                ResponseModel.ShipId = oldData.ShipId;

                ResponseModel.Message = "Quarterly Return Information Is Updated Successfully";
            }
            else if (_quaterlyReturnRepository.Exists(x=>x.ShipId== model.ShipId && x.ReportYearId==model.ReportYearId && x.ReturnReportNoId==model.ReturnReportNoId)){
                ResponseModel.Message = "Quarterly Return Information Is Alredy Inserted";
            }
            else
            {
                model.QuaterlyReturnSendStatus = false;
                model.QuaterlyReturnStatus = false;
                model.CreateUserDate = DateTime.Now;
                model.CreateUserId = PortalContext.CurrentUser.UserName;
                _quaterlyReturnRepository.Save(model);
                ResponseModel.PrimaryKey = model.QuaterlyReturnId;
                ResponseModel.ShipId = model.ShipId;
                ResponseModel.Message = "Monthly Return Information Is Insert Successfully";
            }
            return ResponseModel;
        }
        public List<YearlyReturn> GetAllYearlyReturn(int returnType)
        {
            List<YearlyReturn> yearlyReturns = new List<YearlyReturn>();

            if (PortalContext.CurrentUser.RoleId == 4)
            {
                yearlyReturns = _yearlyReturnRepository.Filter(x => x.ControlShipInfo.Organization == PortalContext.CurrentUser.BranchInfoIdentity && x.YearlyReturnType==returnType).Include(x => x.YearlyReturnDetails).Include(x => x.ControlShipInfo).Include(x => x.CommonCode).OrderBy(x => x.ShipId).ThenBy(x => x.DeskNoId).ToList();

            }
            else if (PortalContext.CurrentUser.RoleId == 1)
            {
                yearlyReturns = _yearlyReturnRepository.Filter(x => x.ShipId == PortalContext.CurrentUser.ShipId && x.YearlyReturnType == returnType).Include(x => x.YearlyReturnDetails).Include(x => x.ControlShipInfo).Include(x => x.CommonCode).OrderBy(x => x.ShipId).ThenBy(x => x.DeskNoId).ToList();

            }
            else
            {
                yearlyReturns = _yearlyReturnRepository.Filter(x => x.YearlyReturnType == returnType).Include(x => x.YearlyReturnDetails).Include(x => x.ControlShipInfo).Include(x => x.CommonCode).OrderBy(x => x.ShipId).ThenBy(x => x.DeskNoId).ToList();

            }
            return yearlyReturns;
        }
        public List<YearlyReturn> GetAllYearlyReturnByShipId(long shipId)
        {
            List<YearlyReturn> yearlyReturns = _yearlyReturnRepository.Filter(x => x.ShipId == shipId).Include(x => x.YearlyReturnDetails).Include(x => x.ControlShipInfo).Include(x => x.CommonCode).OrderBy(x => x.ShipId).ThenBy(x => x.DeskNoId).ToList();
            return yearlyReturns;
        }
        public List<DefectMachinary> GetDefectMachinary(List<MonthlyReturn> monthlyReturns)
        {
            List<long> a = new List<long>();
            foreach (var item in monthlyReturns)
            {
                a.Add(item.MonthlyReturnId);
            }

            //var query = db.DefectMachinaries.Where(x => a.Contains(x.MonthlyReturnId));

            List<DefectMachinary> DefectMachinary = _defectMachinaryRepository.Filter(x => a.Contains(x.MonthlyReturnId)).Include(x => x.MachineryInfo).ToList();

            return DefectMachinary;
        }

        public List<QuaterlyConsumption> GetQuaterlyConsumptionsByQuaterlyReturnId(long QuaterlyReturnId)
        {
            List<QuaterlyConsumption> quaterlyConsumptions = _quaterlyConsumptionRepository.Filter(x => x.QuaterlyReturnId == QuaterlyReturnId).Include(x => x.MachineryInfo).Include(x => x.CommonCode).ToList();
            return quaterlyConsumptions;
        }
        public List<QuaterlyMainEnginesGeneratorsRunningHour> GetQuaterlyMainEnginesGeneratorsRunningHoursByQuaterlyReturnId(long QuaterlyReturnId)
        {
            List<QuaterlyMainEnginesGeneratorsRunningHour> quaterlyMainEnginesGeneratorsRunningHours = _quaterlyMainEnginesGeneratorsRunningHourRepository.Filter(x => x.QuaterlyReturnId == QuaterlyReturnId).Include(x => x.MachineryInfo).ToList();
            return quaterlyMainEnginesGeneratorsRunningHours;
        }
        public List<MachinaryRunningHour> GetMachinaryRunningHours(List<MonthlyReturn> monthlyReturns)
        {

            List<long> a = new List<long>();
            foreach (var item in monthlyReturns)
            {
                a.Add(item.MonthlyReturnId);
            }
            List<MachinaryRunningHour> models = _machinaryRunningHourRepository.Filter(x => a.Contains(x.MonthlyReturnId)).Include(x => x.MachineryInfo).ToList();
            return models;
        }

        public List<POLExpenseInfo> GetPOLExpenseInfos(List<MonthlyReturn> monthlyReturns)
        {
            List<long> a = new List<long>();
            foreach (var item in monthlyReturns)
            {
                a.Add(item.MonthlyReturnId);
            }
            List<POLExpenseInfo> models = _polExpenseInfoRepository.Filter(x => a.Contains(x.MonthlyReturnId)).Include(x => x.CommonCode).ToList();
            return models;
        }

        public List<ReturnReportNo> returnReportNoByParameter(string returnName)
        {
            return db.ReturnReportNoes.Where(x => x.ReturnName == returnName).OrderBy(x => x.ReturnReportNoId).ToList();
        }
        public List<ReturnReportYear> returnReportYears()
        {
            return db.ReturnReportYears.OrderBy(x => x.Name).ToList();

        }

        public string MonthlyReturnActionStatus(string id)
        {
            var monthlyReturnId = Convert.ToInt64(id);
            var query = $"UPDATE MonthlyReturn SET MonthlyReturnStatus={1} where MonthlyReturnId={monthlyReturnId} and  MonthlyReturnSendStatus={ 1 }";
            int noOfRowUpdated = db.Database.ExecuteSqlCommand(query);
            if (noOfRowUpdated > 0)
            {
                return "Succesfully Granted";
            }
            else
            {
                return "Monthly Return can't be Granted ";
            }
        }
        public string QuaterlyReturnActionStatus(string id)
        {
            var quaterlyReturnId = Convert.ToInt64(id);
            var query = $"UPDATE QuaterlyReturn SET QuaterlyReturnStatus={1} where QuaterlyReturnId={quaterlyReturnId} and  QuaterlyReturnSendStatus={ 1 }";
            int noOfRowUpdated = db.Database.ExecuteSqlCommand(query);
            if (noOfRowUpdated > 0)
            {
                return "Succesfully Granted";
            }
            else
            {
                return "Quaterly Return can't be Granted ";
            }
        }

        private class YearlyReturnViewModel
        {
            public YearlyReturnViewModel()
            {
                YearlyReturnDetails = new List<YearlyReturnDetailViewModel>();
            }

            public long YearlyReturnId { get; set; }
            public Nullable<int> YearId { get; set; }
            public Nullable<long> DeskNoId { get; set; }
            public string FrameNo { get; set; }
            public Nullable<long> ShipId { get; set; }

            public virtual ControlShipInfo ControlShipInfo { get; set; }
            public virtual ICollection<YearlyReturnDetailViewModel> YearlyReturnDetails { get; set; }
        }

        private class YearlyReturnDetailViewModel
        {
            public YearlyReturnDetailViewModel()
            {
            }
            public long YearlyReturnDetailsId { get; set; }
            public Nullable<long> YearlyReturnId { get; set; }
            public string CompartmentAndLocation { get; set; }
            public Nullable<System.DateTime> ExmAndAirPreTestDate { get; set; }
            public string Plates { get; set; }
            public string Frames { get; set; }
            public string Rivets { get; set; }
            public string Cements { get; set; }
            public string PaintsDescription { get; set; }
            public string PaintsState { get; set; }
            public string SuctionAndDischargeLineWhetherClear { get; set; }
            public string SuctionAndDischargeLineState { get; set; }
            public string SuctionAndDischargeNonReturnValvesWhetherState { get; set; }
            public string SuctionAndDischargeNonReturnValvesState { get; set; }
            public string WatertightDoorWhetherTested { get; set; }
            public string WatertightDoorState { get; set; }
            public string DefectsDescription { get; set; }
            public string DefectsActionTaken { get; set; }
        }
    }

}
