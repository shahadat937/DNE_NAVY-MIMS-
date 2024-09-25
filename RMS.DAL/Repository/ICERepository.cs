using RMS.DAL.IRepository;
using RMS.Model;

namespace RMS.DAL.Repository
{
    public class ICERepository : Repository<ICE>, IICERepository
    {
        public ICERepository(RM_AGBEntities context)
            : base(context)
        {
        }
    }
}
