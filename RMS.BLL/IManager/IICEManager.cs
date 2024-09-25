using System;
using System.Collections.Generic;
using RMS.Model;
using RMS.Utility;

namespace RMS.BLL.IManager
{
    public interface IICEManager
    {
        List<ICE> GetAll();

        //long GetMaxId();
        ICE FindOne(long id);
        List<ICE> ICEViewByShipId(int year, long shipId);
        ResponseModel Save(ICE ice);
        List<ICE> GetICEId(long id);
        void Delete(long id);
        List<ICE> GetReportData(DateTime? fromdate, DateTime? toDate, long shipid);
    }
}
