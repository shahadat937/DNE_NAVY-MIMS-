using System.Collections.Generic;
using RMS.Model;
using RMS.Utility;

namespace RMS.BLL.IManager
{
    public interface IUserURLRightManager
    {
        List<UserRightTreeView> GetUserRightTreeView(string userRole);

        List<UserURLRightModel> GetUserRighsByRole(string userRole);

        ResponseModel Save(List<UserURLRightModel> userURLRight);
    }
}
