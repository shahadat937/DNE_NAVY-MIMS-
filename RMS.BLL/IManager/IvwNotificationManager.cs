using RMS.Model;
using System.Collections.Generic;

namespace RMS.BLL.IManager
{
    public interface IvwNotificationManager
    {
      List<vwNotification> GetAll();
      NotificationCustomModel GetNotification();
      List<vwRefitDokingNotification> GetRefitNotificationAll();
      List<vwRefitDokingNotification> GetDokingNotificationAll();

      List<vwRefitDokingNotification> GetRefitInfo();
      List<vwRefitDokingNotification> GetDockingInfo();
    }
}
