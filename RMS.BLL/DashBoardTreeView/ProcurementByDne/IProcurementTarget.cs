﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.BLL.DashBoardTreeView.ProcurementByDne
{
   public interface IProcurementTarget
    {
       List<ProcurementChartsOfAccount> GetTreeView();
    }
}
