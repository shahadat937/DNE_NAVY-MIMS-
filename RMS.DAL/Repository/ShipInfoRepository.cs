using RMS.DAL.IRepository;
using RMS.Model;

namespace RMS.DAL.Repository
{
    public class ShipInfoRepository : Repository<ShipInfo>,IShipInfoRepository
    {
        public ShipInfoRepository(RM_AGBEntities context)
            : base(context)
        {
        }
    }
}
