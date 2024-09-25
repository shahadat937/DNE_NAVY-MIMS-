using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using RMS.BLL.IManager;
using RMS.DAL.IRepository;
using RMS.DAL.Repository;
using RMS.Model;
using RMS.Utility;

namespace RMS.BLL.Manager
{
   public class SignalManager:BaseManager,ISignalManager
    {
       private ISignalRepository _signalRepository;

       public SignalManager()
        {
            _signalRepository = new SignalRepository(Instance.Context);
        }
        public List<signal> GetAll()
        {
            return _signalRepository.All().ToList();
        }
       
       // public signal GetShipcommOrgnaizationById(long OrgnizID)
       //{
       //    return _signalRepository.FindOne(x => x.id = OrgnizID);
       //}
       // public List<ShipcommOrgnaization> FindOne(long id)
       //{
       //    return _shipcommOrgnaizationRepository.Filter(x => x.ControlShipInfoId == id).ToList();
       //}

     

        //public List<ProcurementSchedule> FindOne1(DateTime DateFrom, DateTime DateTo)
        //{
        //    return _procurementRepository.Filter(x => x.LastUpdate <DateFrom && x.LastUpdate >DateTo).ToList();
        //}

       //public object Delete(string id)
       //{
       //    var shipid = Convert.ToInt64(id);
       //    return _shipcommOrgnaizationRepository.Delete(x => x.ProcurementId == shipid);
       //}

    
    }
}
