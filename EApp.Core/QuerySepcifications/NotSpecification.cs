using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace EApp.Core.QuerySepcifications
{
    public class NotSpecification<T> : Specification<T>
    {
        private ISpecification<T> specification;

        public NotSpecification(ISpecification<T> specification) 
        {
            this.specification = specification;
        }

        public override Expression<Func<T, bool>> GetExpression()
        {
            Expression notBody = Expression.Not(this.specification.GetExpression().Body);

            return Expression.Lambda<Func<T, bool>>(notBody, this.specification.GetExpression().Parameters);
        }
    }
}
