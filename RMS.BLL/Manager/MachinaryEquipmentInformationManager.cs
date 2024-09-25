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
   public class MachinaryEquipmentInformationManager:BaseManager,IMachinaryEquipmentInformationManager
        
    {
       RM_AGBEntities db=new RM_AGBEntities();
       private readonly MachinaryEquipmentInformationRepositoty _MachinaryEquipmentInformationRepositoty;
       public MachinaryEquipmentInformationManager()
        {
            _MachinaryEquipmentInformationRepositoty = new MachinaryEquipmentInformationRepositoty(Instance.Context);
        }
       public List<MachinaryEquipmentInformation_Result> GetAll()
       {
           return _MachinaryEquipmentInformationRepositoty.All().ToList();
       }
       public List<MachinaryEquipmentInformation_Result> GetShipId(string id)
       {
           //MachinaryEquipmentInformation mel=new MachinaryEquipmentInformation();
           //string sql = "Select * from MachinaryEquipmentInformation(" + id + ")";
           //mel = db.Database.SqlQuery(sql);
           //DbSqlQuery<EnrollmentDataGroup> dsf = db.Database.SqlQuery<EnrollmentDataGroup>(sql);
           var nmb =(from c in db.MachinaryEquipmentInformation(id) select c).ToList<MachinaryEquipmentInformation_Result>();

           //var ed = db.MachinaryEquipmentInformation(id).Select(x => new { x.ParentCode, x.ShipName }).ToList();
           //var ds = db.MachinaryEquipmentInformation(id);
           //var shipid = Convert.ToInt64(id);
           return nmb;
           //return _MachinaryEquipmentInformationRepositoty.FindOne(x => x.ShipId == id);
       }
       public List<MachinaryEquipmentInformation_Result> FindOne(string id)
       {


           return _MachinaryEquipmentInformationRepositoty.Filter(x=>x.Level1==id).ToList();
       }

       
    }
}
