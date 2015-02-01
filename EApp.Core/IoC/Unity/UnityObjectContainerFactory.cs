using System;

namespace EApp.Core.IoC
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
