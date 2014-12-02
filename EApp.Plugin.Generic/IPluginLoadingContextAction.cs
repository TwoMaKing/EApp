using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using EApp.Core.Plugin;

namespace EApp.Plugin.Generic
{
    public interface IPluginLoadingContextAction<TPluginItem> where TPluginItem : PluginItem<TPluginItem>, new()
    {
        /// <summary>
        /// Execute action before a plugin loads.
        /// If e.Cancel == true then the plugin will cancel running. 
        /// </summary>
       void ExecuteActionOnLoading(IPluginController<TPluginItem> currentRunningPluginController, 
                                   IPluginController<TPluginItem> tickedPluginControllerToRun, 
                                   CancelEventArgs e);

    }
}
