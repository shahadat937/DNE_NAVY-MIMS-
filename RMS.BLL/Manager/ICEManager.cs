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
    public class ICEManager : BaseManager, IICEManager
    {
        private IICERepository _iceRepository;

        public ICEManager()
        {
            _iceRepository = new ICERepository(Instance.Context);
        }

        public List<ICE> GetAll()
        {
            if (PortalContext.CurrentUser.RoleId == 4)
            {
                return _iceRepository.Filter(x => x.ControlShipInfo.Organization == PortalContext.CurrentUser.BranchInfoIdentity).Include(x => x.ControlShipInfo).Include(x => x.CommonCode).ToList();

            }
            else if (PortalContext.CurrentUser.RoleId == 1)
            {
                return _iceRepository.Filter(x => x.ControlShipInfoId == PortalContext.CurrentUser.ShipId).Include(x => x.ControlShipInfo).Include(x => x.CommonCode).ToList();

            }
            else
            {
                return _iceRepository.All().Include(x => x.ControlShipInfo).Include(x => x.CommonCode).ToList();

            }

        }

        public List<ICE> GetICEId(long id)
        {
            List<ICE> ice = _iceRepository.Filter(x => x.IceId == id).Include(x => x.ControlShipInfo).ToList();
            return ice;
        }
        public ICE FindOne(long id)
        {
            return _iceRepository.FindOne(x => x.IceId == id);
        }
        public List<ICE> ICEViewByShipId(int year, long shipId)
        {
            return _iceRepository.Filter(x => x.ControlShipInfoId == shipId && x.Year==year ).Include(x=>x.ControlShipInfo).Include(x=>x.CommonCode).ToList();
        }
        public List<ICE> GetReportData(DateTime? fromdate, DateTime? toDate, long shipid)
        {
            return _iceRepository.Filter(
   x => x.ControlShipInfoId == shipid && x.LastUpdate >= fromdate && x.LastUpdate <= toDate).Include(x => x.ControlShipInfo).ToList();
        }

        public ResponseModel Save(ICE ice)
        {

            var isExist =_iceRepository.Filter(x => x.ControlShipInfoId == ice.ControlShipInfoId && x.Year==ice.Year).FirstOrDefault();
            if (isExist != null)
            {
                ResponseModel.Message = "ICE information Already Exist.";
                return ResponseModel;
            }
            ICE oldData =
                _iceRepository.FindOne(x => x.IceId == ice.IceId);
            if (oldData != null)
            {
                oldData.ControlShipInfoId = ice.ControlShipInfoId;
                oldData.InstructionNo = ice.InstructionNo;
                oldData.IceDate = ice.IceDate;
                oldData.EngineManufacturer = ice.EngineManufacturer;
                oldData.EngineModel = ice.EngineModel;
                oldData.EngineWork = ice.EngineWork;
                oldData.Year = ice.Year;

                oldData.EngineSlNo = ice.EngineSlNo;
                oldData.EngineCoolingSys = ice.EngineCoolingSys;
                oldData.AfterRepairTime = ice.AfterRepairTime;
                oldData.SinceCommission = ice.SinceCommission;
                oldData.MaintenanceRemark = ice.MaintenanceRemark;

                oldData.EngineSlNo1 = ice.EngineSlNo1;
                oldData.EngineCoolingSys1 = ice.EngineCoolingSys1;
                oldData.AfterRepairTime1 = ice.AfterRepairTime1;
                oldData.SinceCommission1 = ice.SinceCommission1;
                oldData.MaintenanceRemark1 = ice.MaintenanceRemark1;

                oldData.EngineSlNo2 = ice.EngineSlNo2;
                oldData.EngineCoolingSys2 = ice.EngineCoolingSys2;
                oldData.AfterRepairTime2 = ice.AfterRepairTime2;
                oldData.SinceCommission2 = ice.SinceCommission2;
                oldData.MaintenanceRemark2 = ice.MaintenanceRemark2;

                oldData.EngineSlNo3 = ice.EngineSlNo3;
                oldData.EngineCoolingSys3 = ice.EngineCoolingSys3;
                oldData.AfterRepairTime3 = ice.AfterRepairTime3;
                oldData.SinceCommission3 = ice.SinceCommission3;
                oldData.MaintenanceRemark3 = ice.MaintenanceRemark3;

                oldData.EngineerRemark1 = ice.EngineerRemark1;
                oldData.EngineerRemark2 = ice.EngineerRemark2;
                oldData.EngineerRemark3 = ice.EngineerRemark3;

                oldData.UserId = PortalContext.CurrentUser.UserName;
                oldData.LastUpdate = DateTime.Now;
                _iceRepository.Edit(oldData);
                ResponseModel.Message = "ICE information is updated successfully.";
            }
            else
            {
                ice.UserId = PortalContext.CurrentUser.UserName;
                ice.LastUpdate = DateTime.Now.Date;
                _iceRepository.Save(ice);
                ResponseModel.Message = "ICE information is saved successfully.";
            }
            return ResponseModel;
        }

        public void Delete(long id)
        {
            _iceRepository.Delete(x => x.IceId == id);
        }
    }
}
