using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Core.Application;
using EApp.Core.WindowsMvc;

namespace EApp.Core
{
    /// <summary>
    /// Represents a class that is responsible for dynamically building a windows mvc controller.
    /// </summary>
    public class WindowsMvcControllerBuilder
    {
        private IControllerFactory controllerFactory;

        public WindowsMvcControllerBuilder() 
        { 
        
        }

        /// <summary>
        /// Gets the associated windows mvc controller factory.
        /// </summary>
        public IControllerFactory GetControllerFactory() 
        {
            return this.controllerFactory;
        }


        /// <summary>
        /// Sets the specified windows mvc controller factory.
        /// </summary>
        public void SetControllerFactory(IControllerFactory controllerFactory) 
        {
            this.controllerFactory = controllerFactory;
        }

        /// <summary>
        ///  Sets the windows mvc controller factory by using the specified type.
        /// </summary>
        public void SetControllerFactory(Type controllerFactoryType) 
        {
            if (EAppRuntime.Instance.App != null &&
                EAppRuntime.Instance.App.ObjectContainer != null)
            {
                if (!EAppRuntime.Instance.App.ObjectContainer.Registered(controllerFactoryType))
                {
                    EAppRuntime.Instance.App.ObjectContainer.RegisterType(controllerFactoryType);
                }

                this.controllerFactory = (IControllerFactory)EAppRuntime.Instance.App.ObjectContainer.Resolve(controllerFactoryType);
            }
            else
            {
                this.controllerFactory = (IControllerFactory)Activator.CreateInstance(controllerFactoryType);
            }
        }

    }
}
