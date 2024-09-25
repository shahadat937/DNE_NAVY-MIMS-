using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using RMS.BLL.IManager;
using RMS.DAL.Repository;
using RMS.Model;
using RMS.Utility;
using RMS.DAL;



namespace RMS.BLL.Manager
{
    public class FuelConsumptions1_ResultManager:BaseManager,IFuelConsumptions1_ResultManager
    {
         RM_AGBEntities db=new RM_AGBEntities();
         private readonly FuelConsumptions1_ResultRepository _FuelConsumptions1_ResultRepository;
       public FuelConsumptions1_ResultManager()
        {
            _FuelConsumptions1_ResultRepository = new FuelConsumptions1_ResultRepository(Instance.Context);
        }

        public List<FuelConsumptions1_Result> GetAll()
        {
            return _FuelConsumptions1_ResultRepository.All().ToList();
        }

        public List<FuelConsumptions1_Result> GetShipId(string id,DateTime formdate ,DateTime todate)
        {
            var nmb = (from c in db.FuelConsumptions1(id,formdate,todate) select c).ToList<FuelConsumptions1_Result>();
            return nmb;
        }

        public List<FuelConsumptions1_Result> FindOne(string id)
        {
            throw new NotImplementedException();
        }
    }
}
