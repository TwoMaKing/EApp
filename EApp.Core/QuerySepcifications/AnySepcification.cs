using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace EApp.Core.QuerySepcifications
{
    public class AnySepcification<T> : Specification<T>
    {
        public override Expression<Func<T, bool>> GetExpression()
        {
            return t => true;
        }
    }
}
