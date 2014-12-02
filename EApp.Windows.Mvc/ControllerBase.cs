using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using EApp.Common.Reflection;
using EApp.Core.Exceptions;
using EApp.Core.WindowsMvc;

namespace EApp.Windows.Mvc
{
    public abstract class ControllerBase<TView> : IController, IActionFilter, IDisposable where TView : IView
    {
        private TView view;

        private ControllerDescriptor controllerDescriptor;

        protected ControllerBase()
        {
            this.controllerDescriptor = ControllerDescriptorFactory.GetControllerDescriptor(this.ControllerName);
        }

        public TView View
        {
            get 
            {
                return this.view;  
            }
        }

        protected virtual string ControllerName 
        {
            get 
            {
                string theCurrentControllerTypeName = this.GetType().Name;

                if (theCurrentControllerTypeName.EndsWith("Controller"))
                {
                    return theCurrentControllerTypeName.Remove(theCurrentControllerTypeName.Length - 10);
                }

                throw new InfrastructureException("The controller name should have a suffix name ending with \"Controller\".");
            }
        }

        protected void Execute(string actionName, IDictionary<string, object> actionParameters)
        {
            ActionDescriptor actionDescriptor = this.controllerDescriptor.FindAction(actionName);

            ActionExecutingContext executingFilterContext = new ActionExecutingContext(actionDescriptor, actionParameters);

            this.OnActionExecuting(executingFilterContext);

            bool canceled = false;

            Exception exception = null;

            try
            {
                this.ExecuteCore(actionDescriptor, actionParameters.Values);
            }
            catch (Exception e)
            {
                canceled = true;

                exception = e;
            }
            finally
            {
                ActionExecutedContext executedFilterContext = new ActionExecutedContext(actionDescriptor, canceled, exception);

                this.OnActionExecuting(executingFilterContext);
            }
        }

        protected virtual void ExecuteCore(ActionDescriptor actionDescriptor, ICollection<object> actionParameters)
        {
            actionDescriptor.Execute(this,actionParameters.ToArray());
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            return;
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            return;
        }

        IView IController.View
        {
            get 
            {
                return (IView)this.view;
            }
            set
            {
                this.view = (TView)value;
            }
        }

        void IController.Execute(string actionName, IDictionary<string, object> actionParameters)
        {
            this.Execute(actionName, actionParameters);
        }

        protected virtual void Dispose(bool disposing) 
        {

        }

        public void Dispose()
        {
            this.Dispose(true);
        }
    }

}
