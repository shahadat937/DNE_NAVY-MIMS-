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
    public class AsAsInfoManager : BaseManager, IAsAsInfoManager
    {
        private IAs_AsInfoRepository _AsAsInfoRepository;

        public AsAsInfoManager()
        {
            _AsAsInfoRepository = new As_AsInfoRepository(Instance.Context);
        }

        public List<AsAndAsInfo> GetAll()
        {
            if (PortalContext.CurrentUser.RoleId == 4)
            {
                return _AsAsInfoRepository.Filter(x => x.ControlShipInfo.Organization == PortalContext.CurrentUser.BranchInfoIdentity).Include(x => x.ControlShipInfo).Include(x => x.CommonCode).ToList();
            }
            else if (PortalContext.CurrentUser.RoleId == 1)
            {
                return _AsAsInfoRepository.Filter(x => x.ControlShipInfoId == PortalContext.CurrentUser.ShipId).Include(x => x.ControlShipInfo).Include(x => x.CommonCode).ToList();

            }
            else
            {
                return _AsAsInfoRepository.All().Include(x => x.ControlShipInfo).Include(x => x.CommonCode).ToList();

            }
        }
        public List<AsAndAsInfo> GetAllByShipId(long ShipId)
        {
            return _AsAsInfoRepository.Filter(x=>x.ControlShipInfoId==ShipId).Include(x => x.ControlShipInfo).ToList();
        }

        public List<AsAndAsInfo> GetAsAsId(long id)
        {
            List<AsAndAsInfo> deckInfos =
                _AsAsInfoRepository.Filter(x => x.AsAndAsIdentity == id).Include(x => x.ControlShipInfo).ToList();
            return deckInfos;
        }

        public List<AsAndAsInfo> UserWiseData(int? verifyType)
        {
            var allData = _AsAsInfoRepository.Filter(x => x.ControlShipInfoId == PortalContext.CurrentUser.ShipId).Include(x => x.ControlShipInfo).ToList();
            return verifyType == 1006
                ? allData.Where(x => x.IsVerified == true).ToList()
                : allData.Where(x => x.IsVerified == false).ToList();
        }

        public ResponseModel VerifiedStatusUpdate(List<AsAndAsInfo> asAsInfos)
        {
            foreach (var item in asAsInfos)
            {
                var oldData = _AsAsInfoRepository.FindOne(x => x.AsAndAsIdentity == item.AsAndAsIdentity);
                if (oldData != null && oldData.IsVerified != item.IsVerified)
                {
                    oldData.IsVerified = item.IsVerified;
                    oldData.VerifiedBy = PortalContext.CurrentUser.UserName;
                    oldData.VerifiedDate = DateTime.Now.Date;
                    _AsAsInfoRepository.Edit(oldData);
                    ResponseModel.Message = "Status Updated Successfully !";
                }
            }
            return ResponseModel;
        }

        public AsAndAsInfo GetAsAsInfoById(long As_AsId)
        {
            return _AsAsInfoRepository.FindOne(x => x.AsAndAsIdentity == As_AsId);
        }

        public List<AsAndAsInfo> FindOne(long id)
        {
            return _AsAsInfoRepository.Filter(x => x.ControlShipInfoId == id).ToList();
        }

        public AsAndAsInfo FindFile(long id)
        {
            return _AsAsInfoRepository.FindOne(x => x.AsAndAsIdentity == id);
        }

        public object Delete(string id)
        {
            var AsAndAsid = Convert.ToInt64(id);
            return _AsAsInfoRepository.Delete(x => x.AsAndAsIdentity == AsAndAsid);
        }

        public ResponseModel Save(AsAndAsInfo asAsInfo)
        {

            AsAndAsInfo oldData = _AsAsInfoRepository.FindOne(x => x.AsAndAsIdentity == asAsInfo.AsAndAsIdentity);
            if (oldData != null)
            {
                //oldData.As_AsId = asAsInfo.As_AsId;
                oldData.ControlShipInfoId = asAsInfo.ControlShipInfoId;
                //oldData.SerialNo = asAsInfo.SerialNo;
                oldData.AsAndAsDate = asAsInfo.AsAndAsDate;
                oldData.ApprovalDate = asAsInfo.ApprovalDate;
                oldData.RaisingDate = asAsInfo.RaisingDate;
                oldData.DescriptionOfAsAs = asAsInfo.DescriptionOfAsAs;
                oldData.ClassId = asAsInfo.ClassId;
                oldData.AuthorityRemark = asAsInfo.AuthorityRemark;
                oldData.LWLM = asAsInfo.LWLM;
                if (asAsInfo.DockingPlace != null)
                {
                    oldData.DockingPlace = asAsInfo.DockingPlace;
                }
                oldData.DeltaTon = asAsInfo.DeltaTon;

                oldData.KMTM = asAsInfo.KMTM;
                oldData.GMTM = asAsInfo.GMTM;
                oldData.KGM = asAsInfo.KGM;
                oldData.LCFM = asAsInfo.LCFM;
                oldData.LCGM = asAsInfo.LCGM;
                oldData.TPCTonCm = asAsInfo.TPCTonCm;
                oldData.MCTM = asAsInfo.MCTM;
                oldData.DFM = asAsInfo.DFM;
                oldData.DAM = asAsInfo.DAM;
                oldData.TRIMM = asAsInfo.TRIMM;
                oldData.THETADeg = asAsInfo.THETADeg;
                oldData.UserId = PortalContext.CurrentUser.UserName;
                oldData.LastUpdate = DateTime.Now;
                _AsAsInfoRepository.Edit(oldData);
                ResponseModel.Message = "As & As information is updated successfully.";
            }
            else
            {
                asAsInfo.EntryDate = DateTime.Now;
                asAsInfo.LastUpdate = DateTime.Now;
                asAsInfo.UserId = PortalContext.CurrentUser.UserName;
                _AsAsInfoRepository.Save(asAsInfo);
                ResponseModel.Message = "As & As information is saved successfully.";
            }
            return ResponseModel;
        }

    }
}
