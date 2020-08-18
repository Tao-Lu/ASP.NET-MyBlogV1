using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    public interface IBaseDAL<T> where T: class, new()
    {
        DbContext dbContext { get; }
        
        void AddEntity(T entity);
        void DeleteEntity(T entity);
        void EditEntity(T entity);
        IQueryable<T> GetEntities(Expression<Func<T, bool>> whereExpression);
        T GetEntity(Expression<Func<T, bool>> firstOrDefaultExpression);
        IQueryable<T> GetEntitiesByPageOrdered<s>(int pageIndex, int pageSize, out int totalCount, Expression<Func<T, bool>> whereExpression, Expression<Func<T, s>> orderByExpression, bool isAsc);
    }
}
