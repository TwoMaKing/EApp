using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EApp.Windows.Mvc
{
    /// <summary>
    /// Contains information that describes a parameter.
    /// </summary>
    public abstract class ParameterDescriptor : ICustomAttributeProvider
    {
        protected ParameterDescriptor() { }

        /// <summary>
        /// Gets the default value of the parameter.
        /// </summary>
        public virtual object DefaultValue 
        {
            get 
            {
                return null;
            } 
        }

        /// <summary>
        /// Gets the name of the parameter.
        /// </summary>
        public abstract string ParameterName { get; }

        /// <summary>
        /// Gets the type of the parameter.
        /// </summary>
        public abstract Type ParameterType { get; }

        public object[] GetCustomAttributes(bool inherit)
        {
            return null;
        }

        public object[] GetCustomAttributes(Type attributeType, bool inherit)
        {
            return null;
        }

        public bool IsDefined(Type attributeType, bool inherit)
        {
            return false;
        }
    }
}
