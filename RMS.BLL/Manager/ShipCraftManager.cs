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
    public class ShipCraftManager :BaseManager,IShipCraftManager
    {
        private IShipCraftRepository _shipCraftRepository;
        private IShipInfoRepository _shipInfoRepository;
        private IControlShipInfoRepository _controlShipInfoRepository;
        public ShipCraftManager ()
        {
            _shipCraftRepository = new ShipCraftRepository(Instance.Context);
            _shipInfoRepository = new ShipInfoRepository(Instance.Context);
            _controlShipInfoRepository=new ControlShipInfoRepository(Instance.Context);
        }

        public List<ShipInfo> GetAll()
        {
            return _shipInfoRepository.All().Include(x=>x.CommonCode).ToList();
        }
        public ShipInfo GetShipById(string id)
        {
            var shipid = Convert.ToInt64(id);
            return _shipInfoRepository.FindOne(x => x.ShipInfoIdentity == shipid);
        }

        public int Delete(string id)
        {
            ShipInfo shipinfobj = GetCostCenterId(id);
            return _shipInfoRepository.Delete(shipinfobj);
        }

        private ShipInfo GetCostCenterId(string id)
        {
            int shipid = Convert.ToInt16(id);
            return _shipInfoRepository.FindOne(x => x.ShipInfoIdentity == shipid);
        }

        public ResponseModel Saving(ShipInfo shipInfo)
        {
            ShipInfo oldData = _shipInfoRepository.FindOne(x=>x.ShipInfoIdentity==shipInfo.ShipInfoIdentity);
            if (oldData != null)
            {
                oldData.ShipId = shipInfo.ShipId;
                oldData.ControlShipInfoId = shipInfo.ControlShipInfoId;
                oldData.ShipName = ShipName(shipInfo.ControlShipInfoId);
                oldData.SQD = shipInfo.SQD;
                oldData.RefitInterval = shipInfo.RefitInterval;
                oldData.RefitIntervalType = shipInfo.RefitIntervalType;
                oldData.RefitDuration = shipInfo.RefitDuration;
                oldData.RefitDurationType = shipInfo.RefitIntervalType;
                oldData.DockingDuration = shipInfo.DockingDuration;
                oldData.DockingDurationType = shipInfo.DockingDurationType;
                oldData.LastRefitDate = shipInfo.LastRefitDate;
                oldData.LastDockingDate = shipInfo.LastDockingDate;
                oldData.Length = shipInfo.Length;
                oldData.Breadth = shipInfo.Breadth;
                oldData.DraughtAFT = shipInfo.DraughtAFT;
                oldData.DraughtFWD = shipInfo.DraughtFWD;
                oldData.Displacement = shipInfo.Displacement;
                oldData.PropellerQty = shipInfo.PropellerQty;
                oldData.RudderQty = shipInfo.RudderQty;
                oldData.SpeedMax = shipInfo.SpeedMax;
                oldData.SpeedCont = shipInfo.SpeedCont;
                oldData.SpeedEcon = shipInfo.SpeedEcon;
                oldData.SpeedMin = shipInfo.SpeedMin;
                oldData.TankCapacityFW = shipInfo.TankCapacityFW;
                oldData.TankCapacityFuel = shipInfo.TankCapacityFuel;
                oldData.TankCapacityLuboil = shipInfo.TankCapacityLuboil;
                oldData.Remarks = shipInfo.Remarks;
                oldData.UserId = PortalContext.CurrentUser.UserName;
                oldData.LastUpdate = DateTime.Now;
                _shipInfoRepository.Edit(oldData);
                ResponseModel.Message = "Ship Inforamtin is updated successfully.";
            }
            else
            {
                ShipInfo exist = _shipInfoRepository.FindOne(x => x.ControlShipInfoId == shipInfo.ControlShipInfoId);
                if (exist != null)
                {
                    ResponseModel.Message = "Ship " + exist.ShipName + " aleady exist to the system.";
                }
                else
                {
                    shipInfo.LastUpdate = DateTime.Now;
                    shipInfo.UserId = PortalContext.CurrentUser.UserName;
                    shipInfo.ShipName = ShipName(shipInfo.ControlShipInfoId);
                    _shipInfoRepository.Save(shipInfo);
                    ResponseModel.Message = "Ship Inforamtin is saved successfully.";
                }
               
                
            }
            return ResponseModel;
        }

        public List<ShipInfo> GetControlShipAll()
        {
            return _shipInfoRepository.All().ToList();
        }

        public ResponseModel SaveRefitDocking(ShipInfo model)
        {
            //int isUpdated = 0;
            ShipInfo oldData = _shipInfoRepository.FindOne(x => x.ShipInfoIdentity == model.ShipInfoIdentity);
            if (oldData != null)
            {
                oldData.RefitInterval = model.RefitInterval;
                oldData.RefitIntervalType = model.RefitIntervalType;
                oldData.RefitDuration = model.RefitDuration;
                oldData.RefitDurationType = model.RefitIntervalType;
                oldData.DockingDuration = model.DockingDuration;
                oldData.DockingDurationType = model.DockingDurationType;
                oldData.LastRefitDate = model.LastRefitDate;
                oldData.LastDockingDate = model.LastDockingDate;
                oldData.NextDockingDate = model.NextDockingDate;
                oldData.NextRefitDate = model.NextRefitDate;
                oldData.LastUpdate = DateTime.Now;
                oldData.UserId = PortalContext.CurrentUser.UserName;
                _shipInfoRepository.Edit(oldData);
                ResponseModel.Message = "Refit / Docking Information has been updated";
            }
            //else
            //{
            //    var obj = new ShipInfo
            //    {
            //    ShipId = model.ShipId,
            //    ControlShipInfoId = model.ControlShipInfoId,
            //    //ShipName = ShipName(model.ControlShipInfoId),
            //    SQD = model.SQD,
            //    RefitInterval = model.RefitInterval,
            //    RefitIntervalType = model.RefitIntervalType,
            //    RefitDuration = model.RefitDuration,
            //    RefitDurationType = model.RefitIntervalType,
            //    DockingDuration = model.DockingDuration,
            //    DockingDurationType = model.DockingDurationType,
            //    LastRefitDate = model.LastRefitDate,
            //    LastDockingDate = model.LastDockingDate,
            //    NextDockingDate = model.NextDockingDate,
            //    NextRefitDate = model.NextRefitDate,
            //    LastUpdate = DateTime.Now,
            //    UserId = PortalContext.CurrentUser.UserName
                
            //    };
              
            //    _shipInfoRepository.Add(obj);
            //    ResponseModel.Message = "Refit / Docking Information is saved successfully.";
            //}
            else
            {
                //ShipInfo exist = _shipInfoRepository.FindOne(x => x.ControlShipInfoId == model.ControlShipInfoId);
                //if (exist != null)
                //{
                //    ResponseModel.Message = "Ship " + exist.ShipName + " aleady exist to the system.";
                //}
                //else
                //{
                    model.LastUpdate = DateTime.Now;
                    model.UserId = PortalContext.CurrentUser.UserName;
                    model.ShipName = ShipName(model.ControlShipInfoId);
                    _shipInfoRepository.Save(model);
                    ResponseModel.Message = "Ship inforamtin is saved successfully.";
                //}


            }
            return ResponseModel;
        }

        public List<ShipInfo> FindOne(long id)
        {
            return _shipInfoRepository.Filter(x=>x.ShipInfoIdentity==id).Include(x=>x.CommonCode).ToList();
        }

        public int FindCallSign(string sign)
        {
            var value = _shipInfoRepository.FindOne(x => x.ShipId == sign.Trim());
            return value != null ? 0 : 1;
        }


        private string ShipName(long shipId)
        {
           string shipName=  _controlShipInfoRepository.FindOne(x => x.ControlShipInfoId == shipId).ControlName;
            return shipName;
        }
    }
}
