using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Infrastructure.Domain
{
    /// <summary>
    /// 实体接口, 所有实现了该接口的类都被认定为实体类
    /// </summary>
    /// <typeparam name="TIdentityKey">TIdentityKey 可以看做为实体的Identity ID or Identity Key 唯一标识符, 理解为数据库表的主键.</typeparam>
    public interface IEntity<TIdentityKey>
    {
        TIdentityKey Id { get; }
    }

    public interface IEntity : IEntity<int> 
    { 

    }
}
