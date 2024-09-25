using RMS.DAL.IRepository;
using RMS.Model;

namespace RMS.DAL.Repository
{
    public class FortnightlyDetailsRepository : Repository<FortnightlyDetail>, IFortnightlyDetailsRepository
    {
        public FortnightlyDetailsRepository(RM_AGBEntities context)
            : base(context)
        {
        }
    }
}
