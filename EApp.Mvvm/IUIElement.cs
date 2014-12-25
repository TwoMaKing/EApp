using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EApp.Mvvm.Bindings;
using EApp.Mvvm.Commands;

namespace EApp.Mvvm
{
    public interface IUIElement
    {
        CommandBindingCollection CommandBindings { get; }

        ViewModelBase DataContext { get; set; }

        Control Owner { get; }
    }
}
