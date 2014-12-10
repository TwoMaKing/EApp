using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using EApp.Core.QueryPaging;
using EApp.Core.QuerySepcifications;
using EApp.Infrastructure.Domain;
using EApp.Infrastructure.UnitOfWork;

namespace EApp.Infrastructure.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        private IRepositoryContext repositoryContext;

        public Repository(IRepositoryContext repositoryContext) 
        {
            this.repositoryContext = repositoryContext;
        }

        public IRepositoryContext RepositoryContext
        {
            get 
            {
                return this.repositoryContext; 
            }
        }

        public void Add(TEntity item)
        {
            this.DoAdd(item);
        }

        public void Add(IEnumerable<TEntity> items)
        {
            this.DoAdd(items);
        }

        public void Update(TEntity item)
        {
            this.DoUpdate(item);
        }

        public void Update(IEnumerable<TEntity> items)
        {
            this.DoUpdate(items);
        }

        public void Delete(TEntity item)
        {
            this.DoDelete(item);
        }

        public void Delete(Guid idOrKey)
        {
            this.DoDeleteByKey(idOrKey);
        }

        public void Delete(IEnumerable<TEntity> items)
        {
            this.DoDelete(items);
        }

        public TEntity FindByKey(Guid idOrKey)
        {
            return this.DoFindByKey(idOrKey);
        }

        public TEntity Find(ISpecification<TEntity> specification)
        {
            return this.DoFind(specification);
        }

        public IQueryable<TEntity> FindAll()
        {
            return this.DoFindAll(new AnySepcification<TEntity>().GetExpression());
        }

        public IPagingResult<TEntity> FindAll(int pageNumber, int pageSize)
        {
            return this.DoFindAll(new AnySepcification<TEntity>().GetExpression(), pageNumber, pageSize);
        }

        public IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>> expression)
        {
            return this.DoFindAll(expression);
        }

        public IPagingResult<TEntity> FindAll(Expression<Func<TEntity, bool>> expression, int pageNumber, int pageSize)
        {
            return this.DoFindAll(expression, pageNumber, pageSize);
        }

        public IQueryable<TEntity> FindAll(ISpecification<TEntity> specification)
        {
            return this.DoFindAll(specification.GetExpression());
        }

        public IPagingResult<TEntity> FindAll(ISpecification<TEntity> specification, int pageNumber, int pageSize)
        {
            return this.DoFindAll(specification.GetExpression(), pageNumber, pageSize);
        }

        #region Protected members

        protected abstract void DoAdd(TEntity item);

        protected abstract void DoAdd(IEnumerable<TEntity> items);

        protected abstract void DoUpdate(TEntity item);

        protected abstract void DoUpdate(IEnumerable<TEntity> items);

        protected abstract void DoDelete(TEntity item);

        protected abstract void DoDelete(IEnumerable<TEntity> items);

        protected abstract void DoDeleteByKey(Guid idOrKey);

        protected abstract TEntity DoFindByKey(Guid idOrKey);

        protected abstract TEntity DoFind(ISpecification<TEntity> specification);

        protected abstract IQueryable<TEntity> DoFindAll(Expression<Func<TEntity, bool>> expression);

        protected abstract IPagingResult<TEntity> DoFindAll(Expression<Func<TEntity, bool>> expression, int pageNumber, int pageSize);

        #endregion

    }
}
