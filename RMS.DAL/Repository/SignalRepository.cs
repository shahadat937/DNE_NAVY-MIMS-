using RMS.DAL.IRepository;
using RMS.Model;

namespace RMS.DAL.Repository
{
    public class SignalRepository : Repository<signal>, ISignalRepository
    {
        public SignalRepository(RM_AGBEntities context)
           : base(context)
        {
        }
    }
}
