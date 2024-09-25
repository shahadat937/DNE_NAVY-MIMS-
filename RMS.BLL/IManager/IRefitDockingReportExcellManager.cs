using RMS.Model;
using RMS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.BLL.IManager
{
    public interface IRefitDockingReportExcellManager
    {
        List<RefitDockingReportExcell> GetAll();
        List<RefitDockingReportExcell> GetAllByDockId( long id);

        long GetMaxId();
        RefitDockingReportExcell FindOne(long id);
        RefitDockingReportExcell FindImage(long id);
        ResponseModel Save(RefitDockingReportExcell documentation);

        void Delete(long id);
    }
}
