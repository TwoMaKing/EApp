using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace EApp.Plugin.Generic
{
    public interface IPluginUnloadingContextAction
    {
        /// <summary>
        /// Execute action before a plugin unloads
        /// </summary>
        void ExecuteActionBeforeUnloading(CancelEventArgs e);

        /// <summary>
        /// Execute action after a plugin unloading is cancelled
        /// </summary>
        void ExecuteActionAfterUnloadCancelled();
    }
}
