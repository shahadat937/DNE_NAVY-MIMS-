using System.Data.Entity;
using RMS.BLL.IManager;
using RMS.DAL.IRepository;
using RMS.DAL.Repository;
using RMS.Model;
using RMS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.BLL.Manager
{
    public class TrainingInfoManager : BaseManager, ITrainingInfoManager
    {
        private ITrainingInfoRepository _trainingInfoRepository;
        public TrainingInfoManager()
        {
            _trainingInfoRepository = new TrainingInfoRepository(Instance.Context);

        }
        public List<TrainingInfo> GetAll()
        {
            return _trainingInfoRepository.All().Include(x=>x.CommonCode).Include(x=>x.CommonCode1).Include(x=>x.CommonCode2).ToList();
        }

        public TrainingInfo GetTrainingInfoById(string id)
        {
            var trainingInfoId = Convert.ToInt32(id);
            return _trainingInfoRepository.FindOne(x => x.TrainingInfoId == trainingInfoId);
        }
        public TrainingInfo GetCourseById(long id)
        {
            //var trainingInfoId = Convert.ToInt32(id);
            return _trainingInfoRepository.FindOne(x => x.NameofCourse == id);
        }

        public TrainingInfo GetTrainingInfoByRankAndCourse(long? Course, long? rank)
        {

            return _trainingInfoRepository.FindOne(x => x.NameofCourse == Course || x.RankId == rank);
            
        }

        public int Delete(string id)
        {
            TrainingInfo trainingInfoObj = GetCostCenterId(id);
            return _trainingInfoRepository.Delete(trainingInfoObj);
        }
        private TrainingInfo GetCostCenterId(string id)
        {
            int trainingInfoId = Convert.ToInt16(id);
            return _trainingInfoRepository.FindOne(x => x.TrainingInfoId == trainingInfoId);
        }

        public ResponseModel Saving(TrainingInfo trainingInfo)
        {
            TrainingInfo oldData = _trainingInfoRepository.FindOne(x => x.TrainingInfoId == trainingInfo.TrainingInfoId);
            if (oldData != null)
            {
                oldData.TrainingInfoId = trainingInfo.TrainingInfoId;
                oldData.ONOorPNO = trainingInfo.ONOorPNO;
                oldData.TrainerName = trainingInfo.TrainerName;
                oldData.RankId = trainingInfo.RankId;
                oldData.TrainingType = trainingInfo.TrainingType;
                oldData.TrainingLocation = trainingInfo.TrainingLocation;
                oldData.TrainingCategory = trainingInfo.TrainingCategory;
                oldData.TrainingFrom = trainingInfo.TrainingFrom;
                oldData.TrainingTo = trainingInfo.TrainingTo;
                oldData.Remarks = trainingInfo.Remarks;
                oldData.UpdateDate = DateTime.Now;
                //oldData.UpdateUserId =Convert.ToInt32(PortalContext.CurrentUser.UserName);
                _trainingInfoRepository.Edit(oldData);
                ResponseModel.Message = "Training inforamtin is updated successfully.";
            }
            else
            {
                TrainingInfo exist = _trainingInfoRepository.FindOne(x => x.TrainingInfoId == trainingInfo.TrainingInfoId);
                if (exist != null)
                {
                    ResponseModel.Message = "Training " + exist.TrainingInfoId + " aleady exist to the system.";
                }
                else
                {
                    trainingInfo.CreateDate = DateTime.Now;
                    //fortnightlyInfo.UpdateUserId =Convert.ToInt32(PortalContext.CurrentUser.UserName);

                    _trainingInfoRepository.Save(trainingInfo);
                    ResponseModel.Message = "Training inforamtin is saved successfully.";
                }


            }
            return ResponseModel;
        }

        public List<TrainingInfo> GetTrainingInfoAll()
        {
            return _trainingInfoRepository.All().ToList();
        }

        public ResponseModel SaveFortnightlyInfo(TrainingInfo trainingInfo)
        {
            TrainingInfo oldData = _trainingInfoRepository.FindOne(x => x.TrainingInfoId == trainingInfo.TrainingInfoId);
            if (oldData != null)
            {
                oldData.TrainingInfoId = trainingInfo.TrainingInfoId;
                oldData.ONOorPNO = trainingInfo.ONOorPNO;
                oldData.TrainerName = trainingInfo.TrainerName;
                oldData.RankId = trainingInfo.RankId;
                oldData.TrainingType = trainingInfo.TrainingType;
                oldData.TrainingLocation = trainingInfo.TrainingLocation;
                oldData.TrainingFrom = trainingInfo.TrainingFrom;
                oldData.TrainingTo = trainingInfo.TrainingTo;
                oldData.Remarks = trainingInfo.Remarks;
                oldData.CreateDate = DateTime.Now;
                oldData.CreateUserId =Convert.ToInt32(PortalContext.CurrentUser.UserName);
                _trainingInfoRepository.Edit(oldData);
                ResponseModel.Message = "Refit / Docking Information has been updated";
            }
            else
            {
                trainingInfo.UpdateDate = DateTime.Now;
                _trainingInfoRepository.Save(trainingInfo);
                ResponseModel.Message = "Fortnightly inforamtin is saved successfully.";
            }
            return ResponseModel;
        }

        public List<TrainingInfo> FindOne(long id)
        {
            return _trainingInfoRepository.Filter(x => x.TrainingInfoId == id).ToList();
        }


        public ResponseModel SaveTrainingInfo(TrainingInfo trainingInfo)
        {
            throw new NotImplementedException();
        }
    }
}
