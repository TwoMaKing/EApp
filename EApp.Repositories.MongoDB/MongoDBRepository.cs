using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using EApp.Common.Query;
using EApp.Core.Query;
using EApp.Core.QuerySepcifications;
using EApp.Domain.Core;
using EApp.Domain.Core.Repositories;
using MongoDB;


namespace EApp.Repositories.MongoDB
{
    public class MongoDBRepository<TAggregateRoot> : Repository<TAggregateRoot>, IRepository<TAggregateRoot> where TAggregateRoot : class, IAggregateRoot<int>, IAggregateRoot
    {
        private IMongoDBRepositoryContext mongoDBRepositoryContext;

        public MongoDBRepository(IRepositoryContext repositoryContext) : base(repositoryContext) 
        {
            if (!(repositoryContext is IMongoDBRepositoryContext))
            {
                throw new ArgumentException("The specified Repository context is not IMongoDBRepositoryContext.");
            }

            this.mongoDBRepositoryContext = (IMongoDBRepositoryContext)repositoryContext;
        }

        public IMongoDBRepositoryContext MongoDBRespositoryContext 
        {
            get 
            {
                return this.mongoDBRepositoryContext;
            }
        }

        protected override void DoDelete(int id)
        {
            this.MongoDBRespositoryContext.MongoDatabase.GetCollection<TAggregateRoot>().Remove(new Document("Id", id), false);
        }

        protected override TAggregateRoot DoFind(ISpecification<TAggregateRoot> specification)
        {
            return this.MongoDBRespositoryContext.MongoDatabase.GetCollection<TAggregateRoot>().FindOne(specification.GetExpression());
        }

        protected override IEnumerable<TAggregateRoot> DoFindAll(Expression<Func<TAggregateRoot, bool>> expression, Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder)
        {
            IEnumerable<TAggregateRoot> aggregateRoots = 
                this.MongoDBRespositoryContext.MongoDatabase.GetCollection<TAggregateRoot>().Find(expression).Documents;

            return aggregateRoots.SortBy<TAggregateRoot, dynamic>(sortPredicate.Compile(), sortOrder).ToList();
        }

        protected override IPagingResult<TAggregateRoot> DoFindAll(Expression<Func<TAggregateRoot, bool>> expression, Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize)
        {
            if (pageNumber <= 0)
            {
                throw new ArgumentOutOfRangeException("pageNumber", pageNumber, "The pageNumber is one-based and should be larger than zero.");
            }

            if (pageSize <= 0)
            {
                throw new ArgumentOutOfRangeException("pageSize", pageSize, "The pageSize is one-based and should be larger than zero.");
            }

            IEnumerable<TAggregateRoot> aggregateRoots =
                this.MongoDBRespositoryContext.MongoDatabase.GetCollection<TAggregateRoot>().Find(expression).Documents;

            IOrderedEnumerable<TAggregateRoot> orderedAggregateRoots = aggregateRoots.SortBy<TAggregateRoot, dynamic>(sortPredicate.Compile(), sortOrder);

            int skip = (pageNumber - 1) * pageSize;

            int take = pageSize;

            IEnumerable<TAggregateRoot> pagedAggregateRoots = orderedAggregateRoots.Skip(skip).Take(take);

            int totalRecords = aggregateRoots.Count();

            int totalPages = (totalRecords + pageSize - 1) / pageSize;

            return new PagingResult<TAggregateRoot>(totalRecords, 
                                                    totalPages, 
                                                    pageNumber, 
                                                    pageSize, 
                                                    pagedAggregateRoots.Select(aggregateRoot => aggregateRoot).ToList());
        }

        protected override void DoPersistAddedItems(IEnumerable<TAggregateRoot> aggregateRoots)
        {
            this.MongoDBRespositoryContext.MongoDatabase.GetCollection<TAggregateRoot>().Insert(aggregateRoots);
        }

        protected override void DoPersistModifiedItems(IEnumerable<TAggregateRoot> aggregateRoots)
        {
            this.MongoDBRespositoryContext.MongoDatabase.GetCollection<TAggregateRoot>().Save(aggregateRoots);
        }

        protected override void DoPersistDeletedItems(IEnumerable<TAggregateRoot> aggregateRoots)
        {
            this.MongoDBRespositoryContext.MongoDatabase.GetCollection<TAggregateRoot>().Remove(aggregateRoots);
        }
    }
}
