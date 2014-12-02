using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EApp.Windows.Mvc
{
    public abstract class ControllerDescriptor : ICustomAttributeProvider
    {
        protected ControllerDescriptor() { }

        public virtual string ControllerName 
        {
            get 
            {
                return null;
            }
        }

        public abstract Type ControllerType { get; }

        /// <summary>
        /// Get all of actions in the controller.
        /// </summary>
        public abstract ActionDescriptor[] GetActions();

        /// <summary>
        /// Find the specified action in the controller by the action name.
        /// </summary>
        public abstract ActionDescriptor FindAction(string actionName);

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
