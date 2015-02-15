using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace EApp.Dapper
{
    public class DbContext : IDbContext, IQueryProvider
    {
        private DbConfiguration dbConfiguration;

        private Database database;

        public DbContext() : 
            this(ConfigurationManager.ConnectionStrings[0].Name)
        { 
            
        }

        public DbContext(string connectionStringSectionName) 
        {
            this.dbConfiguration = DbConfiguration.Configure(connectionStringSectionName);

            this.database = new Database(this.dbConfiguration);
        }

        public DbContext(DbConnection dbConnection) 
        {
            this.dbConfiguration = DbConfiguration.Configure(dbConnection);

            this.database = new Database(this.dbConfiguration);
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable CreateQuery(Expression expression)
        {
            throw new NotImplementedException();
        }

        public TResult Execute<TResult>(Expression expression)
        {
            throw new NotImplementedException();
        }

        public object Execute(Expression expression)
        {
            throw new NotImplementedException();
        }

        public DbConfiguration DbConfiguration
        {
            get 
            {
                return this.dbConfiguration;
            }
        }

        public Database Database
        {
            get 
            {
                return this.database;
            }
        }

        public IDbSet<TEntityType> Set<TEntityType>()
        {
            throw new NotImplementedException();
        }

        public IDbSet Set(Type entityType)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
