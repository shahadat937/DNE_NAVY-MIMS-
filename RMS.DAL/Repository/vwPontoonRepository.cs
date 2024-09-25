using RMS.DAL.IRepository;
using RMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.DAL.Repository
{
    public class vwPontoonRepository : Repository<vwPontoon>, IvwPontoonRepository
    {
        public vwPontoonRepository(RM_AGBEntities context)
            : base(context)
        {
        }
    }
}
