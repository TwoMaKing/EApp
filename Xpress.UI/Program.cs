using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using EApp.Common.IOC;
using EApp.Core.Application;
using EApp.Core.Configuration;
using EApp.Core.Plugin;
using EApp.Plugin.Generic;
using Xpress.Core.Plugin;

namespace Xpress.UI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            EAppRuntime.Instance.Create();

            EAppRuntime.Instance.CurrentApp.PluginHost.Run();
        }
    }
}
