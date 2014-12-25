using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Core.Application;
using EApp.Core.Exceptions;
using EApp.Core.WindowsMvc;

namespace EApp.Windows.Mvc
{
    public class SyncViewAction : IViewAction
    {
        private IView view;

        private IViewDataContainer viewDataContainer;

        public SyncViewAction(IView view, IViewDataContainer viewDataContainer) 
        {
            if (view == null)
            {
                throw new InfrastructureException("The view cannot be null.");
            }

            this.view = view;

            this.viewDataContainer = viewDataContainer;
        }

        public void Action(string actionName)
        {
            this.Action(actionName, new object[] { });
        }

        public void Action(string actionName, ICollection<object> actionParameters)
        {
            string controllerName = this.view.GetType().Name;

            this.Action(actionName, controllerName, actionParameters);
        }

        public void Action(string actionName, string controllerName)
        {
            this.Action(actionName, controllerName, null);
        }

        public void Action(string actionName, string controllerName, ICollection<object> actionParameters)
        {
            IControllerFactory controllerFactory = EAppRuntime.Instance.CurrentApp.WinMvcControllerBuilder.GetControllerFactory();

            Type controllerType = controllerFactory.GetControllerType(controllerName);

            if (controllerType == null)
            {
                throw new InfrastructureException("Please specify an valid controller name or controller type.");
            }

            ControllerDescriptor controllerDescriptor = null;

            if (!ControllerDescriptorFactory.Contains(controllerName))
            {
                ControllerDescriptor newControllerDescriptor = new ReflectedControllerDescriptor(controllerName, controllerType);

                ControllerDescriptorFactory.AddControllerDescriptor(controllerName, newControllerDescriptor);
            }

            controllerDescriptor = ControllerDescriptorFactory.GetControllerDescriptor(controllerName);

            IController controller = controllerFactory.CreateController(controllerName);

            controller.View = this.view;

            if (controller == null)
            {
                throw new InfrastructureException("Please specify an valid controller name or controller type.");
            }

            ActionDescriptor actionDescriptorToExecute = controllerDescriptor.FindAction(actionName);

            if (actionDescriptorToExecute == null)
            {
                throw new InfrastructureException("Please specify an valid action name.");
            }

            ParameterDescriptor[] parameterDescriptorArray = actionDescriptorToExecute.GetParameters();

            SortedList<string, object> argumentValuesPair = new SortedList<string, object>();

            if (!actionParameters.Count.Equals(parameterDescriptorArray.Length))
            {
                throw new InfrastructureException("The length of arguments for the action {0} is incorrect.Please specify the correct arguments matching the action {1}.", actionName, actionName); 
            }

            for (int parameterDescriptorIndex = 0; parameterDescriptorIndex < parameterDescriptorArray.Length; parameterDescriptorIndex++) 
            {
                ParameterDescriptor parameterDescriptor = parameterDescriptorArray[parameterDescriptorIndex];

                argumentValuesPair.Add(parameterDescriptor.ParameterName, actionParameters.ElementAt(parameterDescriptorIndex));
            }

            controller.Execute(actionName, argumentValuesPair);

        }
    }
}
