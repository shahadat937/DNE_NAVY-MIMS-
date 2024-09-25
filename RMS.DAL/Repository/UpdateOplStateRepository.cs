using RMS.DAL.IRepository;
using RMS.Model;

namespace RMS.DAL.Repository
{
    public class UpdateOplStateRepository : Repository<UpdateOPlState>, IUpdateOplStateRepository
    {
        public UpdateOplStateRepository(RM_AGBEntities context) : base(context)
        {
        }
    }
}
