using RMS.BLL.IManager;
using RMS.Model;
using RMS.Utility;
using RMS.Web.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.Security;

namespace RMS.Web.Controllers
{
    [Authorize]
    public class UserController : BaseController
    {
        HttpClient client = new HttpClient();
        private HttpResponseMessage response;

        private readonly IUserManager _userManager;
        private readonly IRoleManager _roleManager;
        private readonly ICommonCodeManager _commonCodeManager;
        private readonly IBranchInfoManager _branchInfoManager;
        private readonly IControlShipInfoManager _controlShipInfoManager;

        public UserController(IUserManager userManager, IRoleManager roleManager, ICommonCodeManager commonCodeManager, IBranchInfoManager branchInfoManager, IControlShipInfoManager controlShipInfoManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _commonCodeManager = commonCodeManager;
            _branchInfoManager = branchInfoManager;
            _controlShipInfoManager = controlShipInfoManager;

        }
        [Authorize(Roles = "PSO,DNE User,Admin Authority,Commanding Officer,OperationAdmin")]
        public ActionResult Index(UserViewModel model)
        {
            model.UserBankInfos = _userManager.GetUserBankInfos();
            return View(model);
        }
        public ActionResult ProfileIndex(UserViewModel model)
        {
            model.User = _userManager.GetUserById(PortalContext.CurrentUser.UserId);
            model.UserProfile = _userManager.FindOneProfile(PortalContext.CurrentUser.UserId);
            model.Sex = _commonCodeManager.GetCommonCodeByType("Sex");
            model.Designation = _commonCodeManager.GetCommonCodeByType("Designation");
            model.User.UserFullName = PortalContext.CurrentUser.UserFullName;
            return View(model);
        }
        public ActionResult SaveProfile(UserViewModel model)
        {
            var validImageTypes = new string[]
            {
                "image/gif",
                "image/jpeg",
                "image/pjpeg",
                "image/png",
                "image/DWG",
                "image/dwg",
                "image/jpg",
                "image/BPG",
                "image/SVG",
                "image/pdf",
                "image/PDF"


            };

            //if (ModelState.IsValid)
            //{
            if (model.SignatureUrl != null && model.SignatureUrl.ContentLength > 0)
            {
                var uploadDir = "~/uploads/UserProfile";

                string file = "User_" + PortalContext.CurrentUser.UserId + "_" + model.SignatureUrl.FileName;
                string ext = Path.GetExtension(file);
                if (ext == ".jpg" || ext == ".png" || ext == ".gif" || ext == ".DWG" || ext == ".dwg" || ext == ".jpeg" || ext == ".pjpeg" || ext == ".BPG" || ext == ".SVG" || ext == ".PDF" || ext == ".pdf")
                {
                    var imagePath = Path.Combine(Server.MapPath(uploadDir), file);
                    var SignatureUrl = Path.Combine(uploadDir, file);
                    model.SignatureUrl.SaveAs(imagePath);
                    model.UserProfile.SignatureUrl = SignatureUrl;
                }
                else
                {
                    TempData["message"] = "Input Only Image";
                    return RedirectToAction("Edit", model);
                }
            }
            if (model.PhotographUrl != null && model.PhotographUrl.ContentLength > 0)
            {
                var uploadDir = "~/uploads/UserProfile";

                string file = "User_" + PortalContext.CurrentUser.UserId + "_" + model.PhotographUrl.FileName;
                string ext = Path.GetExtension(file);
                if (ext == ".jpg" || ext == ".png" || ext == ".gif" || ext == ".DWG" || ext == ".dwg" || ext == ".jpeg" || ext == ".pjpeg" || ext == ".BPG" || ext == ".SVG" || ext == ".PDF" || ext == ".pdf")
                {
                    var imagePath = Path.Combine(Server.MapPath(uploadDir), file);
                    var PhotographUrl = Path.Combine(uploadDir, file);
                    model.PhotographUrl.SaveAs(imagePath);
                    model.UserProfile.PhotographUrl = PhotographUrl;
                }
                else
                {
                    TempData["message"] = "Input Only Image";
                    return RedirectToAction("Edit", model);
                }
            }
            ResponseModel savedata = _userManager.SaveUserPrifile(model.UserProfile, model.User);
            if (savedata.Message != string.Empty)
            {
                TempData["message"] = savedata.Message;
            }
            //}
            return RedirectToAction("ProfileIndex");

            //model.User = _userManager.GetUserById(PortalContext.CurrentUser.UserId);
            //model.UserProfile = _userManager.FindOneProfile(PortalContext.CurrentUser.UserId);
            //model.User.UserFullName = PortalContext.CurrentUser.UserFullName;
            //return View("ProfileIndex", model);
        }
        public ActionResult LogInUserIndex(UserViewModel model)
        {
            model.UserBankInfos = _userManager.GetUserBankInfos().Where(x => x.IsLogin == true).OrderBy(x => x.UserName).ToList();
            return View(model);
        }
        [Authorize(Roles = "PSO,DNE User,Admin Authority,Commanding Officer,OperationAdmin")]
        public ActionResult SearchByKey(UserViewModel model)
        {
            string searchKey = model.SearchKey;
            if (searchKey == null)
            {
                model.UserBankInfos = _userManager.GetUserBankInfos();
            }
            else
            {
                searchKey = searchKey.ToLower();
                model.UserBankInfos = _userManager.GetUserBankInfos().Where(x => x.BranchName.ToLower().Contains(searchKey) || x.UserName.ToLower().Contains(searchKey)).ToList();
                if (!model.UserBankInfos.Any())
                {
                    model.IsSucceed = 0;
                    model.Message = "Data is not found.";
                }
            }
            model.SearchKey = searchKey;
            return View("Index", model);
        }
        [Authorize(Roles = "PSO,DNE User,Admin Authority,Commanding Officer,OperationAdmin")]
        public ActionResult ApproveUserIndex(UserViewModel model)
        {
            model.UserBankInfos = _userManager.GetUserBankInfos();
            return View(model);
        }
        [Authorize(Roles = "PSO,DNE User,Admin Authority,Commanding Officer,OperationAdmin")]
        [HttpGet]
        public ActionResult ActiveUser(string UserId, UserViewModel model)
        {
            bool status = true;
            ResponseModel response = _userManager.UpdateStatus(UserId, status);
            model.UserBankInfos = _userManager.GetUserBankInfos().ToList();
            return View("ApproveUserIndex", model);
        }
        [Authorize(Roles = "PSO,DNE User,Admin Authority,Commanding Officer,OperationAdmin")]
        public ActionResult SearchByKeyforStatus(UserViewModel model)
        {
            string searchKey = model.SearchKey;
            if (searchKey == null)
            {
                model.UserBankInfos = _userManager.GetUserBankInfos();
            }
            else
            {
                searchKey = searchKey.ToLower();
                model.UserBankInfos = _userManager.GetUserBankInfos().Where(x => x.BranchName.ToLower().Contains(searchKey) || x.UserName.ToLower().Contains(searchKey)).ToList();
            }
            return View("ApproveUserIndex", model);
        }
        [Authorize(Roles = "PSO,DNE User,Admin Authority,Commanding Officer,OperationAdmin")]
        [HttpGet]
        public ActionResult InactiveUser(string UserId, UserViewModel model)
        {
            bool status = false;
            ResponseModel response = _userManager.UpdateStatus(UserId, status);
            model.UserBankInfos = _userManager.GetUserBankInfos().ToList();
            return View("ApproveUserIndex", model);
        }
        //public ActionResult ActiveUser(UserViewModel model)
        //{
        //    model.Users = _userManager.GetBankUsersList(model);
        //    ModelState.Clear();
        //    return View(model);
        //}
        //public ActionResult UserActive(UserViewModel model)
        //{
        //    int count = _userManager.UserActive(model.Users);
        //    if (count > 0)
        //    {    
        //        TempData["Msg"] = "Data Successfully updated.";
        //        return RedirectToAction("ActiveUser", model);
        //    }
        //    else
        //    {
        //        TempData["Msg"] = "Data is not updated.";

        //    }
        //    return View(model);

        //}

        //[Authorize(Roles = "UserManagement,AdminUser")]
        [Authorize(Roles = "PSO,DNE User,Admin Authority,Commanding Officer,OperationAdmin")]
        [AllowAnonymous]
        [HttpPost]
        public ActionResult CheckExistingUserName(UserViewModel model)
        {
            bool result;
            //User user = _userManager.FindOne(model.User.UserName);
            UserBankInfo userBankInfo = _userManager.FindOneUserBankInfo(model.User.UserName);
            if (userBankInfo != null)
            {
                result = false;
            }
            else
            {
                result = true;
            }
            return Json(result);
        }
        [Authorize(Roles = "PSO,DNE User,Admin Authority,Commanding Officer,OperationAdmin")]
        [HttpGet]
        public ActionResult Edit(string UserId, string BranchInfoIdentity, UserViewModel model)
        {
            ModelState.Clear();
            model.BankList = _branchInfoManager.GetBankList();
            model.RoleList = _roleManager.GetAll().OrderBy(x => x.Sequence).ToList();
            model.DistrictList = _branchInfoManager.GetDistrictList("1000000000");
            model.BranchList = _branchInfoManager.GetBranchList("1001000000");


            //model.RoleList = PortalContext.CurrentUser.LoweredRoleName == "S" ? : _roleManager.GetAll().Where(x => x.LoweredRoleName != "S").ToList();

            if (UserId != null)
            {
                long userId = Convert.ToInt32(UserId);
                if (userId > 0)
                {
                    model.User = _userManager.GetUserById(userId);
                    model.BankInfo = _branchInfoManager.GetBankInfo(model.User.BranchInfoIdentity);
                    model.BankCode = model.BankInfo.BankCode;
                    model.DistrictList = _branchInfoManager.GetDistrictList(model.BankCode);
                    model.DistrictCode = model.BankInfo.DistrictCode;
                    model.BranchList = _branchInfoManager.GetBranchList(model.DistrictCode);
                    model.BranchCode = model.BankInfo.DistrictCode;
                    model.controlShipInfos = _controlShipInfoManager.GetAll().ToList();
                    model.User.ControlShipInfoId = model.User.ControlShipInfoId;
                }
            }
            //BranchInfo oldData = _userManager.FindOneUserBankInfo.FindOneUserBankInfo(UserId);
            return View(model);
        }
        [Authorize(Roles = "PSO,DNE User,Admin Authority,Commanding Officer,OperationAdmin")]
        public ActionResult Save(UserViewModel model)
        {
            model.BankList = _branchInfoManager.GetBankList();
            model.RoleList = _roleManager.GetAll();
            try
            {

                BranchInfo branchInfo = _branchInfoManager.FineOneByBranchCode(model.BranchCode);
                //model.User.BranchInfoIdentity = branchInfo.BranchInfoIdentity;
                model.BankCode = branchInfo.FirstLevel;

                ResponseModel = _userManager.SaveUser(model.User);
                model.Message = ResponseModel.Message;
                model.IsSucceed = ResponseModel.ResponsStatus;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                model.Message = "System error. " + ex.Message;
                model.IsSucceed = 0;
                return View("Edit", model);
            }

        }
        [Authorize(Roles = "PSO,DNE User,Admin Authority,Commanding Officer,OperationAdmin")]
        [HttpGet]
        public ActionResult ResetPassword()
        {
            var model = new UserViewModel();
            return View("ResetPassword", model);

        }
        [Authorize(Roles = "PSO,DNE User,Admin Authority,Commanding Officer,OperationAdmin")]
        [HttpPost]
        public ActionResult ResetPassword(UserViewModel model)
        {
            if (model.UserIdForReset != null)
            {
                model.User.UserName = model.UserIdForReset;
                ResponseModel = _userManager.ResetUserPassword(model.User);
                ResponseModel = _userManager.ResetUserPasswordTest();
                model.Message = ResponseModel.Message;
                model.IsSucceed = ResponseModel.ResponsStatus;
            }
            return View("ResetPassword", model);
        }
        [Authorize(Roles = "PSO,DNE User,Admin Authority,Commanding Officer,OperationAdmin, Ship User")]
        [HttpGet]
        public ActionResult ChangePassword()
        {
            var model = new UserViewModel();
            return View("ChangePassword", model);
        }
        [Authorize(Roles = "PSO,DNE User,Admin Authority,Commanding Officer,OperationAdmin, Ship User")]
        [HttpPost]
        public ActionResult ChangePassword(UserViewModel model)
        {
            if (model.User.Password != null)
            {
                string password = model.User.Password;
                if (IsAlphaNumeric(password) || IsRegularExpression(password))
                {
                    ResponseModel = _userManager.ChangePassword(model.OldPassword, model.User);
                    model.IsSucceed = ResponseModel.ResponsStatus;
                    model.Message = ResponseModel.Message;
                    if (PortalContext.CurrentUser.WinPassword == false && model.IsSucceed == 1)
                    {
                        FormsAuthentication.SignOut();
                        Session.Clear();
                        PortalContext.CurrentUser.IsFirstTime = false;
                        return RedirectToAction("Login", "Account");
                    }
                }
                else
                {
                    model.Message = "Password must be combination of alphanumeric with An uppercase letter & a lowercase letter.";

                }

            }
            return View("ChangePassword", model);
        }
        [Authorize(Roles = "PSO,DNE User,Admin Authority,Commanding Officer,OperationAdmin")]
        public JsonResult GetDistrictByBankCode(string bankCode)
        {
            var districtList = _branchInfoManager.GetDistrictList(bankCode);
            return Json(districtList, JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "PSO,DNE User,Admin Authority,Commanding Officer,OperationAdmin")]
        public JsonResult GetBranchNameByDistrict(string districtCode)
        {
            var brnchList = _branchInfoManager.GetBranchList(districtCode);
            return Json(brnchList, JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "PSO,DNE User,Admin Authority,Commanding Officer,OperationAdmin")]
        [HttpGet]
        public ActionResult SessionOut(string userName, UserViewModel model)
        {
            ResponseModel response = _userManager.SessionOut(userName);
            model.IsSucceed = response.ResponsStatus;
            model.Message = response.Message;
            model.UserBankInfos = _userManager.GetUserBankInfos();
            return View("Index", model);
        }
        [Authorize(Roles = "PSO,DNE User,Admin Authority,Commanding Officer,OperationAdmin")]
        public JsonResult GetAgentByBranch(string branchCode)
        {
            if (branchCode == "")
            {
                branchCode = "0";
            }
            var agentList = _branchInfoManager.GetControlShipInfoes().Where(x => x.Organization == Convert.ToInt64(branchCode));
            return Json(agentList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetUserBySearchCharacter(string searchCharacter)
        {
            List<User> users = _userManager.GetUsers().Where(x => x.UserName.ToLower().Contains(searchCharacter.ToLower())).Select(lg => new User()
            {
                UserName = lg.UserName,
                UserFullName = lg.UserFullName
            }).ToList();

            return Json(users, JsonRequestBehavior.AllowGet);
        }


        public static bool IsAllLetters(string s)
        {
            return s.Any(Char.IsLetter);
        }
        public static bool IsAlphaNumeric(string s)
        {
            var rg = new Regex(@"^(?=.*[A-Za-z])(?=.*[0-9])[A-Za-z0-9]{8,30}$");
            return rg.IsMatch(s);
        }
        public static bool IsRegularExpression(string s)
        {
            var rg = new Regex(@"^(?=.*[!@#$%^&*()\-_=+`~\[\]{}?|])(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9]).{8,20}$");
            return rg.IsMatch(s);
        }
    }
}