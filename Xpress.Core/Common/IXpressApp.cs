using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Core;
using EApp.Core.Application;

namespace Xpress.Core.Common
{
    public interface IXpressApp : IApp
    {
        /// <summary>
        /// Get the list of resource manager instance. Resource manager is used for storing text, image, icon, stream etc.
        /// </summary>
        IDictionary<string, IResourceManager> ResourceManager { get; }
    }
}
