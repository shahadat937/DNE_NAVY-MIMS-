using System.Linq;
using System.Linq.Expressions;
using RMS.DAL.IRepository;
using RMS.Model;


namespace RMS.DAL.Repository
{
    public class FuelConsumptions1_ResultRepository : Repository<FuelConsumptions1_Result>, IFuelConsumptions1_ResultRepository
    {
        public FuelConsumptions1_ResultRepository(RM_AGBEntities context) : base(context)
        {
        }
    }
}
