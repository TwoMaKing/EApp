using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace EApp.Core.QuerySepcifications
{
    /// <summary>
    /// 规约模式, 其作用是实现了查询语句与查询条件的分离, 查询语句在底层是稳定不变的,
    /// 而查询条件是和具体业务,具体领域有关的, 是易变的, 如果为每一个领域的每一个新需求都写一个新的方法, 就可能会出现重复的代码,不利于程序的扩展
    /// </summary>
    public interface ISpecification<T>
    {

        /*public class NameSpecification : ISpecification
        {
            protected string name;
            public NameSpecification(string name) { this.name = name; }
            public override bool IsSatisifedBy(object obj)
            {
                return (obj as Customer).FirstName.Equals(name);
            }
        }*/

        /// <summary>
        /// 返回指定的对象是否满足当前规约的条件
        /// </summary>
        bool IsSatisfiedBy(T obj);

        /// <summary>
        /// 当前规约与另一个规约做 And 查询组合返回新的组合规约.
        /// </summary>
        ISpecification<T> And(ISpecification<T> otherSpecification);


        /// <summary>
        /// 当前规约与另一个规约做 Or 查询组合返回新的组合规约.
        /// </summary>
        ISpecification<T> Or(ISpecification<T> otherSpecification);


        /// <summary>
        /// 当前规约与另一个规约做 And Not 查询组合返回新的组合规约.
        /// </summary>
        /// <param name="sepcification"></param>
        /// <returns></returns>
        ISpecification<T> AndNot(ISpecification<T> otherSpecification);

        /// <summary>
        /// 返回于当前规约相反的查询规约.
        /// </summary>
        ISpecification<T> Not();

        /// <summary>
        /// 得到代表当前查询规约的Linq 表达式.
        /// </summary>
        Expression<Func<T, bool>> GetExpression();

    }
}
