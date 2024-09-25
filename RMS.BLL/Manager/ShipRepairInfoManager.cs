using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.BLL.IManager;
using RMS.DAL.IRepository;
using RMS.DAL.Repository;
using RMS.Model;
using RMS.Utility;

namespace RMS.BLL.Manager
{
    public class ShipRepairInfoManager : BaseManager, IShipRepairInfoManager
    {
         private readonly IShipRepairInfoRepository _shipRepairInfo;
         public ShipRepairInfoManager()
        {
            _shipRepairInfo = new ShipRepairInfoRepository(Instance.Context);
        }

        public List<ShipRepairInfo> GetAll()
        {
            return _shipRepairInfo.All().Include(x => x.ControlShipInfo).ToList();
        }

        public ShipRepairInfo GetShipId(string id)
        {
            var shipid = Convert.ToInt64(id);
            return _shipRepairInfo.FindOne(x => x.ShipRepairIdentity == shipid);
        }
        public ShipRepairInfo RepairCostDetailsId(string id)
        {
            var ShipRepairId = Convert.ToInt64(id);
            return _shipRepairInfo.Filter(x => x.ShipRepairIdentity == ShipRepairId).Include(x=>x.ControlShipInfo).SingleOrDefault();
        }
        public ResponseModel SaveData(ShipRepairInfo shipRepairInfo)
        {
            ShipRepairInfo oldData = _shipRepairInfo.FindOne(x => x.ShipRepairIdentity == shipRepairInfo.ShipRepairIdentity);
            if (oldData != null)
            {
                oldData.ShipRepairIdentity = shipRepairInfo.ShipRepairIdentity;
                oldData.ShipInfoIdentity = shipRepairInfo.ShipInfoIdentity;
                oldData.FinantialYear = shipRepairInfo.FinantialYear;
                oldData.DockingStart = shipRepairInfo.DockingStart;
                oldData.DockingEnd = shipRepairInfo.DockingEnd;
                oldData.RepairCost = shipRepairInfo.RepairCost;
                oldData.DockingPlace = shipRepairInfo.DockingPlace;
              
                oldData.UserId = PortalContext.CurrentUser.UserName;
                oldData.LastUpdate = DateTime.Now;
                _shipRepairInfo.Edit(oldData);
                ResponseModel.Message = "Ship Repair Information Is Updated Successfully.";
            }
            else
            {


                shipRepairInfo.LastUpdate = DateTime.Now;
                shipRepairInfo.UserId = PortalContext.CurrentUser.UserName;
                _shipRepairInfo.Save(shipRepairInfo);
                ResponseModel.Message = "Ship Repair Information Is Saved Successfully.";

            }
            return ResponseModel;
        }

        public object Delete(string id)
        {
            var shipRepairId = Convert.ToInt64(id);
            return _shipRepairInfo.Delete(x => x.ShipRepairIdentity == shipRepairId);
        }
    }
}
