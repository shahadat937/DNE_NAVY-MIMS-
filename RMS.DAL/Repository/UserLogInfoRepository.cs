using RMS.DAL.IRepository;
using RMS.Model;

namespace RMS.DAL.Repository
{
    public class UserLogInfoRepository:Repository<UserLogInfo>,IUserLogInfoRepository
    {
        public UserLogInfoRepository(RM_AGBEntities context) : base(context)
        {
        }
    }
}
