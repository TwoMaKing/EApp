using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace EApp.Dapper
{
    public interface IDbQuery : IOrderedQueryable, IQueryable, IEnumerable, IListSource 
    { 
    
    }

    public interface IDbQuery<T> : IDbQuery, IOrderedQueryable<T>, IQueryable<T>, IEnumerable<T>
    {

    }
}
