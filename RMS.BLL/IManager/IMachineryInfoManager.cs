using RMS.Model;
using RMS.Utility;
using System.Collections.Generic;

namespace RMS.BLL.IManager
{
    public interface IMachineryInfoManager
    {
        List<MachineryInfo> GetAll();
        List<MachineryInfo> GetAllNextTohMohMachinery();
        List<MachineryInfo> GetMachinaryInfoGroupBy();
        List<CommonCode> GetEquipmentType();
        MachineryInfo GetShip(string id, MachineryInfo machineryInfo);
        MachineryInfo GetMachineryById(string id, MachineryInfo machineryInfo);


        List<MachineryInfo> FindOne(long id);
        ResponseModel Saving(MachineryInfo machineryInfo);
        object Delete(long id);
        List<MachineryInfo> GetFindValue(string bankCode);
        List<MachineryInfo> GetMachinaryInfoByMachineName(string machineName);
        MachineryInfo GetLifeTimeType(long machineId);
        MachineryInfo GetOne(long machinariesInfoIdentity);
        ResponseModel UpdateStatus(long? description, long? mobilityDescription);

        object GetMachinariesInfo(long shipId);
        List<MachineryInfo> GetMachinariesInfoByShipId(long shipId);
        List<MachineryInfo> UserWiseData(int? verifyType);
        ResponseModel VerifiedStatusUpdate(List<MachineryInfo> machineryInfos);
        string GetMashineName(long? description);

        bool ResetTOHMOHTime(decimal rh, long MachineryId);


    }
}
