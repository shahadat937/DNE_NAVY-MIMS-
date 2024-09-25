using System;
using System.Collections.Generic;
using System.Web.Security;
using RMS.BLL.IManager;
using RMS.BLL.Manager;
using RMS.Model;
using RMS.Utility;

namespace RMS.BLL.Providers
{

    public class CustomMemberShipProvider : MembershipProvider
    {
        private IUserManager userManager;
        private IBankInfoManager bankInfoManager;
        private IUserBankInfoManager userBankInfoManager;
        public CustomMemberShipProvider()
        {
            userManager = new UserManager();
            bankInfoManager = new BankInfoManager();
            userBankInfoManager =new UserBankInfoManager();
        }
        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer,
            bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion,
            string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override bool ValidateUser(string username, string password)
        {
            var isExist = userManager.ExistsUserManager(username, password);
            if (isExist)
            {
                UserBankInfo user = userBankInfoManager.GetBankUserInfoX(username);
              
                var aPortalUser = new PortalUser
                {
                    BranchInfoIdentity = user.BranchInfoIdentity,
                   // Code = user.co,
                    BankCodeIdentity = Convert.ToInt64(user.BankCodeIdentity),
                    BankCode =Convert.ToString(user.BankCodeIdentity),
                    BankName = user.BankName,
                    //ZoneCode = user,
                    //ZoneName = user.BranchInfo.ZoneInfo.ZoneName,
                    DistrictCodeIdentity = Convert.ToInt64(user.DistrictCodeIdentity),
                    DistrictName = user.DistrictName,
                    BranchCode = Convert.ToString(user.BranchInfoIdentity),
                    BranchName = user.BranchName,
                    //SubBranchCode = user.su,
                    //SubBranchName = user.SubBranchName,
                   // CountryCode = Convert.ToInt32(user.co),
                    //CurrencyCode = Convert.ToInt32(user.CurrencyCode),
                    UserId = user.UserId,
                    UserName = user.UserName,
                    UserFullName = user.UserFullName,
                    IsValidate = user.ApprovedUser,
                    RoleId = user.RoleId,
                    UserRole = user.RoleName,
                    NativeBranchCode = user.NationalId,
                    LoweredRoleName = user.LoweredRoleName,
                    ShipId=user.ControlShipInfoId,
                    ShipName=user.ShipName,
                    MenuTitle = (user.UserName + ", " + user.UserFullName).Replace(".", string.Empty)
                };
                PortalContext.CurrentUser = aPortalUser;
            }
            return isExist;
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }

        public override string ApplicationName { get; set; }

        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }
    }
}