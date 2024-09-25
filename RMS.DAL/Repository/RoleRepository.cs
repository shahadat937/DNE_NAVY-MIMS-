using RMS.DAL.IRepository;
using RMS.Model;

namespace RMS.DAL.Repository
{
    public class RoleRepository:Repository<Role>,IRoleRepository
    {
        public RoleRepository(RM_AGBEntities context)
            : base(context)
        {
        }
    }
}
