using RMS.DAL.IRepository;
using RMS.Model;

namespace RMS.DAL.Repository
{
    public class LubOilConsumptionRepository : Repository<LubOilConsumption>, ILubOilConsumptionRepository
    {
        public LubOilConsumptionRepository(RM_AGBEntities context)
            : base(context)
        {
        }
    }
}
