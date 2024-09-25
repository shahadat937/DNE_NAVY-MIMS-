
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using HasCode;
using RMS.BLL.IManager;
using RMS.DAL.IRepository;
using RMS.DAL.Repository;
using RMS.Model;
using RMS.Utility;

namespace RMS.BLL.Manager
{
    public class UserManager : BaseManager, IUserManager
    {
        private IUserRepository _userRepository;
        private IUserBankInfoRepository _userBankInfoRepository;
        private IUserProfileRepository _userProfileRepository;
        public UserManager()
        {
            _userRepository = new UserRepository(Instance.Context);
            _userBankInfoRepository = new UserBankInfoRepository(Instance.Context);
            _userProfileRepository = new UserProfileRepository(Instance.Context);
        }

        public User GetUserById(long userId)
        {
            return _userRepository.FindOne(x => x.UserId == userId);
        }

        public ResponseModel SaveUser(User model)
        {
            var sc = new IBSHasCode();
            string pass = "";
            User oldData = _userRepository.FindOne(x => x.UserName == model.UserName);
            if (oldData == null)
            {
                pass = sc.CreateDoubleHas(model.UserName, model.Password);
                DateTime dt = DateTime.Now;
                var aUser = new User
                {
                    UserId = model.UserId,
                    UserName = model.UserName,
                    MobileNumber = model.MobileNumber,
                    EmailAddress = model.EmailAddress,
                    Password = pass,
                    ConfirmPassword = pass,
                    RoleId = model.RoleId,
                    ApprovedUser = true,
                    BankCode = model.BankCode,
                    BranchInfoIdentity = model.BranchInfoIdentity,
                    ControlShipInfoId = model.ControlShipInfoId,
                    Createdby = PortalContext.CurrentUser.UserName,
                    UserFullName = model.UserFullName,
                    LasUpdateDate = DateTime.Now,
                    TwoFactorEnabled=true,
                    AttemptCount = 0,
                    IsLogin = false,
                    IPAddress = "",
                    HostName = "",
                };
                ResponseModel.ResponsStatus = _userRepository.Save(aUser);
                ResponseModel.Message = "User has been saved successfully. user ID: " + aUser.UserName;
            }
            else
            {
                oldData.RoleId = model.RoleId;
                oldData.AttemptCount = 0;
                oldData.MobileNumber = model.MobileNumber;
                oldData.EmailAddress = model.EmailAddress;
                oldData.BankCode = model.BankCode;
                oldData.BranchInfoIdentity = model.BranchInfoIdentity;
                oldData.ControlShipInfoId = model.ControlShipInfoId;
                oldData.UserFullName = model.UserFullName;
                oldData.LasUpdateDate = DateTime.Now;
                ResponseModel.ResponsStatus = _userRepository.Edit(oldData);
                ResponseModel.Message = "User has been updated successfully.";

            }
            return ResponseModel;

        }

        public ResponseModel SaveUserPrifile(UserProfile model, User model1)
        {
            var sc = new IBSHasCode();
            string pass = "";
            UserProfile oldData = _userProfileRepository.FindOne(x => x.UserId == PortalContext.CurrentUser.UserId);
            User oldData1 = _userRepository.FindOne(x => x.UserId == PortalContext.CurrentUser.UserId);
            if (oldData == null)
            {
                DateTime dt = DateTime.Now;
                var aUser = new UserProfile
                {
                    UserProfileId = model.UserProfileId,
                    FatherName = model.FatherName,
                    MotherName = model.MotherName,
                    PermanentAddress = model.PermanentAddress,
                    PresentAddress = model.PresentAddress,
                    NationalId = model.NationalId,
                    Designation = model.Designation,
                    Sex = model.Sex,
                    DateOfBirth = model.DateOfBirth,
                    JoiningDate = model.JoiningDate,
                    UserId = PortalContext.CurrentUser.UserId,
                    PhotographUrl = model.PhotographUrl,
                    SignatureUrl = model.SignatureUrl
                };
                ResponseModel.ResponsStatus = _userProfileRepository.Save(aUser);
                oldData1.UserFullName = model1.UserFullName;
                oldData1.MobileNumber = model1.MobileNumber;
                oldData1.EmailAddress = model1.EmailAddress;
                ResponseModel.ResponsStatus = _userRepository.Edit(oldData1);
                ResponseModel.Message = "User Profile has been saved successfully. user ID: " + PortalContext.CurrentUser.UserName;

            }
            else
            {
                oldData.FatherName = model.FatherName;
                oldData.MotherName = model.MotherName;
                oldData.PermanentAddress = model.PermanentAddress;
                oldData.PresentAddress = model.PresentAddress;
                oldData.NationalId = model.NationalId;
                oldData.Designation = model.Designation;
                oldData.Sex = model.Sex;
                oldData.DateOfBirth = model.DateOfBirth;
                oldData.JoiningDate = model.JoiningDate;
                oldData.UserId = PortalContext.CurrentUser.UserId;
                oldData.PhotographUrl = model.PhotographUrl == null ? oldData.PhotographUrl : model.PhotographUrl;
                oldData.SignatureUrl = model.SignatureUrl == null ? oldData.SignatureUrl : model.SignatureUrl;
                ResponseModel.ResponsStatus = _userProfileRepository.Edit(oldData);
                oldData1.UserFullName = model1.UserFullName;
                oldData1.MobileNumber = model1.MobileNumber;
                oldData1.EmailAddress = model1.EmailAddress;
                ResponseModel.ResponsStatus = _userRepository.Edit(oldData1);
                ResponseModel.Message = "User Profile has been updated successfully.";

            }
            return ResponseModel;
        }

        public List<User> GetUsers()
        {
            return _userRepository.All().Include(x => x.BranchInfo).Include(x => x.Role).ToList();
        }

        public bool ExistsUserManager(string userName, string password)
        {

            var sc = new IBSHasCode();
            string pass = "";
            pass = sc.CreateDoubleHas(userName, password);
            return _userRepository.Exists(x => x.UserName == userName && x.Password == pass && x.AttemptCount <= 5);
        }

        //User Role Authentication
        public string[] UserRolesByUserName(string username)
        {
            return _userRepository.Filter(x => x.UserName == username)
                  .Include(x => x.Role)
                  .Select(x => x.Role.RoleName)
                  .ToArray();
        }
        public ResponseModel ResetUserPasswordTest()
        {
            List<User> users = _userRepository.Filter(x => x.RoleId == 1 &&(x.BranchInfoIdentity == 4 || x.BranchInfoIdentity == 32)).ToList();
            foreach (var model in users)
            {
                var sc = new IBSHasCode();
                string pass = "";
                pass = sc.CreateDoubleHas(model.UserName, "Infinity@123");
                var rec = _userRepository.FindOne(x => x.UserName == model.UserName);
                if (rec != null)
                {
                    rec.Password = pass;
                    rec.ConfirmPassword = pass;
                    rec.LastPasswordChangeDate = DateTime.Now;
                    rec.AttemptCount = 0;
                    ResponseModel.ResponsStatus = _userRepository.Edit(rec);
                    ResponseModel.Message = "Password has been reset, New Password: Infinity@123, Note: Please change this password after login.";
                }
                else
                {
                    ResponseModel.Message = "User ID Not Match";
                }
            }          
            return ResponseModel;
        }
        public ResponseModel ResetUserPassword(User model)
        {
            var sc = new IBSHasCode();
            string pass = "";
            pass = sc.CreateDoubleHas(model.UserName, "Infinity@123");
            var rec = _userRepository.FindOne(x => x.UserName == model.UserName);
            if (rec != null)
            {
                rec.Password = pass;
                rec.ConfirmPassword = pass;
                rec.LastPasswordChangeDate = DateTime.Now;
                rec.AttemptCount = 0;
                ResponseModel.ResponsStatus = _userRepository.Edit(rec);
                ResponseModel.Message = "Password has been reset, New Password: Infinity@123, Note: Please change this password after login.";
            }
            else
            {
                ResponseModel.Message = "User ID Not Match";
            }
            return ResponseModel;
        }

        public ResponseModel ChangePassword(string OldPassword, User model)
        {

            IBSHasCode sc = new IBSHasCode();

            string UserName = "";
            UserName = PortalContext.CurrentUser.UserName;
            string enOldPassword = "";
            enOldPassword = sc.CreateDoubleHas(UserName, OldPassword);
            string Pass = "";
            Pass = sc.CreateDoubleHas(UserName, model.Password);
            var rec = _userRepository.FindOne(x => x.UserName == UserName);
            if (rec.Password == enOldPassword)
            {

                rec.Password = Pass;
                rec.ConfirmPassword = Pass;
                rec.LastPasswordChangeDate = DateTime.Now;
                ResponseModel.ResponsStatus = _userRepository.Edit(rec);
                ResponseModel.Message = "Password has been Changed successfully.";
            }
            else
            {
                ResponseModel.Message = "Old Password Not Match";
            }
            return ResponseModel;
        }

        public int UpdateFailedAttempt(string userName)
        {
            var userInfo = _userRepository.FindOne(x => x.UserName == userName);
            int isSusscced = 0;
            if (userInfo != null)
            {
                userInfo.AttemptCount = userInfo.AttemptCount + 1;
                _userRepository.Edit(userInfo);
                isSusscced = 1;
            }
            return isSusscced;
        }

        public string GetErrorMessage(string userName)
        {
            string msg = "";
            var userInfo = _userRepository.FindOne(x => x.UserName == userName);
            if (userInfo != null)
            {
                if (userInfo.AttemptCount > 500)
                {
                    msg = "You are trying to login with the wrong password more than 500 times. Please contact Head Office Admin.";
                }
                else if (userInfo.ApprovedUser == false)
                {
                    msg = "Your login ID is inactive.";
                }
                else
                {
                    msg = "Invalid username or password.";
                }
            }
            return msg;
        }

        public List<User> FindUser(string userName)
        {
            return _userRepository.Filter(x => x.UserName.ToLower() == userName.ToLower()).ToList();
        }

        public User FindOne(string userName)
        {
            return _userRepository.Filter(x => x.UserName == userName).Include(x => x.Role).Include(x => x.BranchInfo).Include(x => x.BranchInfo.ZoneInfo).FirstOrDefault();
        }

        public List<UserBankInfo> GetUserBankInfos()
        {
            List<UserBankInfo> userBankInfos;
            userBankInfos = _userBankInfoRepository.All().ToList();
            return userBankInfos;
        }

        public UserBankInfo FindOneUserBankInfo(string userName)
        {
            return _userBankInfoRepository.FindOne(x => x.UserName == userName);
        }

        public ResponseModel UpdateStatus(string UserId, bool status)
        {
            long userId = Convert.ToInt32(UserId);
            User oldData = _userRepository.FindOne(x => x.UserId == userId);
            if (oldData != null)
            {
                oldData.ApprovedUser = status;
                ResponseModel.ResponsStatus = _userRepository.Edit(oldData);
                ResponseModel.Message = status ? "User has been activated." : "User has been in active.";
            }
            else
            {
                ResponseModel.Message = "User not found.";
            }
            return ResponseModel;
        }

        public int UpdateLogInStatus(string clientMachineName, string clientIP, long userId, bool IsValidate)
        {
            return _userRepository.UpdateUserLoginStatus(clientMachineName, clientIP, userId, IsValidate);
        }

        public string CheckLogin(long userID)
        {
            return _userRepository.CheckLogin(userID);
        }

        public ResponseModel SessionOut(string userId)
        {
            User oldData = _userRepository.FindOne(x => x.UserName == userId && x.IsLogin==true);
            if (oldData != null)
            {
                oldData.IsLogin = false;
                int isUpdated = _userRepository.Edit(oldData);
                if (isUpdated > 0)
                {
                    ResponseModel.ResponsStatus = 1;
                    ResponseModel.Message = userId + " has been out from the session, please try login.";
                }
                else
                {
                    ResponseModel.ResponsStatus = 0;
                    ResponseModel.Message = "Data is not updated.";
                }
            }
            else
            {
                ResponseModel.ResponsStatus = 0;
                ResponseModel.Message = userId + "User already has out the session.";
            }
            return ResponseModel;
        }

        public UserProfile FindOneProfile(long userId)
        {
            return _userProfileRepository.FindOne(x => x.UserId == userId);
        }

        public bool IsUserInRole(string username, string roleName)
        {

            return _userRepository.Filter(x => x.UserName == username)
                .Include(x => x.Role)
                .Any(x => x.Role.RoleName == roleName);
        }

        public static Task GetVerifiedUserIdAsync()
        {
            throw new NotImplementedException();
        }
    }
}
