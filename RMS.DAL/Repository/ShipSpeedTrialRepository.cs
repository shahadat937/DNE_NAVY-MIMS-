using RMS.DAL.IRepository;
using RMS.Model;

namespace RMS.DAL.Repository
{
   public class ShipSpeedTrialRepository:Repository<ShipSpeedTrial>,IShipSpeedTrialRepository
   {
       public ShipSpeedTrialRepository(RM_AGBEntities context)
           : base(context)
       {
       }
   }
}
