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
   public class ShipInactiveOrgManager:BaseManager,IShipInactiveOrgManager
    {
        private IShipInactiveOrgRepository _shipInactiveOrgRepository;

        public ShipInactiveOrgManager()
        {
            _shipInactiveOrgRepository = new ShipInactiveOrgRepository(Instance.Context);
        }
        public List<ShipInactiveOrg> GetAll()
        {
            return _shipInactiveOrgRepository.All().Include(x => x.ShipInfo).Include(x => x.CommonCode).ToList();
        }


        public ShipInactiveOrg GetShipInactiveOrgById(long shipInactiveOrgIdentity)
       {
           return _shipInactiveOrgRepository.FindOne(x => x.SInactiveOrgIdentity == shipInactiveOrgIdentity);
       }
       public List<ShipInactiveOrg> FindOne(long id)
       {
           return _shipInactiveOrgRepository.Filter(x => x.SInactiveOrgIdentity == id).ToList();
       }

       public object Delete(string id)
       {
           var shipid = Convert.ToInt64(id);
           return _shipInactiveOrgRepository.Delete(x => x.SInactiveOrgIdentity == shipid);
       }

       public ResponseModel Save(ShipInactiveOrg shipInactiveOrg)
       {

           ShipInactiveOrg oldData = _shipInactiveOrgRepository.FindOne(x => x.SInactiveOrgIdentity == shipInactiveOrg.SInactiveOrgIdentity);
           if (oldData != null)
           {
               oldData.SInactiveOrgIdentity = shipInactiveOrg.SInactiveOrgIdentity;
               oldData.ShipInfoIdentity = shipInactiveOrg.ShipInfoIdentity;
               oldData.OrgId = shipInactiveOrg.OrgId;
               oldData.TotalShip = shipInactiveOrg.TotalShip;
               oldData.OrgId = shipInactiveOrg.OrgId;
               oldData.Operational = shipInactiveOrg.Operational;
               oldData.NonOperational= shipInactiveOrg.NonOperational;
               oldData.sDescription = shipInactiveOrg.sDescription;
               oldData.Remark = shipInactiveOrg.Remark;
               oldData.RefitDateFrom = shipInactiveOrg.RefitDateFrom;
               oldData.RefitDateTo = shipInactiveOrg.RefitDateTo;
               oldData.DockingDateFrom = shipInactiveOrg.DockingDateFrom;
               oldData.DockingDateTo= shipInactiveOrg.DockingDateTo;
               oldData.UserID = PortalContext.CurrentUser.UserName;
               oldData.LastUpdate = DateTime.Now;
               _shipInactiveOrgRepository.Edit(oldData);
               ResponseModel.Message = "Ship Inactive organization information is updated successfully.";
           }
           else
           {

               shipInactiveOrg.UserID = PortalContext.CurrentUser.UserName;
               shipInactiveOrg.LastUpdate = DateTime.Now.Date;
               _shipInactiveOrgRepository.Save(shipInactiveOrg);
               ResponseModel.Message = "Ship Inactive Organization information is saved successfully.";

           }
           return ResponseModel;
    }

    //   public List<ShipInactive> GetDataForReport(DateTime fromdate, DateTime toDate, long shipid)
    //   {
    //       return _shipInactiveRepository.Filter(
    //x => x.ShipIdentity == shipid && x.LastUpdate >= fromdate && x.LastUpdate <= toDate).Include(x => x.ShipInfo).ToList();
    //   }
    }
}
