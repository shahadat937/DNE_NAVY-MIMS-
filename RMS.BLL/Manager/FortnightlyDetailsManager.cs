using RMS.BLL.IManager;
using RMS.DAL.IRepository;
using RMS.DAL.Repository;
using RMS.Model;
using RMS.Utility;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace RMS.BLL.Manager
{
    public class FortnightlyDetailsManager : BaseManager, IFortnightlyDetailsManager
    {
        private IFortnightlyDetailsRepository _fortnightlyDetailsRepository;
        private IFortnightlyInfoRepository _fortnightlyInfoRepository;
        
        public FortnightlyDetailsManager()
        {
            _fortnightlyDetailsRepository = new FortnightlyDetailsRepository(Instance.Context);

        }
        public List<FortnightlyDetail> GetAll()
        {
            return _fortnightlyDetailsRepository.All().Include(x => x.FortnightlyInfo).ToList();
        }
        public FortnightlyDetail GetFortnightlyDetailById(string id)
        {
            var fortnightlyDetailId = Convert.ToInt32(id);
            return _fortnightlyDetailsRepository.FindOne(x => x.FortnightlyDeatailsId == fortnightlyDetailId);
        }
        //public int Delete(string id)
        //{
        //    FortnightlyDetail fortnightlyDetailObj = GetCostCenterId(id);
        //    return _fortnightlyDetailsRepository.Delete(fortnightlyDetailObj);
        //}

        public object Delete(string id)
        {
            var shipid = Convert.ToInt64(id);
            return _fortnightlyDetailsRepository.Delete(x => x.FortnightlyDeatailsId == shipid);
        }
        private FortnightlyDetail GetCostCenterId(string id)
        {
            int fortnightlyDetailId = Convert.ToInt32(id);
            return _fortnightlyDetailsRepository.FindOne(x => x.FortnightlyDeatailsId == fortnightlyDetailId);
        }
        public ResponseModel Saving(FortnightlyDetail fortnightlyDetail)
        {
            FortnightlyDetail oldData = new FortnightlyDetail();
            oldData = _fortnightlyDetailsRepository.FindOne(x => x.FortnightlyDeatailsId == fortnightlyDetail.FortnightlyDeatailsId);
            if (oldData != null)
            {
                
                oldData.FortnightlyDeatailsId = fortnightlyDetail.FortnightlyDeatailsId;
                oldData.FortnightlyId = fortnightlyDetail.FortnightlyId;
                oldData.RefitDockSelectedStartDate = fortnightlyDetail.RefitDockSelectedStartDate;
                oldData.RefitDockSelectedEndDate = fortnightlyDetail.RefitDockSelectedEndDate;
                oldData.RefitStartDate = fortnightlyDetail.RefitStartDate;
                oldData.RefitEndDate = fortnightlyDetail.RefitEndDate;
                oldData.DockingDate = fortnightlyDetail.DockingDate;
                oldData.UnDockingDate = fortnightlyDetail.UnDockingDate;
                oldData.ProgressTime = fortnightlyDetail.ProgressTime;
                oldData.TotalWorkProgress = fortnightlyDetail.TotalWorkProgress;
                oldData.UpdateDate = DateTime.Now;
                _fortnightlyDetailsRepository.Edit(oldData);
                ResponseModel.Message = "Fortnightly details inforamtin is updated successfully.";
            }
            else
            {
                //FortnightlyDetail exist = _fortnightlyDetailsRepository.FindOne(x => x.FortnightlyDeatailsId == fortnightlyDetail.FortnightlyDeatailsId);
                //if (exist != null)
                //{
                //    ResponseModel.Message = "Fortnightly details " + exist.FortnightlyDeatailsId + " aleady exist to the system.";
                //}
              
                    fortnightlyDetail.CreateDate = DateTime.Now;
                    _fortnightlyDetailsRepository.Save(fortnightlyDetail);
                    ResponseModel.Message = "Fortnightly details inforamtin is saved successfully.";

                   
                
            }
            return ResponseModel;
        }

        public List<FortnightlyDetail> GetFortnightlyDetailAll()
        {
            return _fortnightlyDetailsRepository.All().ToList();
        }
        public ResponseModel SaveFortnightlyDetail(FortnightlyDetail fortnightlyDetail)
        {
            FortnightlyDetail oldData = _fortnightlyDetailsRepository.FindOne(x => x.FortnightlyDeatailsId == fortnightlyDetail.FortnightlyDeatailsId);
            if (oldData != null)
            {
                oldData.CreateDate = DateTime.Now;
                oldData.CreateUserId = Convert.ToInt32(PortalContext.CurrentUser.UserName);
                _fortnightlyDetailsRepository.Edit(oldData);
                ResponseModel.Message = "Refit / Docking Information has been updated";
            }
            else
            {
                fortnightlyDetail.UpdateDate = DateTime.Now;
                _fortnightlyDetailsRepository.Save(fortnightlyDetail);
                ResponseModel.Message = "Fortnightly details inforamtin is saved successfully.";
            }
            return ResponseModel;
        }
        public List<FortnightlyDetail> FindOne(long id)
        {
            return _fortnightlyDetailsRepository.Filter(x => x.FortnightlyDeatailsId == id).ToList();
        }
    }
}
