using RMS.DAL.IRepository;
using RMS.Model;

namespace RMS.DAL.Repository
{
   public class FuelConsumptionRepository :Repository<FuelConsumption>,IFuelConsumptionRepository
    {
       public FuelConsumptionRepository(RM_AGBEntities context)
           : base(context)
        {
        }

    }
}
