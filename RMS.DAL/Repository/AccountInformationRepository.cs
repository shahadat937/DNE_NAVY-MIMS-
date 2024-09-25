using System;
using System.Linq;
using RMS.DAL.IRepository;
using RMS.Model;
using System.Linq.Expressions;

namespace RMS.DAL.Repository
{
    public class AccountInformationRepository:Repository<AccountInformation>,IAccountInformationRepository
    {
        public AccountInformationRepository(RM_AGBEntities context) : base(context)
        {
        }
        public IQueryable<AccountInformation> GetAccountInfoByCode(Expression<Func<AccountInformation, bool>> predicate)
        {
            return Context.AccountInformations.Where(predicate);
        }
    }
}
