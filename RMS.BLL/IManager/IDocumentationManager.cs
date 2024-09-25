using System.Collections.Generic;
using RMS.Model;
using RMS.Utility;

namespace RMS.BLL.IManager
{
    public interface IDocumentationManager
    {
        List<Documentation> GetAll();

        long GetMaxId();
        Documentation FindOne(long id);
        Documentation FindImage(long id);
        ResponseModel Save(Documentation documentation);

        void Delete(long id);
    }
}
