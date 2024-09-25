using System;
using System.Linq;
using RMS.Model;
using System.Linq.Expressions;

namespace RMS.DAL.IRepository
{
    public interface IAccountInformationRepository: IRepository<AccountInformation>
    {
        IQueryable<AccountInformation> GetAccountInfoByCode(Expression<Func<AccountInformation, bool>> predicate);
    }
}
