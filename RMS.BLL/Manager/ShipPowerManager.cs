using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using RMS.BLL.IManager;
using RMS.DAL.Repository;
using RMS.Model;
using RMS.Utility;

namespace RMS.BLL.Manager
{
    public class ShipPowerManager :BaseManager,IShipPowerManager
    {
        private readonly ShipPowerRepository _shipPowerRepository;
        public ShipPowerManager()
        {
            _shipPowerRepository = new ShipPowerRepository(Instance.Context);
        }

        public List<ShipPower> GetAll()
        {
            return _shipPowerRepository.All().Include(x=>x.ControlShipInfo).ToList();
        }
        public ShipPower GetShipId(string id)
        {
            var shipid = Convert.ToInt64(id);
            return _shipPowerRepository.FindOne(x => x.ShipPowerIdentity == shipid);
        }
        public object Delete(string id)
        {
            var shipid = Convert.ToInt64(id);
            return _shipPowerRepository.Delete(x => x.ShipPowerIdentity == shipid);
        }

        public ResponseModel SaveData(ShipPower shipPower)
        {

            ShipPower oldData = _shipPowerRepository.FindOne(x => x.ShipPowerIdentity == shipPower.ShipPowerIdentity);
            if (oldData != null)
            {
                oldData.ShipPowerIdentity = shipPower.ShipPowerIdentity;
                oldData.ShipId = shipPower.ShipId;
                oldData.Description = shipPower.Description;
                oldData.Speed = shipPower.Speed;
                oldData.Duration = shipPower.Duration;
                oldData.EnduranceHr = shipPower.EnduranceHr;
                oldData.EnduranceNm = shipPower.EnduranceNm;
                oldData.ShaftSpeed = shipPower.ShaftSpeed;
                oldData.Power = shipPower.Power;
                oldData.FuelConsumption = shipPower.FuelConsumption;
                oldData.UserId = PortalContext.CurrentUser.UserName;
                oldData.Lastupdate = DateTime.Now;
                _shipPowerRepository.Edit(oldData);
                ResponseModel.Message = "Machinery information is updated successfully.";
            }
            else
            {


                shipPower.Lastupdate = DateTime.Now;
                shipPower.UserId = PortalContext.CurrentUser.UserName;
                _shipPowerRepository.Save(shipPower);
                ResponseModel.Message = "Machinery information is saved successfully.";

            }
            return ResponseModel;
        }

      
     
    }
}
