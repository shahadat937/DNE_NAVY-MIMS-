using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc.Routing.Constraints;
using RMS.BLL.DashBoardTreeView.UpdateOperationState;
using RMS.Model;
using RMS.Utility;

namespace RMS.BLL.IManager
{
    public interface IUpdateOplStateManager
    {
        //IEnumerable GetOplList();
        //ResponseModel SaveAccountInfo(UpdateOPlState model);
        UpdateOPlState GetOplByCode(long accountCode);
        List<UpdateOPlState> GetOplTeeView(long BankCode);
        List<UpdateChartsOfAccount> GetAll();
    }
}
