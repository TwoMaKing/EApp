using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace EApp.Core.QuerySepcifications
{
    public abstract class CompositeSpecification<T> : Specification<T>, ICompositeSpecification<T>
    {
        private ISpecification<T> left;

        private ISpecification<T> right;

        public CompositeSpecification(ISpecification<T> left, ISpecification<T> right) 
        {
            this.left = left;

            this.right = right;
        }

        public ISpecification<T> Left
        {
            get 
            {
                return this.left; 
            }
        }

        public ISpecification<T> Right
        {
            get 
            { 
                return this.right; 
            }
        }
    }
}
