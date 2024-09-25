using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using RMS.BLL.IManager;
using RMS.DAL.IRepository;
using RMS.DAL.Repository;
using RMS.Model;
using RMS.Utility;

namespace RMS.BLL.Manager
{
   public class RefitDockingScheduleManager :BaseManager,IRefitDockingScheduleManager
   {
       private IRefitDockingScheduleRepository _refitDockingSchedule;

       public RefitDockingScheduleManager()
       {
           _refitDockingSchedule =new RefitDockingScheduleRepository(Instance.Context);
       }

       public List<RefitDockingSchedule> GetAll()
       {
           return _refitDockingSchedule.All().Include(x=>x.ControlShipInfo).Include(x=>x.CommonCode).ToList();
       }

       public RefitDockingSchedule GetShipId(string id)
       {
           var refitid = Convert.ToInt64(id);
           return _refitDockingSchedule.FindOne(x => x.RefitDockingIdentity == refitid);
       }
        public RefitDockingSchedule GetRefitDockingScheduleId(string id)
        {
            var refitid = Convert.ToInt64(id);
            return _refitDockingSchedule.Filter(x => x.RefitDockingIdentity == refitid).Include(x=>x.ControlShipInfo).SingleOrDefault();
        }
        public ResponseModel SaveData(RefitDockingSchedule refitDockingSchedule)
       {
           RefitDockingSchedule oldData = _refitDockingSchedule.FindOne(x => x.RefitDockingIdentity == refitDockingSchedule.RefitDockingIdentity);
           if (oldData != null)
           {
               oldData.RefitDockingIdentity = refitDockingSchedule.RefitDockingIdentity;
               oldData.ControlShipInfoId = refitDockingSchedule.ControlShipInfoId;
               oldData.LastRefitFrom = refitDockingSchedule.LastRefitFrom;
               oldData.LastRefitTo = refitDockingSchedule.LastRefitTo;
               oldData.LastDockingFrom = refitDockingSchedule.LastDockingFrom;
               oldData.LastDockingTo = refitDockingSchedule.LastDockingTo;
               oldData.Docked = refitDockingSchedule.Docked;
               oldData.PNextRefitFrom = refitDockingSchedule.PNextRefitFrom;
               oldData.PNextRefitTo = refitDockingSchedule.PNextRefitTo;
               oldData.PNextDockingFrom = refitDockingSchedule.PNextDockingFrom;
               oldData.PNextDockingTo = refitDockingSchedule.PNextDockingTo;
                oldData.StatusId = refitDockingSchedule.StatusId;
                oldData.Reason = refitDockingSchedule.Reason;
               oldData.UpdatedBy = PortalContext.CurrentUser.UserName;
               oldData.LastUpdate = DateTime.Now;
         
               _refitDockingSchedule.Edit(oldData);
               ResponseModel.Message = "Refit Docking Schedule is updated successfully.";
           }
           else
           {


               refitDockingSchedule.LastUpdate = DateTime.Now;
               refitDockingSchedule.UserId = PortalContext.CurrentUser.UserName;
               refitDockingSchedule.EntryDate = DateTime.Now;
               refitDockingSchedule.UpdatedBy = PortalContext.CurrentUser.UserName;
               refitDockingSchedule.IsVerified = false;
               _refitDockingSchedule.Save(refitDockingSchedule);
               ResponseModel.Message = "Refit Docking Schedule is saved successfully.";

           }
           return ResponseModel;
       }

       public object Delete(string id)
       {
           var refitId = Convert.ToInt64(id);
           return _refitDockingSchedule.Delete(x => x.RefitDockingIdentity == refitId);
       }

       public List<RefitDockingSchedule> GetDataForReport(DateTime fromdate, DateTime toDate, long shipid)
       {
           return _refitDockingSchedule.Filter(
    x => x.ControlShipInfoId == shipid && x.LastUpdate >= fromdate && x.LastUpdate <= toDate).Include(x => x.ControlShipInfo).ToList();
       }

       public vwPreviousDateValue GetPreviousDate(long? controlCode)
       {
           return _refitDockingSchedule.PreviousDate(controlCode);
       }
   }
}
