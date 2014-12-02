using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using EApp.Common.Reflection;

namespace EApp.Windows.Mvc
{
    public class ReflectedControllerDescriptor : ControllerDescriptor
    {
        private string controllerName;

        private Type controllerType;

        private ReflectedActionDescriptor[] actionDescriptorArray;

        public ReflectedControllerDescriptor(string controllerName, Type controllerType) 
        {
            this.controllerName = controllerName;

            this.controllerType = controllerType;

            Reflector reflector = new Reflector(controllerType);

            List<ReflectedActionDescriptor> actionDescriptorList = new List<ReflectedActionDescriptor>();

            foreach (KeyValuePair<string, MethodInfo> methodInfoKeyValue in reflector.ObjectMethods)
            {
                string methodName = methodInfoKeyValue.Key;
                
                MethodInfo methodInfo = methodInfoKeyValue.Value;

                ReflectedActionDescriptor actionDescriptor = new ReflectedActionDescriptor(methodInfo, methodName, this);

                actionDescriptorList.Add(actionDescriptor);
            }

            this.actionDescriptorArray = actionDescriptorList.ToArray();
        }

        public override string ControllerName
        {
            get
            {
                return this.controllerName;
            }
        }

        public override Type ControllerType
        {
            get 
            {
                return this.controllerType;
            }
        }

        public override ActionDescriptor[] GetActions()
        {
            return this.actionDescriptorArray;
        }

        public override ActionDescriptor FindAction(string actionName)
        {
            return this.actionDescriptorArray.SingleOrDefault(a => a.ActionName.Equals(actionName));
        }
    }
}
