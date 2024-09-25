using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using RMS.BLL.IManager;
using RMS.DAL.IRepository;
using RMS.DAL.Repository;
using RMS.Model;
using RMS.Utility;

namespace RMS.BLL.Manager
{
    public class BranchInfoManager : BaseManager, IBranchInfoManager
    {
        private IBranchInfoRepository _branchInfoRepository;
        private IZoneInfoRepository _zoneInfoRepository;
        private IBankInfoRepository _bankInfoRepository;
        private IControlShipInfoRepository _controlShipInfoRepository;
        public BranchInfoManager()
        {
            _branchInfoRepository = new BranchInfoRepository(Instance.Context);
            _zoneInfoRepository = new ZoneInfoRepository(Instance.Context);
            _bankInfoRepository = new BankInfoRepository(Instance.Context);
            _controlShipInfoRepository = new ControlShipInfoRepository(Instance.Context);
        }

        public List<BranchInfo> GetBankList()
        {
            return _branchInfoRepository.Filter(x => x.BranchLevel == "1").ToList();
        }
        public int TotalOpl(long branchId)
        {
            int opl = _controlShipInfoRepository.Filter(x => x.Status == 941 && x.Organization==branchId && x.ShipType== 1056).Count();
            return opl;
        }
        public int TotalNonOpl(long branchId)
        {
            int nonOpl = _controlShipInfoRepository.Filter(x => x.Status == 942 && x.Organization == branchId && x.ShipType == 1056).Count();
            return nonOpl;
        }
        public List<BranchInfo> GetDistrictList(string bankCode)
        {
            return _branchInfoRepository.Filter(x => x.FirstLevel == bankCode && x.BranchLevel == "2").ToList();
        }

        public List<BranchInfo> GetBranchList(string districtCode)
        {
            return _branchInfoRepository.Filter(x => x.SecondLevel == districtCode && x.BranchLevel == "3").ToList();
        }
        public List<BranchInfo> GetBranchList()
        {
            return _branchInfoRepository.Filter(x => x.BranchLevel == "3").ToList();
        }
        public List<ZoneInfo> ZoneInfoes()
        {
            return _zoneInfoRepository.All().ToList();
        }

        public ZoneInfo FineOneZoneInfo(long zoneInfoIdenity)
        {
            return _zoneInfoRepository.FindOne(x => x.ZoneInfoIdentity == zoneInfoIdenity);
        }
        public ResponseModel SaveZoneInfo(ZoneInfo model)
        {
            ZoneInfo oldData = _zoneInfoRepository.FindOne(x => x.ZoneCode == model.ZoneCode);
            if (oldData == null)
            {
                var obj = new ZoneInfo()
                {
                    ZoneCode = model.ZoneCode,
                    ZoneName = model.ZoneName,
                    BankCode = PortalContext.CurrentUser.BankCode
                };
                ResponseModel.ResponsStatus = _zoneInfoRepository.Save(obj);
                ResponseModel.Message = "Information has been saved.";
            }
            else
            {
                oldData.ZoneCode = model.ZoneCode;
                oldData.ZoneName = model.ZoneName;
                oldData.BankCode = PortalContext.CurrentUser.BankCode;
                ResponseModel.ResponsStatus = _zoneInfoRepository.Edit(oldData);
                ResponseModel.Message = "Information has been updated.";
            }
            return ResponseModel;
        }

        public ResponseModel DeleteZoneInfo(long zoneInfoIdentity)
        {
            ZoneInfo oldData = _zoneInfoRepository.FindOne(x => x.ZoneInfoIdentity == zoneInfoIdentity);
            if (oldData != null)
            {
                ResponseModel.ResponsStatus = _zoneInfoRepository.Delete(x => x.ZoneInfoIdentity == zoneInfoIdentity);
                ResponseModel.ResponsStatus = 1;
                ResponseModel.Message = "Data has been deleted.";
            }
            else
            {
                ResponseModel.Message = "Data is not deleted.";
            }
            return ResponseModel;
        }

        public BranchInfo FineOneByBranchInfoIdentity(long? branchInfoIdentiy)
        {
            return _branchInfoRepository.FindOne(x => x.BranchInfoIdentity == branchInfoIdentiy);
        }
        public string BranchName(long? BranchIdentity)
        {
            return _branchInfoRepository.FindOne(x => x.BranchInfoIdentity == BranchIdentity).BranchName.ToString();
        }

        public ResponseModel SaveBankInfo(BranchInfo model)
        {
            BranchInfo oldData = _branchInfoRepository.FindOne(x => x.BranchCode == model.BranchCode);
            if (oldData == null)
            {
                var obj = new BranchInfo()
                {
                    BranchCode = model.BranchCode,
                    BranchName = model.BranchName,
                    ContactPerson = model.ContactPerson ?? "",
                    Address = model.Address ?? "",
                    Telephone = model.Telephone ?? "",
                    Cellphone = model.Cellphone ?? "",
                    Email = model.Email ?? "",
                    Fax = model.Fax ?? "",
                    CountryCode = PortalContext.CurrentUser.CountryCode,
                    ZoneInfoIdentity = 4,
                    BranchLevel = "1",
                    FirstLevel = model.BranchCode,
                    SecondLevel = "",
                    ThirdLevel = "",
                    FourthLevel = "",
                    FifthLevel = "",
                    BranchType = "0",
                    NativeBranchCode = model.NativeBranchCode ?? "",
                    CurrencyCode = PortalContext.CurrentUser.CurrencyCode,
                    OwnBranchCode = model.OwnBranchCode ?? "",
                    UserId = PortalContext.CurrentUser.UserId,
                    LastUpdate = DateTime.Now,
                    ServerName = model.ServerName ?? "",
                    AccountNoFC = model.AccountNoFC ?? "",
                    AccountNoLC = model.AccountNoLC ?? "",
                    MinimumCoverFund = model.MinimumCoverFund,
                    WorkingTimeFrom = model.WorkingTimeFrom ?? 10,
                    WorkingTimeTo = model.WorkingTimeTo ?? 18,
                    MinimumNRDBalance = model.MinimumNRDBalance
                };
                ResponseModel.ResponsStatus = _branchInfoRepository.Save(obj);
                ResponseModel.Message = "Information has been added.";
            }
            else
            {
                oldData.BranchName = model.BranchName;
                ResponseModel.ResponsStatus = _branchInfoRepository.Edit(oldData);
                ResponseModel.Message = "Information is updated.";
            }
            return ResponseModel;
        }

        public ResponseModel DeleteBank(long branchInfoIdentity)
        {
            BranchInfo oldData = _branchInfoRepository.FindOne(x => x.BranchInfoIdentity == branchInfoIdentity);
            if (oldData != null)
            {
                BranchInfo isExist = _branchInfoRepository.FindOne(x => x.FirstLevel == oldData.BranchCode && x.BranchLevel == "2");
                if (isExist == null)
                {
                    ResponseModel.ResponsStatus = _branchInfoRepository.Delete(x => x.BranchInfoIdentity == branchInfoIdentity);
                    ResponseModel.ResponsStatus = 1;
                    ResponseModel.Message = "Information has been deleted.";
                }
                else
                {
                    ResponseModel.Message = "Information is not deleted, there are some division are available.";
                }
            }
            else
            {
                ResponseModel.Message = "Information is not deleted.";
            }
            return ResponseModel;
        }

        public ResponseModel SaveDistrictInfo(BranchInfo model)
        {
            BranchInfo oldData = _branchInfoRepository.FindOne(x => x.BranchCode == model.BranchCode);
            if (model.CountryCode != 112587)
            {
                model.BranchType = "9";
            }
            if (oldData == null)
            {
                var obj = new BranchInfo()
                {
                    BranchCode = model.BranchCode,
                    BranchName = model.BranchName,
                    BBDistrict = model.BBDistrict,
                    ContactPerson = model.ContactPerson ?? "",
                    Address = model.Address ?? "",
                    Telephone = model.Telephone ?? "",
                    Cellphone = model.Cellphone ?? "",
                    Email = model.Email ?? "",
                    Fax = model.Fax ?? "",
                    CountryCode = PortalContext.CurrentUser.CountryCode,
                    ZoneInfoIdentity = 4,
                    BranchLevel = "2",
                    FirstLevel = model.FirstLevel,
                    SecondLevel = model.BranchCode,
                    ThirdLevel = "",
                    FourthLevel = "",
                    FifthLevel = "",
                    BranchType = "0",
                    NativeBranchCode = model.NativeBranchCode ?? "",
                    CurrencyCode = PortalContext.CurrentUser.CurrencyCode,
                    OwnBranchCode = model.OwnBranchCode ?? "",
                    UserId = PortalContext.CurrentUser.UserId,
                    LastUpdate = DateTime.Now,
                    ServerName = model.ServerName ?? "",
                    AccountNoFC = model.AccountNoFC ?? "",
                    AccountNoLC = model.AccountNoLC ?? "",
                    MinimumCoverFund = model.MinimumCoverFund,
                    WorkingTimeFrom = model.WorkingTimeFrom ?? 10,
                    WorkingTimeTo = model.WorkingTimeTo ?? 18,
                    MinimumNRDBalance = model.MinimumNRDBalance
                };
                ResponseModel.ResponsStatus = _branchInfoRepository.Save(obj);
                ResponseModel.Message = "Information has been added.";
            }
            else
            {
                oldData.BranchName = model.BranchName;
                ResponseModel.ResponsStatus = _branchInfoRepository.Edit(oldData);
                ResponseModel.Message = "Information is updated.";
            }
            return ResponseModel;
        }

        public ResponseModel DeleteDistrict(long branchInfoIdentity)
        {
            BranchInfo oldData = _branchInfoRepository.FindOne(x => x.BranchInfoIdentity == branchInfoIdentity);
            if (oldData != null)
            {
                BranchInfo isExist = _branchInfoRepository.FindOne(x => x.SecondLevel == oldData.BranchCode && x.BranchLevel == "3");
                if (isExist == null)
                {
                    ResponseModel.ResponsStatus = _branchInfoRepository.Delete(x => x.BranchInfoIdentity == branchInfoIdentity);
                    ResponseModel.ResponsStatus = 1;
                    ResponseModel.Message = "Information has been deleted.";
                }
                else
                {
                    ResponseModel.Message = "Information is not deleted, there are some branches are available.";
                }
            }
            else
            {
                ResponseModel.Message = "Information is not deleted.";
            }
            return ResponseModel;
        }

        public List<BranchInfo> GetBranchListByBankAndDistrict(string bankCode, string districtCode)
        {
            return _branchInfoRepository.Filter(x => x.FirstLevel == bankCode && x.SecondLevel == districtCode && x.BranchLevel == "3").ToList();
        }

        public ResponseModel SaveBranchInfo(BranchInfo model)
        {
            BranchInfo oldData = _branchInfoRepository.FindOne(x => x.BranchCode == model.BranchCode);
            if (model.CountryCode != 112587)
            {
                model.BranchType = "9";
            }
            if (oldData == null)
            {

                var obj = new BranchInfo()
                {
                    BranchCode = model.BranchCode,
                    BranchName = model.BranchName,
                    ContactPerson = model.ContactPerson ?? "",
                    Address = model.Address ?? "",
                    Telephone = model.Telephone ?? "",
                    Cellphone = model.Cellphone ?? "",
                    Email = model.Email ?? "",
                    Fax = model.Fax ?? "",
                    CountryCode = model.CountryCode,
                    ZoneInfoIdentity = model.ZoneInfoIdentity,
                    BranchLevel = "3",
                    FirstLevel = model.FirstLevel,
                    SecondLevel = model.SecondLevel,
                    ThirdLevel = model.BranchCode,
                    FourthLevel = "",
                    FifthLevel = "",
                    BranchType = "0",
                    NativeBranchCode = model.NativeBranchCode ?? "",
                    CurrencyCode = model.CurrencyCode,
                    OwnBranchCode = model.OwnBranchCode ?? "",
                    UserId = PortalContext.CurrentUser.UserId,
                    LastUpdate = DateTime.Now,
                    ServerName = model.ServerName ?? "",
                    AccountNoFC = model.AccountNoFC ?? "",
                    AccountNoLC = model.AccountNoLC ?? "",
                    MinimumCoverFund = model.MinimumCoverFund,
                    WorkingTimeFrom = model.WorkingTimeFrom ?? 10,
                    WorkingTimeTo = model.WorkingTimeTo ?? 18,
                    MinimumNRDBalance = model.MinimumNRDBalance,
                    BBDistrict = model.BBDistrict
                };
                ResponseModel.ResponsStatus = _branchInfoRepository.Save(obj);
                ResponseModel.Message = "A New Branch information has been added.";
            }
            else
            {
                oldData.BranchName = model.BranchName;
                oldData.ContactPerson = model.ContactPerson ?? "";
                oldData.Address = model.Address ?? "";
                oldData.Telephone = model.Telephone ?? "";
                oldData.Cellphone = model.Cellphone ?? "";
                oldData.Email = model.Email ?? "";
                oldData.Fax = model.Fax ?? "";
                oldData.CountryCode = model.CountryCode;
                oldData.ZoneInfoIdentity = model.ZoneInfoIdentity ?? 1;
                oldData.BranchLevel = "3";
                oldData.FirstLevel = model.FirstLevel;
                oldData.SecondLevel = model.SecondLevel;
                oldData.ThirdLevel = model.BranchCode;
                oldData.FourthLevel = "";
                oldData.FifthLevel = "";
                oldData.BranchType = "0";
                oldData.NativeBranchCode = model.NativeBranchCode ?? "";
                oldData.CurrencyCode = model.CurrencyCode;
                oldData.OwnBranchCode = model.OwnBranchCode ?? "";
                oldData.UserId = PortalContext.CurrentUser.UserId;
                oldData.LastUpdate = DateTime.Now;
                oldData.ServerName = model.ServerName ?? "";
                oldData.AccountNoFC = model.AccountNoFC ?? "";
                oldData.AccountNoLC = model.AccountNoLC ?? "";
                oldData.MinimumCoverFund = model.MinimumCoverFund;
                oldData.WorkingTimeFrom = model.WorkingTimeFrom ?? 10;
                oldData.WorkingTimeTo = model.WorkingTimeTo ?? 18;
                oldData.MinimumNRDBalance = model.MinimumNRDBalance;
                oldData.BBDistrict = model.BBDistrict;
                ResponseModel.ResponsStatus = _branchInfoRepository.Edit(oldData);
                ResponseModel.Message = "Branch information is updated.";
            }
            return ResponseModel;
        }

        public BranchInfo FineOneByBranchCode(string branchCode)
        {
            return _branchInfoRepository.FindOne(x => x.BranchCode == branchCode);
        }

        public List<BranchInfo> GetExchangeHouseDivisionList(string bankCode)
        {
            return _branchInfoRepository.Filter(x => x.FirstLevel == bankCode && x.BranchLevel == "2" && x.BranchType == "9").ToList();
        }


        public List<BranchInfo> GetSubBranchInfoes(string branchCode)
        {
            return _branchInfoRepository.Filter(x => x.ThirdLevel == branchCode && x.BranchLevel == "4" && x.BranchType == "9").ToList();
        }
        public List<ControlShipInfo> GetControlShipInfoes()
        {
            return _controlShipInfoRepository.Filter(x => x.ControlLevel == 1).ToList();
        }
        public ResponseModel SaveSubBranchInfo(BranchInfo model)
        {
            BranchInfo oldData = _branchInfoRepository.FindOne(x => x.BranchCode == model.BranchCode);
            if (oldData == null)
            {
                var obj = new BranchInfo()
                {
                    BranchCode = model.BranchCode,
                    BranchName = model.BranchName,
                    ContactPerson = model.ContactPerson ?? "",
                    Address = model.Address ?? "",
                    Telephone = model.Telephone ?? "",
                    Cellphone = model.Cellphone ?? "",
                    Email = model.Email ?? "",
                    Fax = model.Fax ?? "",
                    CountryCode = model.CountryCode,
                    ZoneInfoIdentity = model.ZoneInfoIdentity ?? 1,
                    BranchLevel = "4",
                    FirstLevel = model.FirstLevel,
                    SecondLevel = model.SecondLevel,
                    ThirdLevel = model.ThirdLevel,
                    FourthLevel = model.BranchCode,
                    FifthLevel = "",
                    BranchType = "9",
                    NativeBranchCode = model.NativeBranchCode ?? "",
                    CurrencyCode = model.CurrencyCode,
                    OwnBranchCode = model.OwnBranchCode ?? "",
                    UserId = PortalContext.CurrentUser.UserId,
                    LastUpdate = DateTime.Now,
                    ServerName = model.ServerName ?? "",
                    AccountNoFC = model.AccountNoFC ?? "",
                    AccountNoLC = model.AccountNoLC ?? "",
                    MinimumCoverFund = model.MinimumCoverFund,
                    WorkingTimeFrom = model.WorkingTimeFrom ?? 10,
                    WorkingTimeTo = model.WorkingTimeTo ?? 18,
                    MinimumNRDBalance = model.MinimumNRDBalance
                };
                ResponseModel.ResponsStatus = _branchInfoRepository.Save(obj);
                ResponseModel.Message = "A New Sub Branch information has been added.";
            }
            else
            {
                oldData.BranchName = model.BranchName;
                oldData.ContactPerson = model.ContactPerson ?? "";
                oldData.Address = model.Address ?? "";
                oldData.Telephone = model.Telephone ?? "";
                oldData.Cellphone = model.Cellphone ?? "";
                oldData.Email = model.Email ?? "";
                oldData.Fax = model.Fax ?? "";
                oldData.CountryCode = model.CountryCode;
                oldData.ZoneInfoIdentity = model.ZoneInfoIdentity ?? 1;
                oldData.BranchLevel = "4";
                oldData.FirstLevel = model.FirstLevel;
                oldData.SecondLevel = model.SecondLevel;
                oldData.ThirdLevel = model.ThirdLevel;
                oldData.FourthLevel = model.BranchCode;
                oldData.FifthLevel = "";
                oldData.BranchType = "9";
                oldData.NativeBranchCode = model.NativeBranchCode ?? "";
                oldData.CurrencyCode = model.CurrencyCode;
                oldData.OwnBranchCode = model.OwnBranchCode ?? "";
                oldData.UserId = PortalContext.CurrentUser.UserId;
                oldData.LastUpdate = DateTime.Now;
                oldData.ServerName = model.ServerName ?? "";
                oldData.AccountNoFC = model.AccountNoFC ?? "";
                oldData.AccountNoLC = model.AccountNoLC ?? "";
                oldData.MinimumCoverFund = model.MinimumCoverFund;
                oldData.WorkingTimeFrom = model.WorkingTimeFrom ?? 10;
                oldData.WorkingTimeTo = model.WorkingTimeTo ?? 18;
                oldData.MinimumNRDBalance = model.MinimumNRDBalance;
                ResponseModel.ResponsStatus = _branchInfoRepository.Edit(oldData);
                ResponseModel.Message = "Sub Branch information is updated.";
            }
            return ResponseModel;
        }

        public ResponseModel DeleteBranchInfo(long branchInfoIdentity)
        {
            BranchInfo oldData = _branchInfoRepository.FindOne(x => x.BranchInfoIdentity == branchInfoIdentity);
            if (oldData != null)
            {
                BranchInfo isExist =
                    _branchInfoRepository.FindOne(x => x.ThirdLevel == oldData.BranchCode && x.BranchLevel == "4");
                if (isExist == null)
                {
                    ResponseModel.ResponsStatus =
                        _branchInfoRepository.Delete(x => x.BranchInfoIdentity == branchInfoIdentity);
                    ResponseModel.ResponsStatus = 1;
                    ResponseModel.Message = "Branch information has been deleted.";
                }
                else
                {
                    ResponseModel.Message =
                        "Branch information is not deleted, becasue of there are some sub branch are available in this branch.";
                }
            }
            else
            {
                ResponseModel.Message = "Invalid branch code.";
            }
            return ResponseModel;
        }

        public ResponseModel DeleteSubBranchInfo(long branchInfoIdentity)
        {
            BranchInfo oldData = _branchInfoRepository.FindOne(x => x.BranchInfoIdentity == branchInfoIdentity);
            if (oldData != null)
            {
                ResponseModel.ResponsStatus = _branchInfoRepository.Delete(x => x.BranchInfoIdentity == branchInfoIdentity);
                ResponseModel.ResponsStatus = 1;
                ResponseModel.Message = "Sub Branch information has been deleted.";
            }
            else
            {
                ResponseModel.Message = "Invalid branch code.";
            }
            return ResponseModel;
        }

        public List<BranchInfo> GetFourthLevelList()
        {
            return _branchInfoRepository.Filter(x => x.BranchLevel == "4" && x.FirstLevel == PortalContext.CurrentUser.BankCode).ToList();
        }

        public List<BranchInfo> ExchangeHouseList()
        {
            return _branchInfoRepository.Filter(x => x.CountryCode != 112587 && x.BranchLevel == "4" && x.FirstLevel == PortalContext.CurrentUser.BankCode).ToList();
        }

        //public BankInfo GetBranchInfoIdentity(string bankName, string branchInfo, string district, string routingNumber)
        //{
        //    long branchInfoIdentity = 0;
        //    var bankInfo = new BankInfo();
        //    var bankList = new List<BranchNamePercentage>();
        //    var disttirctList = new List<BranchNamePercentage>();
        //    var branchList = new List<BranchNamePercentage>();
        //    string databaseBankCode = "";
        //    string databaseDistrictCode = "";
        //    string databaseBranchCode = "";
        //    if (routingNumber.Trim() != string.Empty)
        //    {
        //        routingNumber = routingNumber.PadLeft(9, '0');
        //        bankInfo = _bankInfoRepository.FindOne(x => x.RoutingNo == routingNumber);
        //        if (bankInfo != null)
        //        {
        //            bankInfo.BranchAccuracyPercentage = 100;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //    //else if (bankName != string.Empty && district !=string.Empty && branchInfo != string.Empty)
        //    else if (bankName != string.Empty && branchInfo != string.Empty)
        //    {
        //        //Bank Searching
        //        var bankInfoes = _branchInfoRepository.Filter(x => x.BranchLevel == "1");
        //        foreach (var item in bankInfoes)
        //        {
        //            var obj = new BranchNamePercentage();
        //            double percentage = StringCompare(bankName.ToLower(), item.BranchName.ToLower(),"bank");
        //            obj.BranchCode = item.BranchCode;
        //            obj.BranchName = item.BranchName;
        //            obj.Percantage = percentage;
        //            bankList.Add(obj);
        //        }
        //        if (bankInfoes.Any())
        //        {
        //            double maxBankNamePerCentage = bankList.Max(x => x.Percantage);
        //            databaseBankCode = bankList.FirstOrDefault(x => x.Percantage == maxBankNamePerCentage).BranchCode;
        //            //District Searching

        //            var districtInfos = _branchInfoRepository.Filter(x => x.FirstLevel == databaseBankCode && x.BranchLevel == "2" && x.CountryCode == 112587);
        //            foreach (var item in districtInfos)
        //            {
        //                var obj = new BranchNamePercentage();
        //                double percentage = StringCompare(district.Trim().ToLower(), item.BranchName.Trim().ToLower(),"district");
        //                obj.BranchCode = item.BranchCode;
        //                obj.BranchName = item.BranchName;
        //                obj.Percantage = percentage;
        //                disttirctList.Add(obj);
        //            }
        //            if (districtInfos.Any())
        //            {
        //                double maxDistrictNamePerCentage = disttirctList.Max(x => x.Percantage);
        //                databaseDistrictCode = disttirctList.FirstOrDefault(x => x.Percantage == maxDistrictNamePerCentage).BranchCode;
        //                //Branch Searching
        //                var branchInfoes = _branchInfoRepository.Filter(x => x.SecondLevel == databaseDistrictCode && x.BranchLevel == "3" && x.CountryCode == 112587);
        //                foreach (var item in branchInfoes)
        //                {
        //                    var obj = new BranchNamePercentage();
        //                    double percentage = StringCompare(branchInfo.ToLower(), item.BranchName.ToLower(),"branch");
        //                    obj.BranchCode = item.BranchCode;
        //                    obj.BranchName = item.BranchName;
        //                    obj.Percantage = percentage;
        //                    branchList.Add(obj);
        //                }
        //                if (branchInfoes.Any())
        //                {
        //                    double maxBranchNamePerCentage = branchList.Max(x => x.Percantage);
        //                    databaseBranchCode = branchList.OrderBy(x=>x.BranchName).FirstOrDefault(x => x.Percantage == maxBranchNamePerCentage).BranchCode;
        //                    bankInfo = maxBankNamePerCentage < 10 ? _bankInfoRepository.FindOne(x => x.Code == "999999999") : _bankInfoRepository.FindOne(x => x.Code == databaseBranchCode);
        //                    bankInfo.BranchAccuracyPercentage = Convert.ToDecimal(maxBranchNamePerCentage);
        //                }
        //                else
        //                {
        //                    return null;
        //                }
        //            }
        //        }
        //    }

        //    return bankInfo;
        //}

        public BankInfo GetBankInfo(long respondingBranch)
        {
            return _bankInfoRepository.FindOne(x => x.BranchInfoIdentity == respondingBranch);
        }

        public List<BankInfo> GetBankListFromBankInfo()
        {
            return _bankInfoRepository.Filter(x => x.BranchLevel == "1").ToList();
        }
        public List<BankInfo> GetOrgFromBankInfo()
        {
            return _bankInfoRepository.Filter(x => x.BranchLevel == "3").ToList();
        }
        public List<BankInfo> GetDistrictListFromBankInfo(string bankCode)
        {
            return
                _bankInfoRepository.Filter(
                    x => x.BankCode == bankCode && x.BranchLevel == "2" && x.CountryCode == 112587).ToList();
        }

        public List<BranchInfo> GetOrganizationList()
        {
            return
                _branchInfoRepository.Filter(x =>x.BranchLevel=="3").ToList();
        }
        public List<BranchInfo> GetBranchListByBankCode()
        {
            return
                _branchInfoRepository.Filter(
                    x =>x.FirstLevel == PortalContext.CurrentUser.BankCode && x.BranchLevel == "3").ToList();
        }

        public BranchInfo FindExchangeByLCAccountNo(string accountLC)
        {
            return _branchInfoRepository.FindOne(x => x.AccountNoLC == accountLC);
        }

        public ZoneInfo FineOneZoneInfoByCode(string zoneCode)
        {
            return _zoneInfoRepository.FindOne(x => x.ZoneCode == zoneCode);
        }

        public List<BranchInfo> GetDistrictListByIdentity(string bankCodeIdentity)
        {
            long bankIdentity = Convert.ToInt32(bankCodeIdentity);
            string bankCode = _branchInfoRepository.FindOne(x => x.BranchInfoIdentity == bankIdentity).BranchCode;
            return _branchInfoRepository.Filter(x => x.BranchLevel == "2" && x.FirstLevel == bankCode).ToList();
        }

        public List<BranchInfo> GetBranchListByDistrictIdentity(int districtIdentity)
        {
            string districtCode = _branchInfoRepository.FindOne(x => x.BranchInfoIdentity == districtIdentity).BranchCode;
            return _branchInfoRepository.Filter(x => x.BranchLevel == "3" && x.SecondLevel == districtCode).ToList();

        }

        static double StringCompare(string a, string b,string type)
        {
            a = a.Trim();
            b = b.Trim();

            a = a.Replace("bank", "");
            a = a.Replace("bangladesh", "");
            a = a.Replace("limited", "");
            a = a.Replace("ltd", "");
            a = a.Replace("-", "");
            a = a.Replace(",","");
            a = a.Replace("branch", "");
            a = a.Replace("br","");
            a = a.Replace("br.","");
            a = a.Replace("district", "");
            a = a.Replace("dist", "");
            a = a.Replace(".","");
            a = a.Replace("the", "");
            a = a.Replace(" ","");
            a = a.Replace("zone", "");
            a = a.Replace("(","");
            a = a.Replace(")", "");
            a = a.Replace("bazar", "");
            a = a.Replace("'", "");

            b = b.Replace("bank", "");
            b = b.Replace("bangladesh", "");
            b = b.Replace("limited", "");
            b = b.Replace("ltd", "");
            b = b.Replace("-", "");
            b = b.Replace(",", "");
            b = b.Replace("branch", "");
            b = b.Replace("br", "");
            b = b.Replace("br.", "");
            b = b.Replace("district", "");
            b = b.Replace("dist", "");
            b = b.Replace(".", "");
            b = b.Replace("the", "");
            b = b.Replace(" ", "");
            b = b.Replace("zone", "");
            b = b.Replace("(", "");
            b = b.Replace(")", "");
            b = b.Replace("bazar", "");
            b = b.Replace("'", "");
            a = Regex.Replace(a, @"[0-9\-]", string.Empty);
            b = Regex.Replace(b, @"[0-9\-]", string.Empty);
            if (a == b) //Same string, no iteration needed.
                return 100;
            if ((a.Length == 0) || (b.Length == 0)) //One is empty, second is not
            {
                return 0;
            }
            double maxLen = a.Length > b.Length ? a.Length : b.Length;
            int minLen = a.Length < b.Length ? a.Length : b.Length;
            int sameCharAtIndex = 0;
            int distictBreak = 0;
            for (int i = 0; i < minLen; i++) //Compare char by char
            {
                if (type == "district" || type == "branch")
                {
                    if (a[i] == b[i])
                    {
                        sameCharAtIndex++;
                    }
                    else
                    {
                        distictBreak = distictBreak + 1;
                        if (distictBreak > 1)
                        {
                            break;  
                        }
                    }
                }
                else
                {
                    if (a[i] == b[i])
                    {
                        sameCharAtIndex++;
                    }
                }
            }
            return sameCharAtIndex / maxLen * 100;
        }

    }
}
