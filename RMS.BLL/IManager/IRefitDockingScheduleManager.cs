using RMS.Model;
using RMS.Utility;
using System;
using System.Collections.Generic;

namespace RMS.BLL.IManager
{
    public interface IRefitDockingScheduleManager
    {
        List<RefitDockingSchedule> GetAll();
        RefitDockingSchedule GetShipId(string id);
        RefitDockingSchedule GetRefitDockingScheduleId(string id);
        ResponseModel SaveData(RefitDockingSchedule refitDockingSchedule);
        object Delete(string id);
        List<RefitDockingSchedule> GetDataForReport(DateTime fromdate, DateTime toDate, long shipid);
        vwPreviousDateValue GetPreviousDate(long? controlCode);
    }
}
