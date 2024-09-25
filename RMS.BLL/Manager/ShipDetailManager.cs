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
    public class ShipDetailManager : BaseManager, IShipDetailManager
    {
        private IShipDetailRepository _shipDetailRepository;
        public ShipDetailManager()
        {
            _shipDetailRepository = new ShipDetailRepository(Instance.Context);
        }

        public List<ShipDetail> GetAll()
        {
            return _shipDetailRepository.All().Include(x => x.ControlShipInfo).Include(x => x.CommonCode).ToList();
        }

        public void Save(ShipDetail model)
        {

            ShipDetail oldData = _shipDetailRepository.FindOne(x => x.ShipDetailsIdentity == model.ShipDetailsIdentity);
            if (oldData != null)
            {
                oldData.ShipId = model.ShipId;
                oldData.SpecificationType = model.SpecificationType;
                oldData.Specification = model.Specification;
                oldData.SpecificationValue = model.SpecificationValue??"";
                oldData.AdditionalValue = model.AdditionalValue??"";
                oldData.Remarks = model.Remarks??"";
                oldData.UserId = PortalContext.CurrentUser.UserName;
                oldData.LastUpdate = DateTime.Now;
                _shipDetailRepository.Edit(oldData);
            }
            else
            {
                var obj = new ShipDetail
                {
                    ShipId = model.ShipId,
                    SpecificationType = model.SpecificationType,
                    Specification = model.Specification,
                    SpecificationValue = model.SpecificationValue ?? "",
                    AdditionalValue = model.AdditionalValue ?? "",
                    Remarks = model.Remarks ?? "",
                    UserId = PortalContext.CurrentUser.UserName,
                    LastUpdate = DateTime.Now
                };
                _shipDetailRepository.Save(obj);
            }
        }

        public void Delete(long Id)
        {
            var oldData = _shipDetailRepository.FindOne(x => x.ShipDetailsIdentity == Id);
            if (oldData != null)
            {
                _shipDetailRepository.Delete(oldData);
            }
        }

        public ShipDetail GetShipDetailById(long shipId)//
        {
            return _shipDetailRepository.FindOne(x => x.ShipDetailsIdentity == shipId);
        }

        public ShipDetail GetDetailById(long shipDetailsIdentity)
        {
            return _shipDetailRepository.FindOne(x => x.ShipDetailsIdentity == shipDetailsIdentity);
        }
    }
}
