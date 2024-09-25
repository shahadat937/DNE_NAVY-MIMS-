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
    public class ShipSpeedTrialManager : BaseManager, IShipSpeedTrialManager
    {
        private IShipSpeedTrialRepository _shipSpeedTrialRepository;

        public ShipSpeedTrialManager()
        {
            _shipSpeedTrialRepository = new ShipSpeedTrialRepository(Instance.Context);
        }


        //public void Save(ShipSpeedTrial model)
        //{
        //   
        //}

        public ShipSpeedTrial GetShipSpeedById(long shipSpeedIdentity)
        {
            return _shipSpeedTrialRepository.FindOne(x => x.ShipSpeedTrialIdentity == shipSpeedIdentity);
        }


        public List<ShipSpeedTrial> GetAll()
        {
            return _shipSpeedTrialRepository.All().Include(x => x.ControlShipInfo).ToList();
        }

        public ResponseModel Save(ShipSpeedTrial shipSpeedTrial)
        {
            ShipSpeedTrial oldData = _shipSpeedTrialRepository.FindOne(x => x.ShipSpeedTrialIdentity == shipSpeedTrial.ShipSpeedTrialIdentity);
            if (oldData != null)
            {
                oldData.ShipId = shipSpeedTrial.ShipId;
                oldData.Description = shipSpeedTrial.Description ?? "";
                oldData.PortSpeed = shipSpeedTrial.PortSpeed;
                oldData.StbdSpeed = shipSpeedTrial.StbdSpeed;
                oldData.StableSpeed = shipSpeedTrial.StableSpeed;
                oldData.Remarks = shipSpeedTrial.Remarks;
                oldData.UserId = PortalContext.CurrentUser.UserName;
                oldData.Lastupdate = DateTime.Now;
                _shipSpeedTrialRepository.Edit(oldData);
                ResponseModel.Message = "Machinery information is updated successfully.";
            }
            else
            {

                ShipSpeedTrial exist = _shipSpeedTrialRepository.FindOne(x => x.ShipId == shipSpeedTrial.ShipId );
                if (exist != null)
                {
                    ResponseModel.Message = "Ship name already exist in the system.";
                }
                else
                {
                    shipSpeedTrial.Lastupdate = DateTime.Now;
                    shipSpeedTrial.UserId = PortalContext.CurrentUser.UserName;
                    _shipSpeedTrialRepository.Save(shipSpeedTrial);
                    ResponseModel.Message = "Machinery information is saved successfully.";
                }
            }
            return ResponseModel;

        }

        public object Delete(string id)
        {
            var shipid = Convert.ToInt64(id);
            return _shipSpeedTrialRepository.Delete(x =>x.ShipSpeedTrialIdentity == shipid);
            
        }
    }
}
