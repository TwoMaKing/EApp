using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Common.Reflection
{
    public static class TypeHelper
    {
        public static bool IsNullable(this Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("The type cannot be null.");
            }

            return type.IsGenericType && 
                   type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }
    }
}
