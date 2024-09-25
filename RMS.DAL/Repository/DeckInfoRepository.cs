using RMS.DAL.IRepository;
using RMS.Model;

namespace RMS.DAL.Repository
{
    public class DeckInfoRepository : Repository<DeckInfo>,IDeckInfoRepository
    {
        public DeckInfoRepository(RM_AGBEntities context)
            : base(context)
        {
        }
    }
}
