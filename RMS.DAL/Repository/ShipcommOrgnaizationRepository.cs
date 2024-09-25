using RMS.DAL.IRepository;
using RMS.Model;

namespace RMS.DAL.Repository
{
    public class ShipcommOrgnaizationRepository : Repository<ShipcommOrgnaization>, IShipcommOrgnaizationRepository
    {
        public ShipcommOrgnaizationRepository(RM_AGBEntities context)
           : base(context)
        {
        }
    }
}
