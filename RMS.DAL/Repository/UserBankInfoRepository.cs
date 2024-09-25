using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.DAL.IRepository;
using RMS.Model;

namespace RMS.DAL.Repository
{
    public class UserBankInfoRepository : Repository<UserBankInfo>, IUserBankInfoRepository
    {
        public UserBankInfoRepository(RM_AGBEntities context)
            : base(context)
        {
        }
    }
}
