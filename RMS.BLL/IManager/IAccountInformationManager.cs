using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Model;
using RMS.Utility;

namespace RMS.BLL.IManager
{
    public interface IAccountInformationManager
    {
        IEnumerable GetAccountList();
        ResponseModel SaveAccountInfo(AccountInformation model);
        AccountInformation GetAccountInfoByCode(string AccountCode);
        List<AccountInformation> GetAccountInfoTeeView(string BankCode);
    }
}
