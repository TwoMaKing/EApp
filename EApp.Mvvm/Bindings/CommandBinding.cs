using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Mvvm.Commands;

namespace EApp.Mvvm.Bindings
{
    public class CommandBinding
    {
        public event ExecutedEventHandler Executed;

        public event CanExecuteEventHandler CanExecute;

        public CommandBinding(ICommand command) : this(command, null) { }

        public CommandBinding(ICommand command, ExecutedEventHandler executed) : this(command, executed, null) { }

        public CommandBinding(ICommand command, ExecutedEventHandler executed, CanExecuteEventHandler canExecute) 
        {
            this.Command = command;

            if (executed != null)
            {
                this.Executed += new ExecutedEventHandler(executed);
            }

            if (canExecute != null)
            {
                this.CanExecute += new CanExecuteEventHandler(canExecute);
            }
        }

        public ICommand Command
        {
            get;
            private set;
        }
    }
}
