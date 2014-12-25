using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Core.DomainDriven.Domain
{
    /// <summary>
    /// 为了实现聚合的概念，每个聚合根的实体类，使其实现IAggregateRoot接口
    /// </summary>
    /// <typeparam name="TIdentityKey">TIdentityKey 可以看做为实体的Identity ID or Identity Key 唯一标识符, 不要理解为数据库表的主键.</typeparam>
    public interface IAggregateRoot<TIdentityKey> : IEntity<TIdentityKey>
    {

    }

    /// <summary>
    /// 框架提供的默认的聚合根接口，Identity Key 为 int.
    /// </summary>
    public interface IAggregateRoot : IAggregateRoot<int>, IEntity
    { 

    }
}
