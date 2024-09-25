using RMS.DAL.IRepository;
using RMS.Model;

namespace RMS.DAL.Repository
{
    public class UserURLRightRepository : Repository<UserURLRight>,IUserURLRightRepository
    {
        public UserURLRightRepository(RM_AGBEntities context)
            : base(context)
        {
        }
    }
}
