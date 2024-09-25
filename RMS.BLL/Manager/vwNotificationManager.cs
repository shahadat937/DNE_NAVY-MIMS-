using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.SqlServer;
using System.Linq;
using RMS.BLL.IManager;
using RMS.DAL.IRepository;
using RMS.DAL.Repository;
using RMS.Model;
using System.Data.SqlClient;
namespace RMS.BLL.Manager
{
    public class vwNotificationManager:BaseManager,IvwNotificationManager
    {
        private readonly IvwNotificationRepository _vwNotificationRepository;
        private readonly IvwRefitDokingNotificationRepository _vwRefitDokingNotificationRepository;
        private readonly IMajorDemandOrProcureRepository _majorDemandOrProcureRepository;
        private readonly IMessageInfoRepository _messageInfoRepository;
        private readonly IControlShipInfoManager _controlShipInfoManager;

        public vwNotificationManager()
        {
            _vwNotificationRepository = new vwNotificationRepository(Instance.Context);
            _vwRefitDokingNotificationRepository = new vwRefitDokingNotificationRepository(Instance.Context);
            _majorDemandOrProcureRepository = new MajorDemandOrProcureRepository(Instance.Context);
            _messageInfoRepository = new MessageInfoRepository(Instance.Context);
            _controlShipInfoManager=new ControlShipInfoManager();
        }

        public List<vwRefitDokingNotification> GetRefitNotificationAll()
        {
            return _vwRefitDokingNotificationRepository.All().ToList();
        }
        public List<vwRefitDokingNotification> GetDokingNotificationAll()
        {
            return _vwRefitDokingNotificationRepository.All().ToList();
        }

        public List<vwRefitDokingNotification> GetRefitInfo()
        {
            return
                _vwRefitDokingNotificationRepository.Filter(x => x.NextRefitFromDay > 0 && x.NextRefitFromDay < 31).ToList();
        }
        public List<vwRefitDokingNotification> GetDockingInfo()
        {
            return
                _vwRefitDokingNotificationRepository.Filter(x => x.NextDokingFromDay > 0 && x.NextDokingFromDay < 31).ToList();
        }
        public List<vwNotification> GetAll()
        {
            return _vwNotificationRepository.All().ToList();
        }
        public NotificationCustomModel GetNotification()
        {
            var ConShip = _controlShipInfoManager.ShipBranchInfo();
            //var ltNotify = _vwNotificationRepository.Filter(x => x.NxtLifeTime > 0).ToList();
            //var query = (from post in ltNotify join meta in ConShip on post.ShipName equals meta.ControlName select post).ToList();
            //var remittanceInfoes = query;

            var RNotify = _vwRefitDokingNotificationRepository.Filter(x => x.NextRefitFromDay > 0 && x.NextRefitFromDay < 31).ToList();
            var query1 = (from post in RNotify join meta in ConShip on post.ControlShipInfoId equals meta.ControlShipInfoId select post).ToList();
            var Refit = query1;
            var DNotify = _vwRefitDokingNotificationRepository.Filter(x => x.NextDokingFromDay > 0 && x.NextDokingFromDay < 31).ToList();
            var query2 = (from post in DNotify join meta in ConShip on post.ControlShipInfoId equals meta.ControlShipInfoId select post).ToList();
            var Docking = query2;
            var MejorNotify = _majorDemandOrProcureRepository.Filter(x => SqlFunctions.DateDiff("DD", x.LastUpdate, DateTime.Now) < 8).ToList();
            var query3 = (from post in MejorNotify join meta in ConShip on post.ShipIdentity equals meta.ControlShipInfoId select post).ToList();
            var MejorDemand = query3;
            var ConShipMsg = _controlShipInfoManager.FMsgShipBankInfo();
            var SignalNotify = _messageInfoRepository.Filter(x => SqlFunctions.DateDiff("DD", x.LastUpdate, DateTime.Now) < 8).ToList();
            //if (remittanceInfoes.Any())
            //{
                var obj = new NotificationCustomModel
                {
                    //RunningHour = remittanceInfoes.Count,
                    NextRefitFrom = Refit.Count,
                    NextDockingFrom = Docking.Count,
                    MejorDemand = MejorDemand.Count,
                };
                return obj;
            //}
            //return null;
        }
    }
}
