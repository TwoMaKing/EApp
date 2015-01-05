using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using EApp.Core.DomainDriven.Domain;

namespace EApp.Common.Query
{
    public interface IQueryBuilder<TEntity> : IQueryBuilder<TEntity, int> where TEntity : class, IEntity
    {

    }
}
