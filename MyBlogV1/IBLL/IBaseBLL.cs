using DBSession;
using IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public interface IBaseBLL<T> where T: class, new()
    {
        IDbSession dbSession { get; }
        IBaseDAL<T> baseDAL { get; set; }

        bool AddEntity(T entity);
        bool DeleteEntity(T entity);
        bool EditEntity(T entity);
        IQueryable<T> GetEntities(Expression<Func<T, bool>> whereExpression);
        T GetEntity(Expression<Func<T, bool>> firstOrDefaultExpression);
        IQueryable<T> GetEntitiesByPageOrdered<s>(int pageIndex, int pageSize, out int totalCount, Expression<Func<T, bool>> whereExpression, Expression<Func<T, s>> orderByExpression, bool isAsc);
    }
}
