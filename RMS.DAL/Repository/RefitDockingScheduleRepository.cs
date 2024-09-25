using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.DAL.IRepository;
using RMS.Model;

namespace RMS.DAL.Repository
{
   public class RefitDockingScheduleRepository :Repository<RefitDockingSchedule>,IRefitDockingScheduleRepository
   {
       public RefitDockingScheduleRepository(RM_AGBEntities context) : base(context)
       {
       }
       public vwPreviousDateValue PreviousDate(long? controlCode)
       {
            if (controlCode == null)
            {
                controlCode = 0;
            }
            string query = "select  top 1  a.PNextDockingFrom,a.PNextDockingTo,a.PNextRefitFrom,a.PNextRefitTo from RefitDockingSchedule a where a.ControlShipInfoId =" + controlCode + " ORDER BY a.RefitDockingIdentity DESC";
           return Context.Database.SqlQuery<vwPreviousDateValue>(query).SingleOrDefault();

       }
   }
}
