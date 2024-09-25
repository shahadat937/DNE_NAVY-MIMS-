using RMS.DAL.IRepository;
using RMS.Model;

namespace RMS.DAL.Repository
{
   public class MachineryRunningRepository :Repository<MachineryRunningInfo>,IMachineryRunningRepository
   {
       public MachineryRunningRepository(RM_AGBEntities context)
           : base(context)
       {
       }
   }
}
