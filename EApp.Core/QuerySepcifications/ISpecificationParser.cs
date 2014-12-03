using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Core.QuerySepcifications
{
    /// <summary>
    /// 将Specification解析为某一持久化框架可以认识的对象，比如LINQ Expression或者NHibernate的Criteria.
    /// </summary>
    public interface ISpecificationParser<TCriteria>
    {
        TCriteria Parse<T>(ISpecification<T> specification);
    }
}
