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
   public class ShipMovementManager:BaseManager,IShipMovementManager
    {
        private IShipMovementRepository _shipMovementRepository;

        public ShipMovementManager()
        {
            _shipMovementRepository=new ShipMovementRepository(Instance.Context);
        }
        public List<ShipMovement> GetAll()
        {
            return _shipMovementRepository.All().Include(x => x.ControlShipInfo).ToList();
            //return _shipMovementRepository.All().ToList();
        }

      
       public ShipMovement GetShipMovementById(long shipMovementIdentity)
       {
           return _shipMovementRepository.FindOne(x => x.ShipEmployedIdentity == shipMovementIdentity);
       }
       public List<ShipMovement> FindOne(long id)
       {
           return _shipMovementRepository.Filter(x => x.ShipEmployedIdentity == id).ToList();
       }

       public object Delete(string id)
       {
           var shipid = Convert.ToInt64(id);
           return _shipMovementRepository.Delete(x => x.ShipEmployedIdentity == shipid);
       }

       public ResponseModel Save(ShipMovement shipMovement)
       {
       
           ShipMovement oldData = _shipMovementRepository.FindOne(x => x.ShipEmployedIdentity == shipMovement.ShipEmployedIdentity);
           if (oldData != null)
           {
               oldData.ShipEmployedIdentity = shipMovement.ShipEmployedIdentity;
               oldData.ShipIdentity = shipMovement.ShipIdentity;
               oldData.CompletionDate = shipMovement.CompletionDate;
               oldData.RefitDate = shipMovement.RefitDate;
               oldData.UnrefitDate = shipMovement.UnrefitDate;
               oldData.DockingDate = shipMovement.DockingDate;
               oldData.UndockingDate = shipMovement.UndockingDate;
               oldData.LetterNo = shipMovement.LetterNo;
               oldData.TimeAwaitingOrderHour = shipMovement.TimeAwaitingOrderHour;
               oldData.TimeAwaitingOrderMiniute = shipMovement.TimeAwaitingOrderMiniute;

               oldData.MaintenancePeriod = shipMovement.MaintenancePeriod;
               oldData.PowerTrial = shipMovement.PowerTrial;
               oldData.NonOperational = shipMovement.NonOperational;
               oldData.InspectionBy = shipMovement.InspectionBy;
               oldData.AtSea = shipMovement.AtSea;

               oldData.DistanceRun = shipMovement.DistanceRun;
               oldData.AtNormalNotice = shipMovement.AtNormalNotice;
               oldData.TimeUnderWayHour = shipMovement.TimeUnderWayHour;
               oldData.TimeUnderWayMiniute = shipMovement.TimeUnderWayMiniute;
               oldData.MachineryUnderTrialRemarks = shipMovement.MachineryUnderTrialRemarks;

               oldData.CompletionDate = shipMovement.CompletionDate;
               oldData.EngineerOfficerRemarks = shipMovement.EngineerOfficerRemarks;
               oldData.AdministrativeAuthorityRemarks = shipMovement.AdministrativeAuthorityRemarks;
            
               oldData.UserId = PortalContext.CurrentUser.UserName;
               oldData.LastUpdate = DateTime.Now;
               _shipMovementRepository.Edit(oldData);
               ResponseModel.Message = "Ship Employed information is updated successfully.";
           }
           else
           {

            DateTime datetime = DateTime.Now;

            int currQuarter = (datetime.Month - 1) / 3 + 1;
            DateTime dtLastDay = new DateTime(datetime.Year, 3 * currQuarter + 1, 1).AddDays(-1);
            bool isExist = _shipMovementRepository.Exists(x => x.ShipIdentity == shipMovement.ShipIdentity);
               if (isExist == false)
               {
                   //shipMovement.LastQuarterDate = dtLastDay;
                   shipMovement.LastUpdate = DateTime.Now;
                   shipMovement.UserId = PortalContext.CurrentUser.UserName;
                   _shipMovementRepository.Save(shipMovement);
                   ResponseModel.Message = "Ship Employed information is saved successfully.";
               }
               else
               {
                   ResponseModel.Message = "This Ship Employed Quarter Data Exist In System.";
               }

           }
           return ResponseModel;
    }

       public List<ShipMovement> GetDataForReport(DateTime fromdate, DateTime toDate, long shipid)
       {
           return _shipMovementRepository.Filter(
    x => x.ShipIdentity == shipid && x.LastUpdate >= fromdate && x.LastUpdate <= toDate).Include(x => x.ControlShipInfo).ToList();
       }
    }
}
