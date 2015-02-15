using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace EApp.Dapper
{
    public abstract class DbQuery : IDbQuery
    {
        public Type ElementType
        {
            get { throw new NotImplementedException(); }
        }

        public Expression Expression
        {
            get { throw new NotImplementedException(); }
        }

        public IQueryProvider Provider
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool ContainsListCollection
        {
            get { throw new NotImplementedException(); }
        }

        public IList GetList()
        {
            throw new NotImplementedException();
        }
    }

}
