using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using EApp.Core.Application;
using EApp.Windows.Mvc;

namespace Xpress.Mvc
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

            EAppRuntime.Instance.CurrentApp.WinMvcControllerBuilder.SetControllerFactory(new DefaultControllerFactory());

            Application.Run(new Cost());
        }
    }
}
