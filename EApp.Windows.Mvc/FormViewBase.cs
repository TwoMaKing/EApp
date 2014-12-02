using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EApp.Core.WindowsMvc;

namespace EApp.Windows.Mvc
{
    public partial class FormViewBase : Form, IView, IViewDataContainer
    {
        private IViewAction view;

        public FormViewBase()
        {
            InitializeComponent();
        }

        public bool IsAsync
        {
            get { return false; }
        }

        public IDictionary<string, object> ViewData
        {
            get;
            set;
        }

        public IViewAction View
        {
            get
            {
                if (this.view == null)
                {
                    if (IsAsync)
                    {
                        this.view = new AsynViewAction();
                    }
                    else
                    {
                        this.view = new SyncViewAction(this, this);
                    }
                }

                return this.view;
            }
        }

    }
}
