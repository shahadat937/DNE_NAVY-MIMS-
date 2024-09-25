using System;
using System.Collections.Generic;
using System.Linq;
using RMS.Model;
using RMS.Utility;



namespace RMS.BLL.IManager
{
    public interface INewMachinaryEquipmentInformation_ResultManager
    {
        List<NewMachinaryEquipmentInformation_Result> GetAll();
        List<NewMachinaryEquipmentInformationN_Result> GetShipId(string id);
        List<NewMachinaryEquipmentInformation_Result> FindOne(string id);
    }
}
