using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using RMS.BLL.IManager;
using RMS.DAL.Repository;
using RMS.Model;
using RMS.Utility;
using RMS.DAL;



namespace RMS.BLL.Manager
{
    public class NewMachinaryEquipmentInformation_ResultManager:BaseManager,INewMachinaryEquipmentInformation_ResultManager
    {
         RM_AGBEntities db=new RM_AGBEntities();
         private readonly NewMachinaryEquipmentInformation_ResultRepository _NewMachinaryEquipmentInformation_ResultRepository;
         public NewMachinaryEquipmentInformation_ResultManager()
        {
            _NewMachinaryEquipmentInformation_ResultRepository = new NewMachinaryEquipmentInformation_ResultRepository(Instance.Context);
        }

         public List<NewMachinaryEquipmentInformation_Result> GetAll()
        {
            return _NewMachinaryEquipmentInformation_ResultRepository.All().ToList();
        }

         public List<NewMachinaryEquipmentInformationN_Result> GetShipId(string id)
         {
             //return _NewMachinaryEquipmentInformation_ResultRepository.GetShipInfoById(id);
             //var snb = Context.Database.SqlQuery<NewMachinaryEquipmentInformation_Result>("select * from  dbo.NewMachinaryEquipmentInformationN("+id+")").ToList();

             //var nmb = (from c in db.NewMachinaryEquipmentInformationN(id) select c).ToList<NewMachinaryEquipmentInformationN_Result>();
             var nmb = db.NewMachinaryEquipmentInformationN(id).ToList();
             return nmb.ToList();
         }

         public List<NewMachinaryEquipmentInformation_Result> FindOne(string id)
        {
            throw new NotImplementedException();
        }
    }
}
