using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using RMS.DAL.IRepository;
using RMS.Model;


namespace RMS.DAL.Repository
{
    public class NewMachinaryEquipmentInformation_ResultRepository : Repository<NewMachinaryEquipmentInformation_Result>, INewMachinaryEquipmentInformation_ResultRepository
    {
        public NewMachinaryEquipmentInformation_ResultRepository(RM_AGBEntities context)
            : base(context)
        {
        }
     
    }
}
