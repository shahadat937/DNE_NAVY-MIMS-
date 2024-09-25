using RMS.DAL.IRepository;
using RMS.Model;

namespace RMS.DAL.Repository
{
    public class MessageInfoRepository : Repository<MessageInfo>,IMessageInfoRepository
    {
        public MessageInfoRepository(RM_AGBEntities context)
            : base(context)
        {
        }
    }
}
