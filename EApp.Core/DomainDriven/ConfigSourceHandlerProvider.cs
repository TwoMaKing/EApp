using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using EApp.Core.Application;
using EApp.Core.Configuration;
using EApp.Core.DomainDriven.Commands;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace EApp.Core.DomainDriven
{
    public class ConfigSourceHandlerProvider : IHandlerProvider
    {
        private List<object> handlerList = new List<object>();

        public IEnumerable GetHandlers()
        {
            //HandlerElementCollection handlerElements = EAppRuntime.Instance.CurrentApp.ConfigSource.Config.Handlers;

            //if (handlerElements != null &&
            //    handlerElements.Count > 0)
            //{
            //    for (int handlerIndex = 0; handlerIndex < handlerElements.Count; handlerIndex++) 
            //    {
            //        HandlerElement handlerElement = handlerElements[handlerIndex];

            //        string handlerTypeName = handlerElement.Type;

            //        Type handlerType = Type.GetType(handlerTypeName);

            //        //UnityConfigurationSection section = (UnityConfigurationSection)ConfigurationManager.GetSection(configSectionName);
            //    } 

            //}

            UnityContainer unityContainer = EAppRuntime.Instance.CurrentApp.ObjectContainer.GetWrapperContainer<UnityContainer>();

            IUnityContainer handlerContainer = unityContainer.CreateChildContainer();

            handlerContainer = handlerContainer.LoadConfiguration("handlers");

            return handlerContainer.ResolveAll(typeof(ICommandHandler<>));

        }
    }
}
