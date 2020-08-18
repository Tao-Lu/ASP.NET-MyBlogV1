using DBSession;
using IBLL;
using IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public abstract class BaseBLL<T> : IBaseBLL<T> where T : class, new()
    {
        public IDbSession dbSession
        {
            get
            {
                return DbSessionFactory.CreateDbSession();
            }
        }

        public IBaseDAL<T> baseDAL { get; set; }

        // an abstract method which must be implemented by child class
        // in order to get and set baseDAL
        public abstract void SetBaseDAL();

        // when a child class inherit this class
        // the constructor of this class will be exectuted first
        public BaseBLL()
        {
            SetBaseDAL();
        }

        public bool AddEntity(T entity)
        {
            baseDAL.AddEntity(entity);
            return dbSession.SaveChanges();
        }

        public bool DeleteEntity(T entity)
        {
            baseDAL.DeleteEntity(entity);
            return dbSession.SaveChanges();
        }

        public bool EditEntity(T entity)
        {
            baseDAL.EditEntity(entity);
            return dbSession.SaveChanges();
        }

        public IQueryable<T> GetEntities(Expression<Func<T, bool>> whereExpression)
        {
            return baseDAL.GetEntities(whereExpression);
        }

        public IQueryable<T> GetEntitiesByPageOrdered<s>(int pageIndex, int pageSize, out int totalCount, Expression<Func<T, bool>> whereExpression, Expression<Func<T, s>> orderByExpression, bool isAsc)
        {
            return baseDAL.GetEntitiesByPageOrdered<s>(pageIndex, pageSize, out totalCount, whereExpression, orderByExpression, isAsc);
        }

        // checks result is null or not
        public T GetEntity(Expression<Func<T, bool>> firstOrDefaultExpression)
        {
            return baseDAL.GetEntity(firstOrDefaultExpression);
        }
    }
}
