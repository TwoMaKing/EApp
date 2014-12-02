using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Core.Plugin
{
    public class AppPluginContainer : IPluginContainer
    {
        public IPluginServiceProvider ServiceProvider
        {
            get { throw new NotImplementedException(); }
        }

        public IPluginProvider pluginProvider
        {
            get { throw new NotImplementedException(); }
        }

        public IHost Host
        {
            get { throw new NotImplementedException(); }
        }
    }
}
