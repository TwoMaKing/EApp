using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace EApp.Core.QuerySepcifications
{
    public class OrSpecification<T> : CompositeSpecification<T>
    {
        public OrSpecification(ISpecification<T> left, ISpecification<T> right) : base(left, right) { }

        public override Expression<Func<T, bool>> GetExpression()
        {
            return this.Left.GetExpression().Or(this.Right.GetExpression());
        }
    }
}
