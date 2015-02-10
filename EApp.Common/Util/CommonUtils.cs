using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EApp.Common.Util
{
    public static class CommonUtils
    {
        /// <summary>
        /// Gets a type in all loaded assemblies of current app domain.
        /// </summary>
        public static Type GetType(string fullName)
        {
            if (fullName == null)
            {
                return null;
            }

            Type t = null;

            if (fullName.StartsWith("System.Nullable`1["))
            {
                string genericTypeStr = fullName.Substring("System.Nullable`1[".Length).Trim('[', ']');
                if (genericTypeStr.Contains(","))
                {
                    genericTypeStr = genericTypeStr.Substring(0, genericTypeStr.IndexOf(",")).Trim();
                }
                t = typeof(Nullable<>).MakeGenericType(GetType(genericTypeStr));
            }

            if (t != null)
            {
                return t;
            }

            try
            {
                t = Type.GetType(fullName);
            }
            catch
            {
                // log
            }

            if (t == null)
            {
                try
                {
                    Assembly[] Assemblies = AppDomain.CurrentDomain.GetAssemblies();

                    for (int i = Assemblies.Length - 1; i >= 0; i--)
                    {
                        Assembly assembly = Assemblies[i];
                        try
                        {
                            t = assembly.GetType(fullName);
                        }
                        catch
                        {

                        }

                        if (t != null)
                        {
                            break;
                        }
                    }
                }
                catch
                {
                    // log
                }
            }

            return t;
        }
    }
}
