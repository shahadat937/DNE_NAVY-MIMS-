using System;
using System.Data.Entity;
using System.Net.NetworkInformation;
using RMS.BLL.IManager;
using RMS.DAL.IRepository;
using RMS.DAL.Repository;
using RMS.Model;
using RMS.Utility;

namespace RMS.BLL.Manager
{
    public class UserLogInfoManager:BaseManager,IUserLogInfoManager
    {
        private IUserLogInfoRepository _userLogInfoRepository;
        private IUserRepository _userRepository;

        public UserLogInfoManager()
        {
            _userLogInfoRepository = new UserLogInfoRepository(Context);
            _userRepository= new UserRepository(Context);
        }

        public int SaveToEvent(string clientMachineName, string ipAddress, string physicalAddress)
        {
            //string nicCards = "";
            UserLogInfo eventLog = new UserLogInfo();
            eventLog.LoginUserId = PortalContext.CurrentUser.UserName;
            eventLog.Workstation = clientMachineName;
            eventLog.IPAddress = ipAddress;
            eventLog.MACAddress = physicalAddress;
            eventLog.EventTime = DateTime.UtcNow;
            eventLog.Country = "Bangladesh";
            eventLog.Location = "Dhaka";
            eventLog.Remarks = "";
            int isSaved = _userLogInfoRepository.Save(eventLog);

            var userInfo = _userRepository.FindOne(x => x.UserName == PortalContext.CurrentUser.UserName);
            if (userInfo != null)
            {
                //userInfo.LastActivityDate = DateTime.UtcNow;
                userInfo.AttemptCount = 0;
                _userRepository.Edit(userInfo);
            }
            return isSaved;
        }
    }
}
