using Dappers;
using EApp.Core;
using EApp.Core.Query;
using EApp.Core.QuerySepcifications;
using EApp.Domain.Core;
using EApp.Domain.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EApp.Respositories.Dapper
{
    public abstract class DapperRepository<TAggregateRoot> : Repository<TAggregateRoot> where TAggregateRoot : class, IAggregateRoot<int>, IAggregateRoot, new()
    {
        private IDapperRepositoryContext dapperRepositoryContext;

        public DapperRepository(IRepositoryContext repositoryContext) :base(repositoryContext)
        {
            if (repositoryContext is IDapperRepositoryContext)
            {
                this.dapperRepositoryContext = (IDapperRepositoryContext)repositoryContext;
            }
            else
            {
                throw new ArgumentException("The specified Repository context is not the instance of IDapperRepositoryContext.");
            }
        }

        protected IDapperRepositoryContext DapperRepositoryContext 
        {
            get
            {
                return this.dapperRepositoryContext;
            }
        }

        protected override void DoDelete(int id)
        {
            throw new NotImplementedException();
        }

        protected override TAggregateRoot DoFind(ISpecification<TAggregateRoot> specification)
        {
            throw new NotImplementedException();
        }

        protected override IEnumerable<TAggregateRoot> DoFindAll(Expression<Func<TAggregateRoot, bool>> expression, Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder)
        {
            throw new NotImplementedException();
        }

        protected override IPagingResult<TAggregateRoot> DoFindAll(Expression<Func<TAggregateRoot, bool>> expression, Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        protected override void DoPersistAddedItems(IEnumerable<TAggregateRoot> aggregateRoots)
        {

        }

        protected override void DoPersistModifiedItems(IEnumerable<TAggregateRoot> aggregateRoots)
        {

        }

        protected override void DoPersistDeletedItems(IEnumerable<TAggregateRoot> aggregateRoots)
        {

        }
    }
}
