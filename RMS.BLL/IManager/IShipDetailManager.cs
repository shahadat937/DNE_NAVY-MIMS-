using System.Collections.Generic;
using RMS.Model;

namespace RMS.BLL.IManager
{
    public interface IShipDetailManager
    {
        List<ShipDetail> GetAll();
        void Save(ShipDetail shipDetail);
        //int EditCenter(ShipDetail model);
        void  Delete(long Id);
        ShipDetail GetShipDetailById(long shipId);
        ShipDetail GetDetailById(long shipIdentity);
        //List<vwShipDetail> GetvwAll();
        //List<vwShipDetail> vwPdf();
    }
}
     
