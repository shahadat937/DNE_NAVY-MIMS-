using System;
using System.Collections;
using System.Collections.Generic;
using RMS.Model;
using RMS.Utility;

namespace RMS.BLL.IManager
{
   public interface IMachineryRunningManager
    {
       List<MachineryRunningInfo> GetAll();
       MachineryRunningInfo GetShip(string id, MachineryRunningInfo machineryRunningInfo);
       List<MachineryRunningInfo> FindOne(long id);

       object Delete(string id);
       ResponseModel Saving(MachineryRunningInfo machineryRunningInfo);
       MachineryRunningInfo GetShipId(string searchKey);
       List<MachineryRunningInfo> GetReportData(DateTime fromdate, DateTime toDate, long shipid);
    }
}
