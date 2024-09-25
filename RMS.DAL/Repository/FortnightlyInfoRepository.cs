using RMS.DAL.IRepository;
using RMS.Model;

namespace RMS.DAL.Repository
{
    public class FortnightlyInfoRepository : Repository<FortNightly>, IFortnightlyInfoRepository
    {
        public FortnightlyInfoRepository(RM_AGBEntities context)
            : base(context)
        {
        }
    }
}
