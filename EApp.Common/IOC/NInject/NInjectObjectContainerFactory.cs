using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Core;

namespace EApp.Common.IOC.NInject
{
    public class NInjectObjectContainerFactory : IObjectContainerFactory
    {
        public IObjectContainer ObjectContainer
        {
            get { return new NInjectObjectContainer(); }
        }
    }
}
