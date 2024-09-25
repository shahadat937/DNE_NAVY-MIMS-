using System.Collections.Generic;
using System.Linq;
using RMS.BLL.IManager;
using RMS.DAL;
using RMS.DAL.IRepository;
using RMS.DAL.Repository;
using RMS.Model;

namespace RMS.BLL.Manager
{
    public class UserBankInfoManager:BaseManager,IUserBankInfoManager
    {
        private readonly IUserBankInfoRepository _userBankInfoRepository;
        public UserBankInfoManager()
        {
            _userBankInfoRepository = new UserBankInfoRepository(Context);
        }
        public UserBankInfo GetBankUserInfoX(string userName)
        {
            return _userBankInfoRepository.FindOne(x => x.UserName == userName);
        }
    }
}
