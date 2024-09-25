using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Model;
using RMS.Utility;

namespace RMS.BLL.IManager
{
   public interface IMajorDemandOrProcureManager
    {
        List<MajorDemandOrProcure> GetAll();
        List<MajorDemandOrProcure> GetNotificationAll();
       MajorDemandOrProcure GetMajorDemand(string id, MajorDemandOrProcure majorDemandOrProcure);
       ResponseModel Saving(MajorDemandOrProcure majorDemandOrProcure);
       object Delete(string id);
       List<MajorDemandOrProcure> UserWiseData(int? verifyType);
       ResponseModel VerifiedStatusUpdate(List<MajorDemandOrProcure> majorDemandOrProcures);
    }
}
