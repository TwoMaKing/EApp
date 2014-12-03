using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using EApp.Core.QuerySepcifications;
using EApp.Infrastructure.Domain;
using EApp.Infrastructure.UnitOfWork;

namespace EApp.Infrastructure.Repository
{
    public abstract class RepositoryBase<TAggregateRoot> : IRepository<TAggregateRoot> where TAggregateRoot : class, IAggregateRoot
    {
        private IRepositoryContext repositoryContext;

        public RepositoryBase(IRepositoryContext repositoryContext) 
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

        public void Add(TAggregateRoot item)
        {
            this.DoAdd(item);
        }

        public void Add(IEnumerable<TAggregateRoot> items)
        {
            this.DoAdd(items);
        }

        public void Update(TAggregateRoot item)
        {
            this.DoUpdate(item);
        }

        public void Update(IEnumerable<TAggregateRoot> items)
        {
            this.DoUpdate(items);
        }

        public void Delete(TAggregateRoot item)
        {
            this.DoDelete(item);
        }

        public void Delete(Guid idOrKey)
        {
            this.DoDeleteByKey(idOrKey);
        }

        public void Delete(IEnumerable<TAggregateRoot> items)
        {
            this.DoDelete(items);
        }

        public TAggregateRoot FindByKey(Guid idOrKey)
        {
            return this.DoFindByKey(idOrKey);
        }

        public TAggregateRoot Find(ISpecification<TAggregateRoot> specification)
        {
            return this.DoFind(specification);
        }

        public IQueryable<TAggregateRoot> FindAll()
        {
            return this.FindAll(new AnySepcification<TAggregateRoot>());
        }

        public IQueryable<TAggregateRoot> FindAll(Expression<Func<TAggregateRoot, bool>> expression)
        {
            return this.DoFindAll(expression, null, null);
        }

        public IQueryable<TAggregateRoot> FindAll(Expression<Func<TAggregateRoot, bool>> expression, int pageNumber, int pageSize)
        {
            return this.DoFindAll(expression, pageNumber, pageSize);
        }

        public IQueryable<TAggregateRoot> FindAll(ISpecification<TAggregateRoot> specification)
        {
            return this.DoFindAll(specification, null, null);
        }

        public IQueryable<TAggregateRoot> FindAll(ISpecification<TAggregateRoot> specification, int pageNumber, int pageSize)
        {
            return this.DoFindAll(specification, pageNumber, pageSize);
        }

        #region

        protected abstract void DoAdd(TAggregateRoot item);

        protected abstract void DoAdd(IEnumerable<TAggregateRoot> items);

        protected abstract void DoUpdate(TAggregateRoot item);

        protected abstract void DoUpdate(IEnumerable<TAggregateRoot> items);

        protected abstract void DoDelete(TAggregateRoot item);

        protected abstract void DoDelete(IEnumerable<TAggregateRoot> items);

        protected abstract void DoDeleteByKey(Guid idOrKey);

        protected abstract TAggregateRoot DoFindByKey(Guid idOrKey);

        protected abstract TAggregateRoot DoFind(ISpecification<TAggregateRoot> specification);

        protected abstract IQueryable<TAggregateRoot> DoFindAll(Expression<Func<TAggregateRoot, bool>> expression, int? pageNumber, int? pageSize);

        protected abstract IQueryable<TAggregateRoot> DoFindAll(ISpecification<TAggregateRoot> specification, int? pageNumber, int? pageSize);

        #endregion
    }
}
