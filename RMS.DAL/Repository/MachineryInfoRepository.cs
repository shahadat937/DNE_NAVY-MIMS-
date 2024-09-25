using RMS.DAL;
using RMS.DAL.IRepository;
using RMS.Model;

namespace RMS.DAL.Repository
{
   public class MachineryInfoRepository :Repository<MachineryInfo>,IMachineryInfoRepository
    {
       public MachineryInfoRepository(RM_AGBEntities context)
           : base(context)
       {
           
       }
    }
}
