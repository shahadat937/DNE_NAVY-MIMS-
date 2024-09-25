using RMS.DAL.IRepository;
using RMS.Model;

namespace RMS.DAL.Repository
{
    public class ShipCraftRepository : Repository<ShipInfo>,IShipCraftRepository
    {
        public ShipCraftRepository(RM_AGBEntities context)
            : base(context)
        {
        }
    }
}
