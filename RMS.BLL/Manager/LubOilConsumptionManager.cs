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
    public class LubOilConsumptionManager : BaseManager, ILubOilConsumptionManager
    {
        private readonly ILubOilConsumptionRepository _lubOilConsumptionRepository;

        public LubOilConsumptionManager()
        {
            _lubOilConsumptionRepository = new LubOilConsumptionRepository(Instance.Context);
        }


        public LubOilConsumption GetShipEdit(string id, LubOilConsumption lubOilConsumption)
        {
            var shipid = Convert.ToInt64(id);
            var costCentre = _lubOilConsumptionRepository.FindOne(x => x.LubOilConsumtionIdentity == shipid);
            return costCentre;
        }


        public List<LubOilConsumption> GetAll()
        {

            return _lubOilConsumptionRepository.All().Include(x => x.ControlShipInfo).Include(x => x.CommonCode).ToList();

        }

        public object Delete(string id)
        {
            var machineid = Convert.ToInt64(id);
            return _lubOilConsumptionRepository.Delete(x => x.LubOilConsumtionIdentity == machineid);
        }

        public ResponseModel Saving(LubOilConsumption lubOilConsumption)
        {

            LubOilConsumption oldData = _lubOilConsumptionRepository.FindOne(x => x.LubOilConsumtionIdentity == lubOilConsumption.LubOilConsumtionIdentity);
            if (oldData != null)
            {
                oldData.LubOilConsumtionIdentity = lubOilConsumption.LubOilConsumtionIdentity;
                oldData.ShipId = lubOilConsumption.ShipId;
                oldData.Machinery = lubOilConsumption.Machinery;
                oldData.LubOilType = lubOilConsumption.LubOilType;
                oldData.DateFrom = lubOilConsumption.DateFrom;
                oldData.DateTo = lubOilConsumption.DateTo;
                oldData.Location = lubOilConsumption.Location;
                oldData.RunningHour = lubOilConsumption.RunningHour;
                oldData.Price = lubOilConsumption.Price;
                oldData.Unit = lubOilConsumption.Unit;
                oldData.Quantity = lubOilConsumption.Quantity;
                oldData.UserId = PortalContext.CurrentUser.UserName;
                oldData.LastUpdate = DateTime.Now;
                _lubOilConsumptionRepository.Edit(oldData);
                ResponseModel.Message = "Machinery information is updated successfully.";
            }
            else
            {

                LubOilConsumption exist = _lubOilConsumptionRepository.FindOne(x => x.ShipId == lubOilConsumption.ShipId && x.Machinery == lubOilConsumption.Machinery);
                if (exist != null)
                {
                    ResponseModel.Message = "Machine " + exist.Machinery + " already exist to the system.";
                }
                else
                {
                    lubOilConsumption.LastUpdate = DateTime.Now;
                    lubOilConsumption.UserId = PortalContext.CurrentUser.UserName;
                    _lubOilConsumptionRepository.Save(lubOilConsumption);
                    ResponseModel.Message = "Machinery information is saved successfully.";
                }


            }
            return ResponseModel;
        }
    }
}
