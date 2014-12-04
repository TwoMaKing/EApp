using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace EApp.Core.QuerySepcifications
{
    /// <summary>
    /// The base Specification class.
    /// </summary>
    public abstract class Specification<T> : ISpecification<T>
    {
        /// <summary>
        /// Evaluates a LINQ expression to its corresponding specification.
        /// </summary>
        public static Specification<T> Eval(Expression<Func<T, bool>> expression)
        {
            return new ExpressionSpecification<T>(expression);
        }


        public bool IsSatisfiedBy(T obj)
        {
            return this.GetExpression().Compile()(obj);
        }

        public ISpecification<T> And(ISpecification<T> otherSpecification)
        {
            return new AndSpecification<T>(this, otherSpecification);
        }

        public ISpecification<T> Or(ISpecification<T> otherSpecification)
        {
            return new OrSpecification<T>(this, otherSpecification);
        }

        public ISpecification<T> AndNot(ISpecification<T> otherSpecification)
        {
            return new AndNotSpecification<T>(this, otherSpecification);
        }

        public ISpecification<T> Not()
        {
            return new NotSpecification<T>(this);
        }

        public abstract Expression<Func<T, bool>> GetExpression();

    }
}
