using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using RMS.BLL.IManager;
using RMS.DAL.IRepository;
using RMS.DAL.Repository;
using RMS.Model;
using RMS.Utility;

namespace RMS.BLL.Manager
{
    public class EventLogManager:BaseManager,IEventLogManager
    {
        private IEventLogRepository _eventLogRepository;
        
        public EventLogManager()
        {
            _eventLogRepository=new EventLogRepository(Context);
        }

        //public int SaveToEvent(string ipandMaxAddress, string localMachine, string macAddress)
        //{
        //    EventLog eventLog = new EventLog();
        //    eventLog.UserId = PortalContext.CurrentUser.UserName;
        //    eventLog.EventName = "Saved to data base";
        //    eventLog.EventTime = DateTime.Now;
        //    eventLog.PreviousInfo = "";
        //    eventLog.CurrentInfo = "IP: " + ipandMaxAddress + " MAC: " + macAddress;
        //    eventLog.Workstation = localMachine;
        //    eventLog.BranchCode = PortalContext.CurrentUser.BrCode;
        //    int isSaved = _eventLogRepository.Save(eventLog);
        //    return isSaved;
        //}
    }
}
