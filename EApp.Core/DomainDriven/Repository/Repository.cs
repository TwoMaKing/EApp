using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using EApp.Core.DomainDriven.Domain;
using EApp.Core.Query;
using EApp.Core.QuerySepcifications;

namespace EApp.Core.DomainDriven.Repository
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

        public void Delete(int id)
        {
            this.DoDeleteByKey(id);
        }

        public void Delete(IEnumerable<TEntity> items)
        {
            this.DoDelete(items);
        }

        public TEntity FindByKey(int id)
        {
            return this.DoFindByKey(id);
        }

        public TEntity Find(ISpecification<TEntity> specification)
        {
            return this.DoFind(specification);
        }

        public IEnumerable<TEntity> FindAll()
        {
            return this.DoFindAll(new AnySepcification<TEntity>().GetExpression());
        }

        public IPagingResult<TEntity> FindAll(int pageNumber, int pageSize)
        {
            return this.DoFindAll(new AnySepcification<TEntity>().GetExpression(), pageNumber, pageSize);
        }

        public IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> expression)
        {
            return this.DoFindAll(expression);
        }

        public IPagingResult<TEntity> FindAll(Expression<Func<TEntity, bool>> expression, int pageNumber, int pageSize)
        {
            return this.DoFindAll(expression, pageNumber, pageSize);
        }

        public IEnumerable<TEntity> FindAll(ISpecification<TEntity> specification)
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

        protected abstract void DoDeleteByKey(int id);

        protected abstract TEntity DoFindByKey(int id);

        protected abstract TEntity DoFind(ISpecification<TEntity> specification);

        protected abstract IEnumerable<TEntity> DoFindAll(Expression<Func<TEntity, bool>> expression);

        protected abstract IPagingResult<TEntity> DoFindAll(Expression<Func<TEntity, bool>> expression, int pageNumber, int pageSize);

        //protected virtual IPagingResult<TEntity> DoFindAll(Expression<Func<TEntity, bool>> expression, int pageNumber, int pageSize) 
        //{
        //    if (pageNumber <= 0)
        //    {
        //        throw new ArgumentOutOfRangeException("pageNumber", pageNumber, "The pageNumber is one-based and should be larger than zero.");
        //    }

        //    if (pageSize <= 0)
        //    {
        //        throw new ArgumentOutOfRangeException("pageSize", pageSize, "The pageSize is one-based and should be larger than zero.");
        //    }

        //    IEnumerable<TEntity> allEntities = this.DoFindAll(expression);

        //    int skip = (pageNumber - 1) * pageSize;
            
        //    int take = pageSize;

        //    IEnumerable<TEntity> pagedEntities = allEntities.Skip(skip).Take(take);

        //    int totalRecords = allEntities.Count();

        //    int totalPages = (totalRecords + pageSize - 1) / pageSize;

        //    return new PagingResult<TEntity>(totalRecords, totalPages, pageNumber, pageSize, pagedEntities.Select(entity => entity).ToList());
        //}

        #endregion

    }
}
