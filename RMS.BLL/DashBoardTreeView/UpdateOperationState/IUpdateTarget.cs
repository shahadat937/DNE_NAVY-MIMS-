using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.BLL.DashBoardTreeView.UpdateOperationState
{
   public interface IUpdateTarget
    {
       List<UpdateChartsOfAccount> GetTreeView();
    }
}
