using RMS.DAL.IRepository;
using RMS.Model;

namespace RMS.DAL.Repository
{
    public class As_AsInfoRepository : Repository<AsAndAsInfo>, IAs_AsInfoRepository
    {
       public As_AsInfoRepository(RM_AGBEntities context)
           : base(context)
        {
        }
    }
}
