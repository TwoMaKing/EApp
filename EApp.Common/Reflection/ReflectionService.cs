using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EApp.Common.Reflection
{
    public static class ReflectionService
    {
        public static TAttribute[] GetCustomAttributes<TAttribute>(this Type type)
        {
            object[] attributes = type.GetCustomAttributes(typeof(TAttribute), false);

            return attributes as TAttribute[];
        }

        public static Type GetMemberType(this MemberInfo member)
        {
            if (member == null)
            {
                return null;
            }

            switch (member.MemberType)
            {
                case MemberTypes.Field: return (member as FieldInfo).FieldType;
                case MemberTypes.Property: return (member as PropertyInfo).PropertyType;
                case MemberTypes.Method: return (member as MethodInfo).ReturnType;
            }

            return null;
        }

        public static TAttribute[] GetCustomAttributes<TAttribute>(this MemberInfo member)
        {
            if (member == null)
            {
                return default(TAttribute[]);
            }

            return member.GetCustomAttributes(typeof(TAttribute), false) as TAttribute[];
        }

    }
}
