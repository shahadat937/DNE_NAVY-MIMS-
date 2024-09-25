using System.Collections.Generic;
using System.Linq;
using RMS.BLL.IManager;
using RMS.DAL;
using RMS.DAL.IRepository;
using RMS.DAL.Repository;
using RMS.Model;

namespace RMS.BLL.Manager
{
    public class UserBankInfoMvcManager:BaseManager,IUserBankInfoMvcManager
    {
        private readonly IUserBankInfoMvcRepository _userBankInfoMvcRepository;
        public UserBankInfoMvcManager()
        {
            _userBankInfoMvcRepository = new UserBankInfoMvcRepository(Context);
        }
        public List<UserBankInfoMVC> GetBankUserInfoX(string userName)
        {
            return _userBankInfoMvcRepository.Filter(x => x.UserName == userName).ToList();
        }
    }
}
