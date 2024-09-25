using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.DAL.IRepository;
using RMS.Model;
using System.Linq.Expressions;

namespace RMS.DAL.Repository
{
    public class CommonCodeRepository :Repository<CommonCode>, ICommonCodeRepository
    {
        public CommonCodeRepository(RM_AGBEntities context) : base(context)
        {
        }
        public IQueryable<CommonCode> GetCommonCode(Expression<Func<CommonCode, bool>> predicate)
        {
            return Context.CommonCodes.Where(predicate);
        }
        public IQueryable<CommonCode> GetCommonCodeByType(Expression<Func<CommonCode, bool>> predicate)
        {
            return Context.CommonCodes.Where(predicate);
        }
        public IQueryable<CommonCode> GetCommonCodeByCode(Expression<Func<CommonCode, bool>> predicate)
        {
            return Context.CommonCodes.Where(predicate);
        }
        public IQueryable<CommonCode> GetCommonType(Expression<Func<CommonCode, bool>> predicate)
        {
            return Context.CommonCodes.Where(predicate);
        }
    }
}
