using System.Collections.Generic;
using System.Linq;
using RMS.Model;
using RMS.Utility;

namespace RMS.BLL.IManager
{
   public interface IMachinaryEquipmentInformationManager
    {
       List<MachinaryEquipmentInformation_Result> GetAll();
       List<MachinaryEquipmentInformation_Result> GetShipId(string id);
       List<MachinaryEquipmentInformation_Result> FindOne(string id);
    }
}
