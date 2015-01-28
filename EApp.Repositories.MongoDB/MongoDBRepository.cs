using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using EApp.Core.DomainDriven.Domain;
using EApp.Core.DomainDriven.Repository;
using EApp.Core.Query;
using EApp.Core.QuerySepcifications;
using MongoDB;


namespace EApp.Repositories.MongoDB
{
    public class MongoDBRepository<TEntity> : Repository<TEntity>, IRepository<TEntity>, IUnitOfWorkRepository where TEntity : class, IEntity<int>, IEntity
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

        protected override void DoAdd(TEntity item)
        {
            if (item != null) 
            {
                this.RepositoryContext.RegisterAdded(item, this);
            }
        }

        protected override void DoAdd(IEnumerable<TEntity> items)
        {
            if (items == null)
            {
                return;
            }

            foreach (TEntity entity in items)
            {
                this.DoAdd(entity);
            }
        }

        protected override void DoUpdate(TEntity item)
        {
            if (item != null)
            {
                this.RepositoryContext.RegisterModified(item, this);
            }
        }

        protected override void DoUpdate(IEnumerable<TEntity> items)
        {
            if (items == null)
            {
                return;
            }

            foreach (TEntity item in items)
            {
                this.DoUpdate(item);
            }
        }

        protected override void DoDelete(TEntity item)
        {
            if (item != null)
            {
                this.RepositoryContext.RegisterDeleted(item, this);
            }
        }

        protected override void DoDelete(IEnumerable<TEntity> items)
        {
            if (items == null)
            {
                return;
            }

            foreach (TEntity item in items)
            {
                this.DoDelete(item);
            }
        }

        protected override void DoDeleteByKey(int id)
        {
            this.MongoDBRespositoryContext.MongoDatabase.GetCollection<TEntity>().Delete(new Document("Id", id), false);
        }

        protected override TEntity DoFindByKey(int id)
        {
            return this.MongoDBRespositoryContext.MongoDatabase.GetCollection<TEntity>().FindOne(new Document { { "Id", id } });
        }

        protected override TEntity DoFind(ISpecification<TEntity> specification)
        {
            return this.MongoDBRespositoryContext.MongoDatabase.GetCollection<TEntity>().FindOne(specification.GetExpression());
        }

        protected override IEnumerable<TEntity> DoFindAll(Expression<Func<TEntity, bool>> expression)
        {
            return this.MongoDBRespositoryContext.MongoDatabase.GetCollection<TEntity>().Find(expression).Documents;
        }

        protected override IPagingResult<TEntity> DoFindAll(Expression<Func<TEntity, bool>> expression, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public void PersistAddedItem(IEntity entity)
        {
            this.MongoDBRespositoryContext.MongoDatabase.GetCollection<TEntity>().Insert(entity);
        }

        public void PersistModifiedItem(IEntity entity)
        {
            this.MongoDBRespositoryContext.MongoDatabase.GetCollection<TEntity>().Update(entity);
        }

        public void PersistDeletedItem(IEntity entity)
        {
            this.MongoDBRespositoryContext.MongoDatabase.GetCollection<TEntity>().Delete(entity, false);
        }
    }
}
