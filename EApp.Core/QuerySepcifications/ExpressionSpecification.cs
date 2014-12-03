using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace EApp.Core.QuerySepcifications
{
    public class ExpressionSpecification<T> : Specification<T>
    {
        private Expression<Func<T, bool>> expression;

        public ExpressionSpecification(Expression<Func<T, bool>> expression)
        {
            this.expression = expression;
        }

        public override Expression<Func<T, bool>> GetExpression()
        {
            return this.expression;
        }
    }
}
