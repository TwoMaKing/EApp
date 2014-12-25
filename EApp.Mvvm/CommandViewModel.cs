using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Mvvm.Commands;

namespace EApp.Mvvm
{
    public class CommandViewModel : ViewModelBase
    {
        private ICommand command;

        public CommandViewModel(string displayName, ICommand command)
        {
            this.DisplayName = displayName;
            this.command = command;
        }

        public ICommand Command 
        {
            get 
            {
                return this.command;
            }
        }
    }
}
