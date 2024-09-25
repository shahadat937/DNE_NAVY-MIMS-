using RMS.DAL.IRepository;
using RMS.Model;

namespace RMS.DAL.Repository
{
    public class vwRefitDokingNotificationRepository:Repository<vwRefitDokingNotification>, IvwRefitDokingNotificationRepository
    {
        public vwRefitDokingNotificationRepository(RM_AGBEntities context)
            : base(context)
        {
        }
    }
}
