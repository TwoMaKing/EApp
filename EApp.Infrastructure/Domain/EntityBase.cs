using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace EApp.Infrastructure.Domain
{
    /// <summary>
    /// 实体基类
    /// </summary>
    /// <typeparam name="TIdentityKey"></typeparam>
    public abstract class EntityBase<TIdentityKey> : IEntity<TIdentityKey>, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected TIdentityKey key;

        private bool modified;

        private readonly static object lockObject = new object();

        private Dictionary<string, object> changedProperties = new Dictionary<string, object>();

        public EntityBase() 
        {
            this.PropertyChanged -= new PropertyChangedEventHandler(EntityPropertyChanged);
            this.PropertyChanged += new PropertyChangedEventHandler(EntityPropertyChanged);
        }

        /// <summary>
        /// 实体的唯一标识符, 不要理解为数据库表的主键.
        /// </summary>
        public TIdentityKey Key
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

        public bool Modified 
        {
            get 
            {
                return changedProperties != null &&
                       changedProperties.Count > 0;
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
                   obj is EntityBase<TIdentityKey> &&
                   this == (EntityBase<TIdentityKey>)obj;
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

        public static bool operator ==(EntityBase<TIdentityKey> entityX, EntityBase<TIdentityKey> entityY)
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

        public static bool operator !=(EntityBase<TIdentityKey> entityX, EntityBase<TIdentityKey> entityY)
        {
            return !entityX.Equals(entityY);
        }

        #endregion

        protected virtual void OnPropertyChanged(string propertyName, object oldvalue, object newValue) 
        {
            if (this.PropertyChanged != null)
            {
                if (oldvalue != newValue)
                {
                    PropertyChangedEventArgs e = new PropertyChangedEventArgs(propertyName, oldvalue, newValue);
                
                    this.PropertyChanged(this, e);
                }
            }
        }

        protected virtual void EntityPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) 
        {
            if (sender == this)
            {
                PropertyChangedEventArgs args = (PropertyChangedEventArgs)e;

                lock (lockObject)
                {
                    if (!this.changedProperties.ContainsKey(args.PropertyName))
                    {
                        this.changedProperties.Add(args.PropertyName, args.NewValue);
                    }
                    else
                    {
                        this.changedProperties[args.PropertyName] = args.NewValue;
                    }
                }
            }
        }
    }

    /// <summary>
    /// 框架提供的默认实体基类，Identity Key 为 GUID.
    /// </summary>
    public abstract class EntityBase : EntityBase<Guid>, IEntity
    {
        public EntityBase() : this(Guid.Empty) { }

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
