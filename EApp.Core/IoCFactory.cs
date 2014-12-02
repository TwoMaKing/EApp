using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Core.Application;

namespace EApp.Core
{
    public sealed class IoCFactory
    {
        private static IObjectContainerFactory currentObjectContainerFacotory;

        static IoCFactory() 
        {
            currentObjectContainerFacotory = CreateObjectContainerFactory("");
        }

        public static IObjectContainer CurrentObjectContainer 
        {
            get 
            {
                return currentObjectContainerFacotory.ObjectContainer;
            }
        }

        public static IObjectContainerFactory CreateObjectContainerFactory(string iocFactoryName)
        {
            return null; //(IObjectContainerFactory)Activator.CreateInstance(iocFactoryName);
        }
    }
}
