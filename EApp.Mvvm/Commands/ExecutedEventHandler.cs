using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Mvvm.Commands
{
    public delegate void ExecutedEventHandler(object sender, ExecutedEventArgs e);
    
    public delegate void CanExecuteEventHandler(object sender, CanExecuteEventArgs e);
}
