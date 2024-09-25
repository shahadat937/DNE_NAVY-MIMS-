using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.UI;
using RMS.BLL.IManager;
using RMS.DAL.IRepository;
using RMS.DAL.Repository;
using RMS.Model;
using RMS.Utility;

namespace RMS.BLL.Manager
{
    public class DeckInfoManager : BaseManager, IDeckInfoManager
    {
        private IDeckInfoRepository _deckInfoRepository;
        public DeckInfoManager()
        {
            _deckInfoRepository = new DeckInfoRepository(Instance.Context);
        }

        public List<DeckInfo> GetAll()
        {
            return _deckInfoRepository.All().Include(x => x.CommonCode).Include(x => x.ControlShipInfo).ToList();
        }

        public DeckInfo FindOne(string id)
        {
            long deckInfoIdentity = Convert.ToInt32(id);
            return _deckInfoRepository.FindOne(x => x.DeckInfoIdentity == deckInfoIdentity);
        }

        public object Delete(string id)
        {
            var shipid = Convert.ToInt64(id);
            return _deckInfoRepository.Delete(x => x.DeckInfoIdentity == shipid);
        }
       

        public ResponseModel Save(DeckInfo deckInfo)
        {
            DeckInfo oldData = _deckInfoRepository.FindOne(x => x.DeckInfoIdentity == deckInfo.DeckInfoIdentity);
            if (oldData != null)
            {
                oldData.DeckInfoIdentity = deckInfo.DeckInfoIdentity;
                oldData.ControlShipInfoId = deckInfo.ControlShipInfoId;
                oldData.DechNo = deckInfo.DechNo;
                oldData.Compartment = deckInfo.Compartment;
                oldData.HullStatus = deckInfo.HullStatus;
                oldData.ReportDateFrom = deckInfo.ReportDateFrom;
                oldData.ReportDateTo = deckInfo.ReportDateTo;
                oldData.Location = deckInfo.Location;
                oldData.CheckDate = deckInfo.CheckDate;
                oldData.StatePlates = deckInfo.StatePlates;
                oldData.StateFrames = deckInfo.StateFrames;
                oldData.StateCement = deckInfo.StateCement;
                oldData.StateRivets = deckInfo.StateRivets;
                oldData.PaintDescriptio = deckInfo.PaintDescriptio;
                oldData.PaintState = deckInfo.PaintState;
                oldData.SanctionDisLineS = deckInfo.SanctionDisLineS ?? "";
                oldData.SanctDisNonRS = deckInfo.SanctDisNonRS ?? "";
                oldData.SanctDisNonRW = deckInfo.SanctDisNonRW ?? "";
                oldData.SanctionDisLineW = deckInfo.SanctionDisLineW ?? "";
                oldData.HeadCoverWT = deckInfo.HeadCoverWT??"";
                oldData.HeadCoverS = deckInfo.HeadCoverS??"";
                oldData.DefectDes = deckInfo.DefectDes??"";
                oldData.DefectActionTaken = deckInfo.DefectActionTaken??"";
                oldData.UserID = PortalContext.CurrentUser.UserName;
                oldData.LastUpdate = DateTime.Now;
                _deckInfoRepository.Edit(oldData);
                ResponseModel.Message = "Deck information is updated successfully.";
            }
            else
            {
                var obj = new DeckInfo
                {
                    DeckInfoIdentity = deckInfo.ControlShipInfoId,
                    ControlShipInfoId = deckInfo.ControlShipInfoId,
                    HullStatus = deckInfo.HullStatus,
                    ReportDateFrom = deckInfo.ReportDateFrom,
                    ReportDateTo = deckInfo.ReportDateTo,
                    DechNo = deckInfo.DechNo,
                    Compartment = deckInfo.Compartment,
                    Location = deckInfo.Location,
                    CheckDate = deckInfo.CheckDate,
                    StatePlates = deckInfo.StatePlates ?? "",
                    StateFrames = deckInfo.StateFrames ?? "",
                    StateCement = deckInfo.StateCement ?? "",
                    StateRivets = deckInfo.StateRivets ?? "",
                    PaintDescriptio = deckInfo.PaintDescriptio ?? "",
                    PaintState = deckInfo.PaintState ?? "",
                    SanctionDisLineS = deckInfo.SanctionDisLineS ?? "",
                    SanctDisNonRS = deckInfo.SanctDisNonRS ?? "",
                    SanctDisNonRW = deckInfo.SanctDisNonRW ?? "",
                    SanctionDisLineW = deckInfo.SanctionDisLineW ?? "",
                    HeadCoverWT = deckInfo.HeadCoverWT ?? "",
                    HeadCoverS = deckInfo.HeadCoverS ?? "",
                    DefectDes = deckInfo.DefectDes ?? "",
                    DefectActionTaken = deckInfo.DefectActionTaken ?? "",
                    CreateUserId = PortalContext.CurrentUser.UserName,
                    CreateLastUpdate = DateTime.Now,
                    UserID = PortalContext.CurrentUser.UserName,
                    LastUpdate = DateTime.Now
                };
                _deckInfoRepository.Add(obj);
                ResponseModel.Message = "Deck information is saved successfully.";
            }
            return ResponseModel;
        }
        public List<DeckInfo> GetAllByShipId(string id)
        {
            var conid = Convert.ToInt64(id);
            List<DeckInfo> deckInfos = _deckInfoRepository.Filter(x => x.ControlShipInfoId == conid).Include(x => x.ControlShipInfo).Include(x => x.CommonCode).ToList();
            return deckInfos;
        }

        public List<DeckInfo> GetReportData(DateTime fromdate, DateTime toDate, long shipid)
        {
            return _deckInfoRepository.Filter(
   x => x.ControlShipInfoId == shipid && x.ReportDateFrom >= fromdate && x.ReportDateTo <= toDate).Include(x => x.ControlShipInfoId).Include(x => x.CommonCode).ToList();
        }

       
    }
}

