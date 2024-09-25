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
    public class MachineryInfoManager : BaseManager, IMachineryInfoManager
    {
        private readonly IMachineryInfoRepository _machineryInfoRepository;
        private readonly IQuaterlyMainEnginesGeneratorsRunningHourRepository _quaterlyMainEnginesGeneratorsRunningHourRepository;
        private readonly ICommonCodeRepository _commonCodeRepository;
        private readonly IMachinaryRunningHourRepository _machinaryRunningHourRepository;
        private readonly IQuaterlyReturnRepository _quaterlyReturnRepository;
        private readonly IMonthlyReturnRepository _monthlyReturnRepository;
        RM_AGBEntities db = new RM_AGBEntities();

        public MachineryInfoManager()
        {

            _machineryInfoRepository = new MachineryInfoRepository(Instance.Context);
            _commonCodeRepository = new CommonCodeRepository(Instance.Context);
            _machinaryRunningHourRepository = new MachinaryRunningHourRepository(Instance.Context);
            _quaterlyMainEnginesGeneratorsRunningHourRepository = new QuaterlyMainEnginesGeneratorsRunningHourRepository(Instance.Context);
            _quaterlyReturnRepository = new QuaterlyReturnRepository(Instance.Context);
            _monthlyReturnRepository = new MonthlyReturnRepository(Instance.Context);

        }

        public List<MachineryInfo> GetAll()
        {
            if (PortalContext.CurrentUser.LoweredRoleName == "S" || PortalContext.CurrentUser.LoweredRoleName == "V")
            {
                return _machineryInfoRepository.Filter(x => x.ControlShipInfoId == PortalContext.CurrentUser.ShipId).Include(x => x.ControlShipInfo).ToList();
            }
            else
            {
                return _machineryInfoRepository.All().Include(x => x.ControlShipInfo).ToList();

            }
        }
        public List<MachineryInfo> GetAllNextTohMohMachinery()
        {
            List<MachineryInfo> NextTohMohMachinery = new List<MachineryInfo>();
            if (PortalContext.CurrentUser.LoweredRoleName == "S" || PortalContext.CurrentUser.LoweredRoleName == "V")
            {
                List<MachineryInfo> machineryInfo = _machineryInfoRepository.Filter(x => x.ControlShipInfoId == PortalContext.CurrentUser.ShipId && (x.TOH > 0 || x.MOH > 0)).Include(x => x.ControlShipInfo).ToList();


                foreach (var item in machineryInfo.ToList())
                {
                    //List<QuaterlyMainEnginesGeneratorsRunningHour> machinaryRunningHour = _quaterlyMainEnginesGeneratorsRunningHourRepository.Filter(x => x.ShipId == item.ControlShipInfoId && x.MachineGeneratorId == item.MachineryInfoIdentity).Select(x=>x).ToList();
                    QuaterlyMainEnginesGeneratorsRunningHour quaterlyMainEnginesGeneratorsRunningHour = new QuaterlyMainEnginesGeneratorsRunningHour();
                    var machinaryRunningHour = _quaterlyMainEnginesGeneratorsRunningHourRepository.Filter(x => x.ShipId == item.ControlShipInfoId && x.MachineGeneratorId == item.MachineryInfoIdentity).GroupBy(a => a)
                        .Select(g => g.Where(a => a.SinceInstallationHr == g.Max(x => x.SinceInstallationHr))).FirstOrDefault();
                    if (machinaryRunningHour != null)
                    {
                        quaterlyMainEnginesGeneratorsRunningHour = machinaryRunningHour.FirstOrDefault();

                        int ReportYear = Convert.ToInt32(_quaterlyReturnRepository.Filter(x => x.QuaterlyReturnId == quaterlyMainEnginesGeneratorsRunningHour.QuaterlyReturnId).Include(x => x.ReturnReportYear).Select(x => x.ReturnReportYear.Name).SingleOrDefault());
                        int ReportNo = _quaterlyReturnRepository.Filter(x => x.QuaterlyReturnId == quaterlyMainEnginesGeneratorsRunningHour.QuaterlyReturnId).Select(x => x.ReturnReportNoId).SingleOrDefault();


                        MachinaryRunningHour afterQuarterlyRunningHr = runningHourMonthlyCalAfterQuarterly(ReportYear, ReportNo, item.ControlShipInfoId);
                        double rh = Convert.ToDouble(machinaryRunningHour.Max(x => x.SinceInstallationHr)) + Convert.ToDouble(afterQuarterlyRunningHr.RunningHr);
                        double rm = Convert.ToDouble(machinaryRunningHour.Max(x => x.SinceInstallationMin)) + Convert.ToDouble(afterQuarterlyRunningHr.RunningMin) / 60;
                        double totalrh = rh + rm;
                        if ((Convert.ToDouble(item.DueTOHTime) - totalrh) < 100 && Convert.ToDouble(item.DueTOHTime) > 0 && totalrh > 0)
                        {
                            item.TotalRunningHr = totalrh;
                            NextTohMohMachinery.Add(item);
                        }
                    }

                }

                return NextTohMohMachinery;
            }
            else
            {
                List<MachineryInfo> machineryInfo = _machineryInfoRepository.Filter(x => (x.TOH > 0 || x.MOH > 0)).Include(x => x.ControlShipInfo).ToList();
                foreach (var item in machineryInfo.ToList())
                {
                    //List<QuaterlyMainEnginesGeneratorsRunningHour> machinaryRunningHour = _quaterlyMainEnginesGeneratorsRunningHourRepository.Filter(x => x.ShipId == item.ControlShipInfoId && x.MachineGeneratorId == item.MachineryInfoIdentity).Select(x=>x).ToList();
                    QuaterlyMainEnginesGeneratorsRunningHour quaterlyMainEnginesGeneratorsRunningHour = new QuaterlyMainEnginesGeneratorsRunningHour();
                    var machinaryRunningHour = _quaterlyMainEnginesGeneratorsRunningHourRepository.Filter(x => x.ShipId == item.ControlShipInfoId && x.MachineGeneratorId == item.MachineryInfoIdentity).GroupBy(a => a)
                        .Select(g => g.Where(a => a.SinceInstallationHr == g.Max(x => x.SinceInstallationHr))).FirstOrDefault();
                    if (machinaryRunningHour != null)
                    {
                        quaterlyMainEnginesGeneratorsRunningHour = machinaryRunningHour.FirstOrDefault();

                        int ReportYear = Convert.ToInt32(_quaterlyReturnRepository.Filter(x => x.QuaterlyReturnId == quaterlyMainEnginesGeneratorsRunningHour.QuaterlyReturnId).Include(x => x.ReturnReportYear).Select(x => x.ReturnReportYear.Name).SingleOrDefault());
                        int ReportNo = _quaterlyReturnRepository.Filter(x => x.QuaterlyReturnId == quaterlyMainEnginesGeneratorsRunningHour.QuaterlyReturnId).Select(x => x.ReturnReportNoId).SingleOrDefault();


                        MachinaryRunningHour afterQuarterlyRunningHr = runningHourMonthlyCalAfterQuarterly(ReportYear, ReportNo, item.ControlShipInfoId);
                        double rh = Convert.ToDouble(machinaryRunningHour.Max(x => x.SinceInstallationHr)) + Convert.ToDouble(afterQuarterlyRunningHr.RunningHr);
                        double rm = Convert.ToDouble(machinaryRunningHour.Max(x => x.SinceInstallationMin)) + Convert.ToDouble(afterQuarterlyRunningHr.RunningMin) / 60;
                        double totalrh = rh + rm;
                        if ((Convert.ToDouble(item.DueTOHTime) - totalrh) < 100 && Convert.ToDouble(item.DueTOHTime) > 0 && totalrh > 0)
                        {
                            item.TotalRunningHr = totalrh;
                            NextTohMohMachinery.Add(item);
                        }
                    }

                }

                return NextTohMohMachinery;

            }
        }

        private MachinaryRunningHour runningHourMonthlyCalAfterQuarterly(int year, int quarterlyNo, long ShipId)
        {
            List<MachinaryRunningHour> MachinaryRunningHours = new List<MachinaryRunningHour>();
            if (quarterlyNo == 13)
            {
                var MonthlyReturnId = _monthlyReturnRepository.Filter(x => x.ShipId == ShipId && x.ReportYearId == year && x.ReportMonthId >= 4 && x.ReportMonthId <= 6).Select(x => x.MonthlyReturnId).ToList();
                MachinaryRunningHours = _machinaryRunningHourRepository.Filter(x => MonthlyReturnId.Contains(x.MonthlyReturnId)).ToList();

            }
            else if (quarterlyNo == 14)
            {
                var MonthlyReturnId = _monthlyReturnRepository.Filter(x => x.ShipId == ShipId && x.ReportYearId == year && x.ReportMonthId >= 7 && x.ReportMonthId <= 9).Select(x => x.MonthlyReturnId).ToList();
                MachinaryRunningHours = _machinaryRunningHourRepository.Filter(x => MonthlyReturnId.Contains(x.MonthlyReturnId)).ToList();
            }
            else if (quarterlyNo == 15)
            {
                var MonthlyReturnId = _monthlyReturnRepository.Filter(x => x.ShipId == ShipId && x.ReportYearId == year && x.ReportMonthId >= 10 && x.ReportMonthId <= 12).Select(x => x.MonthlyReturnId).ToList();
                MachinaryRunningHours = _machinaryRunningHourRepository.Filter(x => MonthlyReturnId.Contains(x.MonthlyReturnId)).ToList();
            }
            else if (quarterlyNo == 16)
            {
                var MonthlyReturnId = _monthlyReturnRepository.Filter(x => x.ShipId == ShipId && x.ReportYearId == year + 1 && x.ReportMonthId >= 1 && x.ReportMonthId <= 3).Select(x => x.MonthlyReturnId).ToList();
                MachinaryRunningHours = _machinaryRunningHourRepository.Filter(x => MonthlyReturnId.Contains(x.MonthlyReturnId)).ToList();
            }
            MachinaryRunningHour MachinaryRunningHour = new MachinaryRunningHour
            {
                RunningHr = MachinaryRunningHours.Sum(x => x.RunningHr),
                RunningMin = MachinaryRunningHours.Sum(x => x.RunningMin)
            };
            return MachinaryRunningHour;
        }


        public List<MachineryInfo> GetMachinaryInfoGroupBy()
        {
            List<MachineryInfo> groupedmachineryInfoList = _machineryInfoRepository.Filter(x => x.MachineryCategoryId == 1079).ToList();
            return groupedmachineryInfoList;
        }
        public List<CommonCode> GetEquipmentType()
        {
            List<CommonCode> EquipmentType = _commonCodeRepository.Filter(x => x.Type == "EquipmentType").ToList();
            return EquipmentType;
        }
        public MachineryInfo GetShip(string id, MachineryInfo machineryInfo)
        {
            var shipid = Convert.ToInt64(id);
            return _machineryInfoRepository.FindOne(x => x.MachineryInfoIdentity == shipid);
        }
        public MachineryInfo GetMachineryById(string id, MachineryInfo machineryInfo)
        {
            var MachineryInfoId = Convert.ToInt64(id);
            return _machineryInfoRepository.Filter(x => x.MachineryInfoIdentity == MachineryInfoId).Include(x => x.ControlShipInfo).Include(x => x.CommonCode).Include(x => x.CommonCode1).FirstOrDefault();
        }



        public List<MachineryInfo> FindOne(long id)
        {
            return _machineryInfoRepository.Filter(x => x.ControlShipInfoId == id).ToList();
        }

        public ResponseModel Saving(MachineryInfo machineryInfo)
        {

            MachineryInfo oldData = _machineryInfoRepository.FindOne(x => x.MachineryInfoIdentity == machineryInfo.MachineryInfoIdentity);
            if (oldData != null)
            {
                //oldData.MachineryInfoIdentity = machineryInfo.ShipId;
                oldData.ControlShipInfoId = machineryInfo.ControlShipInfoId;
                oldData.Machinery = machineryInfo.Machinery;
                oldData.MachineryCategoryId = machineryInfo.MachineryCategoryId;
                oldData.Quantity = machineryInfo.Quantity;
                oldData.Location = machineryInfo.Location;
                oldData.Model = machineryInfo.Model;
                oldData.Power = machineryInfo.Power;
                oldData.PowerUnit = machineryInfo.PowerUnit;
                oldData.RPM = machineryInfo.RPM;
                oldData.jpFiveConsumtion = machineryInfo.jpFiveConsumtion;
                oldData.MachinariesState = machineryInfo.MachinariesState;
                oldData.LifeTimeType = machineryInfo.LifeTimeType;
                oldData.ExpiredLifeDate = machineryInfo.ExpiredLifeDate;
                oldData.ExpiredLifeDateAlerm = machineryInfo.ExpiredLifeDateAlerm;
                oldData.DueTOHTime = machineryInfo.DueTOHTime;
                oldData.DueMOHTime = machineryInfo.DueMOHTime;
                oldData.Pressure = machineryInfo.Pressure;
                oldData.MakerAddress = machineryInfo.MakerAddress;
                oldData.InstallationDate = machineryInfo.InstallationDate;
                oldData.TOH = machineryInfo.TOH;
                oldData.MOH = machineryInfo.MOH;
                oldData.ConsumptionFuel = machineryInfo.ConsumptionFuel;
                oldData.ConsumptionFuelUnit = machineryInfo.ConsumptionFuelUnit;
                oldData.LubOilGrade = machineryInfo.LubOilGrade;
                oldData.ConsuptionLuboil = machineryInfo.ConsuptionLuboil;
                oldData.ConsumptionLuboilUnit = machineryInfo.ConsumptionLuboilUnit;
                oldData.UpdateBy = PortalContext.CurrentUser.UserName;
                oldData.LastUpdate = DateTime.Now;
                _machineryInfoRepository.Edit(oldData);
                ResponseModel.Message = "Machinery information is updated successfully.";
            }
            else
            {

                machineryInfo.Quantity = 1;
                machineryInfo.CreateDate = DateTime.Now;
                machineryInfo.UserId = PortalContext.CurrentUser.UserName;
                machineryInfo.UpdateBy = PortalContext.CurrentUser.UserName;
                machineryInfo.LastUpdate = DateTime.Now;
                machineryInfo.IsVerified = false;
                _machineryInfoRepository.Save(machineryInfo);
                ResponseModel.Message = "Machinery information is saved successfully.";



            }
            return ResponseModel;
        }

        public object Delete(long id)
        {
            return _machineryInfoRepository.Delete(x => x.MachineryInfoIdentity == id);
        }

        public List<MachineryInfo> GetFindValue(string bankCode)
        {
            long ShipId = Convert.ToInt64(bankCode);
            return _machineryInfoRepository.Filter(x => x.ControlShipInfoId == ShipId).ToList();
        }
        public List<MachineryInfo> GetMachinaryInfoByMachineName(string machineryCategoryId)
        {
            long controlShipInfoId = PortalContext.CurrentUser.ShipId;
            long branchInfoIdentity = PortalContext.CurrentUser.BranchInfoIdentity;
            long MachineryCategoryId = Convert.ToInt64(machineryCategoryId);
            if (PortalContext.CurrentUser.LoweredRoleName == "S" || PortalContext.CurrentUser.LoweredRoleName == "V")
            {
                return _machineryInfoRepository.Filter(x => x.EquipmentTypeId == MachineryCategoryId && x.ControlShipInfoId == PortalContext.CurrentUser.ShipId).Include(x => x.ControlShipInfo).ToList();
            }
            else
            {
                if (controlShipInfoId != 0 || PortalContext.CurrentUser.UserName == "dne")
                {
                    return _machineryInfoRepository.Filter(x => x.EquipmentTypeId == MachineryCategoryId).Include(x => x.ControlShipInfo).ToList();
                }
                else
                {
                    return _machineryInfoRepository.Filter(x => x.EquipmentTypeId == MachineryCategoryId && x.ControlShipInfo.Organization==branchInfoIdentity).Include(x => x.ControlShipInfo).ToList();
                }
            }

        }
        public bool ResetTOHMOHTime(decimal rh, long MachineryId)
        {
            MachineryInfo machineryInfo = _machineryInfoRepository.FindOne(x => x.MachineryInfoIdentity == MachineryId);
            machineryInfo.DueTOHTime = 0;
            while(machineryInfo.DueTOHTime >= rh)
            {
                machineryInfo.DueTOHTime = machineryInfo.DueTOHTime + machineryInfo.TOH;
            }
            db.SaveChanges();
            return true;
        }
        public MachineryInfo GetLifeTimeType(long machineId)
        {
            return _machineryInfoRepository.FindOne(x => x.MachineryInfoIdentity == machineId);
        }

        public MachineryInfo GetOne(long machinariesInfoIdentity)
        {
            return _machineryInfoRepository.FindOne(x => x.MachineryInfoIdentity == machinariesInfoIdentity);
        }

        public ResponseModel UpdateStatus(long? description, long? mobilityDescription)
        {
            var Old = _machineryInfoRepository.FindOne(x => x.MachineryInfoIdentity == description);
            if (Old != null)
            {
                //Old.MachinariesState = mobilityDescription;
                Old.Quantity = mobilityDescription;
                Old.LastUpdate = DateTime.Now;
                _machineryInfoRepository.Edit(Old);
                ResponseModel.Message = "Status Update Successfully";
            }
            return ResponseModel;
        }

        public object GetMachinariesInfo(long shipId)
        {

            return
                _machineryInfoRepository.Filter(
                    x => x.ControlShipInfoId == shipId).Select(lg => new
                    {
                        Value = lg.MachineryInfoIdentity,
                        Text = lg.Machinery + lg.MachinaryBangla
                    }).ToList();

        }
        public List<MachineryInfo> GetMachinariesInfoByShipId(long shipId)
        {
            List<MachineryInfo> machineryInfos = _machineryInfoRepository.Filter(x => x.ControlShipInfoId == shipId).ToList();
            return machineryInfos;

        }

        public List<MachineryInfo> UserWiseData(int? verifyType)
        {
            var Mdata = new List<MachineryInfo>();
            var allData = _machineryInfoRepository.Filter(x => x.ControlShipInfoId == PortalContext.CurrentUser.ShipId).Include(x => x.ControlShipInfo).ToList();
            if (verifyType != null)
            {
                Mdata = verifyType == 1006
                    ? allData.Where(x => x.IsVerified == true).ToList()
                    : allData.Where(x => x.IsVerified == false).ToList();
            }
            return Mdata;
        }

        public ResponseModel VerifiedStatusUpdate(List<MachineryInfo> machineryInfos)
        {
            foreach (var item in machineryInfos)
            {
                var oldData = _machineryInfoRepository.FindOne(x => x.MachineryInfoIdentity == item.MachineryInfoIdentity);
                if (oldData != null && oldData.IsVerified != item.IsVerified)
                {
                    oldData.IsVerified = item.IsVerified;
                    oldData.VerifiedBy = PortalContext.CurrentUser.UserName;
                    oldData.VerifiedDate = DateTime.Now.Date;

                    _machineryInfoRepository.Edit(oldData);
                    ResponseModel.Message = "Status Updated Successfully !";
                }
            }
            return ResponseModel;
        }

        public string GetMashineName(long? description)
        {
            return _machineryInfoRepository.FindOne(x => x.MachineryInfoIdentity == description).Machinery;
        }
    }
}
