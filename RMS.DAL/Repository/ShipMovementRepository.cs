using RMS.DAL.IRepository;
using RMS.Model;

namespace RMS.DAL.Repository
{
   public class ShipMovementRepository:Repository<ShipMovement>,IShipMovementRepository
    {
       public ShipMovementRepository(RM_AGBEntities context)
           : base(context)
        {
        }
    }
}
