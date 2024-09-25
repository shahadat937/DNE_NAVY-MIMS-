using System.Collections.Generic;
using RMS.Model;

namespace RMS.BLL.IManager
{
    public interface IUserBankInfoMvcManager
    {
        List<UserBankInfoMVC> GetBankUserInfoX(string userName);
    }
}
