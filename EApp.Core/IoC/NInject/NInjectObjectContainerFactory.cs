using System;

namespace EApp.Core.IoC
{
    public class NInjectObjectContainerFactory : IObjectContainerFactory
    {
        public IObjectContainer ObjectContainer
        {
            get { return new NInjectObjectContainer(); }
        }
    }
}
