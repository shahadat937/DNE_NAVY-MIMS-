using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Model;
using System.Linq.Expressions;

namespace RMS.DAL.IRepository
{
    public interface ICommonCodeRepository:IRepository<CommonCode>
    {
        IQueryable<CommonCode> GetCommonCode(Expression<Func<CommonCode, bool>> predicate);
        IQueryable<CommonCode> GetCommonCodeByType(Expression<Func<CommonCode, bool>> predicate);
        IQueryable<CommonCode> GetCommonType (Expression<Func<CommonCode, bool>> predicate);
        IQueryable<CommonCode> GetCommonCodeByCode(Expression<Func<CommonCode, bool>> predicate);
    }
}
