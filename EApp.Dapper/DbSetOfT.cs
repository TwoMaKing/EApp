using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace EApp.Dapper
{
    public class DbSet<T> : DbQuery<T>, IDbSet<T>
    {

        public DbSet(IDbConnection connection, IQueryProvider queryProvider) : base(connection, queryProvider)
        {
        
        }

        public new IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public T Get(object id)
        {
            throw new NotImplementedException();
        }

        public void Insert(T entity)
        {
            throw new NotImplementedException();
        }

        public R Insert<R>(T entity, Expression<Func<T, R>> resultSelector)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }

        public R Update<R>(T entity, Expression<Func<T, R>> resultSelector)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void Batch(IEnumerable<T> entities, Expression<Action<IRepository<T>, T>> operation)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<R> Batch<R>(IEnumerable<T> entities, Expression<Func<IRepository<T>, T, R>> operation)
        {
            throw new NotImplementedException();
        }

        object IRepository.Get(object id)
        {
            throw new NotImplementedException();
        }

        public void Insert(object entity)
        {
            throw new NotImplementedException();
        }

        public void Update(object entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(object entity)
        {
            throw new NotImplementedException();
        }
    }
}
