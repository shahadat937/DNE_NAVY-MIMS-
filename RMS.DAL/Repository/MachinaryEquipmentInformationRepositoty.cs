using RMS.DAL.IRepository;
using RMS.Model;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace RMS.DAL.Repository
{
    public class MachinaryEquipmentInformationRepositoty : Repository<MachinaryEquipmentInformation_Result>, IMachinaryEquipmentInformationRepositoty
   {
       public MachinaryEquipmentInformationRepositoty(RM_AGBEntities context)
           : base(context)
       {
           
       }
       //public IQueryable<MachinaryEquipmentInformation> GetBranchInfoByBranch(Expression<Func<MachinaryEquipmentInformation, bool>> predicate)
       //{
       //    return Context.MachinaryEquipmentInformation(predicate);
       //}
    }
}
