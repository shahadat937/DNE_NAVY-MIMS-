using RMS.DAL.IRepository;
using RMS.Model;

namespace RMS.DAL.Repository
{
    public class VwShipBranchInfoRepository : Repository<vwShipBranchInfo>, IVwShipBranchInfoRepository
    {
        public VwShipBranchInfoRepository(RM_AGBEntities context)
            : base(context)
        {
        }
    }
}
