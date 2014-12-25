using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EApp.Mvvm.Commands
{
    public interface ICommandSource
    {
        ICommand Command { get; }

        object CommandParameter { get; }

        IUIElement CommandTarget { get; }
    }
}
