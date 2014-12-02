using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.DomainDriven.Infrastructure.Domain
{
    /// <summary>
    /// 实体基类
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class EntityBase<TKey> : IEntity<TKey>
    {
        protected TKey key;

        /// <summary>
        /// 实体的唯一标识符, 不要理解为数据库表的主键.
        /// </summary>
        public TKey Key
        {
            get 
            {  
                return this.key;
            }
            protected set
            { 
                this.key = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return obj != null &&
                   obj is EntityBase<TKey> &&
                   this == obj;
        }

        public override int GetHashCode()
        {
            if (this.key == null)
            {
                return base.GetHashCode();
            }

            return this.key.GetHashCode();
        }

        #region 操作 == 和 != 符重载

        public static bool operator ==(EntityBase<TKey> entityX, EntityBase<TKey> entityY)
        {
            if (entityX == null && entityY == null)
            {
                return true;
            }

            if (entityX == null || entityY == null)
            {
                return false;
            }

            return entityY.Key.Equals(entityY.Key);
        }

        public static bool operator !=(EntityBase<TKey> entityX, EntityBase<TKey> entityY)
        {
            return !entityX.Equals(entityY);
        }

        #endregion
    }

    /// <summary>
    /// 框架提供的默认实体基类，Identity Key 为 GUID.
    /// </summary>
    public abstract class EntityBase : EntityBase<Guid> 
    {
        public EntityBase() : this(Guid.Empty)
        { 
        
        }

        public EntityBase(Guid key) 
        {
            if (key == Guid.Empty)
            {
                key = Guid.NewGuid();
            }

            this.Key = key;
        }
    }
}
