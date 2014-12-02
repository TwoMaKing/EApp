using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EApp.Core;
using EApp.Core.Plugin;

namespace EApp.Plugin.Generic
{
    public partial class FormPluginOverlayBase : Form, IPlugin
    {
        public FormPluginOverlayBase()
        {
            InitializeComponent();
        }

        public event EventHandler<PluginLoadedEventArgs> Loaded;

        public event EventHandler Unloaded;

        public LifetimeMode Lifetime
        {
            get 
            { 
                return LifetimeMode.KeepAlive; 
            }
        }

        public virtual void Run(IPluginServiceProvider serviceProvider)
        {
            return;
        }

        public void Unload()
        {
            return;
        }

    }
}
