using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Core;
using EApp.Core.Application;
using EApp.Core.Configuration;

namespace Xpress.Core.Common
{
    public class XpressApp : App, IXpressApp
    {
        private IDictionary<string, IResourceManager> resourceManagers = new Dictionary<string, IResourceManager>();

        public XpressApp(IConfigSource configSource) : base(configSource)
        { 
            
        }

        public IDictionary<string, IResourceManager> ResourceManager
        {
            get 
            { 
                return resourceManagers; 
            }
        }
    }
}
