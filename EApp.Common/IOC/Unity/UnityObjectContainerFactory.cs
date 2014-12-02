using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Core;

namespace EApp.Common.IOC.Unity
{
    public class UnityObjectContainerFactory : IObjectContainerFactory
    {
        public IObjectContainer ObjectContainer
        {
            get 
            {
                return new UnityObjectContainer();
            }
        }
    }
}
