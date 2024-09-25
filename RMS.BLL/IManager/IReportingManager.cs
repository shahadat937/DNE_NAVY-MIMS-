using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Model;
using RMS.Utility;

namespace RMS.BLL.IManager
{
    public interface IReportingManager
    {
        List<Reporting> GetReportingTeeView();
        List<BranchInfo> ExchangeCompany();
        List<BranchInfo> BankInfo();
        Reporting GetReportParameterBySlNo(string slNo);
    }
}
