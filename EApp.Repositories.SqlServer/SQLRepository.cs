using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using EApp.Common.Cache;
using EApp.Common.Util;
using EApp.Core;
using EApp.Core.Query;
using EApp.Core.QuerySepcifications;
using EApp.Data;
using EApp.Domain.Core;
using EApp.Domain.Core.Repositories;

namespace EApp.Repositories.SQL
{
    /// <summary>
    /// Repository used for Sql Server.
    /// </summary>
    public abstract class SqlRepository<TAggregateRoot> : Repository<TAggregateRoot> 
        where TAggregateRoot : class, IAggregateRoot<int>, IAggregateRoot
    {
        protected delegate void AppendChildToAggregateRoot(TAggregateRoot aggregateRoot, int childEntityId);

        private ISqlRepositoryContext sqlRepositoryContext;

        private ICacheManager cacheManager = CacheFactory.GetCacheManager();

        public SqlRepository(IRepositoryContext repositoryContext) : base(repositoryContext) 
        {
            if (repositoryContext is ISqlRepositoryContext)
            {
                this.sqlRepositoryContext = repositoryContext as ISqlRepositoryContext;
            }
        }

        protected ISqlRepositoryContext SqlRepositoryContext 
        {
            get 
            {
                return this.sqlRepositoryContext;
            }
        }

        protected override void DoDelete(int id)
        {
            TAggregateRoot aggregateRoot = DoFindByKey(id);

            this.DoDelete(aggregateRoot);
        }

        protected override TAggregateRoot DoFindByKey(int id)
        {
            string cacheAggregateRootId = typeof(TAggregateRoot).Name + "_" + id.ToString();

            if (this.cacheManager.ContainsKey(cacheAggregateRootId))
            {
                return this.cacheManager.GetItem<TAggregateRoot>(cacheAggregateRootId);
            }

            TAggregateRoot currentAggregateRoot = default(TAggregateRoot);

            string querySqlById = this.GetAggregateRootQuerySqlById();

            using (IDataReader reader = DbGateway.Default.ExecuteReader(querySqlById, new object[] { id }))
            {
                var currentAggregateRoots = this.BuildAggregateRootsFromDataReader(reader);

                if (currentAggregateRoots != null &&
                    currentAggregateRoots.Count() > 0)
                {
                    currentAggregateRoot = currentAggregateRoots.FirstOrDefault();

                    Dictionary<string, AppendChildToAggregateRoot> childCallbacks = this.BuildChildCallbacks();

                    if (childCallbacks != null &&
                        childCallbacks.Count > 0)
                    {
                        foreach (KeyValuePair<string, AppendChildToAggregateRoot> callbackItem in childCallbacks)
                        {
                            string childEntityForeignKey = reader[callbackItem.Key].ToString();

                            int childEntityId = Convertor.ConvertToInteger(childEntityForeignKey).Value;

                            callbackItem.Value(currentAggregateRoot, childEntityId);
                        }
                    }
                }

                if (!reader.IsClosed)
                {
                    reader.Close();
                }
            }

            this.cacheManager.AddItem<TAggregateRoot>(cacheAggregateRootId, currentAggregateRoot);

            return currentAggregateRoot;
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

        #region Protected methods for finding the specified aggregate root

        protected abstract string GetAggregateRootQuerySqlById();

        protected virtual IEnumerable<TAggregateRoot> BuildAggregateRootsFromDataReader(IDataReader dataReader)
        {
            if (dataReader == null ||
                dataReader.IsClosed)
            {
                return null;
            }

            List<TAggregateRoot> aggregateRoots = new List<TAggregateRoot>();

            while (dataReader.Read())
            {
                TAggregateRoot aggregateRoot = this.BuildAggregateRootFromDataReader(dataReader);

                aggregateRoots.Add(aggregateRoot);
            }

            return aggregateRoots;
        }

        protected abstract TAggregateRoot BuildAggregateRootFromDataReader(IDataReader dataReader);

        protected abstract Dictionary<string, AppendChildToAggregateRoot> BuildChildCallbacks();

        #endregion

    }
}
