using RMS.DAL.IRepository;
using RMS.Model;

namespace RMS.DAL.Repository
{
    public class QuaterlyReturnRepository : Repository<QuaterlyReturn>, IQuaterlyReturnRepository
    {
        public QuaterlyReturnRepository(RM_AGBEntities context)
            : base(context)
        {
        }
    }

}
