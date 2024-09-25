using System;
using System.Collections.Generic;
using RMS.Model;
using RMS.Utility;

namespace RMS.BLL.IManager
{
    public interface IProcurementScheduleManager
  {
      List<ProcurementSchedule> GetAll();
    
      ProcurementSchedule GetProcurmentmentById(long ProcurementID);
      List<ProcurementSchedule> FindOne(long id);
      List<ProcurementSchedule> FindOne1(DateTime DateFrom, DateTime DateTo); 
      object Delete(string id);
      ResponseModel Save(ProcurementSchedule procurement);
  }
}
