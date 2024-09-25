using RMS.DAL.IRepository;
using RMS.Model;

namespace RMS.DAL.Repository
{
   public class DamageMachineriesInfoRepository :Repository<DamageMachineriesInfo>,IDamageMachineriesInfoRepository
   {
       public DamageMachineriesInfoRepository(RM_AGBEntities context)
           : base(context)
       {
       }
   }
}
