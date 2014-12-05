using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Core;
using EApp.Core.QuerySepcifications;
using EApp.Infrastructure.Domain;
using EApp.Infrastructure.Repository;
using EApp.Common.DataAccess;

namespace EApp.Repositories.SqlServer
{
    public abstract class SqlServerRepositoryContext : RepositoryContextBase
    {

        public override void RegisterAddedEntity(IEntity<Guid> entity)
        {
          
        }


        public override void Commit()
        {
            
        }

        public override void Rollback()
        {
            throw new NotImplementedException();
        }
    }
}
