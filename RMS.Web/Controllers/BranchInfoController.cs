using System;
using System.Linq;
using System.Web.Mvc;
using RMS.BLL.IManager;
using RMS.Model;
using RMS.Utility;
using RMS.Web.Models.ViewModel;

namespace RMS.Web.Controllers
{
    [Authorize]
    public class BranchInfoController : BaseController
    {
        private readonly IBranchInfoManager _branchInfoManager;
        private readonly ICommonCodeManager _commoncodeManager;
        public BranchInfoController(IBranchInfoManager branchInfoManager, ICommonCodeManager commoncodeManager)
        {
            _branchInfoManager = branchInfoManager;
            _commoncodeManager = commoncodeManager;
        }
        public JsonResult GetEmployeesBySearchCharacter(string searchCharacter)
        {
            var employees = "";//_employeeManager.GetEmployeesByCardIdAndName(searchCharacter);
            return Json(employees, JsonRequestBehavior.AllowGet);
        }


        [Authorize(Roles = "PSO,DNE User,Admin Authority,Commanding Officer,OperationAdmin")]
        public ActionResult ZoneIndex(string ZoneInfoIdentity, string zoneCode, BranchInfoViewModel model)
        {
            model.ZoneInfos = _branchInfoManager.ZoneInfoes().OrderBy(x => x.ZoneName).ToList();
            if (ZoneInfoIdentity != null)
            {
                long zoneInfoIdenity = Convert.ToInt32(ZoneInfoIdentity);
                model.ZoneInfo = _branchInfoManager.FineOneZoneInfo(zoneInfoIdenity);
            }
            return View(model);
        }
        [Authorize(Roles = "PSO,DNE User,Admin Authority,Commanding Officer,OperationAdmin")]
        public ActionResult SaveZoneInfo(string save, string update, BranchInfoViewModel model)
        {
            try
            {
                if (save == "Add New")
                {
                    var zoneInfo = _branchInfoManager.FineOneZoneInfoByCode(model.ZoneInfo.ZoneCode);
                    if (zoneInfo != null)
                    {
                        model.IsSucceed = 0;
                        model.Message = "Already exist the code " + model.ZoneInfo.ZoneCode;
                        model.ZoneInfos = _branchInfoManager.ZoneInfoes().OrderBy(x => x.ZoneName).ToList();
                        return View("ZoneIndex", model);
                    }
                }
                ResponseModel response = _branchInfoManager.SaveZoneInfo(model.ZoneInfo);
                model.IsSucceed = response.ResponsStatus;
                model.Message = response.Message;
            }
            catch (Exception ex)
            {
                model.IsSucceed = 0;
                model.Message = "Data is not saved. " + ex.Message;
            }
            model.ZoneInfos = _branchInfoManager.ZoneInfoes().OrderBy(x => x.ZoneName).ToList();
            return View("ZoneIndex", model);
        }
        [Authorize(Roles = "PSO,DNE User,Admin Authority,Commanding Officer,OperationAdmin")]
        public ActionResult DeleteZoneInfo(string ZoneInfoIdentity, string zoneCode, BranchInfoViewModel model)
        {
            try
            {
                if (ZoneInfoIdentity != null)
                {
                    long zoneInfoIdentity = Convert.ToInt32(ZoneInfoIdentity);
                    ResponseModel response = _branchInfoManager.DeleteZoneInfo(zoneInfoIdentity);
                    model.IsSucceed = response.ResponsStatus;
                    model.Message = response.Message;
                }
            }
            catch (Exception ex)
            {
                model.IsSucceed = 0;
                model.Message = "Data is not deleted." + ex.Message;
            }
            
            model.ZoneInfos = _branchInfoManager.ZoneInfoes().OrderBy(x => x.ZoneName).ToList();
            return View("ZoneIndex", model);
        }
        [Authorize(Roles = "PSO,DNE User,Admin Authority,Commanding Officer,OperationAdmin")]
        public ActionResult BankIndex(string BranchInfoIdentity, BranchInfoViewModel model)
        {
            model.BranchInfos = _branchInfoManager.GetBankList().OrderBy(x => x.BranchName).ToList();
            if (BranchInfoIdentity != null)
            {
                long branchInfoIdentiy = Convert.ToInt32(BranchInfoIdentity);
                model.BranchInfo = _branchInfoManager.FineOneByBranchInfoIdentity(branchInfoIdentiy);
            }
            return View(model);
        }
        [Authorize(Roles = "PSO,DNE User,Admin Authority,Commanding Officer,OperationAdmin")]
        public ActionResult SaveBankInfo(string save, string update, BranchInfoViewModel model)
        {
            if (save == "Add New")
            {
                var branchInfo = _branchInfoManager.FineOneByBranchCode(model.BranchInfo.BranchCode);
                if (branchInfo != null)
                {
                    model.IsSucceed = 0;
                    model.Message = "Already exist the code " + model.BranchInfo.BranchCode;
                    model.BranchInfos = _branchInfoManager.GetBankList().OrderBy(x => x.BranchName).ToList();
                    return View("BankIndex", model);
                }
            }
            ResponseModel response = _branchInfoManager.SaveBankInfo(model.BranchInfo);
            model.IsSucceed = response.ResponsStatus;
            model.Message = response.Message;
            model.BranchInfos = _branchInfoManager.GetBankList().OrderBy(x => x.BranchName).ToList();
            return View("BankIndex", model);
        }
        [Authorize(Roles = "PSO,DNE User,Admin Authority,Commanding Officer,OperationAdmin")]
        public ActionResult DeleteBankInfo(string BranchInfoIdentity, string branchCode, BranchInfoViewModel model)
        {
            if (BranchInfoIdentity != null)
            {
                long branchInfoIdentity = Convert.ToInt32(BranchInfoIdentity);
                ResponseModel response = _branchInfoManager.DeleteBank(branchInfoIdentity);
                model.IsSucceed = response.ResponsStatus;
                model.Message = response.Message;
                //model.BranchInfo = _branchInfoManager.FineOneByBranchInfoIdentity(branchInfoIdentity);
            }
            model.BranchInfos = _branchInfoManager.GetBankList().OrderBy(x => x.BranchName).ToList();
            return View("BankIndex", model);
        }
        [Authorize(Roles = "PSO,DNE User,Admin Authority,Commanding Officer,OperationAdmin")]
        public ActionResult DistrictIndex(string BranchInfoIdentity, string FirstLevel, BranchInfoViewModel model)
        {
            model.BankList = _branchInfoManager.GetBankList().Select(lg => new BranchInfo()
            {
                FirstLevel = lg.FirstLevel,
                SecondLevel = lg.SecondLevel,
                ThirdLevel = lg.ThirdLevel,
                BranchInfoIdentity = lg.BranchInfoIdentity,
                BranchCode = lg.BranchCode,
                BranchName = lg.BranchName + " [" + lg.BranchCode + "]"

            }).OrderBy(x => x.BranchName).ToList(); ;
            if (model.BankCode != null)
            {
                model.DistrictList = _branchInfoManager.GetDistrictList(model.BankCode).Select(lg => new BranchInfo()
                {

                    FirstLevel = lg.FirstLevel,
                    SecondLevel = lg.SecondLevel,
                    ThirdLevel = lg.ThirdLevel,
                    BranchInfoIdentity = lg.BranchInfoIdentity,
                    BranchCode = lg.BranchCode,
                    BranchName = lg.BranchName + " [" + lg.BranchCode + "]"
                }).OrderBy(x => x.BranchName).ToList(); ;
            }
            if (BranchInfoIdentity != null)
            {
                long branchInfoIdentity = Convert.ToInt32(BranchInfoIdentity);
                model.BranchInfo = _branchInfoManager.FineOneByBranchInfoIdentity(branchInfoIdentity);
                model.DistrictList = _branchInfoManager.GetDistrictList(FirstLevel).Select(lg => new BranchInfo()
                {

                    FirstLevel = lg.FirstLevel,
                    SecondLevel = lg.SecondLevel,
                    ThirdLevel = lg.ThirdLevel,
                    BranchInfoIdentity = lg.BranchInfoIdentity,
                    BranchCode = lg.BranchCode,
                    BranchName = lg.BranchName + " [" + lg.BranchCode + "]"
                }).OrderBy(x => x.BranchName).ToList(); ;
                model.BankCode = FirstLevel;
            }
            return View(model);
        }
        [Authorize(Roles = "PSO,DNE User,Admin Authority,Commanding Officer,OperationAdmin")]
        public ActionResult SaveDistrictInfo(string save, string update, BranchInfoViewModel model)
        {
            if (save == "Add New")
            {
                var branchInfo = _branchInfoManager.FineOneByBranchCode(model.BranchInfo.BranchCode);
                if (branchInfo != null)
                {
                    model.IsSucceed = 0;
                    model.Message = "Already exist the code " + model.BranchInfo.BranchCode;
                    model.BankList = _branchInfoManager.GetBankList().OrderBy(x => x.BranchName).Select(lg => new BranchInfo()
                    {

                        FirstLevel = lg.FirstLevel,
                        SecondLevel = lg.SecondLevel,
                        ThirdLevel = lg.ThirdLevel,
                        BranchInfoIdentity = lg.BranchInfoIdentity,
                        BranchCode = lg.BranchCode,
                        BranchName = lg.BranchName + " [" + lg.BranchCode + "]"
                    }).OrderBy(x => x.BranchName).ToList(); ;
                    model.DistrictList = _branchInfoManager.GetDistrictList(model.BranchInfo.FirstLevel).Select(lg => new BranchInfo()
                    {

                        FirstLevel = lg.FirstLevel,
                        SecondLevel = lg.SecondLevel,
                        ThirdLevel = lg.ThirdLevel,
                        BranchInfoIdentity = lg.BranchInfoIdentity,
                        BranchCode = lg.BranchCode,
                        BranchName = lg.BranchName + " [" + lg.BranchCode + "]"
                    }).OrderBy(x => x.BranchName).ToList();
                    return View("DistrictIndex", model);
                }
            }
            ResponseModel response = _branchInfoManager.SaveDistrictInfo(model.BranchInfo);
            model.IsSucceed = response.ResponsStatus;
            model.Message = response.Message;
            model.BankList = _branchInfoManager.GetBankList().OrderBy(x => x.BranchName).Select(lg => new BranchInfo()
            {

                FirstLevel = lg.FirstLevel,
                SecondLevel = lg.SecondLevel,
                ThirdLevel = lg.ThirdLevel,
                BranchInfoIdentity = lg.BranchInfoIdentity,
                BranchCode = lg.BranchCode,
                BranchName = lg.BranchName + " [" + lg.BranchCode + "]"
            }).OrderBy(x => x.BranchName).ToList();
            var bankCode = _branchInfoManager.FineOneByBranchCode(model.BranchInfo.BranchCode);
            model.DistrictList = _branchInfoManager.GetDistrictList(bankCode.FirstLevel).Select(lg => new BranchInfo()
            {

                FirstLevel = lg.FirstLevel,
                SecondLevel = lg.SecondLevel,
                ThirdLevel = lg.ThirdLevel,
                BranchInfoIdentity = lg.BranchInfoIdentity,
                BranchCode = lg.BranchCode,
                BranchName = lg.BranchName + " [" + lg.BranchCode + "]"
            }).OrderBy(x => x.BranchName).ToList();
            model.BankCode = bankCode.FirstLevel;
            model.BranchInfo.BranchCode = model.BranchInfo.FirstLevel;
            return View("DistrictIndex", model);
        }
        [Authorize(Roles = "PSO,DNE User,Admin Authority,Commanding Officer,OperationAdmin")]
        public ActionResult DeleteDistrictInfo(string BranchInfoIdentity, string FirstLevel, BranchInfoViewModel model)
        {
            if (BranchInfoIdentity != null)
            {
                long branchInfoIdentity = Convert.ToInt32(BranchInfoIdentity);
                ResponseModel response = _branchInfoManager.DeleteDistrict(branchInfoIdentity);
                model.IsSucceed = response.ResponsStatus;
                model.Message = response.Message;
                model.BranchInfo.BranchCode = FirstLevel;
            }
            model.BankList = _branchInfoManager.GetBankList().OrderBy(x => x.BranchName).Select(lg => new BranchInfo()
            {

                FirstLevel = lg.FirstLevel,
                SecondLevel = lg.SecondLevel,
                ThirdLevel = lg.ThirdLevel,
                BranchInfoIdentity = lg.BranchInfoIdentity,
                BranchCode = lg.BranchCode,
                BranchName = lg.BranchName + " [" + lg.BranchCode + "]"
            }).OrderBy(x => x.BranchName).ToList();
            model.DistrictList = _branchInfoManager.GetDistrictList(FirstLevel).Select(lg => new BranchInfo()
            {

                FirstLevel = lg.FirstLevel,
                SecondLevel = lg.SecondLevel,
                ThirdLevel = lg.ThirdLevel,
                BranchInfoIdentity = lg.BranchInfoIdentity,
                BranchCode = lg.BranchCode,
                BranchName = lg.BranchName + " [" + lg.BranchCode + "]"
            }).OrderBy(x => x.BranchName).ToList();
            model.BankCode = FirstLevel;
            return View("DistrictIndex", model);
        }
        [Authorize(Roles = "PSO,DNE User,Admin Authority,Commanding Officer,OperationAdmin")]
        public ActionResult BranchIndex(string BranchInfoIdentity, string FirstLevel, string SecondLevel, BranchInfoViewModel model)
        {
            model.BankList = _branchInfoManager.GetBankList().Select(lg => new BranchInfo()
            {
                BranchInfoIdentity = lg.BranchInfoIdentity,
                BranchCode = lg.BranchCode,
                BranchName = lg.BranchName + " [" + lg.BranchCode + "]"
            }).OrderBy(x => x.BranchName).ToList();
            if (model.BankCode != null && model.DistrictCode != null)
            {
                model.DistrictList = _branchInfoManager.GetDistrictList(model.BankCode).Select(lg => new BranchInfo()
                {

                    FirstLevel = lg.FirstLevel,
                    SecondLevel = lg.SecondLevel,
                    ThirdLevel = lg.ThirdLevel,
                    BranchInfoIdentity = lg.BranchInfoIdentity,
                    BranchCode = lg.BranchCode,
                    BranchName = lg.BranchName + " [" + lg.BranchCode + "]"
                }).OrderBy(x => x.BranchName).ToList(); ;
                model.BranchInfos = _branchInfoManager.GetBranchListByBankAndDistrict(model.BankCode, model.DistrictCode).Select(lg => new BranchInfo()
                {

                    FirstLevel = lg.FirstLevel,
                    SecondLevel = lg.SecondLevel,
                    ThirdLevel = lg.ThirdLevel,
                    BranchInfoIdentity = lg.BranchInfoIdentity,
                    BranchCode = lg.BranchCode,
                    BranchName = lg.BranchName + " [" + lg.BranchCode + "]"
                }).OrderBy(x => x.BranchName).ToList();
            }
            if (BranchInfoIdentity != null)
            {
                long branchInfoIdentity = Convert.ToInt32(BranchInfoIdentity);
                model.BranchInfo = _branchInfoManager.FineOneByBranchInfoIdentity(branchInfoIdentity);
                model.DistrictList = _branchInfoManager.GetDistrictList(FirstLevel).Select(lg => new BranchInfo()
                {

                    FirstLevel = lg.FirstLevel,
                    SecondLevel = lg.SecondLevel,
                    ThirdLevel = lg.ThirdLevel,
                    BranchInfoIdentity = lg.BranchInfoIdentity,
                    BranchCode = lg.BranchCode,
                    BranchName = lg.BranchName + " [" + lg.BranchCode + "]"
                }).OrderBy(x => x.BranchName).ToList(); ;
                model.BranchInfos = _branchInfoManager.GetBranchListByBankAndDistrict(FirstLevel, SecondLevel).Select(lg => new BranchInfo()
                {

                    FirstLevel = lg.FirstLevel,
                    SecondLevel = lg.SecondLevel,
                    ThirdLevel = lg.ThirdLevel,
                    BranchInfoIdentity = lg.BranchInfoIdentity,
                    BranchCode = lg.BranchCode,
                    BranchName = lg.BranchName + " [" + lg.BranchCode + "]"
                }).OrderBy(x => x.BranchName).ToList(); ;
                model.BankCode = FirstLevel;
                model.DistrictCode = SecondLevel;
            }
            model.ZoneInfos = _branchInfoManager.ZoneInfoes();
            return View(model);
        }
        [Authorize(Roles = "PSO,DNE User,Admin Authority,Commanding Officer,OperationAdmin")]
        public ActionResult SaveBranchInfo(string save, string update, BranchInfoViewModel model)
        {
            if (save == "Add New")
            {
                var branchInfo = _branchInfoManager.FineOneByBranchCode(model.BranchInfo.BranchCode);
                if (branchInfo != null)
                {
                    model.IsSucceed = 0;
                    model.Message = "Already exist the code " + model.BranchInfo.BranchCode;
                    model.ZoneInfos = _branchInfoManager.ZoneInfoes();
                    model.BankList = _branchInfoManager.GetBankList().Select(lg => new BranchInfo()
                    {
                        FirstLevel = lg.FirstLevel,
                        SecondLevel = lg.SecondLevel,
                        ThirdLevel = lg.ThirdLevel,
                        BranchInfoIdentity = lg.BranchInfoIdentity,
                        BranchCode = lg.BranchCode,
                        BranchName = lg.BranchName + " [" + lg.BranchCode + "]"
                    }).OrderBy(x => x.BranchName).ToList(); ;
                    model.BranchInfo = _branchInfoManager.FineOneByBranchCode(model.BranchInfo.BranchCode);
                    model.DistrictList = _branchInfoManager.GetDistrictList(model.BranchInfo.FirstLevel).Select(lg => new BranchInfo()
                    {

                        FirstLevel = lg.FirstLevel,
                        SecondLevel = lg.SecondLevel,
                        ThirdLevel = lg.ThirdLevel,
                        BranchInfoIdentity = lg.BranchInfoIdentity,
                        BranchCode = lg.BranchCode,
                        BranchName = lg.BranchName + " [" + lg.BranchCode + "]"
                    }).OrderBy(x => x.BranchName).ToList(); ;
                    model.BranchInfos = _branchInfoManager.GetBranchListByBankAndDistrict(model.BranchInfo.FirstLevel, model.BranchInfo.SecondLevel).Select(lg => new BranchInfo()
                    {

                        FirstLevel = lg.FirstLevel,
                        SecondLevel = lg.SecondLevel,
                        ThirdLevel = lg.ThirdLevel,
                        BranchInfoIdentity = lg.BranchInfoIdentity,
                        BranchCode = lg.BranchCode,
                        BranchName = lg.BranchName + " [" + lg.BranchCode + "]"
                    }).OrderBy(x => x.BranchName).ToList(); ;
                    return View("BranchIndex", model);
                }
            }
            model.BranchInfo.FirstLevel = model.BankCode;
            model.BranchInfo.SecondLevel = model.DistrictCode;
            ResponseModel response = _branchInfoManager.SaveBranchInfo(model.BranchInfo);
            model.IsSucceed = response.ResponsStatus;
            model.Message = response.Message;
            model.BankList = _branchInfoManager.GetBankList().Select(lg => new BranchInfo()
            {

                FirstLevel = lg.FirstLevel,
                SecondLevel = lg.SecondLevel,
                ThirdLevel = lg.ThirdLevel,
                BranchInfoIdentity = lg.BranchInfoIdentity,
                BranchCode = lg.BranchCode,
                BranchName = lg.BranchName + " [" + lg.BranchCode + "]"
            }).OrderBy(x => x.BranchName).ToList(); ;
            model.BranchInfo = _branchInfoManager.FineOneByBranchCode(model.BranchInfo.BranchCode);
            model.DistrictList = _branchInfoManager.GetDistrictList(model.BranchInfo.FirstLevel).Select(lg => new BranchInfo()
            {

                FirstLevel = lg.FirstLevel,
                SecondLevel = lg.SecondLevel,
                ThirdLevel = lg.ThirdLevel,
                BranchInfoIdentity = lg.BranchInfoIdentity,
                BranchCode = lg.BranchCode,
                BranchName = lg.BranchName + " [" + lg.BranchCode + "]"
            }).OrderBy(x => x.BranchName).ToList(); ;
            model.BranchInfos = _branchInfoManager.GetBranchListByBankAndDistrict(model.BranchInfo.FirstLevel, model.BranchInfo.SecondLevel).Select(lg => new BranchInfo()
            {

                FirstLevel = lg.FirstLevel,
                SecondLevel = lg.SecondLevel,
                ThirdLevel = lg.ThirdLevel,
                BranchInfoIdentity = lg.BranchInfoIdentity,
                BranchCode = lg.BranchCode,
                BranchName = lg.BranchName + " [" + lg.BranchCode + "]"
            }).OrderBy(x => x.BranchName).ToList();
            model.BankCode = model.BranchInfo.FirstLevel;
            model.DistrictCode = model.BranchInfo.SecondLevel;
            model.ZoneInfos = _branchInfoManager.ZoneInfoes();
            return View("BranchIndex", model);
        }
        [Authorize(Roles = "PSO,DNE User,Admin Authority,Commanding Officer,OperationAdmin")]
        public ActionResult DeleteBranchInfo(string BranchInfoIdentity, string FirstLevel, string SecondLevel, BranchInfoViewModel model)
        {
            if (BranchInfoIdentity != null)
            {
                long branchInfoIdentity = Convert.ToInt32(BranchInfoIdentity);
                ResponseModel response = _branchInfoManager.DeleteBranchInfo(branchInfoIdentity);
                model.IsSucceed = response.ResponsStatus;
                model.Message = response.Message;
                model.BranchInfo.BranchCode = FirstLevel;
            }
            model.BankList = _branchInfoManager.GetBankList().OrderBy(x => x.BranchName).Select(lg => new BranchInfo()
            {

                FirstLevel = lg.FirstLevel,
                SecondLevel = lg.SecondLevel,
                ThirdLevel = lg.ThirdLevel,
                BranchInfoIdentity = lg.BranchInfoIdentity,
                BranchCode = lg.BranchCode,
                BranchName = lg.BranchName + " [" + lg.BranchCode + "]"
            }).OrderBy(x => x.BranchName).ToList(); ;
            model.DistrictList = _branchInfoManager.GetDistrictList(FirstLevel).Select(lg => new BranchInfo()
            {

                FirstLevel = lg.FirstLevel,
                SecondLevel = lg.SecondLevel,
                ThirdLevel = lg.ThirdLevel,
                BranchInfoIdentity = lg.BranchInfoIdentity,
                BranchCode = lg.BranchCode,
                BranchName = lg.BranchName + " [" + lg.BranchCode + "]"
            }).OrderBy(x => x.BranchName).ToList();
            model.BranchInfos = _branchInfoManager.GetBranchListByBankAndDistrict(FirstLevel, SecondLevel).Select(lg => new BranchInfo()
            {

                FirstLevel = lg.FirstLevel,
                SecondLevel = lg.SecondLevel,
                ThirdLevel = lg.ThirdLevel,
                BranchInfoIdentity = lg.BranchInfoIdentity,
                BranchCode = lg.BranchCode,
                BranchName = lg.BranchName + " [" + lg.BranchCode + "]"
            }).OrderBy(x => x.BranchName).ToList();
            model.BankCode = FirstLevel;
            model.DistrictCode = SecondLevel;
            return View("BranchIndex", model);
        }
        [Authorize(Roles = "PSO,DNE User,Admin Authority,Commanding Officer,OperationAdmin")]
        public ActionResult SubBranchIndex(string BranchInfoIdentity, string FirstLevel, string SecondLevel, string ThirdLevel, BranchInfoViewModel model)
        {
            if (model.BankCode != null && model.DistrictCode != null && model.BranchCode != null)
            {
                model.BranchInfo.FirstLevel = model.BankCode;
                model.BranchInfo.SecondLevel = model.DistrictCode;
                model.BranchInfo.ThirdLevel = model.BranchCode;
            }
            else
            {
                model.BranchInfo.FirstLevel = FirstLevel;
                model.BranchInfo.SecondLevel = SecondLevel;
                model.BranchInfo.ThirdLevel = ThirdLevel;
            }
            model.BankList = _branchInfoManager.GetBankList().OrderBy(x => x.BranchName).Select(lg => new BranchInfo()
            {
                BranchInfoIdentity = lg.BranchInfoIdentity,
                BranchCode = lg.BranchCode,
                BranchName = lg.BranchName + " [" + lg.BranchCode + "]"
            }).OrderBy(x => x.BranchName).ToList();
            if (model.BranchInfo.FirstLevel != null && model.BranchInfo.SecondLevel != null && model.BranchInfo.ThirdLevel != null)
            {
                model.DistrictList = _branchInfoManager.GetExchangeHouseDivisionList(model.BranchInfo.FirstLevel).Select(lg => new BranchInfo()
                {
                    FirstLevel = lg.FirstLevel,
                    SecondLevel = lg.SecondLevel,
                    ThirdLevel = lg.ThirdLevel,
                    BranchInfoIdentity = lg.BranchInfoIdentity,
                    BranchCode = lg.BranchCode,
                    BranchName = lg.BranchName + " [" + lg.BranchCode + "]"
                }).OrderBy(x => x.BranchName).ToList();


                model.BranchInfos = _branchInfoManager.GetBranchListByBankAndDistrict(model.BranchInfo.FirstLevel, model.BranchInfo.SecondLevel).Select(lg => new BranchInfo()
                {

                    FirstLevel = lg.FirstLevel,
                    SecondLevel = lg.SecondLevel,
                    ThirdLevel = lg.ThirdLevel,
                    BranchInfoIdentity = lg.BranchInfoIdentity,
                    BranchCode = lg.BranchCode,
                    BranchName = lg.BranchName + " [" + lg.BranchCode + "]"
                }).OrderBy(x => x.BranchName).ToList();

                model.SubBranchInfos = _branchInfoManager.GetSubBranchInfoes(model.BranchInfo.ThirdLevel).Select(lg => new BranchInfo()
                {

                    FirstLevel = lg.FirstLevel,
                    SecondLevel = lg.SecondLevel,
                    ThirdLevel = lg.ThirdLevel,
                    BranchInfoIdentity = lg.BranchInfoIdentity,
                    BranchCode = lg.BranchCode,
                    BranchName = lg.BranchName + " [" + lg.BranchCode + "]"
                }).OrderBy(x => x.BranchName).ToList();
            }
            if (BranchInfoIdentity != null)
            {
                long branchInfoIdentity = Convert.ToInt32(BranchInfoIdentity);
                model.BranchInfo = _branchInfoManager.FineOneByBranchInfoIdentity(branchInfoIdentity);
            }
            model.BankCode = model.BranchInfo.FirstLevel;
            model.DistrictCode = model.BranchInfo.SecondLevel;
            model.BranchCode = model.BranchInfo.ThirdLevel;
            model.ZoneInfos = _branchInfoManager.ZoneInfoes();
            return View(model);
        }
        [Authorize(Roles = "PSO,DNE User,Admin Authority,Commanding Officer,OperationAdmin")]
        public ActionResult SaveSubBranchInfo(string save, string update, BranchInfoViewModel model)
        {
            if (save == "Add New")
            {
                var branchInfo = _branchInfoManager.FineOneByBranchCode(model.BranchInfo.BranchCode);
                if (branchInfo != null)
                {
                    model.IsSucceed = 0;
                    model.Message = "Already exist the code " + model.BranchInfo.BranchCode;
                    model.ZoneInfos = _branchInfoManager.ZoneInfoes();
                    model.BankList = _branchInfoManager.GetBankList().Select(lg => new BranchInfo()
                    {

                        FirstLevel = lg.FirstLevel,
                        SecondLevel = lg.SecondLevel,
                        ThirdLevel = lg.ThirdLevel,
                        BranchInfoIdentity = lg.BranchInfoIdentity,
                        BranchCode = lg.BranchCode,
                        BranchName = lg.BranchName + " [" + lg.BranchCode + "]"
                    }).OrderBy(x => x.BranchName).ToList(); ;
                    model.BranchInfo = _branchInfoManager.FineOneByBranchCode(model.BranchInfo.BranchCode);
                    model.DistrictList = _branchInfoManager.GetExchangeHouseDivisionList(model.BranchInfo.FirstLevel).Select(lg => new BranchInfo()
                    {

                        FirstLevel = lg.FirstLevel,
                        SecondLevel = lg.SecondLevel,
                        ThirdLevel = lg.ThirdLevel,
                        BranchInfoIdentity = lg.BranchInfoIdentity,
                        BranchCode = lg.BranchCode,
                        BranchName = lg.BranchName + " [" + lg.BranchCode + "]"
                    }).OrderBy(x => x.BranchName).ToList(); ;
                    model.BranchInfos = _branchInfoManager.GetBranchListByBankAndDistrict(model.BranchInfo.FirstLevel, model.BranchInfo.SecondLevel).Select(lg => new BranchInfo()
                    {

                        FirstLevel = lg.FirstLevel,
                        SecondLevel = lg.SecondLevel,
                        ThirdLevel = lg.ThirdLevel,
                        BranchInfoIdentity = lg.BranchInfoIdentity,
                        BranchCode = lg.BranchCode,
                        BranchName = lg.BranchName + " [" + lg.BranchCode + "]"
                    }).OrderBy(x => x.BranchName).ToList();
                    model.SubBranchInfos = _branchInfoManager.GetSubBranchInfoes(model.BranchInfo.ThirdLevel).Select(lg => new BranchInfo()
                    {

                        FirstLevel = lg.FirstLevel,
                        SecondLevel = lg.SecondLevel,
                        ThirdLevel = lg.ThirdLevel,
                        BranchInfoIdentity = lg.BranchInfoIdentity,
                        BranchCode = lg.BranchCode,
                        BranchName = lg.BranchName + " [" + lg.BranchCode + "]"
                    }).OrderBy(x => x.BranchName).ToList();
                    return View("SubBranchIndex", model);
                }
            }
            model.BranchInfo.FirstLevel = model.BankCode;
            model.BranchInfo.SecondLevel = model.DistrictCode;
            model.BranchInfo.ThirdLevel = model.BranchCode;
            ResponseModel response = _branchInfoManager.SaveSubBranchInfo(model.BranchInfo);
            model.IsSucceed = response.ResponsStatus;
            model.Message = response.Message;
            model.BankList = _branchInfoManager.GetBankList().Select(lg => new BranchInfo()
            {

                FirstLevel = lg.FirstLevel,
                SecondLevel = lg.SecondLevel,
                ThirdLevel = lg.ThirdLevel,
                BranchInfoIdentity = lg.BranchInfoIdentity,
                BranchCode = lg.BranchCode,
                BranchName = lg.BranchName + " [" + lg.BranchCode + "]"
            }).OrderBy(x => x.BranchName).ToList(); ;
            model.BranchInfo = _branchInfoManager.FineOneByBranchCode(model.BranchInfo.BranchCode);
            model.DistrictList = _branchInfoManager.GetExchangeHouseDivisionList(model.BranchInfo.FirstLevel).Select(lg => new BranchInfo()
            {

                FirstLevel = lg.FirstLevel,
                SecondLevel = lg.SecondLevel,
                ThirdLevel = lg.ThirdLevel,
                BranchInfoIdentity = lg.BranchInfoIdentity,
                BranchCode = lg.BranchCode,
                BranchName = lg.BranchName + " [" + lg.BranchCode + "]"
            }).OrderBy(x => x.BranchName).ToList(); ;
            model.BranchInfos = _branchInfoManager.GetBranchListByBankAndDistrict(model.BranchInfo.FirstLevel, model.BranchInfo.SecondLevel).Select(lg => new BranchInfo()
            {

                FirstLevel = lg.FirstLevel,
                SecondLevel = lg.SecondLevel,
                ThirdLevel = lg.ThirdLevel,
                BranchInfoIdentity = lg.BranchInfoIdentity,
                BranchCode = lg.BranchCode,
                BranchName = lg.BranchName + " [" + lg.BranchCode + "]"
            }).OrderBy(x => x.BranchName).ToList();
            model.SubBranchInfos = _branchInfoManager.GetSubBranchInfoes(model.BranchInfo.ThirdLevel).Select(lg => new BranchInfo()
            {

                FirstLevel = lg.FirstLevel,
                SecondLevel = lg.SecondLevel,
                ThirdLevel = lg.ThirdLevel,
                BranchInfoIdentity = lg.BranchInfoIdentity,
                BranchCode = lg.BranchCode,
                BranchName = lg.BranchName + " [" + lg.BranchCode + "]"
            }).OrderBy(x => x.BranchName).ToList();
            model.BankCode = model.BranchInfo.FirstLevel;
            model.DistrictCode = model.BranchInfo.SecondLevel;
            model.BranchCode = model.BranchInfo.ThirdLevel;
            model.ZoneInfos = _branchInfoManager.ZoneInfoes();
            return View("SubBranchIndex", model);
        }
        [Authorize(Roles = "PSO,DNE User,Admin Authority,Commanding Officer,OperationAdmin")]
        public ActionResult DeleteSubBranchInfo(string BranchInfoIdentity, string FirstLevel, string SecondLevel, string ThirdLevel, BranchInfoViewModel model)
        {
            if (BranchInfoIdentity != null)
            {
                long branchInfoIdentity = Convert.ToInt32(BranchInfoIdentity);
                ResponseModel response = _branchInfoManager.DeleteSubBranchInfo(branchInfoIdentity);
                model.IsSucceed = response.ResponsStatus;
                model.Message = response.Message;
                model.BranchInfo.FirstLevel = FirstLevel;
                model.BranchInfo.SecondLevel = SecondLevel;
                model.BranchInfo.ThirdLevel = ThirdLevel;
            }
            model.BankList = _branchInfoManager.GetBankList().OrderBy(x => x.BranchName).Select(lg => new BranchInfo()
            {

                FirstLevel = lg.FirstLevel,
                SecondLevel = lg.SecondLevel,
                ThirdLevel = lg.ThirdLevel,
                BranchInfoIdentity = lg.BranchInfoIdentity,
                BranchCode = lg.BranchCode,
                BranchName = lg.BranchName + " [" + lg.BranchCode + "]"
            }).OrderBy(x => x.BranchName).ToList(); ;
            model.DistrictList = _branchInfoManager.GetExchangeHouseDivisionList(model.BranchInfo.FirstLevel).Select(lg => new BranchInfo()
            {

                FirstLevel = lg.FirstLevel,
                SecondLevel = lg.SecondLevel,
                ThirdLevel = lg.ThirdLevel,
                BranchInfoIdentity = lg.BranchInfoIdentity,
                BranchCode = lg.BranchCode,
                BranchName = lg.BranchName + " [" + lg.BranchCode + "]"
            }).OrderBy(x => x.BranchName).ToList(); ;
            model.BranchInfos = _branchInfoManager.GetBranchListByBankAndDistrict(model.BranchInfo.FirstLevel, model.BranchInfo.SecondLevel).Select(lg => new BranchInfo()
            {

                FirstLevel = lg.FirstLevel,
                SecondLevel = lg.SecondLevel,
                ThirdLevel = lg.ThirdLevel,
                BranchInfoIdentity = lg.BranchInfoIdentity,
                BranchCode = lg.BranchCode,
                BranchName = lg.BranchName + " [" + lg.BranchCode + "]"
            }).OrderBy(x => x.BranchName).ToList();
            model.SubBranchInfos = _branchInfoManager.GetSubBranchInfoes(model.BranchInfo.ThirdLevel).Select(lg => new BranchInfo()
            {

                FirstLevel = lg.FirstLevel,
                SecondLevel = lg.SecondLevel,
                ThirdLevel = lg.ThirdLevel,
                BranchInfoIdentity = lg.BranchInfoIdentity,
                BranchCode = lg.BranchCode,
                BranchName = lg.BranchName + " [" + lg.BranchCode + "]"
            }).ToList();
            model.BankCode = FirstLevel;
            model.DistrictCode = SecondLevel;
            model.BranchCode = ThirdLevel;
            model.ZoneInfos = _branchInfoManager.ZoneInfoes();
            return View("SubBranchIndex", model);
        }
        public JsonResult GetDistrictsByBankCode(string bankCode)
        {
            var districtList = _branchInfoManager.GetDistrictList(bankCode).Select(lg => new BranchInfo()
            {

                FirstLevel = lg.FirstLevel,
                SecondLevel = lg.SecondLevel,
                ThirdLevel = lg.ThirdLevel,
                BranchInfoIdentity = lg.BranchInfoIdentity,
                BranchCode = lg.BranchCode,
                BranchName = lg.BranchName + " [" + lg.BranchCode + "]"
            }).OrderBy(x => x.BranchName).ToList();
            return Json(districtList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetExchangesHousesByBankCode(string bankCode)
        {
            var districtList = _branchInfoManager.GetExchangeHouseDivisionList(bankCode).Select(lg => new BranchInfo()
            {

                FirstLevel = lg.FirstLevel,
                SecondLevel = lg.SecondLevel,
                ThirdLevel = lg.ThirdLevel,
                BranchInfoIdentity = lg.BranchInfoIdentity,
                BranchCode = lg.BranchCode,
                BranchName = lg.BranchName + " [" + lg.BranchCode + "]"
            }).OrderBy(x => x.BranchName).ToList();
            return Json(districtList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetBranchesByDistrict(string districtCode)
        {
            var districtList = _branchInfoManager.GetBranchList(districtCode).Select(lg => new BranchInfo()
            {

                FirstLevel = lg.FirstLevel,
                SecondLevel = lg.SecondLevel,
                ThirdLevel = lg.ThirdLevel,
                BranchInfoIdentity = lg.BranchInfoIdentity,
                BranchCode = lg.BranchCode,
                BranchName = lg.BranchName + " [" + lg.BranchCode + "]"
            }).OrderBy(x => x.BranchName).ToList();
            return Json(districtList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetBankBySearchCharacter(string searchCharacter)
        {
            var bankList = _branchInfoManager.GetBankList().Where(x=>x.BranchName.ToLower().Contains(searchCharacter.ToLower())).Select(lg => new BranchInfo()
            {
                BranchCode = lg.BranchCode,
                BranchName = lg.BranchName
            }).OrderBy(x => x.BranchName).ToList();
            return Json(bankList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDistrictByBankCodeIdentity(string selectedId)
        {
            var districtList = _branchInfoManager.GetDistrictListByIdentity(selectedId).OrderBy(x=>x.BranchName);
            return Json(districtList, JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "PSO,DNE User,Admin Authority,Commanding Officer,OperationAdmin")]
        public JsonResult GetBrancByDistrictIdentity(string selectedId)
        {
            var brnchList = _branchInfoManager.GetBranchListByDistrictIdentity(Convert.ToInt32(selectedId)).OrderBy(x=>x.BranchName);
            return Json(brnchList, JsonRequestBehavior.AllowGet);
        }
    }
}