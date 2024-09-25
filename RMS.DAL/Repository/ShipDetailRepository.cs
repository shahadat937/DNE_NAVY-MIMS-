using RMS.DAL.IRepository;
using RMS.Model;

namespace RMS.DAL.Repository
{
    public class ShipDetailRepository : Repository<ShipDetail>, IShipDetailRepository
    {
        public ShipDetailRepository(RM_AGBEntities context)
            : base(context)
        {
        }
    }
}
