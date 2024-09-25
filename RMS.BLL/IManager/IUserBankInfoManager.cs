using System.Collections.Generic;
using RMS.Model;

namespace RMS.BLL.IManager
{
    public interface IUserBankInfoManager
    {
        UserBankInfo GetBankUserInfoX(string userName);
    }
}
