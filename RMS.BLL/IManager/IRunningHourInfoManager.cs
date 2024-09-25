using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Model;
using RMS.Utility;

namespace RMS.BLL.IManager
{
  public interface IRunningHourInfoManager
    {
      List<RunningHourInfo> GetAll();
      RunningHourInfo GetMachine(long? id);
      ResponseModel Saving(RunningHourInfo runningHourInfo);
      object Delete(long id);
      List<RunningHourInfo> UserWiseData(int? verifyType);
      ResponseModel VerifiedStatusUpdate(List<RunningHourInfo> runningHourInfos);
    }
}
