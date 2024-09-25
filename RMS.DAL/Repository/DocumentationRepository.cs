using RMS.DAL.IRepository;
using RMS.Model;

namespace RMS.DAL.Repository
{
    public class DocumentationRepository : Repository<Documentation>, IDocumentationRepository
    {
        public DocumentationRepository(RM_AGBEntities context)
            : base(context)
        {
        }
    }
}
