using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EApp.DomainDriven.Infrastructure.Domain
{
    /// <summary>
    /// 值对象的抽象基类, 值对象是只包含值类型成员属性的类的实例.
    /// </summary>
    public abstract class ValueObject<TObject> : IEquatable<TObject> where TObject : ValueObject<TObject>
    {

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public virtual bool Equals(TObject other)
        {
            if (other == null)
                return false;

            Type t = GetType();

            FieldInfo[] fields = t.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            foreach (FieldInfo field in fields)
            {
                object otherValue = field.GetValue(other);
                object thisValue = field.GetValue(this);

                //if the value is null...
                if (otherValue == null)
                {
                    if (thisValue != null)
                        return false;
                }
                //if the value is a datetime-related type...
                else if ((typeof(DateTime).IsAssignableFrom(field.FieldType)) ||
                         ((typeof(DateTime?).IsAssignableFrom(field.FieldType))))
                {
                    string dateString1 = ((DateTime)otherValue).ToLongDateString();
                    string dateString2 = ((DateTime)thisValue).ToLongDateString();
                    if (!dateString1.Equals(dateString2))
                    {
                        return false;
                    }
                    continue;
                }
                //if the value is any collection...
                else if (typeof(IEnumerable).IsAssignableFrom(field.FieldType))
                {
                    IEnumerable otherEnumerable = (IEnumerable)otherValue;
                    IEnumerable thisEnumerable = (IEnumerable)thisValue;

                    if (!otherEnumerable.Cast<object>().SequenceEqual(thisEnumerable.Cast<object>()))
                        return false;
                }
                //if we get this far, just compare the two values...
                else if (!otherValue.Equals(thisValue))
                    return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            IEnumerable<FieldInfo> fields = GetFields();

            const int startValue = 17;
            const int multiplier = 59;

            int hashCode = startValue;

            foreach (FieldInfo field in fields)
            {
                object value = field.GetValue(this);

                if (value != null)
                    hashCode = hashCode * multiplier + value.GetHashCode();
            }

            return hashCode;
        }

        private IEnumerable<FieldInfo> GetFields()
        {
            Type t = GetType();

            var fields = new List<FieldInfo>();

            while (t != typeof(object))
            {
                fields.AddRange(t.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public));

                t = t.BaseType;
            }

            return fields;
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(ValueObject<TObject> left, ValueObject<TObject> right)
        {
            if (ReferenceEquals(left, null))
                return ReferenceEquals(right, null);

            return left.Equals(right);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(ValueObject<TObject> left, ValueObject<TObject> right)
        {
            return !(left == right);
        }

    }


}
