using RMS.DAL.IRepository;
using RMS.Model;

namespace RMS.DAL.Repository
{
   public class ShipPowerRepository : Repository<ShipPower>, IShipPowerRepository
    {
       public ShipPowerRepository(RM_AGBEntities context)
           : base(context)
       {
           
       }
    }
}
