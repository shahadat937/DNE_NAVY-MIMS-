using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Model;
using RMS.Utility;

namespace RMS.BLL.IManager
{
    public interface IDailyTransactionManager
    {
       
        ResponseModel SaveTrans(DailyTransaction model);

        List<DailyTransaction> GetUnverifiedTrans();

        ResponseModel VerifyTransaction(List<string> noOfTransaction);

        ResponseModel ReverseTransaction(List<string> noOfTransaction);

        List<DailyTransaction> GetTransactionList();
        List<DailyTransaction> GetVTransactionList();

        string GetLastVoucherNo();
    }
}
