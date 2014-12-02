using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using EApp.Core.WindowsMvc;

namespace EApp.Windows.Mvc
{
    /// <summary>
    /// Provides information about an action method, such as its name,
    /// parameters, attributes, and filters.
    /// </summary>
    public abstract class ActionDescriptor : ICustomAttributeProvider
    {
        protected ActionDescriptor() { }

        /// <summary>
        /// Gets the name of the action method.
        /// </summary>
        public abstract string ActionName { get; }

        public abstract ControllerDescriptor ControllerDescriptor { get; }

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
            return true;
        }

        /// <summary>
        /// Gets the filter attributes.
        /// </summary>
        public virtual IEnumerable<FilterAttribute> GetFilterAttributes(bool useCache) 
        {
            return null;
        }

        /// <summary>
        /// Returns the parameters of the action method.
        /// </summary>
        public abstract ParameterDescriptor[] GetParameters();

        /// <summary>
        /// Executes the action method by using the specified parameters.
        /// </summary>
        public abstract object Execute(IController controller, object[] parameters);
    }
}
