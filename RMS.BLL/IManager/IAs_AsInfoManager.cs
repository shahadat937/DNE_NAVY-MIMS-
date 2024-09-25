using System.Collections.Generic;
using RMS.Model;
using RMS.Utility;

namespace RMS.BLL.IManager
{
  public interface IAsAsInfoManager
  {
      List<AsAndAsInfo> GetAll();
      List<AsAndAsInfo> GetAllByShipId(long ShipId);

      AsAndAsInfo GetAsAsInfoById(long As_AsId);
      List<AsAndAsInfo> FindOne(long id);
      AsAndAsInfo FindFile(long id);
      object Delete(string id);
      ResponseModel Save(AsAndAsInfo AsAsInfo);
      List<AsAndAsInfo> GetAsAsId(long As_AsId);
      List<AsAndAsInfo> UserWiseData(int? verifyType);
      ResponseModel VerifiedStatusUpdate(List<AsAndAsInfo> asAsInfos);
  }
}
