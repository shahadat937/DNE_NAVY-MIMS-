using System;
using System.Collections.Generic;
using RMS.Model;
using RMS.Utility;

namespace RMS.BLL.IManager
{
    public interface IDeckInfoManager
    {
        List<DeckInfo> GetAll();
        DeckInfo FindOne(string id);
        object Delete(string id);
        ResponseModel Save(DeckInfo deckInfo);

        List<DeckInfo> GetAllByShipId(string id);
        List<DeckInfo> GetReportData(DateTime fromdate, DateTime toDate, long shipid);
        
    }
       
}
