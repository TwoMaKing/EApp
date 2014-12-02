using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Common.IOC
{
    public interface IObjectContainerFactory
    {
        IObjectContainer ObjectContainer { get; }
    }
}
