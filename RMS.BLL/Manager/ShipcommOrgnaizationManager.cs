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
   public class ShipcommOrgnaizationManager:BaseManager,IShipcommOrgnaizationManager
    {
       private IShipcommOrgnaizationRepository _shipcommOrgnaizationRepository;

        public ShipcommOrgnaizationManager()
        {
            _shipcommOrgnaizationRepository = new ShipcommOrgnaizationRepository(Instance.Context);
        }
        public List<ShipcommOrgnaization> GetAll()
        {
            return _shipcommOrgnaizationRepository.All().ToList();
        }
       
        public ShipcommOrgnaization GetShipcommOrgnaizationById(long OrgnizID)
       {
           return _shipcommOrgnaizationRepository.FindOne(x => x.ControlShipInfoId == OrgnizID);
       }
        public List<ShipcommOrgnaization> FindOne(long id)
       {
           return _shipcommOrgnaizationRepository.Filter(x => x.ControlShipInfoId == id).ToList();
       }

     

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
