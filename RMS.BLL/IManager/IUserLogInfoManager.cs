using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.BLL.IManager
{
    public interface IUserLogInfoManager
    {
        int SaveToEvent(string clientMachineName, string ipAddress, string macAddress);
    }
}
