using RMS.DAL.IRepository;
using RMS.Model;

namespace RMS.DAL.Repository
{
   public class ShipInactiveRepository:Repository<ShipInactive>,IShipInactiveRepository
    {
       public ShipInactiveRepository(RM_AGBEntities context)
           : base(context)
        {
        }
    }
}
