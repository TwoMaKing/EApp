using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using EApp.Common.DataAccess;
using EApp.Core;
using EApp.Core.QuerySepcifications;
using EApp.Infrastructure.Domain;
using EApp.Infrastructure.Repository;

namespace EApp.Repositories.SqlServer
{
    public class SqlServerRepositoryContext : RepositoryContext, ISqlServerRepositoryContext
    {
        private DbConnection dbConnection;

        private DbTransaction dbTransaction;

        public SqlServerRepositoryContext() 
        {
            this.dbConnection = DbGateway.Default.OpenConnection();

            this.dbTransaction = this.dbConnection.BeginTransaction();
        }

        public DbTransaction Transaction
        {
            get 
            {
                return this.dbTransaction;
            }
        }

        public override void Commit()
        {
            

        }

        public override void Rollback()
        {
            
        }

        protected override void Dispose(bool disposing)
        {
            DbGateway.Default.CloseConnection(this.dbConnection);
        }
    }
}
