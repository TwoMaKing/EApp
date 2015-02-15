using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Dapper
{
    public interface IDbSet : IDbQuery, IRepository
    {

    }

    public interface IDbSet<T> : IDbQuery<T>, IRepository<T>
    {

    }
}
