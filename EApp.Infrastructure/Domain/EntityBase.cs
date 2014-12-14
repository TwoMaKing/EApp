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

        private TIdentityKey id = default(TIdentityKey);

        private bool modified;

        private static object lockObject = new object();

        private Dictionary<string, object> changedProperties = new Dictionary<string, object>();

        public EntityBase() 
        {
            this.PropertyChanged -= new PropertyChangedEventHandler(EntityPropertyChanged);
            this.PropertyChanged += new PropertyChangedEventHandler(EntityPropertyChanged);
        }

        /// <summary>
        /// 实体的唯一标识符, 不要理解为数据库表的主键.
        /// </summary>
        public TIdentityKey Id
        {
            get 
            {
                return this.id;
            }
            set
            {
                this.id = value;
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
            if (obj == null ||
               !(obj is EntityBase<TIdentityKey>))
            {
                return false;
            }

            EntityBase<TIdentityKey> otherEntity = obj as EntityBase<TIdentityKey>;

            if (ReferenceEquals(this, otherEntity))
            {
                return true;
            }

            return this.Id.Equals(otherEntity.Id);
        }

        public override int GetHashCode()
        {
            if (this.id.Equals(default(TIdentityKey)))
            {
                return base.GetHashCode();
            }

            return this.id.GetHashCode();
        }

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
    public abstract class EntityBase : EntityBase<int>, IEntity
    {
        public EntityBase() : base() { }

        public EntityBase(int id) : base()
        {
            this.Id = id;
        }
    }
}
