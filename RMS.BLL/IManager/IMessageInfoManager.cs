using System.Collections.Generic;
using System.Threading.Tasks;
using RMS.Model;
using RMS.Utility;

namespace RMS.BLL.IManager
{
    public interface IMessageInfoManager
    {
        List<MessageInfo> GetAll();
        Task<string> GetCurrentMessageByRoleId(int roleId);

        MessageInfo GetById(long id);
      
        MessageInfo FindOne(int id);


        void Delete(long id);
        ResponseModel Save(MessageInfo messageInfo);

    }
}
