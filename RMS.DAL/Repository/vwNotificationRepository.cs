using RMS.DAL.IRepository;
using RMS.Model;

namespace RMS.DAL.Repository
{
    public class vwNotificationRepository:Repository<vwNotification>, IvwNotificationRepository
    {
        public vwNotificationRepository(RM_AGBEntities context)
            : base(context)
        {
        }
    }
}
