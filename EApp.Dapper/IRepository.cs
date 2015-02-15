using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace EApp.Dapper
{
    public interface IRepository
    {
        object Get(object id);

        void Insert(object entity);

        void Update(object entity);

        void Delete(object entity);
    }

    public interface IRepository<T> : IRepository
    {
        T Get(object id);

        void Insert(T entity);

        R Insert<R>(T entity, Expression<Func<T, R>> resultSelector);

        void Update(T entity);

        R Update<R>(T entity, Expression<Func<T, R>> resultSelector);

        void Delete(T entity);

        void Delete(Expression<Func<T, bool>> predicate);

        void Batch(IEnumerable<T> entities, Expression<Action<IRepository<T>, T>> operation);

        IEnumerable<R> Batch<R>(IEnumerable<T> entities, Expression<Func<IRepository<T>, T, R>> operation);
    }
}
