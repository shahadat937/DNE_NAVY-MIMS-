using System;
using System.Linq;
using RMS.DAL.IRepository;
using RMS.Model;

namespace RMS.DAL.Repository
{
    public class ControlShipInfoRepository : Repository<ControlShipInfo>,IControlShipInfoRepository
    {
        public ControlShipInfoRepository(RM_AGBEntities context)
            : base(context)
        {
        }
        public int GetMaxControlCode(decimal controlShipInfoIdentity)
        {
            decimal controlCode = Context.Database.SqlQuery<decimal>("select ControlCode from ControlShipInfo where ControlShipInfoId = "+controlShipInfoIdentity+"").SingleOrDefault();
            decimal max = Context.Database.SqlQuery<decimal>("select isnull(max(ControlCode),0)+1 as ControlCode from  ControlShipInfo where ParentCode=" + controlCode + "").SingleOrDefault();
            return Convert.ToInt32(max);
        }
    }
}
