using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using RMS.BLL.IManager;
using RMS.DAL;
using RMS.DAL.IRepository;
using RMS.DAL.Repository;
using RMS.Model;
using RMS.Utility;

namespace RMS.BLL.Manager
{
   public class ShipInactiveManager:BaseManager,IShipInactiveManager
    {
       private IShipInactiveRepository _shipInactiveRepository;
       private IMessageInfoRepository _messageInfoRepository;
       RM_AGBEntities db = new RM_AGBEntities();
        public ShipInactiveManager()
        {
            _shipInactiveRepository = new ShipInactiveRepository(Instance.Context);
            _messageInfoRepository = new MessageInfoRepository(Instance.Context);
        }
        public List<ShipInactive> GetAll()
        {
            return _shipInactiveRepository.All().Include(x => x.ControlShipInfo).Include(x =>x.CommonCode).ToList();
        }


        public ShipInactive GetShipInactiveById(long shipInactiveIdentity)
       {
           return _shipInactiveRepository.FindOne(x => x.SInactiveIdentity == shipInactiveIdentity);
       }


        public string ShipBranchName(long? ShipId)
        {
            return db.ControlShipInfoes.Where(x => x.ControlShipInfoId == ShipId).Include(x => x.BranchInfo).FirstOrDefault().BranchInfo.BranchName.ToString();
        }

        public int ShipStatusUpdate(long? ShipInactiveIdentity)
        {
            int noOfRowUpdated = db.Database.ExecuteSqlCommand($"UPDATE ShipInactive SET ShipStatus='{941}' where SInactiveIdentity={ShipInactiveIdentity}");

            return noOfRowUpdated;
        }
        

        public List<ShipInactive> FindOne(long id)
       {
           return _shipInactiveRepository.Filter(x => x.SInactiveIdentity == id).ToList();
       }

       public object Delete(string id)
       {
           var shipid = Convert.ToInt64(id);
           return _shipInactiveRepository.Delete(x => x.SInactiveIdentity == shipid);
       }

       public ResponseModel Save(ShipInactive shipInactive)
       {

           ShipInactive oldData = _shipInactiveRepository.FindOne(x => x.SInactiveIdentity == shipInactive.SInactiveIdentity);
           if (oldData != null)
           {
               oldData.SInactiveIdentity = shipInactive.SInactiveIdentity;
               oldData.ControlShipInfoIdentity = shipInactive.ControlShipInfoIdentity;
               oldData.CrashDetails = shipInactive.CrashDetails;
               oldData.InactiveDate = shipInactive.InactiveDate;
               oldData.TakenStap = shipInactive.TakenStap;
               oldData.Reference = shipInactive.Reference;
               oldData.ShipStatus = shipInactive.ShipStatus;            
               oldData.UserID = PortalContext.CurrentUser.UserName;
               oldData.LastUpdate = DateTime.Now;
               _shipInactiveRepository.Edit(oldData);
               ResponseModel.Message = "Ship Inactive information is updated successfully.";
           }
           else
           {

               shipInactive.UserID = PortalContext.CurrentUser.UserName;
               shipInactive.LastUpdate = DateTime.Now.Date;
               _shipInactiveRepository.Save(shipInactive);
               ResponseModel.Message = "Ship Inactive information is saved successfully.";

           }
           return ResponseModel;
    }

    //   public List<ShipInactive> GetDataForReport(DateTime fromdate, DateTime toDate, long shipid)
    //   {
    //       return _shipInactiveRepository.Filter(
    //x => x.ShipIdentity == shipid && x.LastUpdate >= fromdate && x.LastUpdate <= toDate).Include(x => x.ShipInfo).ToList();
    //   }

       public int FindMessage(string sign)
       {
           //var a = _shipInactiveRepository.All();
           //var value = a.Where(x => x.Reference.Trim().ToString() == sign.Trim());
           //return value != null ? 0 : 1;

           var value = _shipInactiveRepository.FindOne(x => x.Reference == sign.Trim());
           return value != null ? 0 : 1;
       }
        public List<ShipInactiveView> GetNonOpsShip()
        {
            List<ShipInactiveView> models = new List<ShipInactiveView>();
            List<ShipInactive> shipInactives= _shipInactiveRepository.Filter(x=>x.ShipStatus== 942).Include(x => x.ControlShipInfo).Include(x => x.CommonCode).ToList();
            foreach (var item in shipInactives)
            {
                ShipInactiveView model = new ShipInactiveView (){
                    ControlShipInfoIdentity=item.ControlShipInfoIdentity,
                    SInactiveIdentity=item.SInactiveIdentity,
                    ControlName=item.ControlShipInfo.ControlName,
                    BranchName= ShipBranchName(item.ControlShipInfoIdentity)
                };
                models.Add(model);

            }
            return models;
        }
    }
}
