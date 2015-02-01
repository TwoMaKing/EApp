using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using EApp.Core.Query;
using EApp.Core.QuerySepcifications;

namespace EApp.Domain.Core.Repositories
{
    public abstract class Repository<TAggregateRoot> : IRepository<TAggregateRoot, int>, IRepository<TAggregateRoot> , IRepositoryPersistence
        where TAggregateRoot : class, IAggregateRoot<int>, IAggregateRoot
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

        #region Aggregate root Creation/Update/Deletion

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

        public void Delete(IEnumerable<TAggregateRoot> items)
        {
            this.DoDelete(items);
        }

        public void Delete(int id)
        {
            this.DoDelete(id);
        }

        #endregion

        #region Find the aggregate root by Id or specification

        public TAggregateRoot FindByKey(int id)
        {
            return this.DoFindByKey(id);
        }

        public TAggregateRoot Find(ISpecification<TAggregateRoot> specification)
        {
            return this.DoFind(specification);
        }

        #endregion

        #region Aggregate roots Queries

        public IEnumerable<TAggregateRoot> FindAll()
        {
            return this.DoFindAll(new AnySepcification<TAggregateRoot>().GetExpression(), TAggregateRoot => TAggregateRoot.Id, SortOrder.None);
        }

        public IEnumerable<TAggregateRoot> FindAll(Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder)
        {
            return this.DoFindAll(new AnySepcification<TAggregateRoot>().GetExpression(), sortPredicate, sortOrder);
        }

        public IPagingResult<TAggregateRoot> FindAll(Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize)
        {
            return this.DoFindAll(new AnySepcification<TAggregateRoot>().GetExpression(), sortPredicate, sortOrder, pageNumber, pageSize);
        }

        public IEnumerable<TAggregateRoot> FindAll(Expression<Func<TAggregateRoot, bool>> expression)
        {
            return this.DoFindAll(expression, TAggregateRoot => TAggregateRoot.Id, SortOrder.None);
        }

        public IEnumerable<TAggregateRoot> FindAll(Expression<Func<TAggregateRoot, bool>> expression, Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder)
        {
            return this.DoFindAll(expression, sortPredicate, sortOrder);
        }

        public IPagingResult<TAggregateRoot> FindAll(Expression<Func<TAggregateRoot, bool>> expression, Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize)
        {
            return this.DoFindAll(expression, sortPredicate, sortOrder, pageNumber, pageSize);
        }

        public IEnumerable<TAggregateRoot> FindAll(ISpecification<TAggregateRoot> specification)
        {
            if (specification == null)
            {
                throw new ArgumentNullException("Query spcification is null. Please specify a specification.");
            }

            return this.DoFindAll(specification.GetExpression(), TAggregateRoot => TAggregateRoot.Id, SortOrder.None);
        }

        public IEnumerable<TAggregateRoot> FindAll(ISpecification<TAggregateRoot> specification, Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder)
        {
            if (specification == null)
            {
                throw new ArgumentNullException("Query spcification is null. Please specify a specification.");
            }

            return this.DoFindAll(specification.GetExpression(), sortPredicate, sortOrder);
        }

        public IPagingResult<TAggregateRoot> FindAll(ISpecification<TAggregateRoot> specification, Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize)
        {
            if (specification == null)
            {
                throw new ArgumentNullException("Query spcification is null. Please specify a specification.");
            }

            return this.DoFindAll(specification.GetExpression(), sortPredicate, sortOrder, pageNumber, pageSize);
        }

        #endregion

        #region Aggregate root persistence

        public void PersistAddedItems(IEnumerable<object> objects)
        {
            this.DoPersistAddedItems(objects.Select(obj => (TAggregateRoot)obj).ToList());
        }

        public void PersistModifiedItems(IEnumerable<object> objects)
        {
            this.DoPersistModifiedItems(objects.Select(obj => (TAggregateRoot)obj).ToList());
        }

        public void PersistDeletedItems(IEnumerable<object> objects)
        {
            this.DoPersistDeletedItems(objects.Select(obj => (TAggregateRoot)obj).ToList());
        }

        #endregion

        #region Protected methods

        #region Aggregate root Creation/Update/Deletion

        protected virtual void DoAdd(TAggregateRoot item)
        {
            this.DoAdd(new TAggregateRoot[] { item });
        }

        protected virtual void DoAdd(IEnumerable<TAggregateRoot> items)
        {
            this.RepositoryContext.RegisterAdded(items, this);
        }

        protected virtual void DoUpdate(TAggregateRoot item)
        {
            this.DoUpdate(new TAggregateRoot[] { item });
        }

        protected virtual void DoUpdate(IEnumerable<TAggregateRoot> items)
        {
            this.RepositoryContext.RegisterModified(items, this);
        }

        protected virtual void DoDelete(TAggregateRoot item) 
        {
            this.DoDelete(new TAggregateRoot[] { item });
        }

        protected virtual void DoDelete(IEnumerable<TAggregateRoot> items)
        {
            this.RepositoryContext.RegisterDeleted(items, this);        
        }

        protected abstract void DoDelete(int id);

        #endregion

        #region Find the aggregate root by Id or specification

        protected virtual TAggregateRoot DoFindByKey(int id) 
        {
            return this.DoFind(new ExpressionSpecification<TAggregateRoot>(aggregateRoot => aggregateRoot.Id.Equals(id)));
        }

        protected abstract TAggregateRoot DoFind(ISpecification<TAggregateRoot> specification);

        #endregion

        #region Aggregate roots Queries

        protected abstract IEnumerable<TAggregateRoot> DoFindAll(Expression<Func<TAggregateRoot, bool>> expression, 
                                                                 Expression<Func<TAggregateRoot, dynamic>> sortPredicate, 
                                                                 SortOrder sortOrder);

        protected abstract IPagingResult<TAggregateRoot> DoFindAll(Expression<Func<TAggregateRoot, bool>> expression, 
                                                                   Expression<Func<TAggregateRoot, dynamic>> sortPredicate, 
                                                                   SortOrder sortOrder, 
                                                                   int pageNumber, 
                                                                   int pageSize);

        #endregion

        #region Aggregate root persistence

        protected abstract void DoPersistAddedItems(IEnumerable<TAggregateRoot> aggregateRoots);

        protected abstract void DoPersistModifiedItems(IEnumerable<TAggregateRoot> aggregateRoots);

        protected abstract void DoPersistDeletedItems(IEnumerable<TAggregateRoot> aggregateRoots);

        #endregion

        #endregion
    }
}
