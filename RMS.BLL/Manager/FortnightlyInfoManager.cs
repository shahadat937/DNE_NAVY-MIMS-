using RMS.BLL.IManager;
using RMS.DAL.IRepository;
using RMS.DAL.Repository;
using RMS.Model;
using RMS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace RMS.BLL.Manager
{
    public class FortnightlyInfoManager : BaseManager, IFortnightlyInfoManager
    {
        private IFortnightlyInfoRepository _fortnightlyInfoRepository;
        //private IControlShipInfoRepository _controlShipInfoRepository;
        public FortnightlyInfoManager()
        {
            _fortnightlyInfoRepository = new FortnightlyInfoRepository(Instance.Context);

        }
        public List<FortNightly> GetAll()
        {
            //return _damageMachineriesInfoRepository.All().Where(x =>x.SerialNo != null).Include(x =>x.ShipInfo).ToList();
            return _fortnightlyInfoRepository.All().Include(x => x.ControlShipInfo).Include(x => x.CommonCode).ToList();
        }

        public FortNightly GetFortnightlyInfoById(string id)
        {
            var fortnightlyInfoId = Convert.ToInt32(id);
            return _fortnightlyInfoRepository.FindOne(x => x.FortNightlyIdentity == fortnightlyInfoId);
        }

        public int Delete(string id)
        {
            FortNightly fortnightlyInfoObj = GetCostCenterId(id);
            return _fortnightlyInfoRepository.Delete(fortnightlyInfoObj);
        }
        private FortNightly GetCostCenterId(string id)
        {
            int fortnightlyInfoId = Convert.ToInt16(id);
            return _fortnightlyInfoRepository.FindOne(x => x.FortNightlyIdentity == fortnightlyInfoId);
        }

        public ResponseModel Saving(FortNightly fortnightlyInfo)
        {
            FortNightly oldData = _fortnightlyInfoRepository.FindOne(x => x.FortNightlyIdentity == fortnightlyInfo.FortNightlyIdentity);
            if (oldData != null)
            {
                oldData.ControlShipIdentity = fortnightlyInfo.ControlShipIdentity;
                oldData.WorkCategoy = fortnightlyInfo.WorkCategoy;
                oldData.WrNo = fortnightlyInfo.WrNo;
                oldData.No = fortnightlyInfo.No;
                oldData.DrType = fortnightlyInfo.DrType;
                oldData.DrDate = fortnightlyInfo.DrDate;
                oldData.Description = fortnightlyInfo.Description;
                oldData.Docket = fortnightlyInfo.Docket;
                oldData.PMH = fortnightlyInfo.PMH;
                oldData.Remarks = fortnightlyInfo.Remarks;

                oldData.LastUpdateDate = DateTime.Now;
                oldData.LastUpdateId = PortalContext.CurrentUser.UserName;
                _fortnightlyInfoRepository.Edit(oldData);
                ResponseModel.Message = "Fortnightly inforamtin is updated successfully.";
            }
            else
            {

                fortnightlyInfo.CreateDate = DateTime.Now;
                fortnightlyInfo.CreateId = PortalContext.CurrentUser.UserName;
                _fortnightlyInfoRepository.Save(fortnightlyInfo);
                ResponseModel.Message = "Fortnightly inforamtin is saved successfully.";
            }
            return ResponseModel;
        }

        public List<FortNightly> GetFortnightlyInfoAll()
        {
            return _fortnightlyInfoRepository.All().ToList();
        }

        public ResponseModel SaveFortnightlyInfo(FortNightly fortnightlyInfo)
        {
            FortNightly oldData = _fortnightlyInfoRepository.FindOne(x => x.FortNightlyIdentity == fortnightlyInfo.FortNightlyIdentity);
            if (oldData != null)
            {
                //oldData.FortnightlyId = fortnightlyInfo.FortnightlyId;
                //oldData.CommonCodeID = fortnightlyInfo.CommonCodeID;
                //oldData.ShipInfoIdentity = fortnightlyInfo.ShipInfoIdentity;
                //oldData.FortnightlyName = fortnightlyInfo.FortnightlyName;
                //oldData.RefitDockSelectedStartDate = fortnightlyInfo.RefitDockSelectedStartDate;
                //oldData.RefitDockSelectedEndDate = fortnightlyInfo.RefitDockSelectedEndDate;
                //oldData.RefitStartDate = fortnightlyInfo.RefitStartDate;
                //oldData.RefitEndDate = fortnightlyInfo.RefitEndDate;
                //oldData.DockingDate = fortnightlyInfo.DockingDate;
                //oldData.UnDockingDate = fortnightlyInfo.UnDockingDate;
                //oldData.ProgressTime = fortnightlyInfo.ProgressTime;
                //oldData.TotalWorkProgress = fortnightlyInfo.TotalWorkProgress;

                //oldData.TotalWorkNo = fortnightlyInfo.TotalWorkNo;
                //oldData.Progress25Percent = fortnightlyInfo.Progress25Percent;
                //oldData.Progress50Percent = fortnightlyInfo.Progress50Percent;
                //oldData.Progress75Percent = fortnightlyInfo.Progress75Percent;
                //oldData.Progress100Percent = fortnightlyInfo.Progress100Percent;
                //oldData.UnfishWork = fortnightlyInfo.UnfishWork;
                //oldData.Remarks = fortnightlyInfo.Remarks;
                //oldData.LastUpdate = DateTime.Now;
                //oldData.UserID = PortalContext.CurrentUser.UserName;
                _fortnightlyInfoRepository.Edit(oldData);
                ResponseModel.Message = "Refit / Docking Information has been updated";
            }
            else
            {
                //fortnightlyInfo.LastUpdate = DateTime.Now;
                //fortnightlyInfo.UserID = PortalContext.CurrentUser.UserName;
                fortnightlyInfo.TwentyFive = false;
                fortnightlyInfo.Fifty = false;
                fortnightlyInfo.SeventyFive = false;
                fortnightlyInfo.Complete = false;
                _fortnightlyInfoRepository.Save(fortnightlyInfo);
                ResponseModel.Message = "Fortnightly inforamtin is saved successfully.";
            }
            return ResponseModel;
        }

        public List<FortNightly> FindOne(long id)
        {
            return _fortnightlyInfoRepository.Filter(x => x.ControlShipIdentity == id).ToList();
        }



        public object UpdateStatus(List<FortNightly> fortnightlyInfos)
        {
            foreach (var fortnightlyInfo in fortnightlyInfos)
            {
                var oldData = _fortnightlyInfoRepository.FindOne(x => x.FortNightlyIdentity == fortnightlyInfo.FortNightlyIdentity);
                if (oldData != null)
                {
                    if (oldData.TwentyFive != fortnightlyInfo.TwentyFive || oldData.Fifty != fortnightlyInfo.Fifty ||
                        oldData.SeventyFive != fortnightlyInfo.SeventyFive ||
                        oldData.Complete != fortnightlyInfo.Complete || oldData.Remarks != fortnightlyInfo.Remarks)
                    {
                        if (oldData.TwentyFive != fortnightlyInfo.TwentyFive)
                        {
                            oldData.TwentyFive = fortnightlyInfo.TwentyFive;
                            oldData.Remarks = fortnightlyInfo.Remarks;
                            oldData.TwentyFiveId = PortalContext.CurrentUser.UserName;
                            oldData.TwentyFiveDate = DateTime.Now;
                        }
                        else if (oldData.Fifty != fortnightlyInfo.Fifty)
                        {
                            oldData.Fifty = fortnightlyInfo.Fifty;
                            oldData.Remarks = fortnightlyInfo.Remarks;
                            oldData.FiftyId = PortalContext.CurrentUser.UserName;
                            oldData.FiftyDate = DateTime.Now;
                        }
                        else if (oldData.SeventyFive != fortnightlyInfo.SeventyFive)
                        {
                            oldData.SeventyFive = fortnightlyInfo.SeventyFive;
                            oldData.Remarks = fortnightlyInfo.Remarks;
                            oldData.SeventyFiveId = PortalContext.CurrentUser.UserName;
                            oldData.SeventyFiveDate = DateTime.Now;
                        }
                        else if (oldData.Complete != fortnightlyInfo.Complete)
                        {
                            oldData.Complete = fortnightlyInfo.Complete;
                            oldData.Remarks = fortnightlyInfo.Remarks;
                            oldData.CompleteId = PortalContext.CurrentUser.UserName;
                            oldData.CompleteDate = DateTime.Now;
                        }
                        oldData.LastUpdateDate = DateTime.Now;
                        oldData.Remarks = fortnightlyInfo.Remarks;
                        oldData.LastUpdateId = PortalContext.CurrentUser.UserName;
                        _fortnightlyInfoRepository.Edit(oldData);
                    }

                }
            }

            return fortnightlyInfos;
        }
    }
}
