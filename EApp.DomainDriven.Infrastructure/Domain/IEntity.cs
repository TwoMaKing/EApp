using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.DomainDriven.Infrastructure.Domain
{
    /// <summary>
    /// 实体接口, 所有实现了该接口的类都被认定为实体类
    /// </summary>
    /// <typeparam name="TKey">Key 可以看做为实体的Identity ID or Identity Key 唯一标识符, 不要理解为数据库表的主键.</typeparam>
    public interface IEntity<out TKey>
    {
        TKey Key { get; }
    }
}
