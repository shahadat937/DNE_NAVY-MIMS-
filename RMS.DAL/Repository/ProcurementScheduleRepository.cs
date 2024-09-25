using RMS.DAL.IRepository;
using RMS.Model;

namespace RMS.DAL.Repository
{
    public class ProcurementScheduleRepository : Repository<ProcurementSchedule>, IProcurementScheduleRepository
    {
       public ProcurementScheduleRepository(RM_AGBEntities context)
           : base(context)
        {
        }
    }
}
