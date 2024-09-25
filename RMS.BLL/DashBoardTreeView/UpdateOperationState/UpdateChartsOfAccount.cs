using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Model;

namespace RMS.BLL.DashBoardTreeView.UpdateOperationState
{
    public class UpdateChartsOfAccount : UpdateOPlState
    {
          public List<UpdateChartsOfAccount> Nodes { get; set; }
         public UpdateChartsOfAccount()
        {
            Nodes = new List<UpdateChartsOfAccount>();
        }
    }
}
