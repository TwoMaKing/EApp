using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace EApp.UI.Plugin
{
    public class PluginLoadedEventArgs : AsyncCompletedEventArgs
    {
        public PluginLoadedEventArgs(Exception error,
                                     bool cancelled,
                                     object userState)
            : base(error, cancelled, userState)
        {

        }
    }
}
