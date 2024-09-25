using RMS.Model;
using RMS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.BLL.IManager
{
    public interface IFortnightlyInfoManager
    {
        List<FortNightly> GetAll();
        FortNightly GetFortnightlyInfoById(string id);
        int Delete(string id);
        ResponseModel Saving(FortNightly fortnightlyInfo);

        List<FortNightly> GetFortnightlyInfoAll();


        ResponseModel SaveFortnightlyInfo(FortNightly fortnightlyInfo);

        List<FortNightly> FindOne(long id);
        object UpdateStatus(List<FortNightly> fortnightlyInfos);
    }
}
