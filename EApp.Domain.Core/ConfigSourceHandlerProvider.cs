using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using EApp.Core.Application;
using EApp.Core.Configuration;

namespace EApp.Domain.Core
{
    public class ConfigSourceHandlerProvider : IHandlerProvider
    {
        public IDictionary<Type, Type> GetHandlers()
        {
            return null;
        }
    }
}
