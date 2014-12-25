using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EApp.Mvvm.Bindings;
using EApp.Mvvm.Commands;

namespace EApp.Mvvm.Controls
{
    public class MvButton : Button, IUIElement, ICommandSource
    {
        public CommandBindingCollection CommandBindings
        {
            get { throw new NotImplementedException(); }
        }

        public ViewModelBase DataContext
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public Control Owner
        {
            get { throw new NotImplementedException(); }
        }

        public ICommand Command
        {
            get { throw new NotImplementedException(); }
        }

        public object CommandParameter
        {
            get { throw new NotImplementedException(); }
        }

        public IUIElement CommandTarget
        {
            get { throw new NotImplementedException(); }
        }
    }
}
