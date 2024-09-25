using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.BLL.IManager;
using RMS.DAL.IRepository;
using RMS.DAL.Repository;
using RMS.Model;
using RMS.Utility;

namespace RMS.BLL.Manager
{
    public class RunningHourInfoManager : BaseManager, IRunningHourInfoManager
    {
        private IRunningHourInfoRepository _RunningHourInfoRepository;

        public RunningHourInfoManager()
        {
            _RunningHourInfoRepository =new RunningHourInfoRepository(Instance.Context);
        }

        public List<RunningHourInfo> GetAll()
        {
            return _RunningHourInfoRepository.All().Include(x =>x.MachineryInfo.ControlShipInfo).ToList();
        }

        public RunningHourInfo GetMachine(long? id)
        {
            return _RunningHourInfoRepository.FindOne(x => x.RunningMachinariesIdentity == id);
        }

        public ResponseModel Saving(RunningHourInfo runningHourInfo)
        {
            RunningHourInfo oldData = _RunningHourInfoRepository.FindOne(x => x.RunningMachinariesIdentity == runningHourInfo.RunningMachinariesIdentity);
            if (oldData != null)
            {

                oldData.MachinariesInfoIdentity = runningHourInfo.MachinariesInfoIdentity;
                oldData.RunningDate = runningHourInfo.RunningDate;
                oldData.RunningHour = runningHourInfo.RunningHour;
      
                oldData.LastUpdateId = PortalContext.CurrentUser.UserName;
                oldData.LastUpdateDate = DateTime.Now;
                _RunningHourInfoRepository.Edit(oldData);
                ResponseModel.Message = "Running information is updated successfully.";
            }
            else
            {


                runningHourInfo.CreateDate = DateTime.Now;
                runningHourInfo.CreateUserId = PortalContext.CurrentUser.UserName;
                runningHourInfo.LastUpdateId = PortalContext.CurrentUser.UserName;
                runningHourInfo.LastUpdateDate = DateTime.Now;
                runningHourInfo.IsVerified = false;
                _RunningHourInfoRepository.Save(runningHourInfo);
                    ResponseModel.Message = "Running information is saved successfully.";
               

            }
            return ResponseModel;
        }

        public object Delete(long id)
        {
            return _RunningHourInfoRepository.Delete(x => x.RunningMachinariesIdentity == id);
        }

        public List<RunningHourInfo> UserWiseData(int? verifyType)
        {
            var allData =
               _RunningHourInfoRepository.Filter(
                   x => x.MachineryInfo.ControlShipInfoId == PortalContext.CurrentUser.ShipId).Include(x => x.MachineryInfo.ControlShipInfo)
                   .ToList();
            return verifyType == 1006
                ? allData.Where(x => x.IsVerified == true).ToList()
                : allData.Where(x => x.IsVerified == false).ToList();
        }

        public ResponseModel VerifiedStatusUpdate(List<RunningHourInfo> runningHourInfos)
        {
            foreach (var item in runningHourInfos)
            {
                var oldData = _RunningHourInfoRepository.FindOne(x => x.RunningMachinariesIdentity == item.RunningMachinariesIdentity);
                if (oldData != null && oldData.IsVerified != item.IsVerified)
                {
                    oldData.IsVerified = item.IsVerified;
                    oldData.VerifiedBy = PortalContext.CurrentUser.UserName;
                    oldData.VerifiedDate = DateTime.Now.Date;

                    _RunningHourInfoRepository.Edit(oldData);
                    ResponseModel.Message = "Status Updated Successfully !";
                }
            }
            return ResponseModel;
        }
    }
}
