using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Core.QuerySepcifications
{
    /// <summary>
    /// 组合规约模式 
    /// 它包含两个属性：Left和Right, ICompositeSpecification是继承于ISpecification接口的,
    /// Left和Right本身也是ISpecification类型，整个Specification的结构就可以看成是一种树状结构
    /// </summary>
    public interface ICompositeSpecification<T> : ISpecification<T>
    {
        /// <summary>
        /// Gets the left side of the specification.
        /// </summary>
        ISpecification<T> Left { get; }

        /// <summary>
        /// Gets the right side of the specification.
        /// </summary>
        ISpecification<T> Right { get; }
    }
}
