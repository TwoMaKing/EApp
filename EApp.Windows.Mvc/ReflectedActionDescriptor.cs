using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using EApp.Common.Reflection;
using EApp.Core.Application;
using EApp.Core.WindowsMvc;

namespace EApp.Windows.Mvc
{
    public class ReflectedActionDescriptor : ActionDescriptor
    {
        private MethodInfo methodInfo;
        
        private string actionName;

        private ControllerDescriptor controllerDescriptor;
            
        private ReflectedParameterDescriptor[] parameterDescriptorArray;

        public ReflectedActionDescriptor(MethodInfo methodInfo, string actionName, ControllerDescriptor controllerDescriptor)
        {
            this.methodInfo = methodInfo;

            this.actionName = actionName;

            this.controllerDescriptor = controllerDescriptor;

            ParameterInfo[] parameterInfoArray = Reflector.GetParameters(methodInfo);

            List<ReflectedParameterDescriptor> parameterDescriptorList = new List<ReflectedParameterDescriptor>();

            if (parameterInfoArray != null)
            {
                for (int parameterIndex = 0; parameterIndex < parameterInfoArray.Length; parameterIndex++)
                {
                    ReflectedParameterDescriptor parameterDescriptor = new ReflectedParameterDescriptor(parameterInfoArray[parameterIndex]);

                    parameterDescriptorList.Add(parameterDescriptor);
                }
            }

            this.parameterDescriptorArray = parameterDescriptorList.ToArray();

        }

        public override string ActionName
        {
            get 
            {
                return this.actionName;
            }
        }

        public MethodInfo MethodInfo 
        {
            get 
            {
                return this.methodInfo;
            }
        }

        public override ControllerDescriptor ControllerDescriptor
        {
            get 
            {
                return this.controllerDescriptor; 
            }
        }

        public override object Execute(IController controller, object[] parameters)
        {
            if (parameters.Length.Equals(0))
            {
                parameters = null;
            }

            return this.methodInfo.Invoke(controller, parameters);
        }

        public override ParameterDescriptor[] GetParameters()
        {
            return this.parameterDescriptorArray;
        }

    }
}
